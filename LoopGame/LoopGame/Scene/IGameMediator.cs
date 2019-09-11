using Oikake.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oikake.Scene
{
    /// <summary>
    /// キャラクター仲介者
    /// </summary>
    interface IGameMediator
    {
        void AddActor(Character character); // 演技者（キャラクター）を追加
        void AddScore(); // 得点を追加
        void AddScore(int num); // 指定された得点を追加
    }
}
