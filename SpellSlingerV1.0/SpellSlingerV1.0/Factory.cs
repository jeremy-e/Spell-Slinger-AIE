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

        //By the time we get to CreateEnemy the EnemySpawner, Dice and EnemySpawnRules have done their job i.e. decided what enemy to spawn. 
        public void CreateEnemy(ENEMY_TYPE enemyType_, int wave_ = 1)
        {
            //TODO: 0 hardcoded in next line for now, will be safe unless multiple towers introduced
            Enemy enemy = new Enemy(enemyType_, gameAssets.TowerList[0].Pos);

            enemy.Texture = gameAssets.EnemyTextureList[(int)enemyType_];
            gameAssets.EnemyList.Add(enemy);
            gameAssets.DrawList.Add(enemy);            
        }

        public void CreatePlayer()
        {
            Tower entity = new Tower();
            entity.Texture = gameAssets.TextureList[(int)PLAYER_SPRITES.TOWER];
            gameAssets.TowerList.Add((Tower)entity);
            gameAssets.DrawList.Add(entity);
        }
    }
}
