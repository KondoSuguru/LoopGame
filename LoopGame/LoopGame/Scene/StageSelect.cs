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
            GameDevice.Instance().GetRenderer().LoadContent("STAGE_SELECT");
            GameDevice.Instance().GetRenderer().LoadContent("kiparupa_anm");
            GameDevice.Instance().GetRenderer().LoadContent("menuBG");
            mStageNo = 1;
            mIsEndFlag = false;
            mIsMenu = false;
            mMenuNum = 0;
            mAnim = new Animation("kiparupa_anm", new Rectangle(0, 0, 64, 64), 0.25f);

            mCursorPosition = new List<Vector2>();
            mCursorPosition.Add(new Vector2(320, 192));
            mCursorPosition.Add(new Vector2(544, 192));
            mCursorPosition.Add(new Vector2(768, 192));
            mCursorPosition.Add(new Vector2(320, 320));
            mCursorPosition.Add(new Vector2(544, 320));
            mCursorPosition.Add(new Vector2(768, 320));
            mCursorPosition.Add(new Vector2(320, 448));
            mCursorPosition.Add(new Vector2(544, 448));
            mCursorPosition.Add(new Vector2(768, 448));

            mMenuCursor = new List<Vector2>()
            {
                new Vector2(Screen.WIDTH/2, Screen.HEIGHT / 2),
                new Vector2(Screen.WIDTH/2, Screen.HEIGHT / 2 + 100),
            };
        }

        public void Draw()
        {
            GameDevice.Instance().GetRenderer().DrawTexture("STAGE_SELECT", Vector2.Zero);
            if (!mIsMenu)
            {
                mAnim.Draw(mCursorPosition[mStageNo - 1]);
            }
            else
            {
                GameDevice.Instance().GetRenderer().DrawTexture("menuBG", Vector2.Zero);
                mAnim.Draw(mMenuCursor[mMenuNum]);
            }
        }

        public void Initialize() {
            //mStageNo = 1;
            mIsEndFlag = false;
            mIsMenu = false;
        }

        public bool IsEnd() {
            return mIsEndFlag;
        }

        public Scene Next() {
            return mNextScene;
        }

        public void Shutdown() {
        }

        public void Update(GameTime gameTime)
        {
            mAnim.Update(gameTime);

            if (Input.GetKeyTrigger(Keys.Escape))
            {
                mIsMenu = !mIsMenu;
                mMenuNum = 0;
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
                }
                if (Input.GetKeyTrigger(Keys.Left))
                {
                    mStageNo--;
                    if (mStageNo < 1)
                    {
                        mStageNo = mCursorPosition.Count();
                    }
                }
                if (Input.GetKeyTrigger(Keys.Up))
                {
                    mStageNo -= 3;
                    if (mStageNo < 1)
                    {
                        mStageNo += mStageCount;
                    }
                }
                if (Input.GetKeyTrigger(Keys.Down))
                {
                    mStageNo += 3;
                    if (mStageNo > mStageCount)
                    {
                        mStageNo -= mStageCount;
                    }
                }
                if (Input.GetKeyTrigger(Keys.Space))
                {
                    mNextScene = Scene.GamePlay;
                    mIsEndFlag = true;
                }
                
            }
            else
            {
                mAnim.SetMotion(0);

                if (Input.GetKeyTrigger(Keys.Up))
                {
                    mMenuNum--;
                }
                if (Input.GetKeyTrigger(Keys.Down))
                {
                    mMenuNum++;
                }
                mMenuNum = Math.Abs(mMenuNum) % 2;

                if (Input.GetKeyTrigger(Keys.Space))
                {
                    if(mMenuNum == 0)
                    {
                        mNextScene = Scene.Title;
                        mIsEndFlag = true;
                    }
                    if(mMenuNum == 1)
                    {
                        Game1.mIsEndGame = true;
                    }
                }
            }
        }
    }
}
