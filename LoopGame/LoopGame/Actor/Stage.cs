using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using LoopGame.Device;
using LoopGame.Utility;
using System.Diagnostics;

namespace LoopGame.Actor {
    class Stage {
        private List<List<Actor>> mMapList; // ListのListで縦横の2次元配列を表現
        private IGameMediator mMediator;

        public Stage(IGameMediator mediator) {
            mMapList = new List<List<Actor>>();
            mMediator = mediator;
        }

        private List<Actor> AddBlock(int lineCnt, string[] line) {
            // 作業用リスト
            var workList = new List<Actor>();

            int colCnt = 0; // 列カウント用
            // 渡された1行から1つずつ作業リストに登録
            foreach (var s in line) {
                try {
                    Actor work = null;
                    switch (s) {
                        case "0": work = new Space(); break;
                        case "1": work = new Wall(); break;
                        case "2": work = new Player(mMediator); break;
                        case "3": work = new Box(); break;
                        case "4": work = new Goal(); break;
                        default: Debug.Assert(false); break;
                    }
                    work.SetPosition(new Vector2(colCnt * GridSize.GRID_SIZE, lineCnt * GridSize.GRID_SIZE));
                    workList.Add(work);
                } catch (Exception e) {
                    Console.WriteLine(e);
                }
                // 列カウンタを増やす
                colCnt++;
            }
            return workList;
        }

        public void Load(string filename, string path = "./csv/") {
            CSVReader csvReader = new CSVReader();
            csvReader.Read(filename, path);

            var data = csvReader.GetData(); // List<string[]>型で取得

            // 1行ごとmapListに追加していく
            for (int lineCnt = 0; lineCnt < data.Count(); lineCnt++) {
                mMapList.Add(AddBlock(lineCnt, data[lineCnt]));
            }
        }

        public void Unload() {
            mMapList.Clear();
        }

        public void Hit(Actor actor) {
            Vector2 pos = actor.GetPosition(); // 左上の座標を取得
            // 配列の何行目何列目にいるかを計算
            int x = (int)pos.X / GridSize.GRID_SIZE;
            int y = (int)pos.Y / GridSize.GRID_SIZE;

            Range yRange = new Range(0, mMapList.Count() - 1); // 行の範囲
            Range xRange = new Range(0, mMapList[0].Count() - 1); // 列の範囲

            for (int row = y - 1; row <= (y + 1); row++) // 縦3行分
            {
                for (int col = x - 1; col <= (x + 1); col++) // 横3列分
                {
                    // 配列外なら何もしない
                    if (xRange.IsOutOfRange(col) || yRange.IsOutOfRange(row)) {
                        continue;
                    }

                    // その場所のオブジェクトを取得
                    Actor obj = mMapList[row][col];

                    // objがSpaceクラスのオブジェクトなら次へ
                    if (obj is Space) {
                        continue;
                    }

                    // 衝突判定
                    //if (obj.IsCollision(actor))
                    //    actor.Hit(obj);
                }
            }
        }

        public bool IsCollisionSide(Vector2 nextPos) {
            if (nextPos.X < 0) {
                nextPos.X = Screen.WIDTH - GridSize.GRID_SIZE;
            } else if (nextPos.X > Screen.WIDTH - GridSize.GRID_SIZE) {
                nextPos.X = 0;
            }
            int posX = (int)nextPos.X / GridSize.GRID_SIZE;
            int posY = (int)nextPos.Y / GridSize.GRID_SIZE;

            Actor obj = mMapList[posY][posX];

            if (obj is Space || obj is Goal || obj is Player) {
                return false;
            } else {
                return true;
            }
        }

        public bool IsCollisionVertical(Vector2 nextPos) {
            if (nextPos.Y < 0) {
                nextPos.Y = Screen.HEIGHT - GridSize.GRID_SIZE;
            } else if (nextPos.Y > Screen.HEIGHT - GridSize.GRID_SIZE) {
                nextPos.Y = 0;
            }
            int posX = (int)nextPos.X / GridSize.GRID_SIZE;
            int posY = (int)nextPos.Y / GridSize.GRID_SIZE;

            Actor obj = mMapList[posY][posX];

            if (obj is Space || obj is Goal || obj is Player) {
                return false;
            } else {
                return true;
            }
        }
    }
}
