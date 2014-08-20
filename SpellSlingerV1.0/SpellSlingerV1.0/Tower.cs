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
            X = Game1.SCREEN_WIDTH / 2 - Width / 2;
            Y = Game1.SCREEN_HEIGHT / 2 - Width / 2;
        }

    }
}
