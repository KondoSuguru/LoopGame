using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LoopGame.Device;

namespace LoopGame.Actor
{
    class ActorMove
    {
        public enum MoveState
        {
            LEFT,
            RIGHT,
            UP,
            DOWN,
            NONE,
        }
        MoveState mState;

        Vector2 mMovePoint;
        float mSpeed;
        IGameMediator mMediator;
        Actor mHitBox;
        bool mIsBoxStageCollide;

        public ActorMove(IGameMediator mediator)
        {
            mMediator = mediator;
            mState = MoveState.NONE;
            mHitBox = null;
            mIsBoxStageCollide = false;
        }

        public void Move(ref Vector2 pos)
        {
            switch (mState)
            {
                case MoveState.NONE:
                    break;
                case MoveState.LEFT:
                    if(mMovePoint.X == Screen.WIDTH - GridSize.GRID_SIZE)
                    {
                        pos.X = mMovePoint.X;
                    }
                    else
                    {
                        if (!mIsBoxStageCollide) {
                            pos.X += mSpeed;
                        }
                        if (mHitBox != null) {
                            if (mHitBox.GetPosition().X <= 1) {
                                mHitBox.SetPosition(new Vector2(Screen.WIDTH - GridSize.GRID_SIZE, mHitBox.GetPosition().Y));
                            } else {
                                mHitBox.Translate(new Vector2(mSpeed, 0f));
                            }
                        }
                    }
                    if (pos.X <= mMovePoint.X)
                    {
                        mState = MoveState.NONE;
                        mHitBox = null;
                        mIsBoxStageCollide = false;
                    }
                    break;
                case MoveState.RIGHT:
                    if (mMovePoint.X == 0)
                    {
                        pos.X = mMovePoint.X;
                    }
                    else
                    {
                        if (!mIsBoxStageCollide) {
                            pos.X += mSpeed;
                        }
                        if (mHitBox != null) {
                            if (mHitBox.GetPosition().X >= Screen.WIDTH - GridSize.GRID_SIZE - 1) {
                                mHitBox.SetPosition(new Vector2(0f, mHitBox.GetPosition().Y));
                            } else {
                                mHitBox.Translate(new Vector2(mSpeed, 0f));
                            }
                        }
                    }
                    if (pos.X >= mMovePoint.X)
                    {
                        mState = MoveState.NONE;
                        mHitBox = null;
                        mIsBoxStageCollide = false;
                    }
                    break;
                case MoveState.UP:
                    if(mMovePoint.Y == Screen.HEIGHT - GridSize.GRID_SIZE)
                    {
                        pos.Y = mMovePoint.Y;
                    }
                    else
                    {
                        if (!mIsBoxStageCollide) {
                            pos.Y += mSpeed;
                        }
                        if (mHitBox != null) {
                            if (mHitBox.GetPosition().Y <= 1) {
                                mHitBox.SetPosition(new Vector2(mHitBox.GetPosition().X, Screen.HEIGHT - GridSize.GRID_SIZE));
                            } else {
                                mHitBox.Translate(new Vector2(0f, mSpeed));
                            }
                        }
                    }
                    if (pos.Y <= mMovePoint.Y)
                    {
                        mState = MoveState.NONE;
                        mHitBox = null;
                        mIsBoxStageCollide = false;
                    }
                    break;
                case MoveState.DOWN:
                    if (mMovePoint.Y == 0)
                    {
                        pos.Y = mMovePoint.Y;
                    }
                    else
                    {
                        if (!mIsBoxStageCollide) {
                            pos.Y += mSpeed;
                        }
                        if (mHitBox != null) {
                            if (mHitBox.GetPosition().Y >= Screen.HEIGHT - GridSize.GRID_SIZE - 1) {
                                mHitBox.SetPosition(new Vector2(mHitBox.GetPosition().X, 0f));
                            } else {
                                mHitBox.Translate(new Vector2(0f, mSpeed));
                            }
                        }
                    }
                    if (pos.Y >= mMovePoint.Y)
                    {
                        mState = MoveState.NONE;
                        mHitBox = null;
                        mIsBoxStageCollide = false;
                    }
                    break;
            }
        }

        public bool MoveLeft(Vector2 pos)
        {
            if (mState != MoveState.NONE)
                return false;
            if (IsStageCollision(new Vector2(pos.X - GridSize.GRID_SIZE, pos.Y))) {
                return false;
            }

            mState = MoveState.LEFT;

            var actors = ActorManager.Instance().GetActors();
            foreach (var actor in actors) {
                if (!(actor is Box)) { //Box以外は興味ない
                    continue;
                }
                if (Actor.IsCollision(new Vector2(pos.X - GridSize.GRID_SIZE, pos.Y), actor)) {
                    mIsBoxStageCollide = ((Box)actor).BranchUpdate(mState);
                    if (mIsBoxStageCollide) {
                        return false;
                    }
                    mHitBox = actor;
                    break;
                }
            }

            mMovePoint = new Vector2(pos.X - GridSize.GRID_SIZE, pos.Y);
            if (mMovePoint.X <= -GridSize.GRID_SIZE)
            {
                mMovePoint.X = Screen.WIDTH - GridSize.GRID_SIZE;
            }
            mSpeed = (mMovePoint.X - pos.X) / (GridSize.GRID_SIZE /4);
            return true;
        }

