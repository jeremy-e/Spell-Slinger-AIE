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

        //TESTING CHANGES DUDES - GYM

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        List<Entity> DrawList;                  //Used to track ALL objects
        List<Texture2D> TextureList;            //tracks ALL textures from DrawList
        List<Enemy> EnemyList;                  //tracking enemies
        List<Tower> TowerList;                  //Who knows we might want multi player one day?

        SpriteManager spriteManager;
        Factory objectFactory;

        public static int SCREEN_WIDTH;
        public static int SCREEN_HEIGHT;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.IsFullScreen = false;

            IsMouseVisible = true;
        }

        //TEST - 11/8/2014

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
            objectFactory = new Factory();
            DrawList = new List<Entity>();                                                          //All objects added to DrawList - use this to draw to screen.
            TextureList = new List<Texture2D>();
            EnemyList = new List<Enemy>();
            TowerList = new List<Tower>();
            spriteManager = new SpriteManager();

            CreateObject("tower");                                                                     //Create objects

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            for (int i = 0; i < SpriteManager.numberOfTextures; i++)
            {
                TextureList.Add(Content.Load<Texture2D>(spriteManager.GetSpriteID(i)));
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

            if (EnemyList.Count <= 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    CreateObject("enemy");                                      //Create Enemies on the fly - waves based on timer.
                }
            }


            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                for (int i = 0; i < EnemyList.Count; i++)
                {
                    EnemyList[i].Y -= 10;                               //Testing movement
                }
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

            //Separate DrawLists for layers / updating positions
            for (int i = 0; i < DrawList.Count; i++)                                                             //Add objects to a draw list
            {
                spriteBatch.Draw(TextureList[DrawList[i].Type], new Rectangle((int)DrawList[i].X, (int)DrawList[i].Y, DrawList[i].Width, DrawList[i].Height), Color.White);
            }

            spriteBatch.End();


            base.Draw(gameTime);
        }

        /// <summary>
        /// This creates the object - still needs to error check item entered by programmer - adds object to drawList
        /// </summary>
        /// <param name="objectName"></param>
        public void CreateObject(string objectName)
        {
            Entity gameEntity = objectFactory.CreateObject(objectName);

            //All objects must be added to the drawlist
            DrawList.Add(gameEntity);

            //Separate objects into individual lists for collisions - Do not create
            if (objectName == "tower")
            {
                //it is ok to do this instead of accessing DrawList[DrawList.count - 1]
                TowerList.Add((Tower)gameEntity);
            }

            if (objectName == "enemy")
            {
                EnemyList.Add((Enemy)gameEntity);
            }
        }
    }
}
