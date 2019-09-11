using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace LoopGame.Scene
{
    class Ending : IScene
    {
        private bool mIsEndFlag;

        public Ending()
        {
        }

        public void Draw()
        {

        }

        public void Initialize()
        {
            mIsEndFlag = false;
        }

        public bool IsEnd()
        {
            return mIsEndFlag;
        }

        public Scene Next()
        {
            return Scene.None;
        }

        public void Shutdown()
        {
        }

        public void Update(GameTime gameTime)
        {
        }
    }
}
