using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    internal class Main
    {
        private SpriteBatch _spriteBatch;

        public Main(SpriteBatch spriteBatch)
        {
            _spriteBatch = spriteBatch;
        }

        public void Draw(Texture2D main, SpriteFont spriteFont)
        {

            _spriteBatch.Draw(main, new Rectangle(0, 0, main.Width, main.Height), Color.White);
            _spriteBatch.DrawString(spriteFont, "Press Enter to start ...", new Vector2(main.Width/2 - 100, main.Height/2), Color.White);
        }
    }
}
