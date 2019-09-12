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

        public Player(IGameMediator mediator) : base("boss_LEFT")
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
            if (Input.GetKeyState(Keys.Left))
            {
                mMove.MoveLeft(mPosition);
            }
            if (Input.GetKeyState(Keys.Right))
            { 
                mMove.MoveRight(mPosition);
            }
            if (Input.GetKeyState(Keys.Up))
            {
                mMove.MoveUp(mPosition);
            }
            if (Input.GetKeyState(Keys.Down))
            {
                mMove.MoveDown(mPosition);
            }
            mMove.Move(ref mPosition);
        }
    }
}
