using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    internal class Hitbox
    {
        public Rectangle TrueHitbox { get; set; }

        public Hitbox(Vector2 _startPosition)
        {
            TrueHitbox = new Rectangle((int)_startPosition.X + 19 * 2, (int)_startPosition.Y + 22 * 2, 25 * 2, 33 * 2);
        }

        public void Update(Vector2 start)
        {
            TrueHitbox = new Rectangle((int)start.X + 19 * 2, (int)start.Y + 22 * 2, 25 * 2, 33 * 2);
        }

       
    }
}
