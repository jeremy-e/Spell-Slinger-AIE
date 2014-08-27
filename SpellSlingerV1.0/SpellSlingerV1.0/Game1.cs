#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using System.Diagnostics;
#endregion

namespace SpellSlingerV1._0
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    /// 
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameAssets gameAssets;
        ViewPort viewPort;
        //EnemySpawner enemySpawner;
        ColliderHandler colliderHandler;

        SpriteManager spriteManager;
        Factory objectFactory;

        public static int SCREEN_WIDTH;
        public static int SCREEN_HEIGHT;
        public static uint waveTimer = 1000;
        public static int wave = 1;

        bool leftMouseButtonDown = false;
        SPELL_TYPE spellSelect = SPELL_TYPE.FIREBALL;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.IsFullScreen = false;

            IsMouseVisible = true;
        }

        //private void CreateWave1()
        //{
        //    //2 six sided dice. 
        //    Dice dice = new Dice(2, 6);

        //    //all dice rolls will give us a ghoul if no other rule is set
        //    EnemySpawnRules rules = new EnemySpawnRules(dice, ENEMY_TYPE.GHOUL);

        //    //if we roll an 11 or 12 (array pos 10 or 11) then give us a running ghoul
        //    rules.SetEnemyRule(ENEMY_TYPE.RUNNING_GHOUL, 10, 2); //if we roll 11 or 12 then give us a running ghoul

        //    //had to move CreatePlayer here as the creation of the spawn circle needs it to exist.
        //    objectFactory.CreatePlayer();

        //    //Circle
        //    Circle circle = new Circle(new Vector2(gameAssets.TowerList[0].X, gameAssets.TowerList[0].Y), 600.0);

        //    //comment below line to remove circle of enemies
        //    //objectFactory.CircleTest();

        //    //create the spawner, this will be effected by the rules and the spawn rate (1000 miliseconds in this case)
        //    //enemySpawner = new EnemySpawner(objectFactory, rules, waveTimer, circle);
        //    objectFactory.CreateTestWave();

        //}

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            SCREEN_WIDTH = graphics.GraphicsDevice.Viewport.Width;
            SCREEN_HEIGHT = graphics.GraphicsDevice.Viewport.Height;
            gameAssets = new GameAssets();
            objectFactory = new Factory(gameAssets);
            spriteManager = new SpriteManager();
            spriteBatch = new SpriteBatch(GraphicsDevice);
            viewPort = new ViewPort(spriteBatch, SCREEN_WIDTH, SCREEN_HEIGHT);
            colliderHandler = new ColliderHandler();
            wave = 1;


            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.

            // TODO: use this.Content to load your game content here
            for (int i = 0; i < SpriteManager.playerNumTextures; i++)
            {
                gameAssets.TextureList.Add(Content.Load<Texture2D>(spriteManager.GetPlayerSpriteFileName(i)));
            }
            for (int i = 0; i < SpriteManager.enemyNumTextures; i++)
            {
                gameAssets.EnemyTextureList.Add(Content.Load<Texture2D>(spriteManager.GetEnemySpriteFileName(i)));
            }
            for (int i = 0; i < SpriteManager.spellNumTextures; i++)
            {
                gameAssets.SpellTextureList.Add(Content.Load<Texture2D>(spriteManager.GetSpellSpriteFileName(i)));
            }
            for (int i = 0; i < SpriteManager.GUINumTextures; i++)
            {
                gameAssets.GUITextureList.Add(Content.Load<Texture2D>(spriteManager.GetGUISpriteFileName(i)));
            }

            objectFactory.CreateTestWave();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            for (int i = 0; i < gameAssets.EnemyListCount; i++)
            {
                gameAssets.EnemyListItem(i).Move(gameTime);                               //Testing movement
            }

            for (int i = 0; i < gameAssets.TowerListCount; i++)
            {
                gameAssets.TowerListItem(i).Update();
            }

            //move viewport
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                viewPort.MoveX(-5);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                viewPort.MoveX(5);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                viewPort.MoveY(5);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                viewPort.MoveY(-5);
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
                int spellX = Mouse.GetState().X - viewPort.X;
                int spellY = Mouse.GetState().Y - viewPort.Y;
                objectFactory.CastSpell(spellSelect, 1, spellX, spellY);
                leftMouseButtonDown = true;
            }

            if (Mouse.GetState().LeftButton == ButtonState.Released)
            {
                leftMouseButtonDown = false;
            }
            //-------------------------------------------SPELLS

            gameAssets.RemoveEntitiesMarkedForDelete();
            
            //COLLISSION TESTING
            //Basic Player/Enemy Collission Test
            //Do we put logic here or collider can handle it?
            for (int i = 0; i < gameAssets.EnemyListCount; i++)
            {
                if (colliderHandler.Collider(gameAssets.TowerListItem(0), gameAssets.EnemyListItem(i)))
                {
                    gameAssets.TowerListItem(0).Capacity++;
                }
            }

            ///if any spells are active we check for collissions against active enemies
            if (gameAssets.SpellListCount > 0)
            {
                for (int i = 0; i < gameAssets.SpellListCount; i++)
                {
                    for (int j = 0; j < gameAssets.EnemyListCount; j++)
                    {
                        if (colliderHandler.Collider(gameAssets.SpellListItem(i), gameAssets.EnemyListItem(j)))
                            gameAssets.EnemyListItem(j).Hit(gameAssets.SpellListItem(i));
                    }
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            for (int i = 0; i < gameAssets.DrawListCount; i++)
            {
                viewPort.Draw(gameAssets.DrawListItem(i));
            }

            //Add GUI to separate drawlist - Always draw on top of everything else

            spriteBatch.End();


            base.Draw(gameTime);
        }

    }
}
