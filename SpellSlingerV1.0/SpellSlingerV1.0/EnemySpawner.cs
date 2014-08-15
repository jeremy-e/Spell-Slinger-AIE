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

        //constructor takes a factory (because we have already created this in Game and dont want to create a second one)
        public EnemySpawner(Factory factory_, uint timerIntervalMs_)
        {
            factoryOrder = factory_;
            spawnTimer = new System.Timers.Timer(timerIntervalMs_);

            spawnTimer.Elapsed += SpawnEnemy;
            spawnTimer.Enabled = true;
        }

        //start the timer again
        private void ResetTimer()
        {
            spawnTimer.Stop();
            spawnTimer.Start();
        }

        //this is only static because the timer needs a static callback, 
        //I am using PushThroughOrder to get back to this EnemySpawner instance
        private void SpawnEnemy(Object source, ElapsedEventArgs e)
        {
            factoryOrder.CreateObject(typeof(Enemy), 1);
            ResetTimer();    
        }
    }
}
