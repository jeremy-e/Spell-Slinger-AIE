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

        public Entity CreateObject(Type type, int wave)
        {
            Entity entity = null;
            Random r = new Random(Guid.NewGuid().GetHashCode());

            if (type == typeof(Tower))
            {
                entity = new Tower();
                entity.Type = SetType((int)TYPE.TOWER, (int)TYPE.TOWER, wave);        //Allows for randomisation of tower texture later
            }
            if (type == typeof(Enemy))
            {
                entity = new Enemy();
                entity.Type = SetType((int)TYPE.ENEMY1, (int)TYPE.ENEMY2, wave);
            }

            //Common entity inits
            entity.Texture = gameAssets.TextureList[entity.Type];

            //Spcific entity inits
            switch (entity.Type)
            {
                case (int)TYPE.TOWER:
                    gameAssets.TowerList.Add((Tower)entity);
                    break;
                default:    //specific enemy types can be split up as required
                    gameAssets.EnemyList.Add((Enemy)entity);
                    break;
            }

            //All objects must be added to the drawlist
            if (entity != null)
            {
                gameAssets.DrawList.Add(entity);
            }

            return entity;
        }

        public int SetType(int typeMin, int typeMax, int wave)
        {
            //wave to be included to weight / randomise probablity of spawns
            Random r = new Random(Guid.NewGuid().GetHashCode());
            int temp = r.Next(typeMin, typeMax + 1);                    //+1 to include max value
            return temp;
        }


    }
}
