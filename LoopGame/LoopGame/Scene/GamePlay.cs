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
        int mRecord;
        private bool mIsMenu;
        private List<Vector2> mMenuCursor;
        private int mMenuNum;
        private Scene mNextScene;
        private Animation mAnim;

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
            GameDevice.Instance().GetSound().LoadBGM("bgm_maoudamashii_healing16");

            mStarName = new List<string>() {
                "goldStar", "goldStar", "goldStar",
                "goldStar", "grayStar", "goldStar",
                "goldStar", "grayStar", "grayStar"
            };

            mStarPosition = new List<Vector2>();
            mStarPosition.Add(new Vector2(Screen.PLAY_WIDTH / 2 - 128, 350)); //左
            mStarPosition.Add(new Vector2(Screen.PLAY_WIDTH / 2, 350)); //右
            mStarPosition.Add(new Vector2(Screen.PLAY_WIDTH / 2 - 64, 300)); //中

            mAnim = new Animation("kiparupa_anm", new Rectangle(0, 0, 64, 64), 0.25f);
            mMenuCursor = new List<Vector2>()
            {
                new Vector2(Screen.WIDTH/2, Screen.HEIGHT / 2 - 100),
                new Vector2(Screen.WIDTH/2, Screen.HEIGHT / 2),
                new Vector2(Screen.WIDTH/2, Screen.HEIGHT / 2 + 100),
            };
        }

        public void Draw()
        {
            var r = GameDevice.Instance().GetRenderer();
            GameDevice.Instance().GetRenderer().DrawTexture("floor", Vector2.Zero);
            ActorManager.Instance().Draw();

            r.DrawTexture("stateFrame", new Vector2(Screen.PLAY_WIDTH, 0));
            r.DrawTexture("resetButton", new Vector2(Screen.PLAY_WIDTH, GridSize.GRID_SIZE * 6));
            r.DrawTexture("undoButton", new Vector2(Screen.PLAY_WIDTH, GridSize.GRID_SIZE * 6));
            
            ActorManager.Instance().DrawWalkCount();
            r.DrawNumberRightEdgeAlignment("number", new Vector2(Screen.PLAY_WIDTH + GridSize.GRID_SIZE * 2, GridSize.GRID_SIZE), mStageNo);
            r.DrawNumberRightEdgeAlignment("number", new Vector2(Screen.PLAY_WIDTH + GridSize.GRID_SIZE * 2f, GridSize.GRID_SIZE * 5), mRecord);

            if (!mIsMenu)
            {
                if (Input.GetKeyState(Keys.X))
                {
                    r.DrawTexture("resetButtonDown", new Vector2(Screen.PLAY_WIDTH, GridSize.GRID_SIZE * 6));
                }
                if (Input.GetKeyState(Keys.Z))
                {
                    r.DrawTexture("undoButtonDown", new Vector2(Screen.PLAY_WIDTH, GridSize.GRID_SIZE * 6));
                }
            }


            if (mIsMenu)
            {
                r.DrawTexture("menuBG", Vector2.Zero);
                mAnim.Draw(mMenuCursor[mMenuNum]);
            }

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

            var rankList = FileManager.LoadRank("./Content/data/rankData.txt", mStageNo);
            mRankA = rankList[0];
            mRankB = rankList[1];
            mRecord = rankList[2];

            mIsMenu = false;
            mMenuNum = 0;
        }

        public bool IsEnd()
        {
            return mIsEndFlag;
        }

        public Scene Next()
        {
            return mNextScene;
        }

        public void Shutdown()
        {
            mStage.Unload();
            ActorManager.Instance().Clear();
        }

        public void Update(GameTime gameTime)
        {
            GameDevice.Instance().GetSound().PlayBGM("bgm_maoudamashii_healing16");
            if (Input.GetKeyTrigger(Keys.Escape))
            {
                mIsMenu = !mIsMenu;
                mMenuNum = 0;
            }

            if (!mIsMenu)
            {
                if (mIsClear)
                {
                    if (Input.GetKeyTrigger(Keys.Space))
                    {
                        mNextScene = Scene.StageSelect;
                        mIsEndFlag = true;
                    }
                    return;
                }

                if (Input.GetKeyUp(Keys.X))
                {
                    mStage.Reset();
                }
                if (ActorManager.Instance().IsClear())
                {
                    mIsClear = true;
                    if (ActorMove.mWalkCount <= mRankA)
                    {
                        mA = 0;
                    }
                    else if (mRankA < ActorMove.mWalkCount && ActorMove.mWalkCount <= mRankB)
                    {
                        mA = 3;
                    }
                    else
                    {
                        mA = 6;
                    }

                    if (ActorMove.mWalkCount < mRecord) {
                        FileManager.WriteRank("./Content/data/rankData.txt", mStageNo, mRecord);
                    }
                }
                ActorManager.Instance().Update(gameTime);
            }
            else
            {
                mAnim.Update(gameTime);
                mAnim.SetMotion(0);

                if (Input.GetKeyTrigger(Keys.Up))
                {
                    mMenuNum--;
                }
                if (Input.GetKeyTrigger(Keys.Down))
                {
                    mMenuNum++;
                }
                mMenuNum = Math.Abs(mMenuNum) % 3;

                if (Input.GetKeyTrigger(Keys.Space))
                {
                    if (mMenuNum == 0)
                    {
                        mNextScene = Scene.Title;
                        mIsEndFlag = true;
                    }
                    if (mMenuNum == 1)
                    {
                        mNextScene = Scene.StageSelect;
                        mIsEndFlag = true;
                    }
                    if(mMenuNum == 2)
                    {
                        Game1.mIsEndGame = true;
                    }
                }
            }
        }

        public Stage GetStage() {
            return mStage;
        }
    }
}
