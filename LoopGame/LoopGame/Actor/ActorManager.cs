using LoopGame.Device;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopGame.Actor
{
    sealed class ActorManager
    {
        private static ActorManager mInstance;
        private List<Actor> mActors;

        private ActorManager()
        {
            mActors = new List<Actor>();
            GameDevice.Instance().GetRenderer().LoadContent("number");
        }

        public static ActorManager Instance()
        {
            if(mInstance == null)
            {
                mInstance = new ActorManager();
            }
            return mInstance;
        }

        public void Initialize()
        {
            if(mActors != null)
            {
                mActors.Clear();
            }
        }

        public void AddActor(Actor actor)
        {
            if (actor == null)
                return;
            mActors.Add(actor);
        }

        public void Update(GameTime gameTime)
        {
            foreach(var a in mActors)
            {
                a.Update(gameTime);
            }

        }

        public void Draw()
        {
            
            foreach (var a in mActors)
            {
                if (a is Wall || a is Goal)
                {
                    a.Draw();
                }
            }

            Player temp = null;
            foreach (var a in mActors)
            {
                if (a is Player)
                {
                    a.Draw();
                    temp = (Player)a;
                }
                else if (a is Box)
                {
                    a.Draw();
                }
            }

            foreach (var a in mActors)
            {
                if (a is Kakushi)
                {
                    a.Draw();
                }
            }

            if (temp != null) {
                GameDevice.Instance().GetRenderer().DrawNumber(
                    "number",
                    new Vector2(Screen.PLAY_WIDTH + GridSize.GRID_SIZE * 1.5f, GridSize.GRID_SIZE * 3),
                    temp.GetMove().GetWalkCount());
            }
        }

        //とりあえず作っただけだからなんとかしてくれ
        public void DrawWalkCount()
        {
            Player temp = null;
            Vector2 drawPos;

            foreach (var a in mActors)
            {
                if (a is Player)
                {
                    temp = (Player)a;
                }
            }

            if (temp == null)
            {
                return;
            }

            if(temp.GetMove().GetWalkCount() < 10)
            {
                drawPos = new Vector2(Screen.PLAY_WIDTH + GridSize.GRID_SIZE * 2.0f, GridSize.GRID_SIZE * 3);
            }
            else if(temp.GetMove().GetWalkCount() < 100)
            {
                drawPos = new Vector2(Screen.PLAY_WIDTH + GridSize.GRID_SIZE * 1.5f, GridSize.GRID_SIZE * 3);
            }
            else
            {
                drawPos = new Vector2(Screen.PLAY_WIDTH + GridSize.GRID_SIZE * 1.0f, GridSize.GRID_SIZE * 3);
            }

            GameDevice.Instance().GetRenderer().DrawNumber("number", drawPos, temp.GetMove().GetWalkCount());
        }

        public void Clear()
        {
            mActors.Clear();
        }

        public List<Actor> GetActors() {
            return mActors;
        }

        public bool IsClear()
        {
            int goalCount = 0;
            int clearGoalCount = 0;

            for (int i = 0; i < mActors.Count; i++)
            {
                for (int j = 0; j < mActors.Count; j++)
                {
                    if (mActors[i] is Goal && mActors[j] is Box)
                    {
                        Goal g = (Goal)mActors[i];
                        Box b = (Box)mActors[j];

                        if (g.GetPosition() == b.GetPosition())
                        {
                            clearGoalCount++;
                        }
                    }
                }

                if(mActors[i] is Goal)
                {
                    goalCount++;
                }
            }

            if(goalCount == clearGoalCount)
            {
                return true;
            }
            return false;
        }
    }
}
