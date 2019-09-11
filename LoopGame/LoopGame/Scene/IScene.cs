using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace LoopGame.Scene
{
    interface IScene
    {
        void Initialize(); 
        void Update(GameTime gameTime); 
        void Draw(); 
        void Shutdown(); 

        bool IsEnd(); 
        Scene Next(); 
    }
}
