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

        public Player() : base("boss_LEFT")
        {
            mMove = new ActorMove();
            GameDevice.Instance().GetRenderer().LoadContent(mFilename);
        }

        public override void Draw()
        {
            GameDevice.Instance().GetRenderer().DrawTexture(mFilename, mPosition);
        }

        public override void Update(GameTime gameTime)
        {
            Move();
        }

        public void Move()
        {
            if (Input.GetKeyTrigger(Keys.Left))
                mMove.MoveLeft(ref mPosition);
            if (Input.GetKeyTrigger(Keys.Right))
                mMove.MoveRight(ref mPosition);
            if (Input.GetKeyTrigger(Keys.Up))
                mMove.MoveUp(ref mPosition);
            if (Input.GetKeyTrigger(Keys.Down))
                mMove.MoveDown(ref mPosition);
        }
    }
}
