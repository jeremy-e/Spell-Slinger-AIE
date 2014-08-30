using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace SpellSlingerV1._0
{
    class Menu : BASE_GAMESTATE
    {
        public Menu()
        {
            CurrentGameState = (int)GAME_STATES.MENU;
        }

        public override void Update(GameTime gameTime)
        {
            Debug.WriteLine("MENU");
        }
    }
}
