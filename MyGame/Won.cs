using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    internal class Won
    {
        private SpriteBatch _spriteBatch;

        public Won(SpriteBatch spriteBatch)
        {
            _spriteBatch = spriteBatch;
        }

        public void Draw(SpriteFont spriteFont, Texture2D won)
        {
           

            _spriteBatch.Draw(won, new Rectangle(-60, -50, 1080, 1000),Color.White);
            _spriteBatch.DrawString(spriteFont, "Press 'R' to restart", new Vector2(350, 550), Color.White);


        }
    }
}
