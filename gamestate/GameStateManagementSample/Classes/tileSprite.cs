using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace tile_r
{



    class tileSprite
    {
        
        public bool Landable
        {
            get {return landable;}
            set {landable = value;}
        }
        bool landable;

        public int SpriteWidth
        {
            get { return spriteWidth; }
            set { spriteWidth = value; }
        }
        int spriteWidth = 32;

        public int SpriteHeight
        {
            get { return spriteHeight; }
            set { spriteHeight = value; }
        }
        int spriteHeight = 32;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        Vector2 position;

        //texture of the tile
        public Texture2D Texture
        {
            get { return spriteTexture; }
            set { spriteTexture = value; }
        }
        Texture2D spriteTexture;

        //the tiles bounding box
        public Rectangle BoundingBox
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, spriteTexture.Width, spriteTexture.Height); }
        }


        public tileSprite()
        { 
        //empty so you can only initialize if you wanna that
        }

        public tileSprite(Texture2D texture, int spriteWidth, int spriteHeight, Vector2 startingPosition, bool isLandlable)
        {

            this.spriteTexture = texture;
            this.spriteWidth = spriteWidth;
            this.spriteHeight = spriteHeight;
            this.Position = startingPosition;
            this.landable = isLandlable;

        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(this.Texture, this.position, Color.White);
        }

    }
}
