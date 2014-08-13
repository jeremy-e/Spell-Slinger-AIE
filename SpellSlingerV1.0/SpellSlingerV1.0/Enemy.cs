using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpellSlingerV1._0
{
    class Enemy : Entity
    {
        //resistance
        //weakness
        //armour
        //health
        //speed

        public Enemy()
        {
            Random r = new Random(Guid.NewGuid().GetHashCode());

            this.X = r.Next(0, Game1.SCREEN_WIDTH);
            this.Y = r.Next(Game1.SCREEN_HEIGHT - 50, Game1.SCREEN_HEIGHT);
            this.Width = 32;
            this.Height = 32;
        }
    }
}
