using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Gameplay
{
    internal class Collectable
    {
        public Hitbox hitBox { get; set; }


        private Texture2D _texture;
        public Vector2 _startPosition;
        private SpriteBatch _spritebatch;
        private Random rdn = new Random();

        public bool collected { get; set; }



        public Collectable(Texture2D texture, SpriteBatch spritebatch)
        {
            _texture = texture;
            _startPosition = new Vector2(rdn.Next(60, 900), rdn.Next(180, 581));
            _spritebatch = spritebatch;

            hitBox = new Hitbox((int)_startPosition.X + 2, (int)_startPosition.Y + 5, 35, 34);

            collected = false;
        }

        public void Draw(Rectangle _currentFrame)
        {
            _spritebatch.Draw(_texture, _startPosition, _currentFrame, Color.White, 0, new Vector2(0, 0), 0.2f, SpriteEffects.None, 0);
        }

        public void Reset()
        {
            collected = false;
            _startPosition = new Vector2(rdn.Next(60, 900), rdn.Next(180, 581));
            hitBox = new Hitbox((int)_startPosition.X + 2, (int)_startPosition.Y + 5, 35, 34);

        }
    }
}
