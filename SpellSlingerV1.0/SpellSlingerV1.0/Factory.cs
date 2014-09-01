using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace SpellSlingerV1._0
{

    class Factory
    {
        GameAssets gameAssets;
        List<EnemySpawnRules> spawnRulesList;
        Dice spawnRulesSelector;

        public Factory(GameAssets gameAssets_)                                        //Default Constructor
        {
            gameAssets = gameAssets_;
            spawnRulesList = new List<EnemySpawnRules>();
            
            InitEnemySpawnRules();
            spawnRulesSelector = new Dice(1, spawnRulesList.Count);
        }

#region TEST WAVES

        private void InitEnemySpawnRules()
        {
            Dice dice = new Dice(1, 100);

            {
                //50% Ghoul, 50% Running Ghoul
                EnemySpawnRules rules = new EnemySpawnRules(dice, ENEMY_TYPE.GHOUL);
                rules.SetEnemyRule(ENEMY_TYPE.RUNNING_GHOUL, 50, 50); //if we roll 11 or 12 then give us a running ghoul                     
                spawnRulesList.Add(rules);
            }
            
            {
                //70% SKELETON_KNIGHT, 30% WEREWOLF
                EnemySpawnRules rules = new EnemySpawnRules(dice, ENEMY_TYPE.SKELETON_KNIGHT);
                rules.SetEnemyRule(ENEMY_TYPE.WEREWOLF, 70, 30); //if we roll 11 or 12 then give us a running ghoul                     
                spawnRulesList.Add(rules);
            }

            {
                //10% Green Dragon, 40% HEAVY_ZOMBIE, 40% SKELETON KNIGHT
                EnemySpawnRules rules = new EnemySpawnRules(dice, ENEMY_TYPE.HEAVY_ZOMBIE);
                rules.SetEnemyRule(ENEMY_TYPE.SKELETON_KNIGHT, 70, 30); //if we roll 11 or 12 then give us a running ghoul                     
                rules.SetEnemyRule(ENEMY_TYPE.GREEN_DRAGON, 70, 30); //if we roll 11 or 12 then give us a running ghoul                     
                spawnRulesList.Add(rules);
            }

            {
                //100% RUNNING GHOUL
                EnemySpawnRules rules = new EnemySpawnRules(dice, ENEMY_TYPE.RUNNING_GHOUL);
                spawnRulesList.Add(rules);
            }

        }

        public void CreateTestWave()
        {
            uint spawnNumber = 0;
            const uint SPAWN_INTERVAL = 5000;
            const uint TIMER_INTERVAL = 350;
            const float ENEMIES_WAVE_ONE = 5.0f;
            const float ENEMIES_INCREMENTER = 0.6f;
            const float ENEMIES_WAVE_END = 50.0f;

            ////2 six sided dice. 
            //Dice dice = new Dice(2, 6);

            ////all dice rolls will give us a ghoul if no other rule is set
            //EnemySpawnRules rules = new EnemySpawnRules(dice, ENEMY_TYPE.GHOUL);

            ////if we roll an 11 or 12 (array pos 10 or 11) then give us a running ghoul
            //rules.SetEnemyRule(ENEMY_TYPE.RUNNING_GHOUL, 8, 4); //if we roll 11 or 12 then give us a running ghoul                     
            //rules.SetEnemyRule(ENEMY_TYPE.HEAVY_ZOMBIE, 0, 4);
            //rules.SetEnemyRule(ENEMY_TYPE.OGRE, 5, 4);
            //rules.SetEnemyRule(ENEMY_TYPE.SKELETON_KNIGHT, 9, 3);
 

            //had to move CreatePlayer here as the creation of the spawn circle needs it to exist.
            CreatePlayer();

            //even though these enemySpawner instances instantly go out of scope. they are not destroyed while their timers are running. 
            for (float i = ENEMIES_WAVE_ONE; i < ENEMIES_WAVE_END; i += ENEMIES_INCREMENTER)
            {
                //grab a random rules                
                EnemySpawnRules rules = spawnRulesList[spawnRulesSelector.Roll()];

                Circle circle = new Circle(new Vector2(gameAssets.TowerListItem(0).X, gameAssets.TowerListItem(0).Y), 400.0);
                EnemySpawner enemySpawner = new EnemySpawner(this, rules, TIMER_INTERVAL, (uint)(spawnNumber * SPAWN_INTERVAL) + 1, (uint)i * 2, circle);
                ++spawnNumber;
            }            
        }

        public void CreateTestWave2()
        {

            //2 six sided dice. 
            Dice dice = new Dice(2, 6);

            //all dice rolls will give us a ghoul if no other rule is set
            EnemySpawnRules rules = new EnemySpawnRules(dice, ENEMY_TYPE.GHOUL);

            //if we roll an 11 or 12 (array pos 10 or 11) then give us a running ghoul
            rules.SetEnemyRule(ENEMY_TYPE.RUNNING_GHOUL, 10, 2); //if we roll 11 or 12 then give us a running ghoul

            //had to move CreatePlayer here as the creation of the spawn circle needs it to exist.
            CreatePlayer();

            //even though these enemySpawner instances instantly go out of scope. they are not destroyed while their timers are running. 
            for (int i = 0; i < 30; ++i)
            {
                Circle circle = new Circle(new Vector2(gameAssets.TowerListItem(0).X, gameAssets.TowerListItem(0).Y), 400.0);
                EnemySpawner enemySpawner = new EnemySpawner(this, rules, (uint)(300 - (i * 10)), (uint)(i * 2000) + 500, (uint)i * 2, circle);
            }
        }

#endregion

        //By the time we get to CreateEnemy the EnemySpawner, Dice and EnemySpawnRules have done their job i.e. decided what enemy to spawn. 
        public void CreateEnemy(ENEMY_TYPE enemyType_, Vector2 enemyPos_, int wave_ = 1)
        {
            //TODO: 0 hardcoded in next line for now, will be safe unless multiple towers introduced
            Enemy enemy = new Enemy(enemyType_, gameAssets.TowerListItem(0).Pos, enemyPos_);

            enemy.Texture = gameAssets.EnemyTextureList[(int)enemyType_];
            gameAssets.EnemyListAdd(enemy);
        }

        public void CreatePlayer()
        {
            Tower entity = new Tower();
            entity.Texture = gameAssets.TextureList[(int)PLAYER_SPRITES.TOWER];
            gameAssets.TowerListAdd(entity);
        }

        public void CastSpell(SPELL_TYPE spellType_, int level_, float x_, float y_)
        {
            Spell spell = new Spell(spellType_, level_, x_, y_);
            spell.Texture = gameAssets.SpellTextureList[(int)spellType_];
            gameAssets.SpellListAdd(spell);
        }

        public void CreateGUIComponent(GUI_SPRITES sprite_, float x_, float y_, int width_, int height_, bool active_)
        {
            GUI_Component button = new GUI_Component(x_, y_, width_, height_, active_);
            button.Texture = gameAssets.GUITextureList[(int)sprite_];
            gameAssets.GUIListAdd(button);
        }

    }
}
