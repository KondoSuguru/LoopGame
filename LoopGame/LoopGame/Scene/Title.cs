using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using LoopGame.Utility;
using Microsoft.Xna.Framework.Input;
using LoopGame.Device;

namespace LoopGame.Scene
{
    class Title : IScene
    {
        private bool mIsEndFlag;

        public Title()
        {
            mIsEndFlag = false;
            GameDevice.Instance().GetRenderer().LoadContent("TITLE");
        }

        public void Draw()
        {
            GameDevice.Instance().GetRenderer().DrawTexture("TITLE", Vector2.Zero);
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
            return Scene.StageSelect;
        }

        public void Shutdown()
        {
        }

        public void Update(GameTime gameTime)
        {
            if (Input.GetKeyTrigger(Keys.Space)) {
                mIsEndFlag = true;
            }
        }
    }
}
