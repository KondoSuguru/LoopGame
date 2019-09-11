using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Oikake.Device;
using Oikake.Util;
using LoopGame.

namespace Oikake.Scene
{
    class GamePlay : IScene
    {
        private enum State
        {
            Empty,
            Maru,
            Batu,
            White
        }
        private static readonly int GridSize = 64;
        private static readonly int BoardSize = 5;
        private static readonly Vector2 BoardOrigin = new Vector2(100, 100);
        private static readonly int[,] board = new int[BoardSize + 2, BoardSize + 2];
        private static readonly int Empty = 0;
        private static readonly int Maru = 1;
        private static readonly int Batu = 2;
        private static readonly int White = 3;
        private int selectX = 2;
        private int selectY = 2;

        public void Draw(Renderer renderer)
        {
            renderer.Begin();

            Vector2 position = new Vector2(64, 0);
            //if (state == State.MaruTurn)
            //{
            //    renderer.DrawTexture("grid", )
            //    spriteBatch.Draw(textureMaru, Vector2.Zero, Color.White);
            //    spriteBatch.Draw(textureNoBandesu, position, Color.White);
            //}
            //else if (state == State.BatuTurn)
            //{
            //    spriteBatch.Draw(textureBatu, Vector2.Zero, Color.White);
            //    spriteBatch.Draw(textureNoBandesu, position, Color.White);
            //}
            //else if (state == State.MaruWin)
            //{
            //    spriteBatch.Draw(textureMaru, Vector2.Zero, Color.White);
            //    spriteBatch.Draw(textureNoKatidesu, position, Color.White);
            //}
            //else if (state == State.BatuWin)
            //{
            //    spriteBatch.Draw(textureBatu, Vector2.Zero, Color.White);
            //    spriteBatch.Draw(textureNoKatidesu, position, Color.White);
            //}
            //else if (state == State.Hikiwake)
            //{
            //    spriteBatch.Draw(textureHikiwake, Vector2.Zero, Color.White);
            //}



            for (int y = 0; y < BoardSize; y++)
            {
                for (int x = 0; x < BoardSize; x++)
                {
                    renderer.DrawTexture("grid", new Vector2(GridSize * x, GridSize * y) + BoardOrigin, Color.White);
                }
            }


            for (int y = 0; y < BoardSize; y++)
            {
                for (int x = 0; x < BoardSize; x++)
                {
                    int data = board[y, x];
                    if (data == Empty)
                    {

                    }
                    else if (data == Maru)
                    {
                        renderer.DrawTexture("maru", new Vector2(GridSize * x, GridSize * y) + BoardOrigin, Color.White);
                    }
                    else if (data == Batu)
                    {
                        renderer.DrawTexture("batu", new Vector2(GridSize * x, GridSize * y) + BoardOrigin, Color.White);
                    }
                    else if (data == White)
                    {
                        renderer.DrawTexture("white", new Vector2(GridSize * x, GridSize * y) + BoardOrigin, Color.White);
                    }
                }
            }

            renderer.DrawTexture("waku", BoardOrigin + new Vector2(selectX * GridSize, selectY * GridSize));
            renderer.End();
        }

        public void Initialize()
        {
            for (int y = 0; y < BoardSize + 2; y++)
            {
                for (int x = 0; x < BoardSize + 2; x++)
                {
                    board[y, x] = 0;
                }
            }

            Random rnd = new Random();
            for (int y = 0; y < BoardSize; y++)
            {
                for (int x = 0; x < BoardSize; x++)
                {
                    board[y, x] = rnd.Next(1, 4);
                }
            }
        }

        public bool IsEnd()
        {
            return false;
        }

        public Scene Next()
        {
            return Scene.Ending;
        }

        public void Shutdown()
        {
        }

