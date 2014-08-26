using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace SpellSlingerV1._0
{
    class GameAssets
    {
        public readonly Object threadSafeLock = new Object();
        public List<Entity> DrawList;                  //Used to track ALL objects
        public List<Texture2D> GUITextureList;
        public List<Texture2D> TextureList;            //tracks ALL textures from DrawList
        public List<Texture2D> EnemyTextureList;       //tracks ALL textures from DrawList
        public List<Texture2D> SpellTextureList;        //tracks spell textures
        public List<Enemy> EnemyList;                  //tracking enemies
        public List<Tower> TowerList;                  //Who knows we might want multi player one day?
        public List<Spell> SpellList;                   //tracks active/current spells

        //This method can definitely be tidied up.. 
        //need to look into how we can write the delete loop just once. 
        public void RemoveEntitiesMarkedForDelete()
        {
            lock (threadSafeLock)
            {
                //Remove inactive spells from SpellList
                if (SpellList.Count > 0)
                {
                    for (int i = SpellList.Count - 1; i >= 0; i--)
                    {
                        if (!SpellList[i].Active)
                        {
                            SpellList.RemoveAt(i);
                        }
                    }
                }

                //Remove inactive enemies from EnemyList
                if (EnemyList.Count > 0)
                {
                    for (int i = EnemyList.Count - 1; i >= 0; i--)
                    {
                        if (!EnemyList[i].Active)
                        {
                            EnemyList.RemoveAt(i);
                        }
                    }
                }

                //Remove any inactive items from draw call - iterate in reverse
                if (DrawList.Count > 0)
                {
                    for (int i = DrawList.Count - 1; i >= 0; i--)
                    {
                        if (!DrawList[i].Active)
                        {
                            DrawList.RemoveAt(i);
                        }
                    }
                }
            }
        }

        public GameAssets()
        {
            DrawList = new List<Entity>();                                                          //All objects added to DrawList - use this to draw to screen.
            TextureList = new List<Texture2D>();
            EnemyTextureList = new List<Texture2D>();
            SpellTextureList = new List<Texture2D>();
            GUITextureList = new List<Texture2D>();
            EnemyList = new List<Enemy>();
            TowerList = new List<Tower>();
            SpellList = new List<Spell>();
        }
    }
}
