using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using LoopGame.Utility;
using Microsoft.Xna.Framework.Input;
using LoopGame.Actor;
using LoopGame.Device;

namespace LoopGame.Scene
{
    class GamePlay : SceneBase, IScene, IGameMediator
    {
        private Stage mStage;
        private bool mIsEndFlag;
        private bool mIsClear;

        public GamePlay()
        {
            GameDevice.Instance().GetRenderer().LoadContent("CLEAR");
            GameDevice.Instance().GetRenderer().LoadContent("floor");
        }

        public void Draw()
        {
            GameDevice.Instance().GetRenderer().DrawTexture("floor", Vector2.Zero);
            ActorManager.Instance().Draw();
            if (mIsClear)
            {
                GameDevice.Instance().GetRenderer().DrawTexture("CLEAR", Vector2.Zero);
            }
        }

        public void Initialize()
        {
            mStage = new Stage(this);
            mStage.Load("TestStage0" + mStageNo.ToString() + ".csv");
            mIsEndFlag = false;
            mIsClear = false;
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
            mStage.Unload();
            ActorManager.Instance().Clear();
        }

        public void Update(GameTime gameTime)
        {
            if (Input.GetKeyTrigger(Keys.Space)) {
                mIsEndFlag = true;
            }

            if (mIsClear)
            {
                return;
            }

            if (Input.GetKeyTrigger(Keys.P)) {
                mStage.Reset();
            }
            if(ActorManager.Instance().IsClear())
            {
                mIsClear = true;
            }

            ActorManager.Instance().Update(gameTime);
        }

        public Stage GetStage() {
            return mStage;
        }
    }
}
