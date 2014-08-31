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

    public enum GUI_SPRITES
    {
        HOTBAR_1,
        HOTBAR_2,
        HOTBAR_3,
        HOTBAR_4,
        HOTBAR_5,
        SPELL_BOOK,
        ARROW_UP,
        ARROW_DOWN,
        ARROW_LEFT,
        ARROW_RIGHT
    }

    class SpriteManager
    {
        public static int playerNumTextures = Enum.GetNames(typeof(PLAYER_SPRITES)).Length;
        public static int enemyNumTextures = Enum.GetNames(typeof(ENEMY_TYPE)).Length;
        public static int spellNumTextures = Enum.GetNames(typeof(SPELL_TYPE)).Length;
        public static int GUINumTextures = Enum.GetNames(typeof(GUI_SPRITES)).Length;

        private string[] playerSpriteFileNames = new string[playerNumTextures];
        private string[] enemySpriteFileNames = new string[enemyNumTextures];
        private string[] spellSpriteFileNames = new string[spellNumTextures];
        private string[] GUISpriteFileNames = new string[GUINumTextures];
        
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
            
            //TODO - ADD Different sprites for these spells. 
            //I Added these so I could do all the resistance/weakness stuff, we dont have to use them for the demo. 
            spellSpriteFileNames[(int)SPELL_TYPE.DESPAIR] = "spell1.png";
            spellSpriteFileNames[(int)SPELL_TYPE.LIGHTNING] = "spell2.png";
            spellSpriteFileNames[(int)SPELL_TYPE.RAPTURE] = "spell1.png";
            ///////////////////////////////////////////////////////////////////////////////////////////////////////


            GUISpriteFileNames[(int)GUI_SPRITES.HOTBAR_1] = "gui_hotbar1.png";
            GUISpriteFileNames[(int)GUI_SPRITES.HOTBAR_2] = "gui_hotbar2.png";
            GUISpriteFileNames[(int)GUI_SPRITES.HOTBAR_3] = "gui_hotbar3.png";
            GUISpriteFileNames[(int)GUI_SPRITES.HOTBAR_4] = "gui_hotbar4.png";
            GUISpriteFileNames[(int)GUI_SPRITES.HOTBAR_5] = "gui_hotbar5.png";
            GUISpriteFileNames[(int)GUI_SPRITES.SPELL_BOOK] = "spellbook.png";
            GUISpriteFileNames[(int)GUI_SPRITES.ARROW_UP] = "arrowUp.png";
            GUISpriteFileNames[(int)GUI_SPRITES.ARROW_DOWN] = "arrowDown.png";
            GUISpriteFileNames[(int)GUI_SPRITES.ARROW_LEFT] = "arrowLeft.png";
            GUISpriteFileNames[(int)GUI_SPRITES.ARROW_RIGHT] = "arrowRight.png";
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

        public string GetGUISpriteFileName(int type)
        {
            return GUISpriteFileNames[type];
        }



    }
}
