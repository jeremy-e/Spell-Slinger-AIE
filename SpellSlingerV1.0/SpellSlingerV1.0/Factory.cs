using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpellSlingerV1._0
{

    class Factory
    {
        GameAssets gameAssets;
        
        public Factory(GameAssets gameAssets_)                                        //Default Constructor
        {
            gameAssets = gameAssets_;
        }

        public Entity CreateObject(Type type)
        {
            Entity entity = null;
            Random r = new Random(Guid.NewGuid().GetHashCode());

            if (type == typeof(Tower))
            {
                entity = new Tower();
                entity.Texture = gameAssets.TextureList[0];
                gameAssets.TowerList.Add((Tower)entity);                
            }
            if (type == typeof(Enemy))
            {
                entity = new Enemy();
                
                //Basic random enemy - wave # may also increase possibility of spawning higher difficulty monsters
                //Weight enemies?  First wave equal chance to spawn monsters 1-3, decreasing as it goes - Discuss (Spitballing idea)

                entity.Texture = gameAssets.TextureList[r.Next((int)TYPE.ENEMY1, (int)TYPE.ENEMY2+1)];
                gameAssets.EnemyList.Add((Enemy)entity);
            }

            //All objects must be added to the drawlist
            if (entity != null)
            {
                gameAssets.DrawList.Add(entity);
            }

            return entity;
        }

    }
}
