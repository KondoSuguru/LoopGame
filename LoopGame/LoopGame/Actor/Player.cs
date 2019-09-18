using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoopGame.Device;
using LoopGame.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LoopGame.Actor
{
    class Player : Actor
    {
        ActorMove mMove;
        IGameMediator mMediator;
        Animation mAnim;

        public Player(IGameMediator mediator) : base("kiparupa_anm")
        {
            GameDevice.Instance().GetRenderer().LoadContent(mFilename);
            mMediator = mediator;
            mMove = new ActorMove(mMediator);
            mAnim = new Animation(mFilename, new Rectangle(0, 0, 64, 64), 0.25f);
        }

        public override void Draw()
        {
            //GameDevice.Instance().GetRenderer().DrawTexture(mFilename, mPosition);
            mAnim.Draw(mPosition);
        }

        public override void Update(GameTime gameTime)
        {
            mAnim.Update(gameTime);
            mAnim.ChangeMotion();

            if (Input.GetKeyState(Keys.Left)) {
                mMove.MoveLeft(ref mPosition);
            }
            if (Input.GetKeyState(Keys.Right)) {
                mMove.MoveRight(ref mPosition);
            }
            if (Input.GetKeyState(Keys.Up)) {
                mMove.MoveUp(ref mPosition);
            }
            if (Input.GetKeyState(Keys.Down)) {
                mMove.MoveDown(ref mPosition);
            }

            //if (Input.GetKeyTrigger(Keys.Left)) {
            //    mMove.MoveLeft(ref mPosition);
            //}
            //if (Input.GetKeyTrigger(Keys.Right)) {
            //    mMove.MoveRight(ref mPosition);
            //}
            //if (Input.GetKeyTrigger(Keys.Up)) {
            //    mMove.MoveUp(ref mPosition);
            //}
            //if (Input.GetKeyTrigger(Keys.Down)) {
            //    mMove.MoveDown(ref mPosition);
            //}
            mMove.Move(ref mPosition);
        }

        public ActorMove GetMove() {
            return mMove;
        }
    }
}
