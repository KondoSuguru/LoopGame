using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Oikake.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oikake.Scene.Effect
{
    class BlurManager
    {
        private float distance; // 画像の距離
        private List<Vector2> positions; // ずらす位置リスト
        private float speed = 0.1f; // 速度

        public BlurManager()
        {
            // 距離初期値
            distance = 0.0f;
            // ずらす位置リスト生成
            positions = new List<Vector2>()
            {
                new Vector2(-1, -1), new Vector2(0, -1), new Vector2(1, -1),
                new Vector2(-1, 0), new Vector2(0, 0), new Vector2(1, 0),
                new Vector2(-1, 1), new Vector2(0, 1), new Vector2(1, 1),
            };
        }

        public void Update(GameTime gameTime)
        {
            if (Input.GetKeyState(Keys.Z))
            {
                distance += speed;
            }
            if (Input.GetKeyState(Keys.X))
            {
                distance -= speed;
            }
        }

        public void Draw(Renderer renderer)
        {
            for (int i = 0; i < positions.Count; i++)
            {
                renderer.DrawRenderTargetTexture(
                    positions[i] * distance,
                    null,
                    0.0f,
                    Vector2.Zero,
                    1.0f,
                    new Color(1.0f, 1.0f, 1.0f, (1.0f / (float)positions.Count)),
                    SpriteEffects.None,
                    1.0f);
            }
        }
    }
}
