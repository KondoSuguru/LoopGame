using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using LoopGame.Device;

namespace LoopGame.Actor
{
    class ActorMove
    {
        IGameMediator mMediator;

        public ActorMove(IGameMediator mediator)
        {
            mMediator = mediator;
        }

        public void MoveLeft(ref Vector2 pos)
        {
            if (mMediator.GetStage().IsCollisionSide(new Vector2(pos.X - GridSize.GRID_SIZE, pos.Y))) {
                return;
            }
            pos.X -= GridSize.GRID_SIZE;
            if(pos.X < 0)
            {
                pos.X = Screen.WIDTH - GridSize.GRID_SIZE;
            }
        }

        public void MoveRight(ref Vector2 pos)
        {
            if (mMediator.GetStage().IsCollisionSide(new Vector2(pos.X + GridSize.GRID_SIZE, pos.Y))) {
                return;
            }
            pos.X += GridSize.GRID_SIZE;
            if (pos.X >= Screen.WIDTH)
            {
                pos.X = 0;
            }
        }

        public void MoveUp(ref Vector2 pos)
        {
            if (mMediator.GetStage().IsCollisionVertical(new Vector2(pos.X, pos.Y - GridSize.GRID_SIZE))) {
                return;
            }
            pos.Y -= GridSize.GRID_SIZE;
            if(pos.Y < 0)
            {
                pos.Y = Screen.HEIGHT - GridSize.GRID_SIZE;
            }
        }

        public void MoveDown(ref Vector2 pos)
        {
            if (mMediator.GetStage().IsCollisionVertical(new Vector2(pos.X, pos.Y + GridSize.GRID_SIZE))) {
                return;
            }
            pos.Y += GridSize.GRID_SIZE;
            if (pos.Y >= Screen.HEIGHT)
            {
                pos.Y = 0;
            }
        }
    }
}
