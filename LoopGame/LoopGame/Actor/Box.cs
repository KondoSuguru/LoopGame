using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoopGame.Device;
using Microsoft.Xna.Framework;

namespace LoopGame.Actor {
    class Box : Actor {
        IGameMediator mMediator;
        ActorMove mMove;

        public Box(IGameMediator mediator) : base("box") {
            mMediator = mediator;
            mMove = new ActorMove(mMediator);
            GameDevice.Instance().GetRenderer().LoadContent(mFilename);
        }

        public override void Draw() {
            GameDevice.Instance().GetRenderer().DrawTexture(mFilename, mPosition);
        }

        public override void Update(GameTime gameTime) {
        }

        public void BranchUpdate(ActorMove.MoveState state, ref bool isNotMove) {
            var pos = mPosition;

            if (state == ActorMove.MoveState.LEFT) {
                if (mPosition.X <= 0f) {
                    pos.X = Screen.WIDTH - GridSize.GRID_SIZE;
                } else {
                    pos.X = mPosition.X - GridSize.GRID_SIZE + 5;
                }
            } else if (state == ActorMove.MoveState.RIGHT) {
                if (mPosition.X >= Screen.WIDTH - GridSize.GRID_SIZE) {
                    pos.X = 0f;
                } else {
                    pos.X = mPosition.X + GridSize.GRID_SIZE + 5;
                }
            } else if (state == ActorMove.MoveState.UP) {
                if (mPosition.Y <= 0f) {
                    pos.Y = Screen.HEIGHT - GridSize.GRID_SIZE;
                } else {
                    pos.Y = mPosition.Y - GridSize.GRID_SIZE + 5;
                }
            } else if (state == ActorMove.MoveState.DOWN) {
                if (mPosition.Y >= Screen.HEIGHT - GridSize.GRID_SIZE) {
                    pos.Y = 0f;
                } else {
                    pos.Y = mPosition.Y + GridSize.GRID_SIZE + 5;
                }
            }

            if (mMove.IsStageCollision(pos) || CollisionOtherBox(pos)) {
                isNotMove = true;
            }
        }

        private bool CollisionOtherBox(Vector2 pos) {
            var actors = ActorManager.Instance().GetActors();
            foreach (var actor in actors) {
                if (!(actor is Box) || actor == this) { //Box以外は興味ない
                    continue;
                }
                if (Actor.IsCollision(pos, actor)) {
                    return true;
                }
            }
            return false;
        }
    }
}
