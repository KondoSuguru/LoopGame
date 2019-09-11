﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oikake.Device
{
    static class Input
    {
        // 移動量
        private static Vector2 velocity = Vector2.Zero;
        // キーボード
        private static KeyboardState currentKey; // 現在のキーの状態
        private static KeyboardState previousKey; // 1フレーム前のキーの状態
        // マウス
        private static MouseState currentMouse; // 現在のマウスの状態
        private static MouseState previousMouse; // 1フレーム前のマウスの状態

        public static void Update()
        {
            // キーボード
            previousKey = currentKey;
            currentKey = Keyboard.GetState();
            // マウス
            previousMouse = currentMouse;
            currentMouse = Mouse.GetState();

            // 更新
            UpdateVelocity();
        }

        // キーボード関連
        public static Vector2 Velocity()
        {
            return velocity;
        }

        private static void UpdateVelocity()
        {
            // 毎ループ初期化
            velocity = Vector2.Zero;

            // 右
            if (currentKey.IsKeyDown(Keys.Right) || (currentKey.IsKeyDown(Keys.D)))
            {
                velocity.X += 1.0f;
            }

            // 左
            if (currentKey.IsKeyDown(Keys.Left) || (currentKey.IsKeyDown(Keys.A)))
            {
                velocity.X -= 1.0f;
            }

            // 上
            if (currentKey.IsKeyDown(Keys.Up) || (currentKey.IsKeyDown(Keys.W)))
            {
                velocity.Y -= 1.0f;
            }

            // 下
            if (currentKey.IsKeyDown(Keys.Down) || (currentKey.IsKeyDown(Keys.S)))
            {
                velocity.Y += 1.0f;
            }

            // 正規化
            if (velocity.Length() != 0)
            {
                velocity.Normalize();
            }
        }

        /// <summary>
        /// キーが押された瞬間か？
        /// </summary>
        /// <param name="key">チェックしたいキー</param>
        /// <returns>現在キーが押されていて、1フレーム前に押されていなければtrue</returns>
        public static bool IsKeyDown(Keys key)
        {
            return currentKey.IsKeyDown(key) && !previousKey.IsKeyDown(key);
        }

        /// <summary>
        /// キーが押された瞬間か？
        /// </summary>
        /// <param name="key">チェックしたいキー</param>
        /// <returns>押された瞬間ならtrue</returns>
        public static bool GetKeyTrigger(Keys key)
        {
            return IsKeyDown(key);
        }

        /// <summary>
        /// キーが押されているか？
        /// </summary>
        /// <param name="key">調べたいキー</param>
        /// <returns>キーが押されていたらtrue</returns>
        public static bool GetKeyState(Keys key)
        {
            return currentKey.IsKeyDown(key);
        }

        // マウス関連
        /// <summary>
        /// マウスの左ボタンが押された瞬間か？
        /// </summary>
        /// <returns>現在押されていて、1フレーム前に押されていなければtrue</returns>
        public static bool IsMouseLButtonDown()
        {
            return currentMouse.LeftButton == ButtonState.Pressed && previousMouse.LeftButton == ButtonState.Released;
        }

        /// <summary>
        /// マウスの左ボタンが離された瞬間か？
        /// </summary>
        /// <returns>現在はなされていて、1フレーム前に押されていたらtrue</returns>
        public static bool IsMouseLButtonUp()
        {
            return currentMouse.LeftButton == ButtonState.Released && previousMouse.LeftButton == ButtonState.Pressed;
        }

        /// <summary>
        /// マウスの左ボタンが押されているか？
        /// </summary>
        /// <returns>左ボタンが押されていたらtrue</returns>
        public static bool IsMouseLButton()
        {
            return currentMouse.LeftButton == ButtonState.Pressed;
        }

        /// <summary>
        /// マウスの右ボタンが押された瞬間か？
        /// </summary>
        /// <returns>現在押されていて、1フレーム前に押されていなければtrue</returns>
        public static bool IsMouseRButtonDown()
        {
            return currentMouse.LeftButton == ButtonState.Pressed && previousMouse.LeftButton == ButtonState.Released;
        }

        /// <summary>
        /// マウスの右ボタンが離された瞬間か？
        /// </summary>
        /// <returns>現在はなされていて、1フレーム前に押されていたらtrue</returns>
        public static bool IsMouseRButtonUp()
        {
            return currentMouse.LeftButton == ButtonState.Released && previousMouse.LeftButton == ButtonState.Pressed;
        }

        /// <summary>
        /// マウスのボタンが押されているか？
        /// </summary>
        /// <returns>右ボタンが押されていたらtrue</returns>
        public static bool IsMouseRButton()
        {
            return currentMouse.LeftButton == ButtonState.Pressed;
        }

        /// <summary>
        /// マウスの位置を返す
        /// </summary>
        public static Vector2 MousePosition
        {
            // プロパティでGetterを生成
            get
            {
                return new Vector2(currentMouse.X, currentMouse.Y);
            }
        }

        /// <summary>
        /// マウスのスクロールホイールの変化量
        /// </summary>
        /// <returns>1フレーム前と現在のホイール量の差分</returns>
        public static int GetMouseWheel()
        {
            return previousMouse.ScrollWheelValue - currentMouse.ScrollWheelValue;
        }
    }
}
