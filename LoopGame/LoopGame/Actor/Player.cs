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

        public Player()
        {
            mPosition = new Vector2(Screen.WIDTH / 2, Screen.HEIGHT / 2);
            mMove = new ActorMove();
            GameDevice.Instance().GetRenderer().LoadContent("boss_LEFT");
        }

        public override void Draw()
        {
            GameDevice.Instance().GetRenderer().DrawTexture("boss_LEFT",mPosition);
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
