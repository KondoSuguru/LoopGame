using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoopGame.Device;
using Microsoft.Xna.Framework;

namespace LoopGame.Actor
{
    class Goal : Actor
    {
        public Goal()
        {
            mPosition = new Vector2(GridSize.GRID_SIZE * 5, GridSize.GRID_SIZE * 9);
            GameDevice.Instance().GetRenderer().LoadContent("boss_LEFT");
        }

        public override void Draw()
        {
            GameDevice.Instance().GetRenderer().DrawTexture("boss_LEFT", mPosition);
        }

        public override void Update(GameTime gameTime)
        {
            
        }
    }
}
