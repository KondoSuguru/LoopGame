using LoopGame.Device;
using LoopGame.Utility;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopGame.Scene {
    class SceneBase {
        protected bool mIsEndFlag;
        protected static int mStageNo;
        protected CountDownTimer mFadeTimer;

        protected enum FadeState
        {
            IN,
            OUT,
            NONE,
        }
        protected FadeState mFadeState;

        protected void FadeInit()
        {
            mFadeTimer = new CountDownTimer(1.0f);
            mFadeState = FadeState.IN;
        }

        protected void SetFadeTime(float time)
        {
            mFadeTimer.SetTime(time);
        }

        protected void SetFadeState(FadeState fadeState)
        {
            mFadeState = fadeState;
        }

        protected void FadeUpdate(GameTime gameTime)
        {
            switch (mFadeState)
            {
                case FadeState.IN: UpdateFadeIn(gameTime); break;
                case FadeState.OUT: UpdateFadeOut(gameTime); break;
                case FadeState.NONE: break;
            }
        }

        protected void FadeDraw()
        {
            switch (mFadeState)
            {
                case FadeState.IN: DrawFadeIn(); break;
                case FadeState.OUT: DrawFadeOut(); break;
                case FadeState.NONE: break;
            }
        }

        protected void UpdateFadeIn(GameTime gameTime)
        {
            mFadeTimer.Update(gameTime);
            if (mFadeTimer.IsTime())
            {
                mFadeTimer.Initialize();
                mFadeState = FadeState.NONE;
            }
        }

        protected void UpdateFadeOut(GameTime gameTime)
        {
            mFadeTimer.Update(gameTime);
            if (mFadeTimer.IsTime())
            {
                mFadeTimer.Initialize();
                mIsEndFlag = true;
            }
        }

        protected void DrawFadeIn()
        {
            GameDevice.Instance().GetRenderer().DrawTexture("fade", Vector2.Zero, 1 - mFadeTimer.Rate());
        }

        protected void DrawFadeOut()
        {
            GameDevice.Instance().GetRenderer().DrawTexture("fade", Vector2.Zero, mFadeTimer.Rate());
        }
    }
}
