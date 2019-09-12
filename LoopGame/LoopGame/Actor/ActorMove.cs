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
        public ActorMove()
        {
        }

        public void MoveLeft(ref Vector2 pos)
        {          
            pos.X -= GridSize.GRID_SIZE;
            if(pos.X <= -GridSize.GRID_SIZE)
            {
                pos.X = Screen.WIDTH - GridSize.GRID_SIZE;
            }
        }

        public void MoveRight(ref Vector2 pos)
        {
            pos.X += GridSize.GRID_SIZE;
            if (pos.X >= Screen.WIDTH)
            {
                pos.X = 0;
            }
        }

        public void MoveUp(ref Vector2 pos)
        {
            pos.Y -= GridSize.GRID_SIZE;
            if(pos.Y <= -GridSize.GRID_SIZE)
            {
                pos.Y = Screen.HEIGHT - GridSize.GRID_SIZE;
            }
        }

        public void MoveDown(ref Vector2 pos)
        {
            pos.Y += GridSize.GRID_SIZE;
            if (pos.Y >= Screen.HEIGHT)
            {
                pos.Y = 0;
            }
        }
    }
}
