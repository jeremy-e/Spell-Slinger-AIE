using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpellSlingerV1._0
{
    class SpriteManager
    {
        private string[] spriteFileName = new string[3];
        public static int numberOfTextures = 3;                         //Update as required

        public SpriteManager()
        {
            spriteFileName[0] = "tower.png";
            spriteFileName[1] = "enemy1.png";
            spriteFileName[2] = "enemy2.png";
        }

        public string GetSpriteFileName(int type)
        {
            return spriteFileName[type];
        }

    }
}
