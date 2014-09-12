using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;
using System.Timers;

namespace SpellSlingerV1._0
{
    class PlayGame : BASE_GAMESTATE
    {
        const int TIME_BETWEEN_SPAWNERS_BASE = 3000;
        const int TIME_BETWEEN_SPAWNERS_MULTI = 100;
        const int POINTS_TO_SPEND_BASE = 200;
        const int POINTS_TO_SPEND_MULTI = 50;
        const int SPAWNER_NUM_MULTI = 2;

        bool leftMouseButtonDown = false;
        SPELL_TYPE spellSelect = SPELL_TYPE.FIREBALL;
        PLAY_STATES playState = PLAY_STATES.ABOUT_TO_GENERATE_WAVE;
        GameAssets gameAssets_;
        ViewPort viewPort_;
        Factory objectFactory_;
        ColliderHandler colliderHandler_;
        GUI gui;
        List<EnemySpawner> currentWave;
        private Timer waveCompleteTimer;
        int waveNum;

        List<int> activeSpellCDs;                                                   //Tracks cooldown time when specific spell cast

        public PlayGame(GameAssets gameAssets, ViewPort viewPort, Factory objectFactory, ColliderHandler colliderHandler)
        {
            //I am assuming PlayGame is only created to start new game
            //correct me if im wrong. 
            waveNum = 1;

            gui = new GUI(objectFactory, viewPort);
            gameAssets_ = gameAssets;
            viewPort_ = viewPort;
            objectFactory_ = objectFactory;
            colliderHandler_ = colliderHandler;
            CurrentGameState = (int)GAME_STATES.PLAY_GAME;

            gui.GUIPlayGame();                                                      //Initialise GUI

            activeSpellCDs = new List<int>();
            for (int i = 0; i < Enum.GetNames(typeof(SPELL_TYPE)).Length; i++)
            {
                activeSpellCDs.Add(0);
            }

            //remove arbitrary timer value
            waveCompleteTimer = new System.Timers.Timer(3000);
            waveCompleteTimer.Elapsed += CreateANewWave;
        }

        //triggered from the waveCompleteTimer.Elapsed
        private void CreateANewWave(Object source, ElapsedEventArgs e)
        {
            waveCompleteTimer.Stop();
            playState = PLAY_STATES.ABOUT_TO_GENERATE_WAVE;
        }

        public override void Update(GameTime gameTime)
        {          
            for (int i = 0; i < gameAssets_.EnemyListCount; i++)                    //Enemy logic
            {
                gameAssets_.EnemyListItem(i).Update(gameTime);
            }

            for (int i = 0; i < gameAssets_.TowerListCount; i++)                    //Player logic
            {
                gameAssets_.TowerListItem(i).Update(gameTime);
            }

            MoveViewPort();                                                         //Viewport control
            viewPort_.Update();

            SpellManagement();                                                      //Spells - suggest input handler later to cover some functions already being handled by this function
            gameAssets_.RemoveEntitiesMarkedForDelete();                            //Removing all objects marked as !active from appropriate lists            
            CollisionTesting(gameTime);                                                     //Collisions

            UpdateState();
        }

        private void UpdateState()
        {
            if (playState == PLAY_STATES.ABOUT_TO_GENERATE_WAVE)
            {
                int pointsToSpendPerSpawner = waveNum * POINTS_TO_SPEND_MULTI + POINTS_TO_SPEND_BASE;
                int numOfSpawners = waveNum * SPAWNER_NUM_MULTI;                
                int timeBetweenSpawners = waveNum * TIME_BETWEEN_SPAWNERS_MULTI + TIME_BETWEEN_SPAWNERS_BASE;

                Debug.WriteLine("pointsToSpendPerSpawner: " + pointsToSpendPerSpawner);
                Debug.WriteLine("numOfSpawners: " + numOfSpawners);
                Debug.WriteLine("timeBetweenSpawners: " + timeBetweenSpawners);

                currentWave = objectFactory_.GenerateWave(pointsToSpendPerSpawner, numOfSpawners, timeBetweenSpawners);
                playState = PLAY_STATES.WAITING_FOR_WAVE_TO_START;
            }
            if (playState == PLAY_STATES.WAITING_FOR_WAVE_TO_START && gameAssets_.EnemyListCount > 0)
            {
                playState = PLAY_STATES.WAVE_IN_PROGRESS;
            }
            if (playState == PLAY_STATES.WAVE_IN_PROGRESS && gameAssets_.EnemyListCount == 0)
            {
                //check that all spawners have stopped spawning
                bool running = false;
                for ( int i = 0; i < currentWave.Count; ++i )
                {
                    if (currentWave[i].Running)
                    {
                        running = true;
                    }
                }

                if (!running)
                {
                    waveNum++;                    
                    playState = PLAY_STATES.WAVE_COMPLETE;                    
                    Debug.WriteLine("BEGIN WAVE" + waveNum);                    
                    waveCompleteTimer.Start();
                }
            }
        }



        void CollisionTesting(GameTime gameTime_)
        {
            //COLLISSION TESTING - Basic Player/Enemy Collission Test - logic here or collider can handle it?
            for (int i = 0; i < gameAssets_.EnemyListCount; i++)
            {
                if (colliderHandler_.Collider(gameAssets_.TowerListItem(0), gameAssets_.EnemyListItem(i)))
                {
                    gameAssets_.TowerListItem(0).Capacity++;

                }
            }

            //spell - enemy
            if (gameAssets_.SpellListCount > 0)
            {
                for (int i = 0; i < gameAssets_.SpellListCount; i++)
                {
                    for (int j = 0; j < gameAssets_.EnemyListCount; j++)
                    {
                        if (colliderHandler_.Collider(gameAssets_.SpellListItem(i), gameAssets_.EnemyListItem(j)))
                        {
                            int essenceReturned = gameAssets_.EnemyListItem(j).Hit(gameAssets_.SpellListItem(i));
                            gameAssets_.TowerListItem(0).Essence += essenceReturned;
                        }                        
                    }
                }
            }


        }



