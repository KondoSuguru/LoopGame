using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using LoopGame.Utility;
using Microsoft.Xna.Framework.Input;

namespace LoopGame.Scene {
    class StageSelect : SceneBase, IScene {
        private bool mIsEndFlag;

        public StageSelect() {
            mStageNo = 0;
            mIsEndFlag = false;
        }

        public void Draw() {
        }

        public void Initialize() {
            mStageNo = 0;
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

        public void Update(GameTime gameTime) {
            if (Input.GetKeyTrigger(Keys.D1)) {
                mStageNo = 1;
                mIsEndFlag = true;
            } else if (Input.GetKeyTrigger(Keys.D2)) {
                mStageNo = 2;
                mIsEndFlag = true;
            }
        }
    }
}
