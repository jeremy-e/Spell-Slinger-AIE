using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace SpellSlingerV1._0
{
    class Intro : BASE_GAMESTATE
    {
        int tempTimer;

        public Intro()
        {
            CurrentGameState = (int)GAME_STATES.INTRO;
            tempTimer = 0;
        }

        public override void Update(GameTime gameTime_)
        {
            Debug.WriteLine("INTRO");
            tempTimer += gameTime_.ElapsedGameTime.Milliseconds;
            if (tempTimer >= 5000)
            {
                Debug.WriteLine("INTRO HAS BEEN PLAYED - SWITCH TO PLAYGAME (WILL BE MENU FIRST) - EXAMPLE ONLY");
                CurrentGameState = (int)GAME_STATES.PLAY_GAME;
            }
        }
    }
}
