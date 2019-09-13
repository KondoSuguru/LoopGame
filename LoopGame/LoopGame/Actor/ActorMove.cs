﻿using Microsoft.Xna.Framework;
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
        IGameMediator mMediator;
        Actor mHitBox;
        bool mIsBoxStageOrOtherBoxCollide;

        public ActorMove(IGameMediator mediator)
        {
            mMediator = mediator;
            mState = MoveState.NONE;
            mHitBox = null;
            mIsBoxStageOrOtherBoxCollide = false;
        }

        public void Move(ref Vector2 pos)
        {
            switch (mState)
            {
                case MoveState.NONE:
                    break;
                case MoveState.LEFT:
                    if (!mIsBoxStageOrOtherBoxCollide) {
                        pos.X += mSpeed;
                    }
                    if (mHitBox != null) {
                        if (mHitBox.GetPosition().X <= 0) {
                            mHitBox.SetPosition(new Vector2(Screen.WIDTH, mHitBox.GetPosition().Y));
                        } else {
                            mHitBox.Translate(new Vector2(mSpeed, 0f));
                        }
                    }
                    if (pos.X <= mMovePoint.X)
                    {
                        StateReset();
                    }
                    break;
                case MoveState.RIGHT:
                    if (!mIsBoxStageOrOtherBoxCollide) {
                        pos.X += mSpeed;
                    }
                    if (mHitBox != null) {
                        if (mHitBox.GetPosition().X >= Screen.WIDTH - GridSize.GRID_SIZE) {
                            mHitBox.SetPosition(new Vector2(-GridSize.GRID_SIZE, mHitBox.GetPosition().Y));
                        } else {
                            mHitBox.Translate(new Vector2(mSpeed, 0f));
                        }
                    }
                    if (pos.X >= mMovePoint.X)
                    {
                        StateReset();
                    }
                    break;
                case MoveState.UP:
                    if (!mIsBoxStageOrOtherBoxCollide) {
                        pos.Y += mSpeed;
                    }
                    if (mHitBox != null) {
                        if (mHitBox.GetPosition().Y <= 0) {
                            mHitBox.SetPosition(new Vector2(mHitBox.GetPosition().X, Screen.HEIGHT));
                        } else {
                            mHitBox.Translate(new Vector2(0f, mSpeed));
                        }
                    }
                    if (pos.Y <= mMovePoint.Y)
                    {
                        StateReset();
                    }
                    break;
                case MoveState.DOWN:
                    if (!mIsBoxStageOrOtherBoxCollide) {
                        pos.Y += mSpeed;
                    }
                    if (mHitBox != null) {
                        if (mHitBox.GetPosition().Y >= Screen.HEIGHT - GridSize.GRID_SIZE) {
                            mHitBox.SetPosition(new Vector2(mHitBox.GetPosition().X, -GridSize.GRID_SIZE));
                        } else {
                            mHitBox.Translate(new Vector2(0f, mSpeed));
                        }
                    }
                    if (pos.Y >= mMovePoint.Y)
                    {
                        StateReset();
                    }
                    break;
            }
        }

        public bool MoveLeft(ref Vector2 pos)
        {
            if (mState != MoveState.NONE)
                return false;
            if (IsStageCollision(new Vector2(pos.X - GridSize.GRID_SIZE, pos.Y), mState)) {
                return false;
            }

            mState = MoveState.LEFT;

            if (!MoveOption(pos)) {
                return false;
            }

            mMovePoint = new Vector2(pos.X - GridSize.GRID_SIZE, pos.Y);
            if (mMovePoint.X <= -GridSize.GRID_SIZE)
            {
                mMovePoint.X = Screen.WIDTH - GridSize.GRID_SIZE;
            }

            if (mMovePoint.X == Screen.WIDTH - GridSize.GRID_SIZE) {
                pos.X = mMovePoint.X + GridSize.GRID_SIZE;
            }
            mSpeed = (mMovePoint.X - pos.X) / (GridSize.GRID_SIZE /4);

            return true;
        }

        public bool MoveRight(ref Vector2 pos)
        {
            if (mState != MoveState.NONE)
                return false;
            if (IsStageCollision(new Vector2(pos.X + GridSize.GRID_SIZE, pos.Y), mState))
                return false;

            mState = MoveState.RIGHT;

            if (!MoveOption(pos)) {
                return false;
            }

            mMovePoint = new Vector2( pos.X + GridSize.GRID_SIZE, pos.Y);
            if (mMovePoint.X >= Screen.WIDTH)
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
            if (IsStageCollision(new Vector2(pos.X, pos.Y - GridSize.GRID_SIZE), mState)) 
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
            if (IsStageCollision(new Vector2(pos.X, pos.Y + GridSize.GRID_SIZE), mState)) 
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

        public bool IsStageCollision(Vector2 nextPos, MoveState state) {
            return mMediator.GetStage().IsCollision(nextPos, state);
        }

        private void StateReset() {
            mState = MoveState.NONE;
            mHitBox = null;
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
                        p.X = Screen.WIDTH - GridSize.GRID_SIZE;
                    } else {
                        p.X = inVec.X - GridSize.GRID_SIZE;
                    }
                } else if (mState == MoveState.RIGHT) {
                    if (inVec.X >= Screen.WIDTH - GridSize.GRID_SIZE - 5) {
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
