using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace SpellSlingerV1._0
{
    class Options : BASE_GAMESTATE
    {
        public Options()
        {
            CurrentGameState = (int)GAME_STATES.OPTIONS;
        }

        public override void Update(GameTime gameTime)
        {
            Debug.WriteLine("OPTIONS");
        }
    }
}
