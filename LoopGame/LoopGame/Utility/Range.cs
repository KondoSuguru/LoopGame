using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopGame.Utility
{
    class Range
    {
        private int mFirst; 
        private int mEnd; 

        public Range(int first, int end)
        {
            mFirst = first;
            mEnd = end;
        }

        public int First()
        {
            return mFirst;
        }

        public int End()
        {
            return mEnd;
        }

        public bool IsWithin(int num)
        {
            if (num < mFirst)
            {
                return false;
            }
            if (num > mEnd)
            {
                return false;
            }

            return true;
        }

        public bool IsOutOfRange()
        {
            return mFirst >= mEnd;
        }


        public bool IsOutOfRange(int num)
        {
            return !IsWithin(num);
        }
    }
}