        void MoveViewPort()
        {
            if (!Keyboard.GetState().IsKeyDown(Keys.A))
            {
                viewPort_.UnSnapLeft();
            }
            if (!Keyboard.GetState().IsKeyDown(Keys.D))
            {
                viewPort_.UnSnapRight();
            }
            if (!Keyboard.GetState().IsKeyDown(Keys.W))
            {
                viewPort_.UnSnapUp();
            }
            if (!Keyboard.GetState().IsKeyDown(Keys.S))
            {
                viewPort_.UnSnapDown();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                viewPort_.SnapToRight(250.0f);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                viewPort_.SnapToLeft(250.0f);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                viewPort_.SnapToUp(-150.0f);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                viewPort_.SnapToDown(150.0f);
            }

            for (int i = 0; i < gameAssets_.GUIListCount; i++)                  //Move GUI Elements with Viewport
            {
                gameAssets_.GUIListItem(i).Update(viewPort_.X, viewPort_.Y);
            }
        }


        public void SetActiveSpell(SPELL_TYPE type_)
        {
            for (int i = 0; i < gameAssets_.GUIListCount; i++)
            {
                if (i == (int)type_)
                {
                    gameAssets_.GUIListItem(i).Active = true;
                }
                else if (i < 5) //hack temp fix
                {
                    gameAssets_.GUIListItem(i).Active = false;
                }
            }
        }


        void SpellManagement()
        {
            //set all existing on screen spells Initialhit=false
            for (int i = 0; i < gameAssets_.SpellListCount; i++)
            {
                gameAssets_.SpellListItem(i).InitialHitFinished();
            }

            //Click to cast
            if (Keyboard.GetState().IsKeyDown(Keys.D1))
            {
                spellSelect = SPELL_TYPE.FIREBALL;
                SetActiveSpell(SPELL_TYPE.FIREBALL);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D2))
            {
                spellSelect = SPELL_TYPE.ICELANCE;
                SetActiveSpell(SPELL_TYPE.ICELANCE);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D3))
            {
                spellSelect = SPELL_TYPE.LIGHTNING;
                SetActiveSpell(SPELL_TYPE.LIGHTNING);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D4))
            {
                spellSelect = SPELL_TYPE.DESPAIR;
                SetActiveSpell(SPELL_TYPE.DESPAIR);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D5))
            {
                spellSelect = SPELL_TYPE.RAPTURE;
                SetActiveSpell(SPELL_TYPE.RAPTURE);
            }

            if (Mouse.GetState().LeftButton == ButtonState.Pressed && !leftMouseButtonDown)
            {
                leftMouseButtonDown = true;
                bool GUIElementClicked = false;
                //Get Mouse position in viewport
                Vector2 mousePos = new Vector2(Mouse.GetState().X - viewPort_.X, Mouse.GetState().Y - viewPort_.Y);

                for (int i = 0; i < gameAssets_.GUIListCount; i++)
                {
                    if (colliderHandler_.Collider(gameAssets_.GUIListItem(i), mousePos) && gameAssets_.GUIListItem(i).Active)
                    {
                        //Logic for GUI Element depending on type
                        //Tower - Upgrades?
                        //Spellbook - spell book
                        //hotbar - select
                        //Debug.WriteLine("PING" + gameTime.TotalGameTime);
                        GUIElementClicked = true;
                    }
                }

                if (!GUIElementClicked && !gameAssets_.TowerListItem(0).SpellCast && activeSpellCDs[(int)spellSelect] <= 0)
                {
                    //We must iterate through current active spell list to see whether the selected spell is currently on cooldown. (may change)
                    int spellX = Mouse.GetState().X - viewPort_.X;
                    int spellY = Mouse.GetState().Y - viewPort_.Y;

                    //Debug.WriteLine("X" + spellX + "Y" + spellY);

                    objectFactory_.CastSpell(spellSelect, gameAssets_.TowerListItem(0).SpellLevel[(int)spellSelect], spellX, spellY);
                    //Player has cast a spell - intiate global cooldown
                    gameAssets_.TowerListItem(0).SpellCast = true;
                    leftMouseButtonDown = true;

                    //Add cooldown time to list
                    activeSpellCDs[(int)spellSelect] = gameAssets_.SpellListItem((int)gameAssets_.SpellListCount - 1).SpellCooldown;
                }
            }



            //Iterate over cooldownlist - if anything > 0 we need to count it down
            for (int i = 0; i < activeSpellCDs.Count; i++)
            {
                if (activeSpellCDs[i] > 0)
                {
                    activeSpellCDs[i] -= 5;                    //Hack - incorporate timers later
                    if (activeSpellCDs[i] <= 0)
                    {
                    //    Debug.WriteLine("Spell is now ready for use: " + Enum.GetNames(typeof(SPELL_TYPE)).ElementAt(i));
                    }
                }

                if (activeSpellCDs[i] < 0)
                {
                    activeSpellCDs[i] = 0;
                }
            }

            if (Mouse.GetState().LeftButton == ButtonState.Released)
            {
                leftMouseButtonDown = false;
            }
        }

        public PLAY_STATES CurrentPlayState
        {
            get { return playState; } 
        }
    }
}
