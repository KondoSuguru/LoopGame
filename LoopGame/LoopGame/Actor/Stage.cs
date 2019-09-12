using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using LoopGame.Device;
using LoopGame.Utility;

namespace LoopGame.Actor {
    class Stage {
        private List<List<Actor>> mMapList; // ListのListで縦横の2次元配列を表現

        public Stage() {
            mMapList = new List<List<Actor>>();
        }

        private List<Actor> AddBlock(int lineCnt, string[] line) {
            // コピー元オブジェクト登録用でディクショナリ
            Dictionary<string, Actor> objectDict = new Dictionary<string, Actor>();

            objectDict.Add("1", new Wall());
            objectDict.Add("2", new Player());
            objectDict.Add("3", new Box());

            // 作業用リスト
            var workList = new List<Actor>();

            int colCnt = 0; // 列カウント用
            // 渡された1行から1つずつ作業リストに登録
            foreach (var s in line) {
                try {
                    // ディクショナリから元データ取り出し、クローン機能で複製
                    Actor work = objectDict[s];
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

        public void Initialize() {
        }

        public void Updata(GameTime gameTime) {
            foreach (var list in mMapList) {
                foreach (var obj in list) {
                    // objがSpaceクラスのオブジェクトなら次へ
                    //if (obj is Space) {
                    //    continue;
                    //}

                    // 更新
                    obj.Update(gameTime);
                }
            }
        }

        public void Hit(Actor actor) {
            Vector2 work = actor.GetPosition(); // 左上の座標を取得
            // 配列の何行目何列目にいるかを計算
            int x = (int)work.X / GridSize.GRID_SIZE;
            int y = (int)work.Y / GridSize.GRID_SIZE;

            Range yRange = new Range(0, mMapList.Count() - 1); // 行の範囲
            Range xRange = new Range(0, mMapList[0].Count() - 1); // 列の範囲

            for (int row = y - 1; row <= (y + 1); row++) // 縦3行分
            {
                for (int col = x - 1; col <= (x + 1); col++) // 横3列分
                {
                    // 配列外なら何もしない
                    if (xRange.IsOutOfRange(col) || yRange.IsOutOfRange(row))
                        continue;

                    // その場所のオブジェクトを取得
                    Actor obj = mMapList[row][col];

                    // objがSpaceクラスのオブジェクトなら次へ
                    //if (obj is Space)
                    //    continue;

                    // 衝突判定
                    //if (obj.IsCollision(actor))
                    //    actor.Hit(obj);
                }
            }
        }
    }
}
