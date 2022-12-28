using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Vector2 = Microsoft.Xna.Framework.Vector2;
namespace MyGame
{
    internal class Character
    {
        private Texture2D _texture;
        public Vector2 _startPosition;
        private SpriteBatch _spritebatch;
        private bool isMoving;
        private SpriteEffects effect;

        public Hitbox hitbox { get; set; }
        public int x { get; set; }

        float startY;
        public bool jumping;
        float jumpspeed;
        public List<Block> Blocks { get; set; }
        public Rectangle frame { get; set; }




        public Character(Texture2D texture, Vector2 startPosition, SpriteBatch spritebatch, List<Block> blocks)
        {
            _texture = texture;
            _startPosition = startPosition;
            _spritebatch = spritebatch;
            x = 0;
             startY = _startPosition.Y;//Starting position
             jumping = false;//Init jumping to false
             jumpspeed = 0;//Default no speed
            hitbox = new Hitbox(_startPosition);
            Blocks = blocks;


        }

        public void Draw(Rectangle _currentFrame)
        {
            _spritebatch.Draw(_texture, _startPosition, _currentFrame, Color.White, 0, new Vector2(0, 0), new Vector2(2, 2), effect, 0);

            
        }

        public bool Move(Rectangle _currentFrame , int speed, Rectangle surface, Rectangle block, Vector2 blockPos)
        {


            frame = _currentFrame;
            isMoving = false;
            if (Keyboard.GetState().IsKeyDown(Keys.Z) )
            {
                if (_startPosition.Y <= 0 || hitbox.TrueHitbox.Intersects(block))
                {
                    _startPosition.Y += 0;
                    blockPos.Y++;
                   
                    
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
                if (_startPosition.X +  _currentFrame.Width >= 736 || hitbox.TrueHitbox.Intersects(block))
                {
                    _startPosition.X += 0;
                    blockPos.X += 50;

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
               
                    if (hitbox.TrueHitbox.Intersects(surface) || hitbox.TrueHitbox.Intersects(/*block*/))
                    {
                        _startPosition.Y += 0;

                    }
                    else
                    {
                        _startPosition.Y += speed;
                        isMoving = true;
                    }
                
                

            }

            hitbox.Update(_startPosition);

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
            //KeyboardState keyState = Keyboard.GetState();
            //if (jumping)  //http://flatformer.blogspot.com/2010/02/making-character-jump-in-xnac-basic.html
            //{
            //    _startPosition.Y += jumpspeed;//Making it go up




            //    jumpspeed += 1;




            //    if (hitbox.TrueHitbox.Intersects(Block))
            //    //If it's farther than ground
            //    {

            //        _startPosition.Y = Block.Y - frame.Height - 45;//Then set it on
            //        jumping = false;
            //    }
            //}
            //else
            //{
            //    if (keyState.IsKeyDown(Keys.Space))
            //    {
            //        jumping = true;
            //        jumpspeed = -14;//Give it upward thrust
            //        startY = _startPosition.Y;
            //    }
            //}

            //return jumping;


            KeyboardState keyState = Keyboard.GetState();
            bool falling = false;
            if (jumping)  //http://flatformer.blogspot.com/2010/02/making-character-jump-in-xnac-basic.html
            {
                if(jumpspeed < 0 && !falling)
                {
                    _startPosition.Y += jumpspeed;//Making it go up

                    jumpspeed += 1;

                }else
                {
                    //if (hitbox.TrueHitbox.Intersects(Block))
                    //{
                    //    _startPosition.Y += 0;
                    //    jumping = false;

                    //}
                    //else
                    //{
                    //    _startPosition.Y += 7;
                    //}

                    jumping = false;
                    falling = true;

                   
                }    
            }
            else
            {
                if (keyState.IsKeyDown(Keys.Space))
                {
                    jumping = true;
                    falling = false;
                    jumpspeed = -14;//Give it upward thrust
                }
            }

            return jumping;
        }
    }
}
