using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopGame.Actor {
    class Stage {
        private List<List<Actor>> mapList; // ListのListで縦横の2次元配列を表現

        public Stage() {
            mapList = new List<List<Actor>>();
        }

        private List<Actor> AddBlock(int lineCnt, string[] line) {
            // コピー元オブジェクト登録用でディクショナリ
            var objectDictionary = new Dictionary<string, Actor>();
            //プレイヤーは0
            objectDictionary.Add("0", new Player());

            // 作業用リスト
            var workList = new List<Actor>();

            int colCnt = 0; // 列カウント用
            // 渡された1行から1つずつ作業リストに登録
            foreach (var s in line) {
                try {
                    Actor work = objectDictionary[s];
                    work.SetPosition(new Vector2(colCnt * work.GetWidth(), lineCnt * work.GetHeigth()));
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
                mapList.Add(AddBlock(lineCnt, data[lineCnt]));
                subMapList.Add(AddBlock(lineCnt, data[lineCnt]));
            }

            // カメラの範囲設定
            Camera.SetMoveArea(
                new Vector2(0, 0),
                new Vector2(
                    WIDTH - Screen.Width,
                    Y_MAX * BLOCK_SIZE - Screen.Height));
        }

        public void Unload() {
            mapList.Clear();
        }

        public void Initialize() {
            for (int y = 0; y < subMapList.Count(); y++) {
                for (int x = 0; x < subMapList[0].Count(); x++) {
                    mapList[y][x] = subMapList[y][x];
                }
            }
            Camera.SetPosition(Vector2.Zero);
        }

        public void Updata(GameTime gameTime) {
            foreach (var list in mapList) {
                foreach (var obj in list) {
                    // objがSpaceクラスのオブジェクトなら次へ
                    if (obj is Space)
                        continue;

                    // 更新
                    obj.Update(gameTime);
                }
            }
        }

        public void Hit(GameObject gameObject) {
            Point work = gameObject.GetRectangle().Location; // 左上の座標を取得
            // 配列の何行目何列目にいるかを計算
            int x = work.X / 32;
            int y = work.Y / 32;

            // 移動で食い込んでるときの修正
            if (x < 1)
                x = 1;
            if (y < 1)
                y = 1;

            Range yRange = new Range(0, mapList.Count() - 1); // 行の範囲
            Range xRange = new Range(0, mapList[0].Count() - 1); // 列の範囲

            for (int row = y - 1; row <= (y + 1); row++) // 縦3行分
            {
                for (int col = x - 1; col <= (x + 1); col++) // 横3列分
                {
                    // 配列外なら何もしない
                    if (xRange.IsOutOfRange(col) || yRange.IsOutOfRange(row))
                        continue;

                    // その場所のオブジェクトを取得
                    GameObject obj = mapList[row][col];

                    // objがSpaceクラスのオブジェクトなら次へ
                    if (obj is Space)
                        continue;

                    // 衝突判定
                    if (obj.IsCollision(gameObject))
                        gameObject.Hit(obj);
                }
            }
        }

        public void Draw(Renderer renderer) {
            // ブロックの描画
            foreach (var list in mapList) {
                foreach (var obj in list) {
                    obj.Draw(renderer);
                }
            }
        }
    }
}
