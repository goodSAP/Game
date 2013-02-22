using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


namespace TestGame.TileEngine
{
    class Tile
    {
        public bool Landable { get; set; }
        public int SpriteWidth { get; set; }
        public int SpriteHeight { get; set; }
        public Vector2 Position { get; set; }
        public Texture2D Texture { get; set; }

        public Rectangle BoundingBox
        {
            get { return new Rectangle((int)this.Position.X, (int)this.Position.Y, this.SpriteWidth, this.SpriteHeight); }
        }

        /*
        public Tile()
        {
            this.SpriteHeight = 32;
            this.SpriteWidth = 32;
        }
         */

        public Tile(Texture2D texture, int spriteWidth, int spriteHeight, Vector2 startingPosition, bool isLandable)
        {
            this.Texture = texture;
            this.SpriteWidth = spriteWidth;
            this.SpriteHeight = spriteHeight;
            this.Position = startingPosition;
            this.Landable = isLandable;
        }

        public void Draw(SpriteBatch sb)
        {
            Vector2 scale = new Vector2(SpriteWidth / (float)this.Texture.Width, SpriteHeight / (float)this.Texture.Height);
            sb.Draw(this.Texture, this.Position, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 1f);
        }
    }
}
