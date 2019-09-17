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
        private int mRankA;
        private int mRankB;
        List<string> mStarName;
        List<Vector2> mStarPosition;
        int mA;

        public GamePlay()
        {
            var r = GameDevice.Instance().GetRenderer();
            r.LoadContent("CLEAR");
            r.LoadContent("floor");
            r.LoadContent("goldStar");
            r.LoadContent("grayStar");

            mStarName = new List<string>() {
                "goldStar", "goldStar", "goldStar",
                "goldStar", "grayStar", "goldStar",
                "goldStar", "grayStar", "grayStar"
            };

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
            r.DrawTexture("CLEAR", Vector2.Zero);
            for (int i = 0; i < mStarPosition.Count; i++) {
                r.DrawTexture(mStarName[i + mA], mStarPosition[i]);
            }
        }

        public void Initialize()
        {
            mStage = new Stage(this);
            mStage.Load("TestStage0" + mStageNo.ToString() + ".csv");
            mIsEndFlag = false;
            mIsClear = false;

            if (mStageNo == 1) {
                mRankA = 46;
                mRankB = 52;
            } else if (mStageNo == 2) {
                mRankA = 54;
                mRankB = 60;
            }
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
                if (ActorMove.mWalkCount <= mRankA) {
                    mA = 0;
                } else if (mRankA < ActorMove.mWalkCount && ActorMove.mWalkCount <= mRankB) {
                    mA = 3;
                } else {
                    mA = 6;
                }
            }

            ActorManager.Instance().Update(gameTime);
        }

        public Stage GetStage() {
            return mStage;
        }
    }
}
