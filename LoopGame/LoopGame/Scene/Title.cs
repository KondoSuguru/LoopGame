using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Oikake.Def;
using Oikake.Device;
using Oikake.Scene.Effect;
using Oikake.Util;

namespace Oikake.Scene
{
    class Title : IScene // シーンインターフェイスを継承
    {
        private bool isEndFlag; // 終了フラグ
        private Sound sound; // サウンドオブジェクト
        private Motion motion; // モーション管理
        private Renderer renderer;
        private ShakeManager shakeManager;
        private BlurManager blurManager;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Title()
        {
            isEndFlag = false;
            var gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();
            shakeManager = new ShakeManager();
            blurManager = new BlurManager();
            renderer = gameDevice.GetRenderer();
            renderer.InitializeRenderTarget(Screen.Width, Screen.Height);
        }

        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="renderer">描画オブジェクト</param>
        public void Draw(Renderer renderer)
        {
            // 通常描画
            //renderer.Begin();
            //renderer.DrawTexture("title", Vector2.Zero);
            //renderer.DrawTexture("puddle", new Vector2(200, 370), motion.DrawingRange());
            //renderer.End();

            // レンダーターゲットでテクスチャへ描画
            renderer.BeginRenderTarget();
            renderer.DrawTexture("title", Vector2.Zero);
            renderer.DrawTexture("puddle", new Vector2(200, 370), motion.DrawingRange());
            renderer.EndRenderTarget();

            // 画面へ通常描画
            renderer.Begin(SpriteSortMode.Deferred, BlendState.Additive);
            // レンダーターゲットでテクスチャへ描画したものを0.5倍サイズで描画
            renderer.DrawRenderTargetTexture(
                Vector2.Zero, null, 0.0f, Vector2.Zero, 1.0f, Color.White);
            shakeManager.Draw(renderer);
            blurManager.Draw(renderer);
            renderer.End();
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            isEndFlag = false;

            motion = new Motion();
            for (int i = 0; i < 6; i++)
            {
                motion.Add(i, new Rectangle(64 * i, 0, 64, 64));
            }
            // 範囲は0～5、モーション切り替え時間は0.2秒で初期化
            motion.Initialize(new Range(0, 5), new CountDownTimer(0.05f));
        }

        /// <summary>
        /// 終了か？
        /// </summary>
        /// <returns>シーンが終わってたらtrue</returns>
        public bool IsEnd()
        {
            return isEndFlag;
        }

        /// <summary>
        /// 次のシーンへ
        /// </summary>
        /// <returns>次のシーン</returns>
        public Scene Next()
        {
            return Scene.GamePlay;
        }

        /// <summary>
        /// 終了
        /// </summary>
        public void Shutdown()
        {
            sound.StopBGM();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="gameTime">ゲーム時間</param>
        public void Update(GameTime gameTime)
        {
            shakeManager.Update(gameTime);
            blurManager.Update(gameTime);
            sound.PlayBGM("titlebgm");
            motion.Update(gameTime);

            // スペースキーが押されたか？
            if (Input.GetKeyTrigger(Keys.Space))
            {
                isEndFlag = true;
                sound.PlaySE("titlese");

            }
            // 1キーが押されたか？
            if (Input.GetKeyTrigger(Keys.D1))
            {
                shakeManager.SetUpBlast(0.8f, new Vector2(Screen.Width / 2.0f, Screen.Height / 2.0f));
            }
        }
    }
}
