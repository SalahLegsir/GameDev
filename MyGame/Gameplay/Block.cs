using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyGame.Gameplay
{
    internal class Block
    {
        public Rectangle block;
        private Vector2 posistion;
        private int speed = 1;
        private Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch { get; set; }

        public Hitbox hitBox { get; set; }


        public Block(int posX, int posY, int width, int height, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            block = new Rectangle(0, 0, width, height);
            posistion = new Vector2(posX, posY);
            hitBox = new Hitbox((int)posistion.X, (int)posistion.Y, width, 2);
            this.spriteBatch = spriteBatch;
        }

        public void Draw(Texture2D blockTexture)
        {

            spriteBatch.Draw(blockTexture, posistion, block, Color.White, 0, new Vector2(0, 0), new Vector2(1, 1), SpriteEffects.None, 0);

        }

        public void Move()
        {

            posistion.X += speed;

            hitBox.Update((int)posistion.X, (int)posistion.Y);
            if (posistion.X <= 200)
            {
                speed = 1;
            }

            if (posistion.X >= 750)
            {
                speed = -1;
            }

        }

        public bool Collision(Rectangle hitbox)
        {
            return hitBox.TrueHitbox.Intersects(hitbox);
        }


    }
}
