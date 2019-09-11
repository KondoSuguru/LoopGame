using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oikake.Scene.Effect
{
    /// <summary>
    /// 振動
    /// </summary>
    class Blast
    {
        private float amount; // 量
        private float magnitude; // 震度
        private Vector2 center; // 中心点

        // プロパティでGeterを記述
        public float Amount { get => amount; }
        public float Magnitude { get => magnitude; }
        public Vector2 Center { get => center; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="magnitude">震度</param>
        /// <param name="center">中心点</param>
        public Blast(float magnitude, Vector2 center)
        {
            amount = magnitude;
            this.magnitude = magnitude;
            this.center = center;
        }


        public void Update(GameTime gameTime)
        {
            // 前のフレームから過ぎた時間を取得
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            // 徐々に振動を減らす
            amount = (amount >= 0.0f) ? (amount - delta) : (0.0f);
        }
    }
}
