using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace MyGame.Gameover
{
    internal class Gameover
    {
        private SpriteBatch _spriteBatch;
        public Button.Button RestartButton { get; set; }

        public Gameover(SpriteBatch spriteBatch, Texture2D restartTexture)
        {
            _spriteBatch = spriteBatch;
            RestartButton = new Button.Button(restartTexture, new Vector2(425, 500), new Gameplay.Hitbox(425, 500, restartTexture.Width, restartTexture.Height));


        }

        public void Draw(Texture2D gameOver, SpriteFont spriteFont)
        {
            _spriteBatch.Draw(gameOver, new Rectangle(250, 300, gameOver.Width, gameOver.Height), Color.White);
            RestartButton.Draw(_spriteBatch);
        }
    }
}
