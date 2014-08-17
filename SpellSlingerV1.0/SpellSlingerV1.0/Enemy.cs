using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpellSlingerV1._0
{
    class Enemy : Entity
    {
        //resistance
        //weakness
        //armour
        //health
        //speed

        private int health;

        private ENEMY_TYPE enemyType;


        public Enemy(ENEMY_TYPE enemyType_)
        {
            enemyType = enemyType_;
            Random r = new Random(Guid.NewGuid().GetHashCode());

            this.X = r.Next(0, Game1.SCREEN_WIDTH);
            this.Y = r.Next(Game1.SCREEN_HEIGHT - 50, Game1.SCREEN_HEIGHT);
            this.Width = 32;
            this.Height = 32;
            SetHealth();
        }

        //TODO:
        //update switch for all enemy types
        private int SetHealth()
        {
            int health = 0;

            Random r = new Random(Guid.NewGuid().GetHashCode());

            switch (enemyType)
            {
                case ENEMY_TYPE.GHOUL:
                    health = r.Next(80, 100 + 1);
                    break;
                case ENEMY_TYPE.RUNNING_GHOUL:
                    health = r.Next(100, 140 + 1);
                    break;
                default:
                    health = r.Next(80, 100 + 1);
                    break;
            }

            return health;
        }

        public int Health
        {
            get { return health; }
            set { health = value; }
        }

        public ENEMY_TYPE EnemyType
        {
            get
            {
                return enemyType;
            }
        }
    }
}
