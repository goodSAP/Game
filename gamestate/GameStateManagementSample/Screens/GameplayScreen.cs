#region File Description
//-----------------------------------------------------------------------------
// GameplayScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
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
using tile_r;
#endregion

namespace GameStateManagement
{
    /// <summary>
    /// This screen implements the actual game logic. It is just a
    /// placeholder to get the idea across: you'll probably want to
    /// put some more interesting gameplay in here!
    /// </summary>
    class GameplayScreen : GameScreen
    {
        #region Fields

        ContentManager content;
        SpriteFont gameFont;

        Vector2 playerPosition = new Vector2(100, 100);
        Vector2 enemyPosition = new Vector2(100, 100);

        Random random = new Random();

        float pauseAlpha;

        #region vars
        GraphicsDeviceManager graphics;
        
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
        float jump = 650;
        Texture2D lifeBar;
        int life = 3;
        MouseState mouse;
        Vector2 test = new Vector2(-613, -327);
        #region Map
        int[,] map = new int[,]
            {                                                                                                                                                                                                /*introduce enemies here*/                                   
/*maximum*/ {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,},
/*visible*/ {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,},
/*heigth->*/{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,3,3,3,3,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,},
            {1,0,0,0,0,0,0,0,0,2,3,4,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,2,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,4,0,0,0,2,4,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,2,3,4,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,3,3,3,3,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,},
            {1,0,0,0,0,0,0,0,0,2,3,4,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,1,1,1,1,1,1,1,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,},

            };
        #endregion


        #endregion


        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public GameplayScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }


        /// <summary>
        /// Load graphics content for the game.
        /// </summary>
        public override void LoadContent()
        {
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");

            gameFont = content.Load<SpriteFont>("gamefont");

            // A real game would probably have more content than this sample, so
            // it would take longer to load. We simulate that by delaying for a
            // while, giving you a chance to admire the beautiful loading screen.


    //        spriteBatch = new SpriteBatch(GraphicsDevice);

            crate = content.Load<Texture2D>("art/crate");
            player = new Player();
            wood = content.Load<Texture2D>("art/floor");

            lifeBar = content.Load<Texture2D>("art/life");

            ninjaTexture = content.Load<Texture2D>("art/Ninja");

            //speederSprite = Content.Load<Texture2D>("Speeder");

            cam.Pos = new Vector2(682.0f, 526.0f);

            gamepad = GamePad.GetState(PlayerIndex.One);

            Createflor();

            


            sprite = content.Load<Texture2D>("art/jasperrunCopy");
            animation.Initialize(sprite, new Vector2(0, 0), 48, 84, 4, 150, Color.White, 1f, true);


            player.Initialize(animation, new Vector2(550, 500));

            KeyboardState prevKey = Keyboard.GetState();
            prevGamePad = GamePad.GetState(PlayerIndex.One);




            // once the load has finished, we use ResetElapsedTime to tell the game's
            // timing mechanism that we have just finished a very long frame, and that
            // it should not try to catch up.
            ScreenManager.Game.ResetElapsedTime();
        }


        /// <summary>
        /// Unload graphics content used by the game.
        /// </summary>
        public override void UnloadContent()
        {
            content.Unload();
        }



        #region Tile

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
                        case 1:
                            Tile tile;
                            Vector2 pos = new Vector2(48 * x, 48 * y);

                            tile = new Tile(wood, 48, 48, pos, true);

                            tileList.Add(tile);

                            Console.WriteLine("x=" + x + " y=" + y + " is " + textureIndex);

                            break;

                        case 2:



                            tile = new Tile(content.Load<Texture2D>("art/fShelf/shelfLeft"), 48, 48, new Vector2(48 * x, 48 * y), true);

                            tileList.Add(tile);

                            Console.WriteLine("x=" + x + " y=" + y + " is " + textureIndex);
                            break;

                        case 3:



                            tile = new Tile(content.Load<Texture2D>("art/fShelf/shelfMid"), 48, 48, new Vector2(48 * x, 48 * y), true);

                            tileList.Add(tile);

