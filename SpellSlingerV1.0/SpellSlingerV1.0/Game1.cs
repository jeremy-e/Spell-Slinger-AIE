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

        BASE_GAMESTATE gameState;
        int currentGameState;

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

            gameState = new Intro();
            currentGameState = -1;

            wave = 1;

            //playGame = new PlayGame(gameAssets, viewPort, objectFactory, colliderHandler);
            //gameState.CurrentGameState = (int)GAME_STATES.PLAY_GAME;

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

            if (currentGameState != gameState.CurrentGameState)
            {
                switch (gameState.CurrentGameState)
                {
                    case (int)GAME_STATES.INTRO:
                        break;
                    case (int)GAME_STATES.MENU:
                        break;
                    case (int)GAME_STATES.PLAY_GAME:
                        gameState = new PlayGame(gameAssets, viewPort, objectFactory, colliderHandler);
                        break;
                    case (int)GAME_STATES.OPTIONS:
                        break;
                    case (int)GAME_STATES.END:
                        break;
                    default:
                        break;
                }
                currentGameState = gameState.CurrentGameState;
            }

            gameState.Update(gameTime);

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
            for (int i = 0; i < gameAssets.GUIListCount; i++)
            {
                if (gameAssets.GUIListItem(i).Active)
                {
                    viewPort.Draw(gameAssets.GUIListItem(i));
                }
            }


            spriteBatch.End();


            base.Draw(gameTime);
        }

    }
}
