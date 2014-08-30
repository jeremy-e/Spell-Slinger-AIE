using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Diagnostics;

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
        
        public PlayGame(GameAssets gameAssets, ViewPort viewPort, Factory objectFactory, ColliderHandler colliderHandler)
        {
            gameAssets_ = gameAssets;
            viewPort_ = viewPort;
            objectFactory_ = objectFactory;
            colliderHandler_ = colliderHandler;
            CurrentGameState = (int)GAME_STATES.PLAY_GAME;
            
            //Initialise GUI
            objectFactory.CreateGUIComponent(GUI_SPRITES.HOTBAR_1, 25, viewPort.ViewPortHeight - 75, 250, 50);
            objectFactory.CreateGUIComponent(GUI_SPRITES.HOTBAR_2, 25, viewPort.ViewPortHeight - 75, 250, 50);
            objectFactory.CreateGUIComponent(GUI_SPRITES.HOTBAR_3, 25, viewPort.ViewPortHeight - 75, 250, 50);
            objectFactory.CreateGUIComponent(GUI_SPRITES.HOTBAR_4, 25, viewPort.ViewPortHeight - 75, 250, 50);
            objectFactory.CreateGUIComponent(GUI_SPRITES.HOTBAR_5, 25, viewPort.ViewPortHeight - 75, 250, 50);

            //Set first spell as active
            gameAssets_.GUIListItem(0).Active = true;
        }

        public void SetActiveSpell(SPELL_TYPE type_)
        {
            for (int i = 0; i < gameAssets_.GUIListCount; i++)
            {
                if (i == (int)type_)
                {
                    gameAssets_.GUIListItem(i).Active = true;
                }
                else
                {
                    gameAssets_.GUIListItem(i).Active = false;
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            
            for (int i = 0; i < gameAssets_.EnemyListCount; i++)
            {
                gameAssets_.EnemyListItem(i).Move(gameTime);                               //Testing movement
            }

            for (int i = 0; i < gameAssets_.TowerListCount; i++)
            {
                gameAssets_.TowerListItem(i).Update();
            }

            //move viewport
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                viewPort_.MoveX(-5);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                viewPort_.MoveX(5);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                viewPort_.MoveY(5);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                viewPort_.MoveY(-5);
            }
            
            //move GUI elements w/viewport
            for (int i = 0; i < gameAssets_.GUIListCount; i++)
            {
                gameAssets_.GUIListItem(i).Update(viewPort_.X, viewPort_.Y);
            }

            //-------------------------------------------SPELLS
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
                //Debug.WriteLine("BOOM");
                int spellX = Mouse.GetState().X - viewPort_.X;
                int spellY = Mouse.GetState().Y - viewPort_.Y;
                objectFactory_.CastSpell(spellSelect, 1, spellX, spellY);
                leftMouseButtonDown = true;
            }

            if (Mouse.GetState().LeftButton == ButtonState.Released)
            {
                leftMouseButtonDown = false;
            }
            //-------------------------------------------SPELLS

            gameAssets_.RemoveEntitiesMarkedForDelete();

            //COLLISSION TESTING
            //Basic Player/Enemy Collission Test
            //Do we put logic here or collider can handle it?
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
    }
}
