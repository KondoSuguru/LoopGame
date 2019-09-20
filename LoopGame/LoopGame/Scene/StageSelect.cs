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
        private bool mIsEndFlag;
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
            var s = GameDevice.Instance().GetSound();
            s.LoadSE("stage_choice");
            s.LoadSE("cursor");
            s.LoadSE("menu");
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
        }

        public void Initialize() {
            mIsEndFlag = false;
            mIsMenu = false;
            var s = GameDevice.Instance().GetSound();
            if (s.IsStoppedBGM()) {
                s.PlayBGM("titleBGM");
            }
        }

        public bool IsEnd() {
            return mIsEndFlag;
        }

        public Scene Next() {
            return mNextScene;
        }

        public void Shutdown() {
            GameDevice.Instance().GetSound().StopBGM();
        }

        public void Update(GameTime gameTime)
        {
            mAnim.Update(gameTime);
            var s = GameDevice.Instance().GetSound();

            if (Input.GetKeyTrigger(Keys.Escape) || Input.GetKeyTrigger(Keys.C))
            {
                mIsMenu = !mIsMenu;
                mMenuNum = 0;
                s.PlaySE("menu");
            }

            if (!mIsMenu)
            {
                mAnim.SetMotion(2);

                if (Input.GetKeyTrigger(Keys.Right))
                {
                    mStageNo++;
                    if (mStageNo > mStageCount)
                    {
                        mStageNo = 1;
                    }
                    s.PlaySE("cursor");
                }
                if (Input.GetKeyTrigger(Keys.Left))
                {
                    mStageNo--;
                    if (mStageNo < 1)
                    {
                        mStageNo = mCursorPosition.Count();
                    }
                    s.PlaySE("cursor");
                }
                if (Input.GetKeyTrigger(Keys.Up))
                {
                    mStageNo -= 3;
                    if (mStageNo < 1)
                    {
                        mStageNo += mStageCount;
                    }
                    s.PlaySE("cursor");
                }
                if (Input.GetKeyTrigger(Keys.Down))
                {
                    mStageNo += 3;
                    if (mStageNo > mStageCount)
                    {
                        mStageNo -= mStageCount;
                    }
                    s.PlaySE("cursor");
                }
                mStageNo = (mStageNo + mStageCount) % mStageCount;

                if (Input.GetKeyTrigger(Keys.Space) || Input.GetKeyTrigger(Keys.Enter))
                {
                    mStageNo++;
                    mNextScene = Scene.GamePlay;
                    mIsEndFlag = true;
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
                    switch (mMenuNum)
                    {
                        case 0:
                            mNextScene = Scene.Title;
                            mIsEndFlag = true;
                            break;
                        case 1:
                            mIsMenu = false;
                            break;
                        case 2: Game1.mIsEndGame = true; break;
                    }
                    s.PlaySE("stage_choice");
                }
            }
        }
    }
}