                            Console.WriteLine("x=" + x + " y=" + y + " is " + textureIndex);
                            break;

                        case 4:



                            tile = new Tile(content.Load<Texture2D>("art/fShelf/shelfRight"), 48, 48, new Vector2(48 * x, 48 * y), true);

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

        #endregion




        #endregion

        #region Update and Draw


        /// <summary>
        /// Updates the state of the game. This method checks the GameScreen.IsActive
        /// property, so the game will stop updating when the pause menu is active,
        /// or if you tab away to a different application.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);

            Update(gameTime);

            mouse = Mouse.GetState();

            // Gradually fade in or out depending on whether we are covered by the pause screen.
            if (coveredByOtherScreen)
                pauseAlpha = Math.Min(pauseAlpha + 1f / 32, 1);
            else
                pauseAlpha = Math.Max(pauseAlpha - 1f / 32, 0);

            if (IsActive)
            {
                // Apply some random jitter to make the enemy move around.
                const float randomization = 10;

                enemyPosition.X += (float)(random.NextDouble() - 0.5) * randomization;
                enemyPosition.Y += (float)(random.NextDouble() - 0.5) * randomization;

                // Apply a stabilizing force to stop the enemy moving off the screen.
                Vector2 targetPosition = new Vector2(
                    ScreenManager.GraphicsDevice.Viewport.Width / 2 - gameFont.MeasureString("Insert Gameplay Here").X / 2, 
                    200);

                enemyPosition = Vector2.Lerp(enemyPosition, targetPosition, 0.05f);

                // TODO: this game isn't very fun! You could probably improve
                // it by inserting something more interesting in this space :-)
            }
        }


        /// <summary>
        /// Lets the game respond to player input. Unlike the Update method,
        /// this will only be called when the gameplay screen is active.
        /// </summary>
        public override void HandleInput(InputState input)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            // Look up inputs for the active player profile.
            int playerIndex = (int)ControllingPlayer.Value;

            KeyboardState keyboardState = input.CurrentKeyboardStates[playerIndex];
            GamePadState gamePadState = input.CurrentGamePadStates[playerIndex];

            // The game pauses either if the user presses the pause button, or if
            // they unplug the active gamepad. This requires us to keep track of
            // whether a gamepad was ever plugged in, because we don't want to pause
            // on PC if they are playing with a keyboard and have no gamepad at all!
            bool gamePadDisconnected = !gamePadState.IsConnected &&
                                       input.GamePadWasConnected[playerIndex];

            if (input.IsPauseGame(ControllingPlayer) || gamePadDisconnected)
            {
                ScreenManager.AddScreen(new PauseMenuScreen(), ControllingPlayer);
            }
            else
            {
                // Otherwise move the player position.
                Vector2 movement = Vector2.Zero;
/*
                if (keyboardState.IsKeyDown(Keys.Left))
                    test.X--;

                if (keyboardState.IsKeyDown(Keys.Right))
                    test.X++;

                if (keyboardState.IsKeyDown(Keys.Up))
                    test.Y--;

                if (keyboardState.IsKeyDown(Keys.Down))
                    test.Y++;
                */
                

                Vector2 thumbstick = gamePadState.ThumbSticks.Left;

                movement.X += thumbstick.X;
                movement.Y -= thumbstick.Y;

                if (movement.Length() > 1)
                    movement.Normalize();

                playerPosition += movement * 2;
            }
        }


        /// <summary>
        /// Draws the gameplay screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            // This game has a blue background. Why? Because!
            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target,
                                               Color.CornflowerBlue, 0, 0);

            // Our player and enemy are both actually just text strings.
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin(SpriteSortMode.BackToFront,
                        BlendState.AlphaBlend,
                        null,
                        null,
                        null,
                        null,
                        cam.get_transformation(ScreenManager.GraphicsDevice /*Send the variable that has your graphic device here*/));





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

            spriteBatch.Draw(lifeBar, new Rectangle((int)cam.Pos.X+(int)test.X, (int)cam.Pos.Y+(int)test.Y, lifeBar.Width,(life*16)), Color.White);
            Console.WriteLine(test.X + " + " + test.Y);
            player.Draw(spriteBatch, rotation);
            //  Console.WriteLine(cam.Pos.X + " " + cam.Pos.Y);
            Console.WriteLine(playerVelocity.X);

            

            spriteBatch.End();

         //   DrawIt(gameTime);

            // If the game is transitioning on or off, fade it out to black.
            if (TransitionPosition > 0 || pauseAlpha > 0)
            {
                float alpha = MathHelper.Lerp(1f - TransitionAlpha, 1f, pauseAlpha / 2);

                ScreenManager.FadeBackBufferToBlack(alpha);
            }
        }





        public void Update(GameTime gameTime)
        {

            gamepad = GamePad.GetState(PlayerIndex.One);
            KeyboardState keyboardState = Keyboard.GetState();
            // Allows the game to exit
    /*        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || keyboardState.IsKeyDown(Keys.Escape))
                this.Exit();
            */
            playerVelocity.X = MathHelper.Clamp(playerVelocity.X, -400f, 400f);


     //       IsMouseVisible = true;
            MouseState mouse = Mouse.GetState();
            //  Console.WriteLine(mouse.X + " " + mouse.Y);


            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;



            if (life < 1)
            {
                LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(),
                                                           new MainMenuScreen());
            }




            if (keyboardState.IsKeyDown(Keys.Q) && !prevKey.IsKeyDown(Keys.Q))
            {

                Ninja ninja = new Ninja(ninjaTexture, player.Position, playerVelocity);
                ninjaList.Add(ninja);

            }

            if (keyboardState.IsKeyDown(Keys.E) && !prevKey.IsKeyDown(Keys.E))
            {
                life--;

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
                
                    if (player.hitbox.Intersects(ninjaList[x].hitbox) && ninjaList[x].standning)
                    {
                         ninjaList.Remove(ninjaList[x]);
                    }

                

                    else if (player.hitbox.Intersects(ninjaList[x].hitbox) && !ninjaList[x].standning)
                {
                    ninjaList[x].respawn(player.Position, playerVelocity);
                    ninjaList[x].standning = true;
                    
                    life--;
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
                                    playerVelocity.X = MathHelper.Clamp(playerVelocity.X, 50f, 400f);

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
                                    playerVelocity.X = MathHelper.Clamp(playerVelocity.X, -400f, -50f);

                                }
                            }
                        }

                        //On the tile

                        if (((playerVelocity.Y >= 0) && ((player.Position.Y + player.Height) > tile.Position.Y)) && !(tile.BoundingBox.Top == player.hitbox.Bottom))
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

            /*if (keyboardState.IsKeyDown(Keys.Left))
            {
                playerVelocity -= new Vector2(7f, 0);
                player.Facing = "left";

                animation.draw = true;
                running = true;
            }*/

            if (keyboardState.IsKeyDown(Keys.Right))
            {
                playerVelocity += new Vector2(7f, 0);
                player.Facing = "right";

                animation.draw = true;
                running = true;
            }


            playerVelocity += new Vector2(gamepad.ThumbSticks.Left.X * 7, 0);

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




           if ((player.Position.X - cam._pos.X) > 1280 * 0.18)
                cam._pos += new Vector2(Math.Abs(playerVelocity.X) / 80, 0f);


            if ((player.Position.X - cam._pos.X) < -1280 * 0.18)
                cam._pos -= new Vector2(Math.Abs(playerVelocity.X) / 80, 0f);


            cam._pos.X = MathHelper.Lerp(cam._pos.X, player.Position.X, 0.1f);


            cam._pos.X = MathHelper.Clamp(cam._pos.X, 682f, 10000);


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



            
        }

     /*   public void DrawIt(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.BackToFront,
                        BlendState.AlphaBlend,
                        null,
                        null,
                        null,
                        null,
                        cam.get_transformation(ScreenManager.GraphicsDevice /*Send the variable that has your graphic device here));*/

        /*



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

            
        }*/




        #endregion
    }
}
