using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopGame.Device {
    abstract class Loader {
        protected string[,] mResources; // リソースアセット名群
        protected int mCounter; // 現在登録しているカウンタ
        protected int mMaxNumber; // 最大登録数
        protected bool mIsEndFlag; // 終了フラグ

        public Loader(string[,] resources) {
            mResources = resources;
        }

        public void Initialize() {
            mCounter = 0;
            mIsEndFlag = false;
            mMaxNumber = 0;

            //  条件がFalseのときに、エラー分を出す
            Debug.Assert(mResources != null,
                "リソースデータ登録情報がおかしいです");
            // 配列から、配列から
            mMaxNumber = mResources.GetLength(0);
        }

        public int RegistMAXNum() {
            return mMaxNumber;
        }

        public int CurrentCount() {
            return mCounter;
        }

        public bool IsEnd() {
            return mIsEndFlag;
        }

        public abstract void Update(GameTime deltaTime);
    }
}
