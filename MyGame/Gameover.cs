﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace MyGame
{
    internal class Gameover
    {
        private SpriteBatch _spriteBatch;

        public Gameover(SpriteBatch spriteBatch)
        {
            _spriteBatch = spriteBatch;
        }

        public void Draw(Texture2D gameOver, SpriteFont spriteFont)
        {
            
            _spriteBatch.Draw(gameOver, new Rectangle(250, 300, gameOver.Width, gameOver.Height), Color.White);
            _spriteBatch.DrawString(spriteFont, "Press 'R' to restart level", new Vector2(350,500), Color.White);


        }
    }
}
