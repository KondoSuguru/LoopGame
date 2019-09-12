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
        Vector2 mMovePoint;
        float mSpeed;

        enum MoveState
        {
            LEFT,
            RIGHT,
            UP,
            DOWN,
            NONE,
        }
        MoveState mState;

        public ActorMove()
        {
            mState = MoveState.NONE;
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
                        pos.X += mSpeed;
                    }
                    if (pos.X <= mMovePoint.X)
                    {
                        mState = MoveState.NONE;
                    }
                    break;
                case MoveState.RIGHT:
                    if (mMovePoint.X == 0)
                    {
                        pos.X = mMovePoint.X;
                    }
                    else
                    {
                        pos.X += mSpeed;
                    }
                    if (pos.X >= mMovePoint.X)
                    {
                        mState = MoveState.NONE;
                    }
                    break;
                case MoveState.UP:
                    if(mMovePoint.Y == Screen.HEIGHT - GridSize.GRID_SIZE)
                    {
                        pos.Y = mMovePoint.Y;
                    }
                    else
                    {
                        pos.Y += mSpeed;
                    }
                    if (pos.Y <= mMovePoint.Y)
                    {
                        mState = MoveState.NONE;
                    }
                    break;
                case MoveState.DOWN:
                    if (mMovePoint.Y == 0)
                    {
                        pos.Y = mMovePoint.Y;
                    }
                    else
                    {
                        pos.Y += mSpeed;
                    }
                    if (pos.Y >= mMovePoint.Y)
                    {
                        mState = MoveState.NONE;
                    }
                    break;

            }
        }

        public void MoveLeft(Vector2 pos)
        {
            if (mState != MoveState.NONE)
                return;
            mState = MoveState.LEFT;
            mMovePoint = new Vector2(pos.X - GridSize.GRID_SIZE, pos.Y);
            if (mMovePoint.X <= -GridSize.GRID_SIZE)
            {
                mMovePoint.X = Screen.WIDTH - GridSize.GRID_SIZE;
            }
            mSpeed = (mMovePoint.X - pos.X) / (GridSize.GRID_SIZE /4);
        }

        public void MoveRight(Vector2 pos)
        {
            if (mState != MoveState.NONE)
                return;
            mState = MoveState.RIGHT;
            mMovePoint = new Vector2( pos.X + GridSize.GRID_SIZE, pos.Y);
            if (mMovePoint.X >= Screen.WIDTH)
            {
                mMovePoint.X = 0;
            }
            mSpeed = (mMovePoint.X - pos.X) / (GridSize.GRID_SIZE / 4);
        }

        public void MoveUp(Vector2 pos)
        {
            if (mState != MoveState.NONE)
                return;
            mState = MoveState.UP;
            mMovePoint = new Vector2(pos.X, pos.Y - GridSize.GRID_SIZE);
            if (mMovePoint.Y <= -GridSize.GRID_SIZE)
            {
                mMovePoint.Y = Screen.HEIGHT - GridSize.GRID_SIZE;
            }
            mSpeed = (mMovePoint.Y - pos.Y) / (GridSize.GRID_SIZE / 4);
        }

        public void MoveDown(Vector2 pos)
        {
            if (mState != MoveState.NONE)
                return;
            mState = MoveState.DOWN;
            mMovePoint = new Vector2(pos.X, pos.Y + GridSize.GRID_SIZE);
            if (mMovePoint.Y >= Screen.HEIGHT)
            {
                mMovePoint.Y = 0;
            }
            mSpeed = (mMovePoint.Y - pos.Y) / (GridSize.GRID_SIZE / 4);
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
