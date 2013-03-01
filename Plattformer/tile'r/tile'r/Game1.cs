using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using TestGame.TileEngine;
using Shooter;

namespace tile_r
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        List<Tile> tileList = new List<Tile>();
        Texture2D crate;
        Player player;
        Texture2D sprite;
        Vector2 playerVelocity = new Vector2(0, 0);
        bool standing = false;
        Rectangle playerRect;
        float rotation;
        KeyboardState prevKey;
        bool test;

        int[,] map = new int[,]
            {
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
            {0,0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,}


            };


        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            player = new Player();

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

            crate = Content.Load<Texture2D>("crate");

            Createflor();

            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();
            
            Animation animation = new Animation();

            sprite = Content.Load<Texture2D>("jasperrun");
            animation.Initialize(sprite, new Vector2(0, 0), 24, 42, 4, 100, Color.White, 1f, true);

            player.Initialize(animation, new Vector2(50, 100));

            KeyboardState prevKey = Keyboard.GetState();
            

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }


        private void Createflor()
        {

            int tileMapWidth = map.GetLength(1);
            int tileMapHeight = map.GetLength(0);

            for (int y = 0; y < tileMapHeight; y++)
            {

                for (int x = 0; x < tileMapWidth; x++)
                {
                    int textureIndex = map[y, x];




                    if (textureIndex == 1)
                    {

                        Tile tile;
                        Vector2 pos = new Vector2(48 * x, 48 * y);

                        tile = new Tile(crate, 48, 48, pos, true);

                        tileList.Add(tile);

                        Console.WriteLine("x=" + x + " y=" + y + " is " + textureIndex);
                    }
                    else
                    {
                        Console.WriteLine("x=" + x + " y=" + y + " is " + textureIndex);
                    }


                }
            }

        }


        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            playerVelocity.X = Clamp(playerVelocity.X, -400f, 400f);
            

            IsMouseVisible = true;
            MouseState mouse = Mouse.GetState();
            Console.WriteLine(mouse.X + " " + mouse.Y);

            playerRect = new Rectangle((int)player.Position.X, (int)player.Position.Y, 24, 42);

            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            

            

            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.F) && !prevKey.IsKeyDown(Keys.F))
            {

                graphics.IsFullScreen = !graphics.IsFullScreen;
                graphics.ApplyChanges();

            }


            if (keyboardState.IsKeyDown(Keys.G) && !prevKey.IsKeyDown(Keys.G))
            {

                graphics.PreferredBackBufferWidth = 1360;
                graphics.PreferredBackBufferHeight = 768;
                graphics.ApplyChanges();

            }


            

            foreach (Tile tile in tileList)
            {
                if (tile.BoundingBox.Intersects(playerRect))
                {


                    if (!standing)
                    {

                        if (playerVelocity.Y < 0)
                            playerVelocity.Y *= -1f;

                        if (playerVelocity.X < 0)
                            playerVelocity.X *= -0.5f;


                        if ((playerVelocity.Y >= 0) && (player.Position.Y > 4))
                        {
                            playerVelocity.Y = 0f;
                            standing = true;
                        }
                    }

                    if (standing)
                    {
                        if ((player.Position.Y + (player.Height / 2) - 5) >= (tile.Position.Y - (tile.SpriteHeight / 2)))
                            player.Position.Y = (tile.Position.Y - (tile.SpriteHeight / 2) - (player.Height / 2) + 5);

                        
                    }
                    

                    
                    break;
                }
                else
                {
                    //  playerVelocity.Y = 9.82f;
                    standing = false;
                    
                }

            }

            if (!standing)
            {
                playerVelocity += new Vector2(0, 9.82f);
            }
            else
            {
                    playerVelocity.X /= 1.016f;
                if (playerVelocity.X < 0)
                {
                    playerVelocity.X+=2;

                }
                else if (playerVelocity.X > 0)
                {
                    playerVelocity.X-=2;
                }

                if (keyboardState.IsKeyDown(Keys.Space) && !prevKey.IsKeyDown(Keys.Space))
                {
                    standing = false;
                    playerVelocity -= new Vector2(0, 350f);
                }


            }



            if (keyboardState.IsKeyDown(Keys.Left))
            {
                playerVelocity -= new Vector2(10f, 0);
            }

            if (keyboardState.IsKeyDown(Keys.Right))
            {
                playerVelocity += new Vector2(10f, 0);
            }

          
            if (keyboardState.IsKeyDown(Keys.Down))
            {

                rotation += 0.1f;
            }

          

       

         

                player.Position += playerVelocity * elapsed;

                player.Update(gameTime);

                prevKey = keyboardState;
                

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            foreach (Tile tile in tileList)
            {
                tile.Draw(spriteBatch);
                
            }

            player.Draw(spriteBatch, rotation);
           // Console.WriteLine(player.Position.X + " " + player.Position.Y);
            Console.WriteLine(standing);

            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }





        public float Clamp(float var, float min, float max)
        {

            if (var < min)
                var = min;

            if (var > max)
                var = max;

            return var;
        
        }


    }
}
