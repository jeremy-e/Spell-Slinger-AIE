﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace SpellSlingerV1._0
{
    class PlayGame
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
        }

        public void Update(GameTime gameTime)
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


            //-------------------------------------------SPELLS
            //Click to cast
            if (Keyboard.GetState().IsKeyDown(Keys.D1))
            {
                spellSelect = SPELL_TYPE.FIREBALL;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D2))
            {
                spellSelect = SPELL_TYPE.ICELANCE;
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