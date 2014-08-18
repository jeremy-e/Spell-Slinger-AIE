#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
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
        EnemySpawner enemySpawner;
        
        SpriteManager spriteManager;
        Factory objectFactory;

        public static int SCREEN_WIDTH;
        public static int SCREEN_HEIGHT;
        public static uint waveTimer = 1000;
        public static int wave = 1;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.IsFullScreen = false;

            IsMouseVisible = true;
        }

        private void CreateWave1()
        {
            //2 six sided dice. 
            Dice dice = new Dice(2, 6);

            //all dice rolls will give us a ghoul if no other rule is set
            EnemySpawnRules rules = new EnemySpawnRules(dice, ENEMY_TYPE.GHOUL);

            //if we roll an 11 or 12 (array pos 10 or 11) then give us a running ghoul
            rules.SetEnemyRule(ENEMY_TYPE.RUNNING_GHOUL, 10, 2); //if we roll 11 or 12 then give us a running ghoul

            //create the spawner, this will be effected by the rules and the spawn rate (1000 miliseconds in this case)
            enemySpawner = new EnemySpawner(objectFactory, rules, waveTimer); 
        }

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
            wave = 1;

            //follow execution to see new randomization code. 
            CreateWave1();


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

            // TODO: Add your update logic here

            if (gameAssets.TowerList.Count <= 0)
            {
                objectFactory.CreatePlayer();
            }

            //if (gameAssets.EnemyList.Count <= 0)
            //{
            //    for (int i = 0; i < 10; i++)
            //    {
            //        objectFactory.CreateObject(typeof(Enemy), wave);                                      //Create Enemies on the fly - waves based on timer.
            //    }
            //}

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                for (int i = 0; i < gameAssets.EnemyList.Count; i++)
                {
                    //gameAssets.EnemyList[i].Y -= 10;                               //Testing movement
                    gameAssets.EnemyList[i].Move(gameTime);                               //Testing movement
                }
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

            //Objects that are marked as inactive will be removed from list

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

            for (int i = 0; i < gameAssets.DrawList.Count; i++)
            {
                viewPort.Draw(gameAssets.DrawList[i]);
            }

            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
