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
            //Entity entity = null;
            Random r = new Random(Guid.NewGuid().GetHashCode());

            if (type == typeof(Tower))
            {
                Tower entity = new Tower();
                entity.Type = SetType((int)TYPE.TOWER, (int)TYPE.TOWER, wave);        //Allows for randomisation of tower texture later
                entity.Texture = gameAssets.TextureList[entity.Type];

                gameAssets.TowerList.Add((Tower)entity);
                gameAssets.DrawList.Add(entity);
            }
            if (type == typeof(Enemy))
            {
                Enemy entity = new Enemy();
                entity.Type = SetType((int)TYPE.ENEMY1, (int)TYPE.ENEMY2, wave);
                entity.Texture = gameAssets.TextureList[entity.Type];

                entity.Health = SetHealth(entity.Type);

                gameAssets.EnemyList.Add((Enemy)entity);
                gameAssets.DrawList.Add(entity);
            }

            return null;

        }

        public int SetType(int typeMin, int typeMax, int wave)
        {
            //wave to be included to weight / randomise probablity of spawns
            Random r = new Random(Guid.NewGuid().GetHashCode());
            int type = r.Next(typeMin, typeMax + 1);                    //+1 to include max value
            return type;
        }

        public int SetHealth(int enemyType)
        {
            int health = 0;
            Random r = new Random(Guid.NewGuid().GetHashCode());

            switch (enemyType)
            {
                case (int)TYPE.ENEMY1:
                    health = r.Next(80, 100 + 1);
                    break;
                case (int)TYPE.ENEMY2:
                    health = r.Next(100, 140 + 1);
                    break;
            }

            return health;
        }

    }
}
