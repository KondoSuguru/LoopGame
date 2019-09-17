using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LoopGame.Device;
using System.Diagnostics;

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
        static readonly int SPEED = 4;
        IGameMediator mMediator;
        Actor mHitBox;
        bool mIsBoxStageOrOtherBoxCollide;
        public static int mWalkCount;

        public ActorMove(IGameMediator mediator)
        {
            mMediator = mediator;
            mState = MoveState.NONE;
            mHitBox = null;
            mIsBoxStageOrOtherBoxCollide = false;
            mWalkCount = 0;
        }

        public void Move(ref Vector2 pos)
        {
            switch (mState)
            {
                case MoveState.NONE:
                    break;
                case MoveState.LEFT:
                    if (!mIsBoxStageOrOtherBoxCollide) {
                        pos.X -= SPEED;
                    }
                    if (mHitBox != null) {
                        if (mHitBox.GetPosition().X <= 0) {
                            mHitBox.SetPosition(new Vector2(Screen.PLAY_WIDTH, mHitBox.GetPosition().Y));
                        }
                        mHitBox.Translate(new Vector2(-SPEED, 0f));
                    }
                    if (pos.X <= mMovePoint.X)
                    {
                        ResetState(ref pos);
                    }
                    break;
                case MoveState.RIGHT:
                    if (!mIsBoxStageOrOtherBoxCollide) {
                        pos.X += SPEED;
                    }
                    if (mHitBox != null) {
                        if (mHitBox.GetPosition().X >= Screen.PLAY_WIDTH - GridSize.GRID_SIZE) {
                            mHitBox.SetPosition(new Vector2(-GridSize.GRID_SIZE, mHitBox.GetPosition().Y));
                        }
                        mHitBox.Translate(new Vector2(SPEED, 0f));
                    }
                    if (pos.X >= mMovePoint.X)
                    {
                        ResetState(ref pos);
                    }
                    break;
                case MoveState.UP:
                    if (!mIsBoxStageOrOtherBoxCollide) {
                        pos.Y -= SPEED;
                    }
                    if (mHitBox != null) {
                        if (mHitBox.GetPosition().Y <= 0) {
                            mHitBox.SetPosition(new Vector2(mHitBox.GetPosition().X, Screen.HEIGHT));
                        }
                        mHitBox.Translate(new Vector2(0f, -SPEED));
                    }
                    if (pos.Y <= mMovePoint.Y)
                    {
                        ResetState(ref pos);
                    }
                    break;
                case MoveState.DOWN:
                    if (!mIsBoxStageOrOtherBoxCollide) {
                        pos.Y += SPEED;
                    }
                    if (mHitBox != null) {
                        if (mHitBox.GetPosition().Y >= Screen.HEIGHT - GridSize.GRID_SIZE) {
                            mHitBox.SetPosition(new Vector2(mHitBox.GetPosition().X, -GridSize.GRID_SIZE));
                        }
                        mHitBox.Translate(new Vector2(0f, SPEED));
                    }
                    if (pos.Y >= mMovePoint.Y)
                    {
                        ResetState(ref pos);
                    }
                    break;
            }
        }

        public bool MoveLeft(ref Vector2 pos)
        {
            if (mState != MoveState.NONE)
                return false;
            if (IsStageCollision(new Vector2(pos.X - GridSize.GRID_SIZE, pos.Y))) {
                return false;
            }

            mState = MoveState.LEFT;

            if (!MoveOption(pos)) {
                return false;
            }

            mMovePoint = new Vector2(pos.X - GridSize.GRID_SIZE, pos.Y);
            if (mMovePoint.X <= -GridSize.GRID_SIZE)
            {
                mMovePoint.X = Screen.PLAY_WIDTH - GridSize.GRID_SIZE;
            }

            if (mMovePoint.X == Screen.PLAY_WIDTH - GridSize.GRID_SIZE) {
                pos.X = mMovePoint.X + GridSize.GRID_SIZE;
            }
            mSpeed = (mMovePoint.X - pos.X) / (GridSize.GRID_SIZE /4);

            return true;
        }

        public bool MoveRight(ref Vector2 pos)
        {
            if (mState != MoveState.NONE)
                return false;
            if (IsStageCollision(new Vector2(pos.X + GridSize.GRID_SIZE, pos.Y)))
                return false;

            mState = MoveState.RIGHT;

            if (!MoveOption(pos)) {
                return false;
            }

            mMovePoint = new Vector2( pos.X + GridSize.GRID_SIZE, pos.Y);
            if (mMovePoint.X >= Screen.PLAY_WIDTH)
            {
                mMovePoint.X = 0;
            }

            if (mMovePoint.X == 0f) {
                pos.X = mMovePoint.X - GridSize.GRID_SIZE;
            }
            mSpeed = (mMovePoint.X - pos.X) / (GridSize.GRID_SIZE / 4);

            return true;
        }

        public bool MoveUp(ref Vector2 pos)
        {
            if (mState != MoveState.NONE)
                return false;
            if (IsStageCollision(new Vector2(pos.X, pos.Y - GridSize.GRID_SIZE))) 
                return false;

            mState = MoveState.UP;

            if (!MoveOption(pos)) {
                return false;
            }

            mMovePoint = new Vector2(pos.X, pos.Y - GridSize.GRID_SIZE);
            if (mMovePoint.Y <= -GridSize.GRID_SIZE)

            {
                mMovePoint.Y = Screen.HEIGHT - GridSize.GRID_SIZE;
            }

            if (mMovePoint.Y == Screen.HEIGHT - GridSize.GRID_SIZE) {
                pos.Y = mMovePoint.Y + GridSize.GRID_SIZE;
            }
            mSpeed = (mMovePoint.Y - pos.Y) / (GridSize.GRID_SIZE / 4);

            return true;
        }

        public bool MoveDown(ref Vector2 pos)
        {
            if (mState != MoveState.NONE)
                return false;
            if (IsStageCollision(new Vector2(pos.X, pos.Y + GridSize.GRID_SIZE))) 
                return false;

            mState = MoveState.DOWN;

            if (!MoveOption(pos)) {
                return false;
            }

            mMovePoint = new Vector2(pos.X, pos.Y + GridSize.GRID_SIZE);
            if (mMovePoint.Y >= Screen.HEIGHT)
            {
                mMovePoint.Y = 0;
            }

            if (mMovePoint.Y == 0f) {
                pos.Y = mMovePoint.Y - GridSize.GRID_SIZE;
            }
            mSpeed = (mMovePoint.Y - pos.Y) / (GridSize.GRID_SIZE / 4);

            return true;
        }

        public bool IsStageCollision(Vector2 nextPos) {
            return mMediator.GetStage().IsCollision(nextPos);
        }

        private void ResetState(ref Vector2 pPos) {
            int px, py;
            px = (int)(pPos.X + 5) / 64;
            py = (int)(pPos.Y + 5) / 64;
            pPos = new Vector2(px, py) * 64;
            if (mHitBox != null)
            {
                int bx, by;
                bx = (int)(mHitBox.GetPosition().X + 5) / 64;
                by = (int)(mHitBox.GetPosition().Y + 5) / 64;
                mHitBox.SetPosition(new Vector2(bx, by) * 64);
            }
            mState = MoveState.NONE;
            mHitBox = null;

            if (!mIsBoxStageOrOtherBoxCollide) {
                mWalkCount += 1;
            }
            mIsBoxStageOrOtherBoxCollide = false;
        }

        private bool MoveOption(Vector2 inVec) {
            var actors = ActorManager.Instance().GetActors();
            foreach (var actor in actors) {
                if (!(actor is Box)) { //Box以外は興味ない
                    continue;
                }

                Vector2 p = inVec;
                if (mState == MoveState.LEFT) {
                    if (inVec.X <= 5f) {
                        p.X = Screen.PLAY_WIDTH - GridSize.GRID_SIZE;
                    } else {
                        p.X = inVec.X - GridSize.GRID_SIZE;
                    }
                } else if (mState == MoveState.RIGHT) {
                    if (inVec.X >= Screen.PLAY_WIDTH - GridSize.GRID_SIZE - 5) {
                        p.X = 0f;
                    } else {
                        p.X = inVec.X + GridSize.GRID_SIZE;
                    }
                } else if (mState == MoveState.UP) {
                    if (inVec.Y <= 5f) {
                        p.Y = Screen.HEIGHT - GridSize.GRID_SIZE;
                    } else {
                        p.Y = inVec.Y - GridSize.GRID_SIZE;
                    }
                } else if (mState == MoveState.DOWN) {
                    if (inVec.Y >= Screen.HEIGHT - GridSize.GRID_SIZE - 5f) {
                        p.Y = 0f;
                    } else {
                        p.Y = inVec.Y + GridSize.GRID_SIZE;
                    }
                }

                if (Actor.IsCollision(p, actor)) {
                    ((Box)actor).BranchUpdate(mState, ref mIsBoxStageOrOtherBoxCollide);
                    if (mIsBoxStageOrOtherBoxCollide) {
                        return false;
                    }
                    mHitBox = actor;
                    break;
                }
            }
            return true;
        }

        public int GetWalkCount() {
            return mWalkCount;
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
