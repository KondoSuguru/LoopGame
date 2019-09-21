using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoopGame.Device;
using LoopGame.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LoopGame.Scene
{
    class RuleScene : IScene
    {
        private bool mIsEndFlag;
        private int mCount;
        private Animation mAnim;
        private Vector2 mAPos, mBPos, mCPos, mDPos;
        private Vector2 mBoxPos;
        private int mAnimCount;

        public RuleScene()
        {
            var r = GameDevice.Instance().GetRenderer();
            r.LoadContent("rule1");
            r.LoadContent("rule2");
            r.LoadContent("box");
            mAnim = new Animation("kiparupa_anm", new Rectangle( 0, 0, 64, 64), 0.25f);
        }

        public void Draw()
        {
            var r = GameDevice.Instance().GetRenderer();

            switch (mCount)
            {
                case 0:
                    r.DrawTexture("rule1", Vector2.Zero);
                    mAnim.Draw(mAPos);
                    mAnim.Draw(mBPos);
                    mAnim.Draw(mCPos);
                    r.DrawTexture("box", mAPos + new Vector2(GridSize.GRID_SIZE, 0));
                    r.DrawTexture("box", mBPos + new Vector2(GridSize.GRID_SIZE, 0));
                    r.DrawTexture("box", mBPos + new Vector2(GridSize.GRID_SIZE * 2, 0));
                    r.DrawTexture("box", new Vector2(GridSize.GRID_SIZE * 13, GridSize.GRID_SIZE * 4));
                    break;
                case 1:
                    r.DrawTexture("rule2", Vector2.Zero);
                    mAnim.Draw(mDPos);
                    r.DrawTexture("box", mBoxPos);
                    break;
            }
        }

        public void Initialize()
        {
            mIsEndFlag = false;
            mCount = 0;
            mAPos = new Vector2(GridSize.GRID_SIZE * 1, GridSize.GRID_SIZE * 4);
            mBPos = new Vector2(GridSize.GRID_SIZE * 6, GridSize.GRID_SIZE * 4);
            mCPos = new Vector2(GridSize.GRID_SIZE * 12, GridSize.GRID_SIZE * 4);
            mDPos = new Vector2(GridSize.GRID_SIZE * 8, GridSize.GRID_SIZE * 3);
            mBoxPos = new Vector2(GridSize.GRID_SIZE * 9, GridSize.GRID_SIZE * 3);
            mAnimCount = 0;
        }

        public bool IsEnd()
        {
            return mIsEndFlag;
        }

        public Scene Next()
        {
            return Scene.StageSelect;
        }

        public void Shutdown()
        {
            
        }

        public void Update(GameTime gameTime)
        {
            var s = GameDevice.Instance().GetSound();
            mAnimCount++;
            mAnim.Update(gameTime);
            mAnim.SetMotion(3);

            if (mAnimCount > 90)
            {
                mAnimCount = 0;
            }

            if (Input.GetKeyTrigger(Keys.Space))
            {
                s.PlaySE("stage_choice");
                mCount++;
            }
            if(mCount == 2)
            {
                mIsEndFlag = true;
            }

            switch (mCount)
            {
                case 0:                
                    RuleAnim(ref mAPos, new Vector2(GridSize.GRID_SIZE * 1, GridSize.GRID_SIZE * 4), new Vector2(GridSize.GRID_SIZE * 2, GridSize.GRID_SIZE * 4));
                    RuleAnim(ref mCPos, new Vector2(GridSize.GRID_SIZE * 12, GridSize.GRID_SIZE * 4), new Vector2(GridSize.GRID_SIZE * 11, GridSize.GRID_SIZE * 4));
                    break;
                case 1:
                    mDPos.X += GridSize.GRID_SIZE / 16;
                    if(mDPos.X >= Screen.WIDTH -  GridSize.GRID_SIZE)
                    {
                        mDPos.X = -GridSize.GRID_SIZE;
                    }
                    mBoxPos.X += GridSize.GRID_SIZE / 16;
                    if (mBoxPos.X >= Screen.WIDTH - GridSize.GRID_SIZE)
                    {
                        mBoxPos.X = -GridSize.GRID_SIZE;
                    }
                    break;

            }
        }

        public void RuleAnim(ref Vector2 pos, Vector2 initPos, Vector2 targetPos)
        {
            if(mAnimCount == 90)
            {
                pos = initPos;
            }

            if (pos.X != targetPos.X)
            {
                if(pos.X > targetPos.X)
                {
                    pos.X -= GridSize.GRID_SIZE / 16;
                }
                else
                {
                    pos.X += GridSize.GRID_SIZE / 16;
                }
            }
        }
    }
}
