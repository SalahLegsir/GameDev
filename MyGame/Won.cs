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
        public Button RestartButton {get; set;}
        public Button NextButton { get; set; }
        public Button HomeButton { get; set; }


        public Won(SpriteBatch spriteBatch, Texture2D restartTexture, Texture2D homeTexture, Texture2D nextTexture)
        {
            _spriteBatch = spriteBatch;
            RestartButton = new Button(restartTexture, new Vector2(350, 550), new Hitbox(350, 550, restartTexture.Width, restartTexture.Height));
            NextButton = new Button(nextTexture, new Vector2(427, 550), new Hitbox(427, 550, nextTexture.Width, nextTexture.Height));
            HomeButton = new Button(homeTexture, new Vector2(427, 550), new Hitbox(427, 550, homeTexture.Width, homeTexture.Height));

        }

        public void Draw(SpriteFont spriteFont, Texture2D won, int level)
        {
           

            _spriteBatch.Draw(won, new Rectangle(-60, -50, 1080, 1000),Color.White);
            RestartButton.Draw(_spriteBatch);
            if(level == 1)
            {
                NextButton.Draw(_spriteBatch);
            }else
            {
                HomeButton.Draw(_spriteBatch);
            }


        }
    }
}
