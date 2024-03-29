﻿using System;
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
            r.LoadContent("clearBG");
            r.LoadContent("gamemenuframe");
            var s = GameDevice.Instance().GetSound();
            s.LoadBGM("tekuteku_arukou");
            s.LoadBGM("clear");
            s.LoadSE("cursor");
            s.LoadSE("choice");
            s.LoadSE("stage_choice");
            s.LoadSE("menu");
            s.LoadSE("menu_close");
            s.LoadSE("reset");

            mStarName = new List<string>() {
                "goldStar", "goldStar", "goldStar",
                "goldStar", "goldStar", "grayStar",
                "goldStar", "grayStar", "grayStar"
            };

            mStarPosition = new List<Vector2>();
            mStarPosition.Add(new Vector2(Screen.PLAY_WIDTH / 2 - 192, 225)); //左
            mStarPosition.Add(new Vector2(Screen.PLAY_WIDTH / 2 - 64, 225)); //中
            mStarPosition.Add(new Vector2(Screen.PLAY_WIDTH / 2 + 64, 225)); //右

            mAnim = new Animation("kiparupa_anm", new Rectangle(0, 0, 64, 64), 0.25f);
            mMenuCursor = new List<Vector2>()
            {
                new Vector2(Screen.WIDTH/2 + 160, Screen.HEIGHT / 2 - 182),
                new Vector2(Screen.WIDTH/2 + 160, Screen.HEIGHT / 2 - 82),
                new Vector2(Screen.WIDTH/2 + 160, Screen.HEIGHT / 2 + 18),
                new Vector2(Screen.WIDTH/2 + 160, Screen.HEIGHT / 2 + 118),

            };
        }

        public void Draw()
        {
            var r = GameDevice.Instance().GetRenderer();
            r.DrawTexture("floor", Vector2.Zero);
            ActorManager.Instance().Draw();

            r.DrawTexture("stateFrame", new Vector2(Screen.PLAY_WIDTH, 0));
            r.DrawTexture("resetButton", new Vector2(Screen.PLAY_WIDTH, GridSize.GRID_SIZE * 6));
            r.DrawTexture("undoButton", new Vector2(Screen.PLAY_WIDTH, GridSize.GRID_SIZE * 6));
            
            ActorManager.Instance().DrawWalkCount();
            r.DrawNumberRightEdgeAlignment("number", new Vector2(Screen.PLAY_WIDTH + GridSize.GRID_SIZE * 2, GridSize.GRID_SIZE), mStageNo);
            r.DrawNumberRightEdgeAlignment("number", new Vector2(Screen.PLAY_WIDTH + GridSize.GRID_SIZE * 2f, GridSize.GRID_SIZE * 5), mRecord);

            if (!mIsMenu && !mIsClear)
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
                r.DrawTexture("gamemenuframe", new Vector2( Screen.WIDTH / 2 - 256, 50));
                r.DrawTexture("titlemodoruDark", new Vector2(Screen.WIDTH / 2 - 192, Screen.HEIGHT / 2 - 190));
                r.DrawTexture("selectmodoruDark", new Vector2(Screen.WIDTH / 2 - 192, Screen.HEIGHT / 2 - 90));
                r.DrawTexture("menutojiruDark", new Vector2(Screen.WIDTH / 2 - 192, Screen.HEIGHT / 2 + 10));
                r.DrawTexture("gameowaruDark", new Vector2(Screen.WIDTH / 2 - 192, Screen.HEIGHT / 2 + 110));

                switch (mMenuNum)
                {
                    case 0: r.DrawTexture("titlemodoru", new Vector2(Screen.WIDTH / 2 - 192, Screen.HEIGHT / 2 - 190)); break;
                    case 1: r.DrawTexture("selectmodoru", new Vector2(Screen.WIDTH / 2 - 192, Screen.HEIGHT / 2 - 90)); break;
                    case 2: r.DrawTexture("menutojiru", new Vector2(Screen.WIDTH / 2 - 192, Screen.HEIGHT / 2 + 10)); break;
                    case 3: r.DrawTexture("gameowaru", new Vector2(Screen.WIDTH / 2 - 192, Screen.HEIGHT / 2 + 110)); break;
                }

                mAnim.Draw(mMenuCursor[mMenuNum]);
            }

            if (mIsClear)
            {
                r.DrawTexture("clearBG", Vector2.Zero);
                r.DrawTexture("CLEAR", new Vector2(Screen.PLAY_WIDTH / 2 - 256, Screen.HEIGHT / 2 - 200));
                for (int i = 0; i < mStarPosition.Count; i++)
                {
                    r.DrawTexture(mStarName[i + mA], mStarPosition[i]);
                }
                r.DrawTexture("selectmodoru", new Vector2(Screen.PLAY_WIDTH / 2 - 192, Screen.HEIGHT - 180));
                mAnim.Draw(new Vector2(Screen.PLAY_WIDTH / 2 + 160, Screen.HEIGHT - 170));
            }

            FadeDraw();
        }

        public void Initialize()
        {
            mStage = new Stage(this);
            mStage.Load("Stage0" + mStageNo.ToString() + ".csv");
            mIsEndFlag = false;
            mIsClear = false;

            var rankList = FileManager.LoadRank("./Content/data/rankData.txt", mStageNo);
            mRankA = rankList[0];
            mRankB = rankList[1];
            mRecord = rankList[2];

            mIsMenu = false;
            mMenuNum = 0;
            FadeInit();
        }

        public bool IsEnd()
        {
            return mIsEndFlag;
        }

        public Scene Next()
        {
            if(mIsClear || mNextScene == Scene.StageSelect)
                mStageNo--;

            return mNextScene;
        }

        public void Shutdown()
        {
            mStage.Unload();
            ActorManager.Instance().Clear();
            GameDevice.Instance().GetSound().StopBGM();
        }

        public void Update(GameTime gameTime)
        {
            var s = GameDevice.Instance().GetSound();
            string bgm = mIsClear ? "clear" : "tekuteku_arukou";
            s.PlayBGM(bgm);

            FadeUpdate(gameTime);
            if (mFadeState == FadeState.OUT)
                return;

            if (Input.GetKeyTrigger(Keys.Q))
            {
                if (mIsClear)
                {
                    return;
                }
                string se = mIsMenu ? "menu_close" : "menu";
                mIsMenu = !mIsMenu;
                mMenuNum = 0;
                s.PlaySE(se);
            }

            if (!mIsMenu)
            {
                if (mIsClear)
                {
                    mAnim.Update(gameTime);
                    mAnim.SetMotion(0);
                    if (Input.GetKeyTrigger(Keys.Space) || Input.GetKeyTrigger(Keys.Enter))
                    {
                        mNextScene = Scene.StageSelect;
                        SetFadeState(FadeState.OUT);
                        s.PlaySE("stage_choice");
                    }
                    return;
                }

                if (Input.GetKeyUp(Keys.X))
                {
                    mStage.Reset();
                    s.PlaySE("reset");
                }
                if (ActorManager.Instance().IsClear())
                {
                    mIsClear = true;
                    s.StopBGM();
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
                        mRecord = ActorMove.mWalkCount;
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
                    s.PlaySE("cursor");
                }
                if (Input.GetKeyTrigger(Keys.Down))
                {
                    mMenuNum++;
                    s.PlaySE("cursor");
                }
                mMenuNum = (mMenuNum + 4) % 4;

                if (Input.GetKeyTrigger(Keys.Space) || Input.GetKeyTrigger(Keys.Enter))
                {
                    string se = "stage_choice";
                    switch (mMenuNum)
                    {
                        case 0:
                            mNextScene = Scene.Title;
                            SetFadeState(FadeState.OUT);
                            break;
                        case 1:
                            mNextScene = Scene.StageSelect;
                            SetFadeState(FadeState.OUT);
                            break;
                        case 2:
                            mIsMenu = false;
                            se = "menu_close";
                            break;
                        case 3:
                            Game1.mIsEndGame = true;
                            break;
                    }
                    s.PlaySE(se);
                }
            }
        }

        public Stage GetStage() {
            return mStage;
        }
    }
}
