using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyGame
{
    internal class Block
    {
        public Rectangle block { get; set; }
        private Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch { get; set; }
        



        public Block(int posX, int posY, int width, int height, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            block = new Rectangle(posX, posY, width, height);
            this.spriteBatch = spriteBatch;
        }

        public void Draw(Texture2D blockTexture)
        {
            
            spriteBatch.Draw(blockTexture, new Vector2(block.X, block.Y), block, Color.White, 0, new Vector2(0, 0), new Vector2(1, 1), SpriteEffects.None, 0);
        }

        public  bool Collision(Rectangle hitbox)
        {
            return block.Intersects(hitbox);
            
            
        }


    }
}
