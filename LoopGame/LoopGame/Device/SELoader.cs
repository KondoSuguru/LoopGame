using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace LoopGame.Device
{
    class SELoader : Loader
    {
        private Sound mSound;

        public SELoader(string[,] resources) : base(resources)
        {
            mSound = GameDevice.Instance().GetSound();
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            // まず終了フラグを有効にして
            mIsEndFlag = true;
            // カウンタが最大に達していないか？
            if (mCounter < mMaxNumber)
            {
                // SEの読み込み
                mSound.LoadSE(mResources[mCounter, 0], mResources[mCounter, 1]);
                // カウンタを増やす
                mCounter += 1;
                // 読み込むものがあったので終了フラグを継続に設定
                mIsEndFlag = false;
            }

        }
    }
}
