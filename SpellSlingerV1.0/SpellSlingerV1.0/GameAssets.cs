using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace SpellSlingerV1._0
{
    class GameAssets
    {
        public List<Entity> DrawList;                  //Used to track ALL objects
        public List<Texture2D> TextureList;            //tracks ALL textures from DrawList
        public List<Enemy> EnemyList;                  //tracking enemies
        public List<Tower> TowerList;                  //Who knows we might want multi player one day?

        public GameAssets()
        {
            DrawList = new List<Entity>();                                                          //All objects added to DrawList - use this to draw to screen.
            TextureList = new List<Texture2D>();
            EnemyList = new List<Enemy>();
            TowerList = new List<Tower>();
        }
    }
}
