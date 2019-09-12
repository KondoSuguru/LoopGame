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
        public Wall() : base("wall")
        {
            GameDevice.Instance().GetRenderer().LoadContent(mFilename);
        }

        public override void Draw() {
            GameDevice.Instance().GetRenderer().DrawTexture(mFilename, mPosition);
        }

        public override void Update(GameTime gameTime)
        {
            
        }
    }
}
