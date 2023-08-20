using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using MyGame.Button;

namespace MyGame.Start
{
    internal class Main
    {
        private SpriteBatch _spriteBatch;
        public Button.Button StartButton { get; set; }

        public Main(SpriteBatch spriteBatch, Texture2D button)
        {
            _spriteBatch = spriteBatch;
            StartButton = new Button.Button(button, new Vector2(200, 300), new Gameplay.Hitbox(237, 358, 440, 162));
        }

        public void Draw(Texture2D main)
        {

            _spriteBatch.Draw(main, new Rectangle(0, 0, main.Width, main.Height), Color.White);
            StartButton.Draw(_spriteBatch);
        }
    }
}
