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
   //     Texture2D speederSprite;
        Vector2 playerVelocity = new Vector2(0, 0);
        bool standing = false;
        float rotation;
        KeyboardState prevKey;
        GamePadState prevGamePad;
        public Camera2D cam = new Camera2D();
        Animation animation = new Animation();
        bool running;
        Texture2D wood;
        GamePadState gamepad;
        List<Ninja> ninjaList = new List<Ninja>();
        Texture2D ninjaTexture;
     //   float tempPos;
        List<Speeder> speederList = new List<Speeder>();
        float jump = 950;
        

        int[,] map = new int[,]
            {
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,3,3,3,3,3,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
            {0,0,0,0,0,0,0,0,0,0,2,3,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,3,3,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
            {0,0,0,0,0,0,0,0,0,0,0,2,3,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,3,3,3,3,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,}


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

            crate = Content.Load<Texture2D>("art/crate");

            wood = Content.Load<Texture2D>("art/floor");

            ninjaTexture = Content.Load<Texture2D>("art/Ninja");

            //speederSprite = Content.Load<Texture2D>("Speeder");

            cam.Pos = new Vector2(682.0f, 334.0f);

            gamepad = GamePad.GetState(PlayerIndex.One);

            Createflor();

            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();


            double dt = (double)1000 / (double)120;
            graphics.SynchronizeWithVerticalRetrace = false;

            this.TargetElapsedTime = TimeSpan.FromMilliseconds(dt);
            graphics.ApplyChanges();
            
            
            sprite = Content.Load<Texture2D>("art/jasperrunCopy");
            animation.Initialize(sprite, new Vector2(0, 0), 48, 84, 4, 150, Color.White, 1f, true);


            player.Initialize(animation, new Vector2(400, -700));

            KeyboardState prevKey = Keyboard.GetState();
            prevGamePad = GamePad.GetState(PlayerIndex.One);

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



                    switch (textureIndex)
                    { 
                        case 1 :
                        Tile tile;
                        Vector2 pos = new Vector2(48 * x, 48 * y);

                        tile = new Tile(wood, 48, 48, pos, true);

                        tileList.Add(tile);

                        Console.WriteLine("x=" + x + " y=" + y + " is " + textureIndex);

                            break;

                        case 2:
                            
                        

                        tile = new Tile(Content.Load<Texture2D>("art/fShelf/shelfLeft"), 48, 48, new Vector2(48 * x, 48 * y), true);

                        tileList.Add(tile);

                        Console.WriteLine("x=" + x + " y=" + y + " is " + textureIndex);
                            break;

                        case 3:



                            tile = new Tile(Content.Load<Texture2D>("art/fShelf/shelfMid"), 48, 48, new Vector2(48 * x, 48 * y), true);

                            tileList.Add(tile);

                            Console.WriteLine("x=" + x + " y=" + y + " is " + textureIndex);
                            break;

                        case 4:



                            tile = new Tile(Content.Load<Texture2D>("art/fShelf/shelfRight"), 48, 48, new Vector2(48 * x, 48 * y), true);

                            tileList.Add(tile);

                            Console.WriteLine("x=" + x + " y=" + y + " is " + textureIndex);
                            break;

                        default:
                            Console.WriteLine("x=" + x + " y=" + y + " is " + textureIndex);
                            break;
                    }


                    /*
                    if (textureIndex == 1)
                    {

                        Tile tile;
                        Vector2 pos = new Vector2(48 * x, 48 * y);

                        tile = new Tile(wood, 48, 48, pos, true);

                        tileList.Add(tile);

                        Console.WriteLine("x=" + x + " y=" + y + " is " + textureIndex);
                    }
                    else
                    {
                        Console.WriteLine("x=" + x + " y=" + y + " is " + textureIndex);
                    }*/


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

            gamepad = GamePad.GetState(PlayerIndex.One);
            KeyboardState keyboardState = Keyboard.GetState();
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || keyboardState.IsKeyDown(Keys.Escape))
                this.Exit();

            playerVelocity.X = Clamp(playerVelocity.X, -400f, 400f);


            IsMouseVisible = true;
            MouseState mouse = Mouse.GetState();
            //  Console.WriteLine(mouse.X + " " + mouse.Y);


            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;



            if (GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed)
            {


                graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;


                graphics.IsFullScreen = !graphics.IsFullScreen;
                graphics.ApplyChanges();
            }



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

            


            if (keyboardState.IsKeyDown(Keys.Q) && !prevKey.IsKeyDown(Keys.Q))
            {

                Ninja ninja = new Ninja(ninjaTexture, player.Position, playerVelocity);
                ninjaList.Add(ninja);

            }

            if (keyboardState.IsKeyDown(Keys.E) && !prevKey.IsKeyDown(Keys.E))
            {
                Speeder speeder = new Speeder(sprite,player.Position + new Vector2(80, -40),player);
                speederList.Add(speeder);

            }

            foreach (Speeder speeder in speederList)
            {
                
            }






            for (int x = ninjaList.Count - 1; x >= 0; x--)
            {
                ninjaList[x].Update(gameTime);
                if ((ninjaList[x].timer >= 1.5f) || ninjaList[x].position.Y > 1500)
                {
                    ninjaList[x].respawn(player.Position, playerVelocity);
                    ninjaList[x].timer = 0f;
                }
                if (ninjaList[x].timer > 0 && ninjaList[x].timer <= 1.5f)
                {
                    if (player.hitbox.Intersects(ninjaList[x].hitbox) && ninjaList[x].standning)
                    {
                        
                        ninjaList.RemoveAt(x);
                    }
                   
                }

                if (player.hitbox.Intersects(ninjaList[x].hitbox) && !ninjaList[x].standning)
                {
                    ninjaList.Remove(ninjaList[x]);
                }

            }











            foreach (Tile tile in tileList)
            {


                foreach (Ninja ninja in ninjaList)
                {
                    if (tile.BoundingBox.Intersects(ninja.hitbox))
                    {
                        ninja.standning = true;
                       
                    }

                    
                }


                if (tile.BoundingBox.Intersects(player.hitbox))
                {


                    if (!standing)
                    {

                        //Right of the tile

                        if ((player.Position.X - player.Width / 2) > (tile.Position.X))
                        {
                            if ((player.Position.Y + player.Height) - 10 > (tile.Position.Y))
                            {
                                if ((player.Position.Y) < (tile.Position.Y + (tile.SpriteHeight)))
                                {
                                    playerVelocity.X = Clamp(playerVelocity.X, 50f, 400f);
                                    
                                }
                            }
                        }

                        //Left of the tile

                        if ((player.Position.X + player.Width / 2) < (tile.Position.X))
                        {
                            if ((player.Position.Y + player.Height) - 10 > (tile.Position.Y))
                            {
                                if ((player.Position.Y) < (tile.Position.Y + (tile.SpriteHeight)))
                                {
                                   playerVelocity.X = Clamp(playerVelocity.X, -400f, -50f);
                                    
                                }
                            }
                        }

                        //On the tile

                        if (((playerVelocity.Y >= 0) && ((player.Position.Y + player.Height) > tile.Position.Y))&&!(tile.BoundingBox.Top==player.hitbox.Bottom))
                        {

                            playerVelocity.Y = 0f;
                            standing = true;
                        }

                        //Beneath the tile

                        if (playerVelocity.Y < 0)
                        {
                            if ((player.Position.Y) > (tile.Position.Y + tile.SpriteHeight - 5))
                                playerVelocity.Y = Math.Abs(playerVelocity.Y);
                                //playerVelocity.Y *= -1f;
                        }



                    }

                    if (standing)
                    {
                        //Inside the tile
                        if (playerVelocity.Y >= 0)
                            if ((player.Position.X + player.Width - 10) > (tile.Position.X))
                            {
                                if ((player.Position.X + 10) < (tile.Position.X + tile.SpriteWidth))
                                {
                                    if ((player.Position.Y + player.Height) > (tile.Position.Y))
                                    {
                                        player.Position.Y = (tile.Position.Y - (player.Height) + 2);
                                    }

                                     /*  if ((player.Position.Y < (tile.Position.Y + tile.SpriteHeight)))
                                       {
                                           player.Position.Y = (tile.Position.Y + tile.SpriteHeight + player.Height - 5);
                                       }*/ 
                                }
                            }

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

                
                

                if ((playerVelocity.X < 0 && player.Facing == "right") || (playerVelocity.X > 0 && player.Facing == "left"))
                    playerVelocity.X /= 1.1f;

                if (!running)
                {
                    playerVelocity.X /= 1.1f;

                    if (playerVelocity.X < 0)
                    {
                        playerVelocity.X += 2;

                    }
                    else if (playerVelocity.X > 0)
                    {
                        playerVelocity.X -= 2;
                    }
                }

                if ((keyboardState.IsKeyDown(Keys.Space) && !prevKey.IsKeyDown(Keys.Space)) || (gamepad.Buttons.A == ButtonState.Pressed && prevGamePad.Buttons.A == ButtonState.Released))
                {
                    standing = false;
                    playerVelocity -= new Vector2(0, jump);
                }


               

            }



            if (keyboardState.IsKeyDown(Keys.A))
            {
                cam.Pos += new Vector2(-2f, 0f);
            }

            if (keyboardState.IsKeyDown(Keys.D))
            {
                cam.Pos += new Vector2(2f, 0f);

            }


            if (keyboardState.IsKeyDown(Keys.S))
            {

                cam.Pos += new Vector2(0f, 2f);
            }

            if (keyboardState.IsKeyDown(Keys.W))
            {

                cam.Pos += new Vector2(0f, -2f);
            }

            if (keyboardState.IsKeyUp(Keys.Left) && keyboardState.IsKeyUp(Keys.Right))
                running = false;

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                playerVelocity -= new Vector2(7f, 0);
                player.Facing = "left";
                
                animation.draw = true;
                running = true;
            }

            if (keyboardState.IsKeyDown(Keys.Right))
            {
                playerVelocity += new Vector2(7f, 0);
                player.Facing = "right";
                
                animation.draw = true;
                running = true;
            }


            playerVelocity += new Vector2(gamepad.ThumbSticks.Left.X*7, 0);

            if (gamepad.ThumbSticks.Left.X < 0)
            {
                player.Facing = "left";
                animation.draw = true;
                running = true;
            }

            if (gamepad.ThumbSticks.Left.X > 0)
            {
                player.Facing = "right";
                animation.draw = true;
                running = true;
            }



            if (keyboardState.IsKeyDown(Keys.Down))
            {

                rotation += 0.1f;
            }




            if ((player.Position.X - cam._pos.X) > graphics.GraphicsDevice.Viewport.Width * 0.30)
                cam._pos += new Vector2(Math.Abs(playerVelocity.X) / 80, 0f);


            if ((player.Position.X - cam._pos.X) < -graphics.GraphicsDevice.Viewport.Width * 0.30)
                cam._pos -= new Vector2(Math.Abs(playerVelocity.X) / 80, 0f);


            


            animation.frameTime = 130f - Math.Abs(playerVelocity.X / 10);


            player.Position += playerVelocity * elapsed;

            player.Update(gameTime);

            prevKey = keyboardState;

            prevGamePad = gamepad;

          //  foreach (Ninja ninja in ninjaList)
            
                



            if (Math.Abs(playerVelocity.X) > 55)
                animation.draw = true;
            else
                animation.draw = false;


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

            spriteBatch.Begin(SpriteSortMode.BackToFront,
                        BlendState.AlphaBlend,
                        null,
                        null,
                        null,
                        null,
                        cam.get_transformation(GraphicsDevice /*Send the variable that has your graphic device here*/));

         
           


            foreach (Ninja ninja in ninjaList)
            {
                ninja.Draw(spriteBatch);
            }

            foreach (Speeder speeder in speederList)
            {
                speeder.Draw(spriteBatch);
            }
            
             foreach (Tile tile in tileList)
            {
                tile.Draw(spriteBatch);

            }


            player.Draw(spriteBatch, rotation);
            //  Console.WriteLine(cam.Pos.X + " " + cam.Pos.Y);
            Console.WriteLine(playerVelocity.X);

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
