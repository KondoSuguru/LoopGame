using LoopGame.Device;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopGame.Actor
{
    sealed class ActorManager
    {
        private static ActorManager mInstance;
        private List<Actor> mActors;

        private ActorManager()
        {
            mActors = new List<Actor>();
        }

        public static ActorManager Instance()
        {
            if(mInstance == null)
            {
                mInstance = new ActorManager();
            }
            return mInstance;
        }

        public void Initialize()
        {
            if(mActors != null)
            {
                mActors.Clear();
            }
        }

        public void AddActor(Actor actor)
        {
            if (actor == null)
                return;
            mActors.Add(actor);
        }

        public void Update(GameTime gameTime)
        {
            foreach(var a in mActors)
            {
                a.Update(gameTime);
            }
        }

        public void Draw()
        {
            
            foreach (var a in mActors)
            {
                a.Draw();
            }
            
        }

        public void Clear()
        {
            mActors.Clear();
        }
    }
}
