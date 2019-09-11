using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoopGame.Device;
using Microsoft.Xna.Framework;

namespace LoopGame.Actor
{
    class Player : Actor
    {
        public Player()
        {
            mPosition = new Vector2(Screen.WIDTH / 2, Screen.HEIGHT / 2);
            GameDevice.Instance().GetRenderer().LoadContent("boss_LEFT");
        }

        public override void Draw()
        {
            GameDevice.Instance().GetRenderer().DrawTexture("boss_LEFT",mPosition);
        }

        public override void Update(GameTime gameTime)
        {
            
        }
    }
}
