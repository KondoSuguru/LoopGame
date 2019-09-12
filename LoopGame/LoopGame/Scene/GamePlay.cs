using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using LoopGame.Utility;
using Microsoft.Xna.Framework.Input;
using LoopGame.Actor;

namespace LoopGame.Scene
{
    class GamePlay : IScene, IGameMediator
    {
        private Stage mStage;
        private bool mIsEndFlag;

        public void Draw()
        {
            ActorManager.Instance().Draw();
        }

        public void Initialize()
        {
            mStage = new Stage(this);
            mStage.Load("TestStage.csv");
            mIsEndFlag = false;
        }

        public bool IsEnd()
        {
            return mIsEndFlag;
        }

        public Scene Next()
        {
            return Scene.Ending;
        }

        public void Shutdown()
        {
            mStage.Unload();
            ActorManager.Instance().Clear();
        }

        public void Update(GameTime gameTime)
        {
            if (Input.GetKeyTrigger(Keys.Space)) {
                mIsEndFlag = true;
            }

            ActorManager.Instance().Update(gameTime);
        }

        public Stage GetStage() {
            return mStage;
        }
    }
}
