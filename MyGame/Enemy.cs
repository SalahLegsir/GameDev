﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    internal class Enemy
    {
        public SpriteBatch _spriteBatch { get; set; }
        public Hitbox ArrowBox { get; set; }
        public int speed { get; set; }
        

        private Random rdn = new Random();
        public Vector2 position;
        public Enemy(SpriteBatch spriteBatch)
        {
            
            _spriteBatch = spriteBatch;
            position = new Vector2(rdn.Next(60, 900), rdn.Next(-60, -40));
            //position = new Vector2(rdn.Next(60, 900), 20);
            ArrowBox = new Hitbox((int)position.X,(int)position.Y, 40, 30);
        }
        public void Fall()
        {
            if(position.Y < 800)
            {
                position.Y++;

                ArrowBox.Update((int)position.X, (int)position.Y);
            }
            
        }
        public void Draw(Texture2D arrow)
        {
            //_spriteBatch.Draw(arrow, position, Color.White);
            _spriteBatch.Draw(arrow, position, new Rectangle(0,0,40,30), Color.White, (float)Math.PI / 2.0f, new Vector2(0, 0), new Vector2(1, 1), SpriteEffects.None, 0);

        }
    }
}
