using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace LoopGame.Device {
    class SELoader : Loader {
        public SELoader(string[,] resources) : base(resources) {
            base.Initialize();
        }

        public override void Update(GameTime gameTime) {
            mIsEndFlag = true;
            if (mCounter < mMaxNumber) {
                GameDevice.Instance().GetSound().LoadSE(mResources[mCounter, 0], mResources[mCounter, 1]);
                mCounter += 1;
                mIsEndFlag = false;
            }

        }
    }
}