        // 4マス以上で消せるようにする
        // 下記の判定に引っかかったものはフラグを立て、終わりに一斉に削除
        public void Update(GameTime gameTime)
        {
            for (int y = 0; y < BoardSize; y++)
            {
                for (int x = 0; x < BoardSize; x++)
                {
                    if (board[y, x] == board[y, x + 1])
                    {
                        if (board[y, x + 1] == board[y, x + 2])
                        {
                            board[y, x] = board[y, x + 1] = board[y, x + 2] = 0;
                        }
                        else if (board[y, x + 1] == board[y + 1, x + 1])
                        {
                            board[y, x] = board[y, x + 1] = board[y + 1, x + 1] = 0;
                        }
                    }
                    else if (board[y, x] == board[y + 1, x])
                    {
                        if (board[y + 1, x] == board[y + 2, x])
                        {
                            board[y, x] = board[y + 1, x] = board[y + 2, x] = 0;
                        }
                        else if (board[y + 1, x] == board[y + 1, x + 1])
                        {
                            board[y, x] = board[y + 1, x] = board[y + 1, x + 1] = 0;
                        }
                    }
                    else if (y > 0)
                    {
                        if (board[y, x] == board[y, x + 1])
                        {
                            if (board[y, x + 1] == board[y - 1, x + 1])
                            {
                                board[y, x] = board[y, x + 1] = board[y - 1, x + 1] = 0;
                            }
                        }
                        else if (board[y, x] == board[y - 1, x])
                        {
                            if (board[y - 1, x] == board[y - 1, x + 1])
                            {
                                board[y, x] = board[y - 1, x] = board[y - 1, x + 1] = 0;
                            }
                        }
                    }
                }
            }

            if (Input.GetKeyTrigger(Keys.Right))
            {
                selectX += 1;
                if (selectX >= BoardSize - 1)
                {
                    selectX -= 1;
                }
            }
            if (Input.GetKeyTrigger(Keys.Left))
            {
                selectX -= 1;
                if (selectX < 1)
                {
                    selectX += 1;
                }
            }
            if (Input.GetKeyTrigger(Keys.Down))
            {
                selectY += 1;
                if (BoardSize - 1 <= selectY)
                {
                    selectY -= 1;
                }
            }
            if (Input.GetKeyTrigger(Keys.Up))
            {
                selectY -= 1;
                if (selectY < 1)
                {
                    selectY += 1;
                }
            }

            List<int> massChange = new List<int>();
            if (Input.GetKeyTrigger(Keys.Q))
            {
                for (int y = selectY - 1; y < selectY + 2; y++)
                {
                    for (int x = selectX - 1; x < selectX + 2; x++)
                    {
                        massChange.Add(board[y, x]);
                    }
                }

                board[selectY + 1, selectX - 1] = massChange[0];
                board[selectY, selectX - 1] = massChange[1];
                board[selectY - 1, selectX - 1] = massChange[2];
                board[selectY + 1, selectX] = massChange[3];
                board[selectY - 1, selectX] = massChange[5];
                board[selectY + 1, selectX + 1] = massChange[6];
                board[selectY, selectX + 1] = massChange[7];
                board[selectY - 1, selectX + 1] = massChange[8];

                massChange.Clear();
            }

            if (Input.GetKeyTrigger(Keys.E))
            {
                for (int y = selectY - 1; y < selectY + 2; y++)
                {
                    for (int x = selectX - 1; x < selectX + 2; x++)
                    {
                        massChange.Add(board[y, x]);
                    }
                }

                board[selectY - 1, selectX + 1] = massChange[0];
                board[selectY, selectX + 1] = massChange[1];
                board[selectY + 1, selectX + 1] = massChange[2];
                board[selectY - 1, selectX] = massChange[3];
                board[selectY + 1, selectX] = massChange[5];
                board[selectY - 1, selectX - 1] = massChange[6];
                board[selectY, selectX - 1] = massChange[7];
                board[selectY + 1, selectX - 1] = massChange[8];

                massChange.Clear();
            }
        }
    }
}
