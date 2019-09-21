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
            r.LoadContent("titleStartDark");
            r.LoadContent("titleEndDark");
            r.LoadContent("titleBG");
            r.LoadContent("floor");
            var s = GameDevice.Instance().GetSound();
            s.LoadSE("choice");
            s.LoadSE("stage_choice");
            s.LoadSE("cursor");
            s.LoadBGM("titleBGM");
        }

        public void Draw() {
            var r = GameDevice.Instance().GetRenderer();
            r.DrawTexture("floor", Vector2.Zero);
            r.DrawTexture("titleBG", Vector2.Zero);
            r.DrawTexture("TITLE", new Vector2(Screen.WIDTH / 2 - 352, Screen.HEIGHT / 2 - 185));
            if (mMode == Mode.Next)
            {
                r.DrawTexture("titleStart", new Vector2(300, Screen.HEIGHT - 150));
                r.DrawTexture("titleEndDark", new Vector2(Screen.WIDTH - 492, Screen.HEIGHT - 150));
            }
            else
            {
                r.DrawTexture("titleStartDark", new Vector2(300, Screen.HEIGHT - 150));
                r.DrawTexture("titleEnd", new Vector2(Screen.WIDTH - 492, Screen.HEIGHT - 150));
            }
            mAnim.Draw(mPositions[(int)mMode]);
        }

        public void Initialize() {
            mStageNo = 0;
            mIsEndFlag = false;
            mMode = Mode.Next;
        }

        public bool IsEnd() {
            return mIsEndFlag;
        }

        public Scene Next() {
            return Scene.Rule;
        }

        public void Shutdown() {
        }

        public void Update(GameTime gameTime) {
            var s = GameDevice.Instance().GetSound();
            s.PlayBGM("titleBGM");
            if (Input.GetKeyTrigger(Keys.Space) || Input.GetKeyTrigger(Keys.Enter)) {
                if (mMode == Mode.Next) {
                    mIsEndFlag = true;
                } else if (mMode == Mode.End) {
                    Game1.mIsEndGame = true;
                }
                s.PlaySE("stage_choice");
            }

            if (Input.GetKeyTrigger(Keys.Right) || Input.GetKeyTrigger(Keys.Left)) {
                if (mMode == Mode.Next) {
                    mMode = Mode.End;
                } else {
                    mMode = Mode.Next;
                }

                s.PlaySE("cursor");
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
