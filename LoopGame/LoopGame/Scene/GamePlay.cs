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
            r.LoadContent("stateFrame");
            r.LoadContent("resetButton");
            r.LoadContent("resetButtonDown");
            r.LoadContent("undoButton");
            r.LoadContent("undoButtonDown");

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
            var r = GameDevice.Instance().GetRenderer();
            GameDevice.Instance().GetRenderer().DrawTexture("floor", Vector2.Zero);
            ActorManager.Instance().Draw();

            r.DrawTexture("stateFrame", new Vector2(Screen.PLAY_WIDTH, 0));
            if (Input.GetKeyState(Keys.X)){
                r.DrawTexture("resetButtonDown", new Vector2(Screen.PLAY_WIDTH, GridSize.GRID_SIZE * 6));
            }
            else {
                r.DrawTexture("resetButton", new Vector2(Screen.PLAY_WIDTH, GridSize.GRID_SIZE * 6));
            }
            if (Input.GetKeyState(Keys.Z))
            {
                r.DrawTexture("undoButtonDown", new Vector2(Screen.PLAY_WIDTH, GridSize.GRID_SIZE * 6));
            }
            else {
                r.DrawTexture("undoButton", new Vector2(Screen.PLAY_WIDTH, GridSize.GRID_SIZE * 6));
            }
            ActorManager.Instance().DrawWalkCount();
            r.DrawNumber("number", new Vector2(Screen.PLAY_WIDTH + GridSize.GRID_SIZE * 2, GridSize.GRID_SIZE), mStageNo);

            if (!mIsClear)
            {
                return;
            }

            r.DrawTexture("CLEAR", new Vector2(Screen.PLAY_WIDTH / 2 - 135, Screen.HEIGHT / 2 - 135));
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
            } else if (mStageNo == 3) {
                mRankA = 75;
                mRankB = 86;
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

            if (Input.GetKeyUp(Keys.X)) {
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
