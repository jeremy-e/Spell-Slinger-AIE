using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpellSlingerV1._0
{
    public enum PLAYER_SPRITES
    {
        TOWER
    }

    class SpriteManager
    {
        public static int playerNumTextures = Enum.GetNames(typeof(PLAYER_SPRITES)).Length;
        public static int enemyNumTextures = Enum.GetNames(typeof(ENEMY_TYPE)).Length;
        public static int spellNumTextures = Enum.GetNames(typeof(SPELL_TYPE)).Length;

        private string[] playerSpriteFileNames = new string[playerNumTextures];
        private string[] enemySpriteFileNames = new string[enemyNumTextures];
        private string[] spellSpriteFileNames = new string[spellNumTextures];
        
        ///List Textures here
        public SpriteManager()
        {
            playerSpriteFileNames[(int)PLAYER_SPRITES.TOWER] = "tower.png";
            
            enemySpriteFileNames[(int)ENEMY_TYPE.GHOUL] = "enemy1.png";
            enemySpriteFileNames[(int)ENEMY_TYPE.RUNNING_GHOUL] = "enemy2.png";
            enemySpriteFileNames[(int)ENEMY_TYPE.HEAVY_ZOMBIE] = "enemy1.png";
            enemySpriteFileNames[(int)ENEMY_TYPE.SKELETON_KNIGHT] = "enemy2.png";
            enemySpriteFileNames[(int)ENEMY_TYPE.OGRE] = "enemy1.png";
            enemySpriteFileNames[(int)ENEMY_TYPE.WEREWOLF] = "enemy2.png";
            enemySpriteFileNames[(int)ENEMY_TYPE.GREEN_DRAGON] = "enemy1.png";

            spellSpriteFileNames[(int)SPELL_TYPE.FIREBALL] = "spell1.png";
            spellSpriteFileNames[(int)SPELL_TYPE.ICELANCE] = "spell2.png";

        }

        public string GetPlayerSpriteFileName(int type)
        {
            return playerSpriteFileNames[type];
        }

        public string GetEnemySpriteFileName(int type)
        {
            return enemySpriteFileNames[type];
        }

        public string GetSpellSpriteFileName(int type)
        {
            return spellSpriteFileNames[type];
        }



    }
}
