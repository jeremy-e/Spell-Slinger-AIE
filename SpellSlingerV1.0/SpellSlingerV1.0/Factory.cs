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

                entity.Type = SetType((int)TYPE.TOWER, (int)TYPE.TOWER);        //Allows for randomisation of tower texture later
                entity.Texture = gameAssets.TextureList[entity.Type];

                gameAssets.TowerList.Add((Tower)entity);                
            }
            if (type == typeof(Enemy))
            {
                entity = new Enemy();

                entity.Type = SetType((int)TYPE.ENEMY1, (int)TYPE.ENEMY2);
                entity.Texture = gameAssets.TextureList[entity.Type];
                
                gameAssets.EnemyList.Add((Enemy)entity);
            }

            //All objects must be added to the drawlist
            if (entity != null)
            {
                gameAssets.DrawList.Add(entity);
            }

            return entity;
        }

        public int SetType(int typeMin, int typeMax)
        {
            Random r = new Random(Guid.NewGuid().GetHashCode());
            int temp = r.Next(typeMin, typeMax + 1);                    //+1 to include max value
            return temp;
        }


    }
}