        public bool MoveRight(Vector2 pos)
        {
            if (mState != MoveState.NONE)
                return false;
            if (IsStageCollision(new Vector2(pos.X + GridSize.GRID_SIZE, pos.Y)))
                return false;

            mState = MoveState.RIGHT;

            var actors = ActorManager.Instance().GetActors();
            foreach (var actor in actors) {
                if (!(actor is Box)) { //Box以外は興味ない
                    continue;
                }
                if (Actor.IsCollision(new Vector2(pos.X + GridSize.GRID_SIZE, pos.Y), actor)) {
                    mIsBoxStageCollide = ((Box)actor).BranchUpdate(mState);
                    if (mIsBoxStageCollide) {
                        return false;
                    }
                    mHitBox = actor;
                    break;
                }
            }

            mMovePoint = new Vector2( pos.X + GridSize.GRID_SIZE, pos.Y);
            if (mMovePoint.X >= Screen.WIDTH)
            {
                mMovePoint.X = 0;
            }
            mSpeed = (mMovePoint.X - pos.X) / (GridSize.GRID_SIZE / 4);
            return true;
        }

        public bool MoveUp(Vector2 pos)
        {
            if (mState != MoveState.NONE)
                return false;
            if (IsStageCollision(new Vector2(pos.X, pos.Y - GridSize.GRID_SIZE))) 
                return false;

            mState = MoveState.UP;

            var actors = ActorManager.Instance().GetActors();
            foreach (var actor in actors) {
                if (!(actor is Box)) { //Box以外は興味ない
                    continue;
                }
                if (Actor.IsCollision(new Vector2(pos.X, pos.Y - GridSize.GRID_SIZE), actor)) {
                    mIsBoxStageCollide = ((Box)actor).BranchUpdate(mState);
                    if (mIsBoxStageCollide) {
                        return false;
                    }
                    mHitBox = actor;
                    break;
                }
            }

            mMovePoint = new Vector2(pos.X, pos.Y - GridSize.GRID_SIZE);
            if (mMovePoint.Y <= -GridSize.GRID_SIZE)

            {
                mMovePoint.Y = Screen.HEIGHT - GridSize.GRID_SIZE;
            }
            mSpeed = (mMovePoint.Y - pos.Y) / (GridSize.GRID_SIZE / 4);
            return true;
        }

        public bool MoveDown(Vector2 pos)
        {
            if (mState != MoveState.NONE)
                return false;
            if (IsStageCollision(new Vector2(pos.X, pos.Y + GridSize.GRID_SIZE))) 
                return false;

            mState = MoveState.DOWN;

            var actors = ActorManager.Instance().GetActors();
            foreach (var actor in actors) {
                if (!(actor is Box)) { //Box以外は興味ない
                    continue;
                }
                if (Actor.IsCollision(new Vector2(pos.X, pos.Y + GridSize.GRID_SIZE), actor)) {
                    mIsBoxStageCollide = ((Box)actor).BranchUpdate(mState);
                    if (mIsBoxStageCollide) {
                        return false;
                    }
                    mHitBox = actor;
                    break;
                }
            }

            mMovePoint = new Vector2(pos.X, pos.Y + GridSize.GRID_SIZE);
            if (mMovePoint.Y >= Screen.HEIGHT)
            {
                mMovePoint.Y = 0;
            }
            mSpeed = (mMovePoint.Y - pos.Y) / (GridSize.GRID_SIZE / 4);
            return true;
        }

        public bool IsStageCollision(Vector2 nextPos) {
            return mMediator.GetStage().IsCollision(nextPos);
        }

        //public void MoveLeft(ref Vector2 pos)
        //{
        //    pos.X -= GridSize.GRID_SIZE;
        //    if (pos.X <= -GridSize.GRID_SIZE)
        //    {
        //        pos.X = Screen.WIDTH - GridSize.GRID_SIZE;
        //    }
        //}

        //public void MoveRight(ref Vector2 pos)
        //{
        //    pos.X += GridSize.GRID_SIZE;
        //    if (pos.X >= Screen.WIDTH)
        //    {
        //        pos.X = 0;
        //    }
        //}

        //public void MoveUp(ref Vector2 pos)
        //{
        //    pos.Y -= GridSize.GRID_SIZE;
        //    if(pos.Y <= -GridSize.GRID_SIZE)
        //    {
        //        pos.Y = Screen.HEIGHT - GridSize.GRID_SIZE;
        //    }
        //}

        //public void MoveDown(ref Vector2 pos)
        //{
        //    pos.Y += GridSize.GRID_SIZE;
        //    if (pos.Y >= Screen.HEIGHT)
        //    {
        //        pos.Y = 0;
        //    }
        //}
    }
}
