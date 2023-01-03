using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1.Effects;
using SharpDX.Direct3D9;
using SharpDX.XAudio2;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.Drawing;
using System.Linq.Expressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace MyGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Character person;
        private Texture2D texture;
        private Vector2 vector;
        private FPS fpsIdle;
        private FPS fpsWalk;
        private FPS fpsPowerShot;
        private Rectangle currentFrame;
        private Sprites sprite;
        Dictionary<string, List<Rectangle>> movements;
        SpriteFont spriteFont;
        Texture2D blockTexture;
        Rectangle hero;
        Rectangle block;
        Rectangle surface;
        Color blockColor = Color.Green;
        Color heroColor = Color.Green;
        Vector2 blockPos = new Vector2(200, 200);
        Texture2D background;
        Texture2D gameOver;
        Rectangle block2 = new Rectangle(600, 500, 100, 30);
        List<Block> blocks;
        Texture2D coin;
        Gameover gameOverScreen;
        Main mainScreen;
        bool enterPressed = false;
        List<Rectangle> coinFrames = new List<Rectangle>();
        Sprites coins = new Sprites(200, 200);
        FPS coinFPS = new FPS();
        Rectangle coinFrame;
       




        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            // Set Resolution https://www.industrian.net/tutorials/changing-display-resolution/#:~:text=MonoGame's%20default%20resolution%20is%20800x480,resolution%20of%20a%20MonoGame%20project.

            

            

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            background = Content.Load<Texture2D>("Background");
            gameOver = Content.Load<Texture2D>("GameOver");
            texture = Content.Load<Texture2D>("Fighter");
            spriteFont = Content.Load<SpriteFont>("start");
            coin = Content.Load<Texture2D>("coin");
            vector = new Vector2(0, 0);
            fpsIdle = new FPS();
            fpsWalk = new FPS();
            fpsPowerShot = new FPS();
            sprite = new Sprites(64, 64);
            movements = sprite.MakeDictionary();


            coinFrames = coins.MakeList(6, 0);

            
        
           




            _graphics.PreferredBackBufferWidth = background.Width;
            _graphics.PreferredBackBufferHeight = background.Height;
            _graphics.ApplyChanges();



            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            blockTexture = new Texture2D(GraphicsDevice, 1, 1);
            blockTexture.SetData(new[] { Color.White });
            block = new Rectangle(200, 200, 70, 70);
            surface = new Rectangle(0, 720, 1000, 20);


            gameOverScreen = new Gameover(_spriteBatch);
            mainScreen = new Main(_spriteBatch);

            blocks = new List<Block>() {
            new Block(100, 500, 100, 30,_spriteBatch),
            new Block(200, 100, 100, 30,_spriteBatch),
            new Block(600, 500, 100, 30,_spriteBatch),
            new Block(600, 100, 100, 30,_spriteBatch)
            

            };

            person = new Character(texture, vector, _spriteBatch);

            person.Blocks = blocks;

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
           
               

            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                enterPressed = true;

            }

            if(enterPressed)
            {
                coinFrame = coinFPS.Fps(gameTime, coinFrames);
                
                if (!person.Attack()) //Kan niet tegelijkertijd aanvallen en lopen
                {
                    if (person.Move(currentFrame, 4, surface, block, blockPos))
                    {
                        currentFrame = fpsWalk.Fps(gameTime, movements["walk"]);



                    }
                    else
                    {

                        currentFrame = fpsIdle.Fps(gameTime, movements["idle"]);
                    }
                }
                else
                {
                    currentFrame = fpsPowerShot.Fps(gameTime, movements["powerShot"]);
                }


                Collision();
            }

            

           
            
                
            
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            if (enterPressed)
            {
                _spriteBatch.Draw(background, new Rectangle(0, 0, background.Width, background.Height), Color.White);
                //_spriteBatch.Draw(background, background1.backgroundPos, background1.backgroundRec, Color.White, 0, new Vector2(0, 0), new Vector2(1, 1), SpriteEffects.None, 0);
                _spriteBatch.Draw(blockTexture, blockPos, block, blockColor, 0, new Vector2(0, 0), new Vector2(1, 1), SpriteEffects.None, 0);
                // _spriteBatch.Draw(blokTexture, new Vector2(person.TrueHitbox.X,person.TrueHitbox.Y), person.TrueHitbox, Color.Orange, 0, new Vector2(0, 0), new Vector2(2, 2), SpriteEffects.None, 0);
                // _spriteBatch.Draw(blokTexture, block, blockColor);
                _spriteBatch.Draw(blockTexture, person.hitbox.TrueHitbox, Color.Orange);

                _spriteBatch.Draw(blockTexture, surface, Color.Black);

                // _spriteBatch.Draw(blokTexture,new Vector2(600,500), block2, Color.White, 0, new Vector2(0, 0), new Vector2(1, 1), SpriteEffects.None, 0);

                foreach (var block in blocks)
                {
                    block.Draw(blockTexture);
                }

                person.Draw(currentFrame);

                _spriteBatch.Draw(coin, new Vector2(0, 0), coinFrame, Color.White, 0, new Vector2(0, 0), new Vector2(1, 1), 0, 0);

                if (person.lost)
                {
                    _spriteBatch.Draw(blockTexture, new Rectangle(0, 0, 1000, 1000), Color.Black);
                    gameOverScreen.Draw(gameOver);
                }
                
            }else
            {
                mainScreen.Draw(background, spriteFont);
            }

            _spriteBatch.End();

            
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }




        public void Collision()
        {
            if(person.hitbox.TrueHitbox.Intersects(block))
            {
                blockColor = Color.Red;
                
            }else
            {
                blockColor = Color.Green;
            }
        }
    }
}