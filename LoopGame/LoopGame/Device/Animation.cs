using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoopGame.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LoopGame.Device {
    class Animation {
        string mFilename;
        Rectangle mSize;
        float mCurrentTimer;
        float mChangeAnimationTimer;
        int mTextureNo;

        public Animation(string filename, Rectangle size, float change = 1f) {
            mFilename = filename;
            mSize = size;
            mChangeAnimationTimer = change * 60;
            mCurrentTimer = 0f;
            mTextureNo = 0;
        }

        public void Update(GameTime gameTime) {
            ChangeMotion();

            mCurrentTimer += 1f;

            if (mCurrentTimer >= mChangeAnimationTimer) {
                mCurrentTimer = 0f;
                Change();
            }
        }

        public void Draw(Vector2 pos) {
            GameDevice.Instance().GetRenderer().DrawTexture(mFilename, pos, mSize);
        }

        public void SetAnimationTime(float time) {
            mChangeAnimationTimer = time;
        }

        public void SetMotion(int nomber) {
            mSize.Y = nomber * mSize.Height;
        }

        private void ChangeMotion() {
            if (Input.GetKeyTrigger(Keys.Left)) {
                SetMotion(2);
            } else if (Input.GetKeyUp(Keys.Left)) {
                SetMotion(0);
            }
            if (Input.GetKeyTrigger(Keys.Right)) {
                SetMotion(3);
            } else if (Input.GetKeyUp(Keys.Right)) {
                SetMotion(1);
            }
        }

        private void Change() {
            mTextureNo += 1;
            if (mTextureNo > 3) {
                mTextureNo = 0;
            }
            mSize.X = mTextureNo * mSize.Width;
        }
    }
}
