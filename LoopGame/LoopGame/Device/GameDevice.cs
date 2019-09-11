using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oikake.Device
{
    /// <summary>
    /// ゲームデバイスクラス
    /// 継承できないので sealed で明示的に
    /// </summary>
    sealed class GameDevice
    {
        // 唯一のインスタンス
        private static GameDevice instance;

        // デバイス関連のフィールド
        private Renderer renderer;
        private Sound sound;
        private static Random random;
        private ContentManager content;
        private GraphicsDevice graphics;
        private GameTime gameTime;

        /// <summary>
        /// コンストラクタ
        /// private 宣言で外部からの new での実体生成はさせない
        /// </summary>
        /// <param name="content"></param>
        /// <param name="graphics"></param>
        private GameDevice(ContentManager content, GraphicsDevice graphics)
        {
            renderer = new Renderer(content, graphics);
            sound = new Sound(content);
            random = new Random();
            this.content = content;
            this.graphics = graphics;
        }

        /// <summary>
        /// GameDevice インスタンスの取得
        /// (Game1 クラスで使う実体生成用)
        /// </summary>
        /// <param name="content">コンテンツ管理者</param>
        /// <param name="graphics">グラフィック機器</param>
        /// <returns>GameDeviceインスタンス</returns>
        public static GameDevice Instance(ContentManager content, GraphicsDevice graphics)
        {
            // インスタンスがまだ生成されていなければ生成する
            if (instance == null)
            {
                instance = new GameDevice(content, graphics);
            }
            return instance;
        }

        /// <summary>
        /// インスタンスの取得
        /// </summary>
        /// <returns>GameDeviceインスタンス</returns>
        public static GameDevice Instance()
        {
            // まだインスタンスが生成されていなければエラー分を出す
            Debug.Assert(instance != null,
                "Game1クラスのInitializeメソッド内で引数付きInstanceメソッドを呼んでくる");

            return instance;
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        { }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="gameTime">ゲーム時間</param>
        public void Update(GameTime gameTime)
        {
            // デバイスで絶対に1回のみ更新が必要なもの
            Input.Update();
            this.gameTime = gameTime;
        }

        /// <summary>
        /// レンダラーオブジェクトの取得
        /// </summary>
        /// <returns>描画オブジェクト</returns>
        public Renderer GetRenderer()
        {
            return renderer;
        }

        /// <summary>
        /// サウンドオブジェクトの取得
        /// </summary>
        /// <returns>サウンドオブジェクト</returns>
        public Sound GetSound()
        {
            return sound;
        }

        /// <summary>
        /// 乱数オブジェクトの取得
        /// </summary>
        /// <returns>乱数オブジェクト</returns>
        public Random GetRandom()
        {
            return random;
        }

        /// <summary>
        /// コンテンツ管理者の取得
        /// </summary>
        /// <returns>コンテンツ管理者オブジェクト</returns>
        public  ContentManager GetContentManager()
        {
            return content;
        }

        /// <summary>
        /// グラフィックスデバイスの取得
        /// </summary>
        /// <returns>グラフィックデバイスオブジェクト</returns>
        public  GraphicsDevice GetGraphicsDevice()
        {
            return graphics;
        }

        /// <summary>
        /// ゲーム時間の取得
        /// </summary>
        /// <returns></returns>
        public GameTime GetGameTime()
        {
            return gameTime;
        }
    }
}
