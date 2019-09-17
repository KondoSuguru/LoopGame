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
        private const int STAGE2_B = 60;
        private const int STAGE2_C = 66;
        List<Vector2> mStarPosition;

        public GamePlay()
        {
            var r = GameDevice.Instance().GetRenderer();
            r.LoadContent("CLEAR");
            r.LoadContent("floor");
            r.LoadContent("goldStar");
            r.LoadContent("grayStar");

            mStarPosition = new List<Vector2>();
            mStarPosition.Add(new Vector2(Screen.PLAY_WIDTH / 2 - 128, 350)); //左
            mStarPosition.Add(new Vector2(Screen.PLAY_WIDTH / 2, 350)); //右
            mStarPosition.Add(new Vector2(Screen.PLAY_WIDTH / 2 - 64, 300)); //中
        }

        public void Draw()
        {
            GameDevice.Instance().GetRenderer().DrawTexture("floor", Vector2.Zero);
            ActorManager.Instance().Draw();
            if (!mIsClear)
            {
                return;
            }

            var r = GameDevice.Instance().GetRenderer();
            r.DrawTexture("CLEAR", new Vector2(Screen.PLAY_WIDTH / 2- 135, Screen.HEIGHT /2 - 135));
            if (mStageNo == 1) {
                if (ActorMove.mWalkCount <= STAGE1_A) {
                    r.DrawTexture("goldStar", mStarPosition[0]);
                    r.DrawTexture("goldStar", mStarPosition[1]);
                    r.DrawTexture("goldStar", mStarPosition[2]);
                } else if (STAGE1_A < ActorMove.mWalkCount && ActorMove.mWalkCount < STAGE1_C) {
                    r.DrawTexture("goldStar", mStarPosition[0]);
                    r.DrawTexture("grayStar", mStarPosition[1]);
                    r.DrawTexture("goldStar", mStarPosition[2]);
                } else {
                    r.DrawTexture("goldStar", mStarPosition[0]);
                    r.DrawTexture("grayStar", mStarPosition[1]);
                    r.DrawTexture("grayStar", mStarPosition[2]);
                }
            } else if (mStageNo == 2) {
                if (ActorMove.mWalkCount <= STAGE2_A) {
                    r.DrawTexture("goldStar", mStarPosition[0]);
                    r.DrawTexture("goldStar", mStarPosition[1]);
                    r.DrawTexture("goldStar", mStarPosition[2]);
                } else if (STAGE2_A < ActorMove.mWalkCount && ActorMove.mWalkCount < STAGE2_C) {
                    r.DrawTexture("goldStar", mStarPosition[0]);
                    r.DrawTexture("grayStar", mStarPosition[1]);
                    r.DrawTexture("goldStar", mStarPosition[2]);
                } else {
                    r.DrawTexture("goldStar", mStarPosition[0]);
                    r.DrawTexture("grayStar", mStarPosition[1]);
                    r.DrawTexture("grayStar", mStarPosition[2]);
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
            if (Input.GetKeyTrigger(Keys.O)) {
                mIsClear = true;
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
