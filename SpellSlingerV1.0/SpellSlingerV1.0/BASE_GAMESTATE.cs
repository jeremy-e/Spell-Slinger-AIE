using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SpellSlingerV1._0
{
    enum GAME_STATES
    {
        INTRO,
        MENU,
        OPTIONS,
        PLAY_GAME,
        END
    }

    enum PLAY_STATES
    {
        ABOUT_TO_GENERATE_WAVE,
        WAITING_FOR_WAVE_TO_START,
        WAVE_IN_PROGRESS,
        WAVE_COMPLETE,
        PAUSE
    }

    class BASE_GAMESTATE
    {
        int currentGameState;

        public virtual void Update(GameTime gameTime_) { ;}
        
        public int CurrentGameState
        {
            get { return currentGameState; }
            set { currentGameState = value; }
        }

        

    }
}
