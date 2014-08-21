using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

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

        private Vector2 playerPos;

        private const float GHOUL_SPEED = 0.10f;
        private const float RUNNING_GHOUL_SPEED = 0.16f;
        
        //direction property (we dont need to store as we can calculate on the fly)
        public Vector2 Direction
        {
            get 
            {
                Vector2 dir = Pos - playerPos;
                dir.Normalize();
                return dir;
            }
        }


        public Enemy(ENEMY_TYPE enemyType_, Vector2 playerPos_, Vector2 pos_)
        {
            pos = pos_;
            enemyType = enemyType_;
            playerPos = playerPos_;

            Random r = new Random(Guid.NewGuid().GetHashCode());

            //this.X = r.Next(0, Game1.SCREEN_WIDTH);
            //this.Y = r.Next(Game1.SCREEN_HEIGHT - 50, Game1.SCREEN_HEIGHT);
            this.Width = 32;
            this.Height = 32;
            Active = true;
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

        //to be called by Update
        //Move enemy towards player
        public void Move(GameTime gameTime_)
        {
            int delta = gameTime_.ElapsedGameTime.Milliseconds;
            Vector2 movementPreDelta = new Vector2();

            if ( EnemyType == ENEMY_TYPE.GHOUL )
                movementPreDelta = (Direction * GHOUL_SPEED);
            else if ( EnemyType == ENEMY_TYPE.RUNNING_GHOUL )
                movementPreDelta = (Direction * RUNNING_GHOUL_SPEED);

            pos -= (movementPreDelta * delta);
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
