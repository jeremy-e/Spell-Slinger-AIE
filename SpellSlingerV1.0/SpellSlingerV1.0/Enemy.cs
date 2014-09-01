using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Microsoft.Xna.Framework;

namespace SpellSlingerV1._0
{
    enum ENEMY_STATUS
    {
        OK,
        RECOVERING
    }

    class Enemy : Entity
    {

        private int health;
        private float speed;
        private ENEMY_TYPE enemyType;
        private Vector2 playerPos;
        private SPELL_TYPE resistance;
        private SPELL_TYPE weakness;
        private ENEMY_STATUS status;
        private Timer recoveryTimer;

        
        public Enemy(ENEMY_TYPE enemyType_, Vector2 playerPos_, Vector2 pos_)
        {
            pos = pos_;
            status = ENEMY_STATUS.OK;
            enemyType = enemyType_;
            InitialiseEnemyVariables();
            playerPos = playerPos_;
            this.Width = 32;
            this.Height = 32;
            Active = true;

            //enemy invincible for this long after a hit
            recoveryTimer = new System.Timers.Timer(500);
            recoveryTimer.Elapsed += EnemyRecovered;
        }

        public ENEMY_STATUS Status
        {
            get{ return status; }
        }

        private void EnemyRecovered(Object source, ElapsedEventArgs e)
        {
            recoveryTimer.Stop();
            status = ENEMY_STATUS.OK;
            drawColour = Color.White;
        } 

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

        //happy to leave this hardcoded for now rather than using const vars
        //at least it is neat and everything is in the same spot. 
        //eventually want to database drive this. 
        private void InitialiseEnemyVariables()
        {
            switch (enemyType)
            {
                case ENEMY_TYPE.GHOUL:
                    health = 60;
                    speed = 0.02f;
                    weakness = SPELL_TYPE.FIREBALL;                    
                    break;
                case ENEMY_TYPE.RUNNING_GHOUL:
                    health = 30;
                    speed = 0.03f;
                    weakness = SPELL_TYPE.FIREBALL;
                    break;
                case ENEMY_TYPE.HEAVY_ZOMBIE:
                    health = 110;
                    speed = 0.04f;
                    weakness = SPELL_TYPE.FIREBALL;
                    resistance = SPELL_TYPE.LIGHTNING;
                    break;
                case ENEMY_TYPE.SKELETON_KNIGHT:
                    health = 150;
                    speed = 0.04f;
                    weakness = SPELL_TYPE.RAPTURE;
                    resistance = SPELL_TYPE.DESPAIR;
                    break;
                case ENEMY_TYPE.OGRE:
                    health = 150;
                    speed = 0.05f;
                    weakness = SPELL_TYPE.ICELANCE;
                    resistance = SPELL_TYPE.FIREBALL;
                    break;
                case ENEMY_TYPE.WEREWOLF:
                    health = 180;
                    speed = 0.05f;
                    weakness = SPELL_TYPE.ICELANCE;
                    resistance = SPELL_TYPE.FIREBALL;
                    break;
                case ENEMY_TYPE.GREEN_DRAGON:
                    health = 250;
                    speed = 0.04f;
                    weakness = SPELL_TYPE.RAPTURE;
                    resistance = SPELL_TYPE.DESPAIR;
                    break;
            }
        }

        //returns amount of essense received if hit results in a kill
        //else returns 0
        public int Hit(Spell spell_)
        {
            //dont kick him while he is down
            if (status != ENEMY_STATUS.RECOVERING)
            {

                float dmg = spell_.Damage;

                //do we have a resistance to this spell? if so half the damage
                if (resistance == spell_.Type)
                    dmg /= 2;

                //are we weak on this spell?
                if (weakness == spell_.Type)
                    dmg *= 2;

                //reduce enemy health by dmg
                health -= (int)dmg;

                if (health <= 0)
                    Active = false;
                else
                {
                    status = ENEMY_STATUS.RECOVERING;
                    drawColour = Color.Red;
                    recoveryTimer.Start();
                }

                //TODO fix the way the essence is calculated
                return 100; 
            }
            return 0;
        }

        //to be called by Update
        //Move enemy towards player
        public void Update(GameTime gameTime_)
        {
            int delta = gameTime_.ElapsedGameTime.Milliseconds;
            Vector2 movementPreDelta = new Vector2();
            movementPreDelta = (Direction * speed);
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
