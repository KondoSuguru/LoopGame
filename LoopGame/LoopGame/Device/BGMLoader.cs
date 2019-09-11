using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace LoopGame.Device {
    class BGMLoader : Loader {
        public BGMLoader(string[,] resources) : base(resources) {
            base.Initialize();
        }

        public override void Update(GameTime deltaT) {
            mIsEndFlag = true;
            if (mCounter < mMaxNumber) {
                GameDevice.Instance().GetSound().LoadBGM(mResources[mCounter, 0], mResources[mCounter, 1]);
                mCounter += 1;
                mIsEndFlag = false;
            }
        }
    }
}
