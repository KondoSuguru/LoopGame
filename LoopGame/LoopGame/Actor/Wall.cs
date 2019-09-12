using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoopGame.Device;
using Microsoft.Xna.Framework;

namespace LoopGame.Actor
{
    class Wall : Actor
    {
        public Wall()
        {
            mPosition = new Vector2(GridSize.GRID_SIZE * 1, GridSize.GRID_SIZE * 1);
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
