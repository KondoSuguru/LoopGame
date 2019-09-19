using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LoopGame.Device {
    class FileManager {
        public static List<int> LoadRank(string filename, int stageNum) {
            using (StreamReader sr = File.OpenText(filename)) {
                string s;
                string sn = "Stage" + stageNum.ToString();
                List<int> data = new List<int>();
                while ((s = sr.ReadLine()) != null/* && s.Contains(sn)*/) {
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
    }
}
