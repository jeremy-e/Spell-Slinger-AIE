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

        public void CreateEnemy(ENEMY_TYPE enemyType_, int wave_ = 1)
        {
            Enemy enemy = new Enemy(enemyType_);
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
