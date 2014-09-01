using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;

namespace SpellSlingerV1._0
{
    class PlayGame : BASE_GAMESTATE
    {
        bool leftMouseButtonDown = false;
        SPELL_TYPE spellSelect = SPELL_TYPE.FIREBALL;
        GameAssets gameAssets_;
        ViewPort viewPort_;
        Factory objectFactory_;
        ColliderHandler colliderHandler_;
        GUI gui;

        List<int> activeSpellCDs;                                                   //Tracks cooldown time when specific spell cast

        public PlayGame(GameAssets gameAssets, ViewPort viewPort, Factory objectFactory, ColliderHandler colliderHandler)
        {
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
            SpellManagement();                                                      //Spells - suggest input handler later to cover some functions already being handled by this function
            gameAssets_.RemoveEntitiesMarkedForDelete();                            //Removing all objects marked as !active from appropriate lists
            CollisionTesting();                                                     //Collisions
        }



        void CollisionTesting()
        {
            //COLLISSION TESTING - Basic Player/Enemy Collission Test - logic here or collider can handle it?
            for (int i = 0; i < gameAssets_.EnemyListCount; i++)
            {
                if (colliderHandler_.Collider(gameAssets_.TowerListItem(0), gameAssets_.EnemyListItem(i)))
                {
                    gameAssets_.TowerListItem(0).Capacity++;

                }
            }

            ///if any spells are active we check for collissions against active enemies
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


            if (!Keyboard.GetState().IsKeyDown(Keys.D)
                && !Keyboard.GetState().IsKeyDown(Keys.A)
                && !Keyboard.GetState().IsKeyDown(Keys.S)
                && !Keyboard.GetState().IsKeyDown(Keys.W))
            {
                viewPort_.UnSnap();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {                
                viewPort_.SnapToX(250.0f);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                viewPort_.SnapToX(-250.0f);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                viewPort_.SnapToY(-150.0f);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                viewPort_.SnapToY(150.0f);
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
                        Debug.WriteLine("Spell is now ready for use: " + Enum.GetNames(typeof(SPELL_TYPE)).ElementAt(i));
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

    }
}
