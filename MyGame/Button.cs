using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    internal class Button
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }

        public bool pressed { get; set; } = false;
        private Color color = Color.White;
        public Hitbox buttonBox;

        private MouseState lastState;

        public Button(Texture2D tex, Vector2 pos, Hitbox box)
        {
            Texture = tex;
            Position = pos;

            buttonBox = box;
        }

        public void Update()
        {
            MouseState ms = Mouse.GetState();
            Rectangle cursor = new Rectangle((int)ms.X, (int)ms.Y, 1, 1);

            if(cursor.Intersects(buttonBox.TrueHitbox))
            {
                if(ms.LeftButton == ButtonState.Pressed && lastState.LeftButton == ButtonState.Released)
                {
                    pressed = true;
                }
                color = Color.DarkGray;
            }
            else
            {
                color = Color.White;
            }

            lastState = ms;


        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, color);
        }

    }
}
