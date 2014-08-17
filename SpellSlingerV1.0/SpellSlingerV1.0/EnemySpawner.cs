using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace SpellSlingerV1._0
{
    class EnemySpawner
    {
        //the timer that controls creation
        private Timer spawnTimer;
        
        //this object used to create enemies when told to spawn (ordering them from the factory :)
        Factory factoryOrder; 

        //the rules that govern the chance of enemy type spawn. 
        EnemySpawnRules esr;

        //constructor takes a factory (because we have already created this in Game and dont want to create a second one)
        public EnemySpawner(Factory factory_, EnemySpawnRules esr_, uint timerIntervalMs_)
        {
            //assign classwide variables
            esr = esr_;
            factoryOrder = factory_;

            //create the timer that will control when enemies are created. 
            spawnTimer = new System.Timers.Timer(timerIntervalMs_);

            //this line adds the SpawnEnemy envent handler (see SpawnEnemy function) that fires when the spawnTimer fires
            spawnTimer.Elapsed += SpawnEnemy;
           
            //kick off the timer
            spawnTimer.Start();
        }

        //start the timer again
        private void ResetTimer()
        {
            spawnTimer.Stop();
            spawnTimer.Start();
        }

        //Tell the factory to make us another enemy, because we only got here from spawnTimer going off!!!
        private void SpawnEnemy(Object source, ElapsedEventArgs e)
        {
            ENEMY_TYPE enemy_type = esr.RandomiseEnemy();
            
            factoryOrder.CreateEnemy(enemy_type);
                        
            ResetTimer();    
        }
    }
}
