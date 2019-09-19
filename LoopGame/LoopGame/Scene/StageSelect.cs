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
        private readonly int mStageCount = 9;
        private Animation mAnim;

        public StageSelect() {
            GameDevice.Instance().GetRenderer().LoadContent("STAGE_SELECT");
            GameDevice.Instance().GetRenderer().LoadContent("kiparupa_anm");
            mStageNo = 1;
            mIsEndFlag = false;
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
        }

        public void Draw() {
            GameDevice.Instance().GetRenderer().DrawTexture("STAGE_SELECT", Vector2.Zero);
            mAnim.Draw(mCursorPosition[mStageNo - 1]);
        }

        public void Initialize() {
            mStageNo = 1;
            mIsEndFlag = false;
        }

        public bool IsEnd() {
            return mIsEndFlag;
        }

        public Scene Next() {
            return Scene.GamePlay;
        }

        public void Shutdown() {
        }

        public void Update(GameTime gameTime)
        {
            mAnim.Update(gameTime);
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
                if(mStageNo < 1)
                {
                    mStageNo += mStageCount;
                }
            }
            if (Input.GetKeyTrigger(Keys.Down))
            {
                mStageNo += 3;
                if(mStageNo > mStageCount)
                {
                    mStageNo -= mStageCount;
                }
            }


            if (Input.GetKeyTrigger(Keys.Space))
            {
                mIsEndFlag = true;
            }
        }
    }
}
