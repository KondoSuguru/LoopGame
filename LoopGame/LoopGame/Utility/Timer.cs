using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace LoopGame.Utility
{
    abstract class Timer
    {
        protected float mLimitTime;
        protected float mCurrentTime; 

        public Timer(float second)
        {
            mLimitTime = 60 * second;
        }


        public Timer()
            : this(1) 
        { }

        public abstract void Initialize();

        public abstract void Update(GameTime gameTime);

        public abstract bool IsTime();

        public abstract float Rate();

        public void SetTime(float second)
        {
            mLimitTime = 60 * second;
        }

        public float Now()
        {
            return mCurrentTime / 60f; 
        }
    }
}
