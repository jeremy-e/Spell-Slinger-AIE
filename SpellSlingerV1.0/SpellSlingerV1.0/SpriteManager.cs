using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpellSlingerV1._0
{
    class SpriteManager
    {
        private string[] spriteID = new string[3];
        public static int numberOfTextures = 3;                         //Update as required

        public SpriteManager()
        {
            spriteID[0] = "tower.png";
            spriteID[1] = "enemy1.png";
            spriteID[2] = "enemy2.png";
        }

        public string GetSpriteID(int type)
        {
            return spriteID[type];
        }

    }
}
