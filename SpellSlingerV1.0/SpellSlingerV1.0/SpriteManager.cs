using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpellSlingerV1._0
{
    public enum TYPE
    {
        TOWER,
        ENEMY1,
        ENEMY2
    }

    class SpriteManager
    {
        public static int numberOfTextures = Enum.GetNames(typeof(TYPE)).Length;
        private string[] spriteFileName = new string[Enum.GetNames(typeof(TYPE)).Length];
        
        ///List Textures here
        public SpriteManager()
        {
            spriteFileName[(int)TYPE.TOWER] = "tower.png";
            spriteFileName[(int)TYPE.ENEMY1] = "enemy1.png";
            spriteFileName[(int)TYPE.ENEMY2] = "enemy2.png";
        }

        public string GetSpriteFileName(int type)
        {
            return spriteFileName[type];
        }

    }
}
