using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using LoopGame.Utility;
using Microsoft.Xna.Framework.Input;
using LoopGame.Device;

namespace LoopGame.Scene {
    class StageSelect : SceneBase, IScene {
        private List<Vector2> mCursorPosition;
        private List<Vector2> mMenuCursor;
        private readonly int mStageCount = 9;
        private Animation mAnim;
        private bool mIsMenu;
        private int mMenuNum;
        private Scene mNextScene;

        public StageSelect() {
            var r = GameDevice.Instance().GetRenderer();
            r.LoadContent("STAGE_SELECT");
            r.LoadContent("kiparupa_anm");
            r.LoadContent("menuBG");
            r.LoadContent("titlemodoru");
            r.LoadContent("selectmodoru");
            r.LoadContent("gameowaru");
            r.LoadContent("menutojiru");
            r.LoadContent("titlemodoruDark");
            r.LoadContent("selectmodoruDark");
            r.LoadContent("gameowaruDark");
            r.LoadContent("menutojiruDark");
            r.LoadContent("selectmenuframe");
            var s = GameDevice.Instance().GetSound();
            s.LoadSE("stage_choice");
            s.LoadSE("cursor");
            s.LoadSE("menu");
            s.LoadSE("menu_close");
            mStageNo = 0;
            mIsEndFlag = false;
            mIsMenu = false;
            mMenuNum = 0;
            mAnim = new Animation("kiparupa_anm", new Rectangle(0, 0, 64, 64), 0.25f);

            mCursorPosition = new List<Vector2>()
            {
                new Vector2(320, 192),new Vector2(544, 192),new Vector2(768, 192),
                new Vector2(320, 320),new Vector2(544, 320),new Vector2(768, 320),
                new Vector2(320, 448),new Vector2(544, 448),new Vector2(768, 448),
            };

            mMenuCursor = new List<Vector2>()
            {
                new Vector2(Screen.WIDTH/2 + 160, Screen.HEIGHT / 2 - 132),
                new Vector2(Screen.WIDTH/2 + 160, Screen.HEIGHT / 2 - 32),
                new Vector2(Screen.WIDTH/2 + 160, Screen.HEIGHT / 2 + 68),
            };
        }

        public void Draw()
        {
            GameDevice.Instance().GetRenderer().DrawTexture("STAGE_SELECT", Vector2.Zero);
            if (!mIsMenu)
            {
                mAnim.Draw(mCursorPosition[mStageNo]);
            }
            else
            {
                GameDevice.Instance().GetRenderer().DrawTexture("menuBG", Vector2.Zero);
                GameDevice.Instance().GetRenderer().DrawTexture("selectmenuframe", new Vector2(Screen.WIDTH / 2 - 256, 100));
                GameDevice.Instance().GetRenderer().DrawTexture("titlemodoruDark", new Vector2(Screen.WIDTH / 2 - 192, Screen.HEIGHT / 2 - 140));
                GameDevice.Instance().GetRenderer().DrawTexture("menutojiruDark", new Vector2(Screen.WIDTH / 2 - 192, Screen.HEIGHT / 2 - 40));
                GameDevice.Instance().GetRenderer().DrawTexture("gameowaruDark", new Vector2(Screen.WIDTH / 2 - 192, Screen.HEIGHT / 2 + 60));
                switch (mMenuNum)
                {
                    case 0: GameDevice.Instance().GetRenderer().DrawTexture("titlemodoru", new Vector2(Screen.WIDTH / 2 - 192, Screen.HEIGHT / 2 - 140)); break;
                    case 1:  GameDevice.Instance().GetRenderer().DrawTexture("menutojiru", new Vector2(Screen.WIDTH / 2 - 192, Screen.HEIGHT / 2 - 40));break;
                    case 2: GameDevice.Instance().GetRenderer().DrawTexture("gameowaru", new Vector2(Screen.WIDTH / 2 - 192, Screen.HEIGHT / 2 + 60)); break;
                }
                mAnim.Draw(mMenuCursor[mMenuNum]);
            }

            FadeDraw();
        }

        public void Initialize() {
            mIsEndFlag = false;
            mIsMenu = false;
            var s = GameDevice.Instance().GetSound();
            if (s.IsStoppedBGM()) {
                s.PlayBGM("titleBGM");
            }
            FadeInit();
        }

        public bool IsEnd() {
            return mIsEndFlag;
        }

        public Scene Next() {
            mStageNo++;
            return mNextScene;
        }

        public void Shutdown() {
            GameDevice.Instance().GetSound().StopBGM();
        }

        public void Update(GameTime gameTime)
        {
            mAnim.Update(gameTime);
            var s = GameDevice.Instance().GetSound();

            FadeUpdate(gameTime);
            if (mFadeState == FadeState.OUT)
                return;

            if (Input.GetKeyTrigger(Keys.Q))
            {
                string se = mIsMenu ? "menu_close" : "menu";
                mIsMenu = !mIsMenu;
                mMenuNum = 0;
                s.PlaySE(se);
            }

            if (!mIsMenu)
            {
                mAnim.SetMotion(2);

                if (Input.GetKeyTrigger(Keys.Right))
                {
                    mStageNo++;
                    s.PlaySE("cursor");
                }
                if (Input.GetKeyTrigger(Keys.Left))
                {
                    mStageNo--;
                    s.PlaySE("cursor");
                }
                if (Input.GetKeyTrigger(Keys.Up))
                {
                    mStageNo -= 3;
                    s.PlaySE("cursor");
                }
                if (Input.GetKeyTrigger(Keys.Down))
                {
                    mStageNo += 3;
                    s.PlaySE("cursor");
                }
                mStageNo = (mStageNo + mStageCount) % mStageCount;

                if (Input.GetKeyTrigger(Keys.Space) || Input.GetKeyTrigger(Keys.Enter))
                {
                    
                    mNextScene = Scene.GamePlay;
                    SetFadeState(FadeState.OUT);
                    s.PlaySE("stage_choice");
                }
            }
            else
            {
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
                mMenuNum = (mMenuNum + 3) % 3;

                if (Input.GetKeyTrigger(Keys.Space) || Input.GetKeyTrigger(Keys.Enter))
                {
                    string se = "";
                    switch (mMenuNum)
                    {
                        case 0:
                            mNextScene = Scene.Title;
                            SetFadeState(FadeState.OUT);
                            se = "stage_choice";
                            break;
                        case 1:
                            mIsMenu = false;
                            se = "menu_close";
                            break;
                        case 2:
                            Game1.mIsEndGame = true;
                            se = "stage_choice";
                            break;
                    }
                    s.PlaySE(se);
                }
            }
        }
    }
}
