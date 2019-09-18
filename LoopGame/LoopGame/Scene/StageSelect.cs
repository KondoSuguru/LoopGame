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

        public StageSelect() {
            GameDevice.Instance().GetRenderer().LoadContent("STAGE_SELECT");
            mStageNo = 0;
            mIsEndFlag = false;
        }

        public void Draw() {
            GameDevice.Instance().GetRenderer().DrawTexture("STAGE_SELECT", new Vector2(Screen.WIDTH / 2 - 290, Screen.HEIGHT / 2 - 125));
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
            } else if (Input.GetKeyTrigger(Keys.D3)) {
                mStageNo = 3;
                mIsEndFlag = true;
            }
        }
    }
}
