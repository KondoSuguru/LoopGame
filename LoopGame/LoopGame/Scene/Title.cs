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
    class Title : IScene {
        private bool mIsEndFlag;
        private List<Vector2> mPositions;
        private enum Mode {
            Next,
            End,

            ModeCount
        }
        Mode mMode;
        Animation mAnim;

        public Title() {
            mIsEndFlag = false;
            mPositions = new List<Vector2>() {
                new Vector2(370f, 360f),
                new Vector2(600f, 360f)
            };
            mMode = Mode.Next;
            mAnim = new Animation("kiparupa_anm", new Rectangle(0, GridSize.GRID_SIZE, GridSize.GRID_SIZE, GridSize.GRID_SIZE), 0.25f);

            var r = GameDevice.Instance().GetRenderer();
            r.LoadContent("kiparupa_anm");
            r.LoadContent("TITLE");
            r.LoadContent("titleStart");
            r.LoadContent("titleEnd");
        }

        public void Draw() {
            var r = GameDevice.Instance().GetRenderer();
            r.DrawTexture("TITLE", new Vector2(Screen.WIDTH / 2 - 352, Screen.HEIGHT / 2 - 200));
            r.DrawTexture("titleStart", new Vector2(300, Screen.HEIGHT - 150));
            r.DrawTexture("titleEnd", new Vector2(Screen.WIDTH - 492, Screen.HEIGHT - 150));

            mAnim.Draw(mPositions[(int)mMode]);
        }

        public void Initialize() {
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

            mAnim.Update(gameTime);
            if (mMode == Mode.Next) {
                mAnim.SetMotion(2);
            } else if (mMode == Mode.End) {
                mAnim.SetMotion(0);
            }
        }
    }
}
