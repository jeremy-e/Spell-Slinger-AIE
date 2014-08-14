using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpellSlingerV1._0
{
    class Enemy:Entity
    {
        public enum ENEMY_TYPE
        {
            TOWER,
            ENEMY1,
            ENEMY2
        }

        public Enemy()
        {
            Random r = new Random(Guid.NewGuid().GetHashCode());

            //this.SpriteID = "enemy1.png";   //Handing over responsibility to SpriteManager
            this.X = r.Next(0,Game1.SCREEN_WIDTH);
            this.Y = r.Next(Game1.SCREEN_HEIGHT-50,Game1.SCREEN_HEIGHT);
            this.Width = 32;
            this.Height = 32;

            this.Type = r.Next((int)TYPE.ENEMY1, (int)TYPE.ENEMY2+1);

        }
    }
}
