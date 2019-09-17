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
        private const int STAGE1_A = 46;
        private const int STAGE1_B = 52;
        private const int STAGE1_C = 58;
        private const int STAGE2_A = 54;

        public GamePlay()
        {
            GameDevice.Instance().GetRenderer().LoadContent("CLEAR");
        }

        public void Draw()
        {
            ActorManager.Instance().Draw();
            if (!mIsClear)
            {
                return;
            }

            var r = GameDevice.Instance().GetRenderer();
            r.DrawTexture("CLEAR", Vector2.Zero);
            if (mStageNo == 1) {
                if (ActorMove.mWalkCount <= STAGE1_A) {
                    r.DrawTexture("", Vector2.Zero);
                } else if (STAGE1_A < ActorMove.mWalkCount && ActorMove.mWalkCount < STAGE1_C) {
                    r.DrawTexture("", Vector2.Zero);
                } else {
                    r.DrawTexture("", Vector2.Zero);
                }
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
