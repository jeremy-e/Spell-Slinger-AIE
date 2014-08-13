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

        private int health;

        public Enemy()
        {
            Random r = new Random(Guid.NewGuid().GetHashCode());

            this.X = r.Next(0, Game1.SCREEN_WIDTH);
            this.Y = r.Next(Game1.SCREEN_HEIGHT - 50, Game1.SCREEN_HEIGHT);
            this.Width = 32;
            this.Height = 32;
        }

        public int Health
        {
            get { return health; }
            set { health = value; }
        }

    }
}
