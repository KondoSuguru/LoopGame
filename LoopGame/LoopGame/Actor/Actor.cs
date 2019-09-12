using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopGame.Actor
{
    abstract class Actor
    {
        protected Vector2 mPosition;
        protected string mFilename;

        public Actor(string filename)
        {
            ActorManager.Instance().AddActor(this);

            mPosition = Vector2.Zero;
            mFilename = filename;
        }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw();

        public void SetPosition(Vector2 position)
        {
            mPosition = position;
        }

        public Vector2 GetPosition() {
            return mPosition;
        }
    }
}
