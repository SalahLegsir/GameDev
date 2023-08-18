using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MyGame
{
    internal class Main
    {
        private SpriteBatch _spriteBatch;
        public Button StartButton { get; set; }

        public Main(SpriteBatch spriteBatch, Texture2D button)
        {
            _spriteBatch = spriteBatch;
            StartButton = new Button(button, new Vector2(200,300), new Hitbox(237, 358, 440, 162));
        }

        public void Draw(Texture2D main, SpriteFont spriteFont)
        {

            _spriteBatch.Draw(main, new Rectangle(0, 0, main.Width, main.Height), Color.White);
            StartButton.Draw(_spriteBatch);
        }
    }
}
