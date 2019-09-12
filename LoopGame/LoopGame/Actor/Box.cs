using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoopGame.Device;
using Microsoft.Xna.Framework;

namespace LoopGame.Actor
{
    class Box : Actor
    {
        IGameMediator mMediator;
        ActorMove mMove;

        public Box(IGameMediator mediator) : base("box")
        {
            mMediator = mediator;
            mMove = new ActorMove(mMediator);
            GameDevice.Instance().GetRenderer().LoadContent(mFilename);
        }

        public override void Draw()
        {
            GameDevice.Instance().GetRenderer().DrawTexture(mFilename, mPosition);
        }

        public override void Update(GameTime gameTime)
        {
        }

        public bool BranchUpdate(ActorMove.MoveState state) {
            bool isMove = false;

            switch (state) {
                case ActorMove.MoveState.LEFT:
                    var pos = new Vector2(mPosition.X - GridSize.GRID_SIZE, mPosition.Y);
                    if (mMove.IsStageCollision(pos) || CollisionOtherBox(pos)) {
                        isMove = true;
                    }
                    break;
                case ActorMove.MoveState.RIGHT:
                    pos = new Vector2(mPosition.X + GridSize.GRID_SIZE, mPosition.Y);
                    if (mMove.IsStageCollision(pos) || CollisionOtherBox(pos)) {
                        isMove = true;
                    }
                    break;
                case ActorMove.MoveState.UP:
                    pos = new Vector2(mPosition.X, mPosition.Y - GridSize.GRID_SIZE);
                    if (mMove.IsStageCollision(pos) || CollisionOtherBox(pos)) {
                        isMove = true;
                    }
                    break;
                case ActorMove.MoveState.DOWN:
                    pos = new Vector2(mPosition.X, mPosition.Y + GridSize.GRID_SIZE);
                    if (mMove.IsStageCollision(pos) || CollisionOtherBox(pos)) {
                        isMove = true;
                    }
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }

            return isMove;
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
