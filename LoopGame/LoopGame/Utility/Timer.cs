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
        // 小クラスでも利用できるようprotected
        protected float limitTime; // 制限時間
        protected float currentTime; // 現在の時間

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="second">制限時間</param>
        public Timer(float second)
        {
            limitTime = 60 * second; // 60fps * 秒
        }

        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        public Timer()
            : this(1) // 1秒
        { }

        // 抽象メソッド
        public abstract void Initialize();

        public abstract void Update(GameTime gameTime);

        public abstract bool IsTime();

        public abstract float Rate();

        /// <summary>
        /// 制限時間を設定
        /// </summary>
        /// <param name="second"></param>
        public void SetTime(float second)
        {
            limitTime = 60 * second;
        }

        /// <summary>
        /// 現在時間の取得
        /// </summary>
        /// <returns></returns>
        public float Now()
        {
            return currentTime / 60f; // 60fps規定なので60で割る
        }
    }
}
