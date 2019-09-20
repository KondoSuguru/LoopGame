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
    class Title : SceneBase, IScene {
        private bool mIsEndFlag;
        private List<Vector2> mPositions;
        private enum Mode {
            Next,
            End,

            ModeCount
        }
        Mode mMode;

        public Title() {
            mIsEndFlag = false;
            mPositions = new List<Vector2>() {
                new Vector2(100f, 500f),
                new Vector2(600f, 500f)
            };
            mMode = Mode.Next;

            GameDevice.Instance().GetRenderer().LoadContent("TITLE");
            GameDevice.Instance().GetRenderer().LoadContent("boss_LEFT");
            GameDevice.Instance().GetRenderer().LoadContent("titleStart");
            GameDevice.Instance().GetRenderer().LoadContent("titleEnd");
        }

        public void Draw() {
            var r = GameDevice.Instance().GetRenderer();
            r.DrawTexture("TITLE", new Vector2(Screen.WIDTH / 2 - 352, Screen.HEIGHT / 2 - 200));
            r.DrawTexture("titleStart", new Vector2(300, Screen.HEIGHT - 150));
            r.DrawTexture("titleEnd", new Vector2(Screen.WIDTH - 492, Screen.HEIGHT - 150));

            if (mMode == Mode.Next) {
                r.DrawTexture("boss_LEFT", mPositions[0], Color.Red);
                r.DrawTexture("boss_LEFT", mPositions[1], Color.White);
            }

            if (mMode == Mode.End) {
                r.DrawTexture("boss_LEFT", mPositions[1], Color.Red);
                r.DrawTexture("boss_LEFT", mPositions[0], Color.White);
            }
        }

        public void Initialize() {
            mStageNo = 1;
            mIsEndFlag = false;
            mMode = Mode.Next;
        }

        public bool IsEnd() {
            return mIsEndFlag;
        }

        public Scene Next() {
            return Scene.StageSelect;
        }

        public void Shutdown() {
        }

        public void Update(GameTime gameTime) {
            if (Input.GetKeyTrigger(Keys.Space)) {
                if (mMode == Mode.Next) {
                    mIsEndFlag = true;
                } else if (mMode == Mode.End) {
                    Game1.mIsEndGame = true;
                }
            }

            if (Input.GetKeyTrigger(Keys.Right) || Input.GetKeyTrigger(Keys.Left)) {
                if (mMode == Mode.Next) {
                    mMode = Mode.End;
                } else {
                    mMode = Mode.Next;
                }
            }
        }
    }
}
