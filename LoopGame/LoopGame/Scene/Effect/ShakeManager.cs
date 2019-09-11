using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Oikake.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oikake.Scene.Effect
{
    /// <summary>
    /// 振動管理クラス
    /// </summary>
    class ShakeManager
    {
        private Blast blast = null;
        private readonly int ShakeStrength = 4; // エフェクトの濃さ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ShakeManager()
        { }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            // ガード説：nullなら抜ける
            if (blast == null)
            {
                return;
            }
            // 振動を更新
            blast.Update(gameTime);
        }

        /// <summary>
        /// 振動設定
        /// </summary>
        /// <param name="magnitude"></param>
        /// <param name="center"></param>
        public void SetUpBlast(float magnitude, Vector2 center)
        {
            blast = new Blast(magnitude, center);
        }

        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="renderer"></param>
        public void Draw(Renderer renderer)
        {
            // ガード説：nullなら抜ける
            if (blast == null)
            {
                return;
            }
            // 振動量がなくなったら抜ける
            if (blast.Amount <= 0.0f)
            {
                blast = null;
                return;
            }
            // 設定されている震度の濃さ分だけエフェクトを描画
            for (int i = 0; i < ShakeStrength; i++)
            {
                // 原点を計算
                Vector2 origin = blast.Center / 2;

                // 描画位置を計算
                Vector2 position = blast.Center - origin;

                // 値を計算
                float alpha = 0.35f * (blast.Amount / blast.Magnitude);
                Color color = new Color(1.0f, 1.0f, 1.0f, alpha);

                // 大きさを計算
                float scale = (1.0f + (blast.Magnitude - blast.Amount) * 0.1f + ((float)(i + 1) / 40.0f));

                // エフェクトを描画
                renderer.DrawRenderTargetTexture(
                    position,
                    null,
                    0.0f,
                    origin,
                    scale,
                    color,
                    SpriteEffects.None,
                    1.0f);
            }
        }
    }
}
