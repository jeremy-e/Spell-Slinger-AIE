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

        //Text to screen
        SpriteFont myFont;
        Vector2 fontPos;

        //Text to screen
        SpriteFont waveStateFont;
        Vector2 waveStateFontPos;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.IsFullScreen = false;

            IsMouseVisible = true;
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

            //CreatePlayer relies on the gameAssets being initialised
            objectFactory.CreatePlayer();

            //Text to screen - initiliase font & position
            myFont = Content.Load<SpriteFont>("myFont");
            fontPos = new Vector2(20, 80);

            //JEREMY!! where is "myFont" defined???
            waveStateFont = Content.Load<SpriteFont>("myFont");
            waveStateFontPos = new Vector2(20, 100);

            
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
            GraphicsDevice.Clear(Color.ForestGreen);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            for (int i = 0; i < gameAssets.DrawListCount; i++)
            {
                if (gameAssets.DrawListItem(i).Active)
                {
                    viewPort.Draw(gameAssets.DrawListItem(i));
                }
            }

            //Add GUI to separate drawlist - Always draw on top of everything else
            for (int i = 0; i < gameAssets.GUIListCount; i++)
            {
                if (gameAssets.GUIListItem(i).Active)
                {
                    viewPort.Draw(gameAssets.GUIListItem(i));
                }
            }

            if (currentGameState == (int)GAME_STATES.PLAY_GAME)
            {
                string output = gameAssets.TowerListItem(0).Essence.ToString();
                spriteBatch.DrawString(myFont, output, fontPos, Color.Black);

                if (gameState is PlayGame)
                {
                    if (((PlayGame)gameState).CurrentPlayState == PLAY_STATES.ABOUT_TO_GENERATE_WAVE)
                    {
                        spriteBatch.DrawString(waveStateFont, "ABOUT_TO_GENERATE_WAVE", waveStateFontPos, Color.Black);
                    }
                    if (((PlayGame)gameState).CurrentPlayState == PLAY_STATES.WAVE_IN_PROGRESS)
                    {
                        spriteBatch.DrawString(waveStateFont, "WAVE_IN_PROGRESS", waveStateFontPos, Color.Black);
                    }
                    if (((PlayGame)gameState).CurrentPlayState == PLAY_STATES.WAITING_FOR_WAVE_TO_START)
                    {
                        spriteBatch.DrawString(waveStateFont, "WAITING_FOR_WAVE_TO_START", waveStateFontPos, Color.Black);
                    }
                    if (((PlayGame)gameState).CurrentPlayState == PLAY_STATES.WAVE_COMPLETE)
                    {
                        spriteBatch.DrawString(waveStateFont, "WAVE_COMPLETE", waveStateFontPos, Color.Black);
                    }
                }
                
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
