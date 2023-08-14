using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Vector2 = Microsoft.Xna.Framework.Vector2;
namespace MyGame
{
    internal class Character
    {
        private bool falling = false;
        private Texture2D _texture;
        public Vector2 _startPosition;
        private SpriteBatch _spritebatch;
        private bool isMoving;
        private SpriteEffects effect;

        public Hitbox hitbox { get; set; }
        public int x { get; set; }

        public bool jumping;
        float jumpspeed;
        public List<Block> Blocks { get; set; }
        public Rectangle frame { get; set; }
        private int gravity = 1;
        public bool lost = false;
        public int health = 3;




        public Character(Texture2D texture, Vector2 startPosition, SpriteBatch spritebatch)
        {
            _texture = texture;
            _startPosition = startPosition;
            _spritebatch = spritebatch;
            x = 0;
            jumping = false;
            jumpspeed = 0;
            hitbox = new Hitbox((int)_startPosition.X + 19 * 2, (int)_startPosition.Y + 22 * 2, 25 * 2, 33 * 2);

            _startPosition.X = 240;
           

        }

        public void Draw(Rectangle _currentFrame)
        {
            _spritebatch.Draw(_texture, _startPosition, _currentFrame, Color.White, 0, new Vector2(0, 0), new Vector2(2, 2), effect, 0);
        }

        public bool Move(Rectangle _currentFrame, int speed, Rectangle surface)
        {


            frame = _currentFrame;
            isMoving = false;
            if (Keyboard.GetState().IsKeyDown(Keys.Z))
            {
                if (_startPosition.Y <= 0)
                {
                    _startPosition.Y += 0;

                   


                }
                else
                {
                    _startPosition.Y -= speed;
                    isMoving = true;
                }

            }

            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                if (_startPosition.X <= 0)
                {
                    _startPosition.X += 0;

                }
                else
                {
                    _startPosition.X -= speed;
                    isMoving = true;

                    if (effect == SpriteEffects.None)
                    {
                        effect = SpriteEffects.FlipHorizontally;
                    }
                }



            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                if (_startPosition.X + hitbox.TrueHitbox.Width >= 895)
                {
                    _startPosition.X += 0;
                   

                }
                else
                {
                    _startPosition.X += speed;
                    x -= speed;
                    isMoving = true;
                    effect = SpriteEffects.None;
                }

            }

            if (Keyboard.GetState().IsKeyDown(Keys.S) || !jumping)
            {
                if (hitbox.TrueHitbox.Intersects(surface))
                {
                    //lost = true;
                    health--;

                    jumpspeed = -200;

                    if (jumpspeed < 0)
                    {
                        _startPosition.Y += jumpspeed;

                        jumpspeed += 1;

                    }

                }


                if (hitbox.TrueHitbox.Intersects(surface) || Blocks[0].Collision(hitbox.TrueHitbox) || Blocks[1].Collision(hitbox.TrueHitbox) || Blocks[2].Collision(hitbox.TrueHitbox) || Blocks[3].Collision(hitbox.TrueHitbox))
                {
                    _startPosition.Y += 0;
                    gravity = 1;
                    falling = false;
                }
                else
                {

                    //lost = false;
                    _startPosition.Y += gravity;

                    if (gravity < 5)
                    {
                        gravity++;
                    }

                    isMoving = true;
                    falling = true;
                }
            }

            if(health <= 0)
            {
                lost = true;
            }else
            {
                lost = false;
            }

            hitbox.Update((int)_startPosition.X + 19 * 2, (int)_startPosition.Y + 22 * 2);

            Jump();
            return isMoving;


        }


        public bool Attack()
        {
            bool isAttack = false;

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                isAttack = true;
            }

            return isAttack;
        }

        public bool Jump()
        {
            //Bron :http://flatformer.blogspot.com/2010/02/making-character-jump-in-xnac-basic.html

            KeyboardState keyState = Keyboard.GetState();

            
            if (jumping)
            {
                if (jumpspeed < 0)
                {
                    _startPosition.Y += jumpspeed;

                    jumpspeed += 1;

                }
                else
                {
                    jumping = false;
                    
                }
            }
            else
            {
                if (keyState.IsKeyDown(Keys.Space) && !falling)
                {
                    jumping = true;
                    
                    jumpspeed = -16;
                }
            }

            return jumping;
        }

        public void Restart()
        {
            health = 3;
            x = 0;
            jumping = false;
            jumpspeed = 0;
            hitbox = new Hitbox((int)_startPosition.X + 19 * 2, (int)_startPosition.Y + 22 * 2, 25 * 2, 33 * 2);

            _startPosition.X = 240;
            _startPosition.Y = 0;
        }
    }
}
