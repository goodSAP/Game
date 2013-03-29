using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tile_r;

namespace tile_r
{
    class Ninja
    {
        public Texture2D texture;
        public Vector2 position;
        public Rectangle hitbox;
        public float timer = 0.0f;
        public const float timeLimit = 1.5f;
        public bool standning;
        Vector2 ninjaVelocity;
        float elapsed;
        Vector2 targetPos;
        Vector2 targetVelocity;
        public bool hitPlayer;
        



        public Ninja(Texture2D texture, Vector2 targetPosition, Vector2 targetVelocity)
        {
            this.texture = texture;
            this.position.X = targetPosition.X + (targetVelocity.X/2.5f);
            this.position.Y = 0 - texture.Height;
            this.ninjaVelocity.Y = (position.Y - targetPosition.Y);
            this.targetPos = targetPosition;
            this.targetVelocity = targetVelocity;
        }

        public void Update(GameTime gametime)
        {
            
            elapsed = (float)gametime.ElapsedGameTime.TotalSeconds;
            

            hitbox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            if (standning)
            {
                timer += elapsed;
                ninjaVelocity = Vector2.Zero;
            }
            else
            {
            
            }

            if ((timer >= timeLimit))
            {
             /*   this.position.X = targetPos.X  + targetVelocity.X;
                this.position.Y = 0 - texture.Height;
                this.ninjaVelocity.Y = position.Y - targetPos.Y;
                
                standning = false;*/
            }
            position.Y -= (ninjaVelocity.Y*2.5f) * elapsed;
            

          
                
            

        }

        public void Draw(SpriteBatch spriteBatch)
        {
           /* Vector2 scale = new Vector2(50 / (float)this.texture.Width, 80 / (float)this.texture.Height);
            spriteBatch.Draw(texture, position, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 1f); 
        */
            spriteBatch.Draw(texture, position, Color.White);
             }

        public void respawn(Vector2 newTargetPos, Vector2 newTargetVelocity) 
        {
            this.position.X = newTargetPos.X + newTargetVelocity.X;
            this.position.Y = 0 - texture.Height*2;
            this.ninjaVelocity.Y = position.Y - newTargetPos.Y;

            standning = false;
        }
        

    }
}

