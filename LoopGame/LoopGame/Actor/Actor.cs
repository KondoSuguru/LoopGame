using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoopGame.Device;

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

        public void Translate(Vector2 translation) {
            mPosition += translation;
        }

        //矩形
        public static bool IsCollision(Vector2 pos, Actor other) {
            Rectangle intersect = Rectangle.Intersect(
                new Rectangle((int)pos.X, (int)pos.Y, GridSize.GRID_SIZE, GridSize.GRID_SIZE),
                new Rectangle((int)other.mPosition.X, (int)other.mPosition.Y, GridSize.GRID_SIZE, GridSize.GRID_SIZE));
            //trueなら当たってない
            if (!intersect.IsEmpty) {
                return true;
            }
            return false;
        }

        internal void SetPosition(object p) {
            throw new NotImplementedException();
        }
    }
}
