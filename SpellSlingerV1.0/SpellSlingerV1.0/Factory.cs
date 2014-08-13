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

            if (type == typeof(Tower))
            {
                entity = new Tower();
                entity.Texture = gameAssets.TextureList[0];
                gameAssets.TowerList.Add((Tower)entity);                
            }
            if (type == typeof(Enemy))
            {
                entity = new Enemy();
                entity.Texture = gameAssets.TextureList[1];
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
