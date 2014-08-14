using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace SpellSlingerV1._0
{
    class Tower : Entity
    {
        public Tower()
        {
            this.Width = 64;
            this.Height = 64;
            this.X = Game1.SCREEN_WIDTH / 2 - 32;
            this.Y = Game1.SCREEN_HEIGHT/2 - 32;
        }

    }
}
