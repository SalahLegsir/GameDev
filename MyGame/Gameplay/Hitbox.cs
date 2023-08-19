
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Gameplay
{
    internal class Hitbox
    {
        public Rectangle TrueHitbox { get; set; }
        private int _startPositionX { get; set; }
        private int _startPositionY { get; set; }
        private int width { get; set; }
        private int height { get; set; }

        public Hitbox(int _startPositionX, int _startPositionY, int w, int h)
        {
            this._startPositionX = _startPositionX;
            this._startPositionY = _startPositionY;
            width = w;
            height = h;
            TrueHitbox = new Rectangle(this._startPositionX, this._startPositionY, width, height);
        }

        public void Update(int _startPositionX, int _startPositionY)
        {
            this._startPositionX = _startPositionX;
            this._startPositionY = _startPositionY;

            TrueHitbox = new Rectangle(this._startPositionX, this._startPositionY, width, height);
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D block)
        {
            spriteBatch.Draw(block, TrueHitbox, Color.Red);
        }


    }
}
