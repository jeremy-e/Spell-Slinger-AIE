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

        public Entity CreateObject(string objectName)
        {
            Entity entity = null;

            if (objectName == "tower")
            {
                entity = new Tower();
                gameAssets.TowerList.Add((Tower)entity);                
            }
            if (objectName == "enemy")
            {
                entity = new Enemy();
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
