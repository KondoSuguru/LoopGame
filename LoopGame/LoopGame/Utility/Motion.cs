using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopGame.Utility
{
    class Motion
    {
        private Range mRange;
        private Timer mTimer; 
        private int mMotionNumber; 

        private Dictionary<int, Rectangle> mRectangles = new Dictionary<int, Rectangle>();

        public Motion()
        {
            Initialize(new Range(0, 0), new CountDownTimer());
        }

        public Motion(Range range, Timer timer)
        {
            Initialize(range, timer);
        }

        public void Initialize(Range range, Timer timer)
        {
            mRange = range;
            mTimer = timer;

            mMotionNumber = range.First();
        }


        public void Add(int index, Rectangle rect)
        {
            if (mRectangles.ContainsKey(index))
            {
                return;
            }
            mRectangles.Add(index, rect);
        }

        private void MotionUpdate()
        {
            mMotionNumber++;

            if (mRange.IsOutOfRange(mMotionNumber))
            {
                mMotionNumber = mRange.First();
            }
        }

        public void Update(GameTime gameTime)
        {
            if (mRange.IsOutOfRange())
            {
                return;
            }

            mTimer.Update(gameTime);
            if (mTimer.IsTime())
            {
                mTimer.Initialize();
                MotionUpdate();
            }
        }

        public Rectangle DrawingRange()
        {
            return mRectangles[mMotionNumber];
        }
    }
}
