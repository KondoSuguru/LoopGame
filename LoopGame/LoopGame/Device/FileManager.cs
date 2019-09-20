using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using LoopGame.Actor;

namespace LoopGame.Device {
    class FileManager {
        public static List<int> LoadRank(string filename, int stageNum) {
            using (StreamReader sr = File.OpenText(filename)) {
                string s;
                string sn = "Stage" + stageNum.ToString();
                List<int> data = new List<int>();
                while (!sr.EndOfStream) {
                    s = sr.ReadLine();
                    if (!s.Contains(sn)) {
                        continue;
                    }
                    var ss = s.Split(',');
                    for (int i = 1; i < ss.Length; i++) {
                        data.Add(int.Parse(ss[i]));
                    }
                    break;
                }

                return data;
            }
        }

        public static void WriteRank(string filename, int stageNum, int currentRecord) {
            var read = new StringBuilder();
            var strArray = File.ReadAllLines(filename, Encoding.UTF8);

            for (int i = 0; i < strArray.GetLength(0); i++) {
                if (i == stageNum - 1) {
                    read.AppendLine(strArray[i].Replace(currentRecord.ToString(), ActorMove.mWalkCount.ToString()));
                } else {
                    read.AppendLine(strArray[i]);
                }
            }

            File.WriteAllText(filename, read.ToString());
        }
    }
}
