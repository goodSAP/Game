using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Shooter;
using tile_r;

namespace tile_r
{
    class Speeder
    {

        public Texture2D texture;
        public Vector2 position;
        public Rectangle hitbox;
        public float timer = 0.0f;
        public const float timeLimit = 1.0f;
        Vector2 Velocity;
        float elapsed;
        public int health = 5;
        public int damage = 5;
        public Player player;

        //Constructor
        public Speeder(Texture2D texture, Vector2 position, Player player)
        {
            this.texture = texture;
            this.position = position;
            this.player = player;

        }

        public void Update(GameTime gametime)
        {
            elapsed = (float)gametime.ElapsedGameTime.TotalSeconds;
            hitbox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            position += Velocity;


        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

    }
}
