﻿using Microsoft.Xna.Framework;
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

        public Actor()
        {
            mPosition = new Vector2(0, 0);
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
