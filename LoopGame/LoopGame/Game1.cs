﻿// このファイルで必要なライブラリのnamespaceを指定
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using LoopGame.Device;
using LoopGame.Utility;
using LoopGame.Scene;

namespace LoopGame
{
    public class Game1 : Game
    {
        // フィールド（このクラスの情報を記述）
        private GraphicsDeviceManager mGraphicsDeviceManager;//グラフィックスデバイスを管理するオブジェクト
        private SpriteBatch mSpriteBatch;//画像をスクリーン上に描画するためのオブジェクト
        private SceneManager mSceneManager;
        public static bool mIsEndGame;

        public Game1()
        {
            //グラフィックスデバイス管理者の実体生成
            mGraphicsDeviceManager = new GraphicsDeviceManager(this);
            //コンテンツデータ（リソースデータ）のルートフォルダは"Contentに設定
            Content.RootDirectory = "Content";
            Window.Title = "今日もおしごとキーパールーパー";

            // Screenクラスの値で画面サイズを設定
            mGraphicsDeviceManager.PreferredBackBufferWidth = Screen.WIDTH;
            mGraphicsDeviceManager.PreferredBackBufferHeight = Screen.HEIGHT;

            mIsEndGame = false;
        }

        protected override void Initialize()
        {
            GameDevice.Instance(Content, GraphicsDevice);

            mSceneManager = new SceneManager();
            mSceneManager.Add(Scene.Scene.Title, new Title());
            mSceneManager.Add(Scene.Scene.Rule, new RuleScene());
            mSceneManager.Add(Scene.Scene.StageSelect, new StageSelect());
            mSceneManager.Add(Scene.Scene.GamePlay, new GamePlay());
            mSceneManager.Add(Scene.Scene.Ending, new Ending());
            mSceneManager.Change(Scene.Scene.Title);

            base.Initialize();// 親クラスの初期化処理呼び出し。絶対に消すな！！
        }

        protected override void LoadContent()
        {
            // 画像を描画するために、スプライトバッチオブジェクトの実体生成
            mSpriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (mIsEndGame)
            {
                Exit();
            }

            GameDevice.Instance().Update(gameTime);
            mSceneManager.Update(gameTime);

            base.Update(gameTime); // 親クラスの更新処理呼び出し。絶対に消すな！！
        }

        protected override void Draw(GameTime gameTime)
        {
            GameDevice.Instance().GetRenderer().Begin();

            // 画面クリア時の色を設定
            GraphicsDevice.Clear(Color.CornflowerBlue);

            mSceneManager.Draw();

            GameDevice.Instance().GetRenderer().End();

            base.Draw(gameTime); // 親クラスの更新処理呼び出し。絶対に消すな！！
        }
    }
}
