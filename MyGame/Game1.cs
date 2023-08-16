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
        private Dictionary<string, List<Rectangle>> movements;
        private SpriteFont spriteFont;
        private Texture2D blockTexture;
        private Rectangle surface;
        private Texture2D background;
        private Texture2D gameOver;
        private Texture2D wonPic;
        private Rectangle block2 = new Rectangle(600, 500, 100, 30);
        private List<Block> blocks;
        private Texture2D coin;
        private Gameover gameOverScreen;
        private Won wonScreen;
        private Main mainScreen;
        private bool enterPressed = false;
        private List<Rectangle> coinFrames = new List<Rectangle>();
        private Sprites coins = new Sprites(200, 200);
        private FPS coinFPS = new FPS();
        private Rectangle coinFrame;
        private Hitbox coinBox = new Hitbox(15,28,175,170);
        private Texture2D heart;
        private bool won = false;
        private Enemy arrowEnemy;
        private Texture2D arrow;
        private int currentLevel;
        private List<Enemy> arrows;


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

            currentLevel = 1;

            background = Content.Load<Texture2D>("Background");
            gameOver = Content.Load<Texture2D>("GameOver");
            texture = Content.Load<Texture2D>("Fighter");
            spriteFont = Content.Load<SpriteFont>("start");
            coin = Content.Load<Texture2D>("coin");
            heart = Content.Load<Texture2D>("heart48");
            wonPic = Content.Load<Texture2D>("won");
            arrow = Content.Load<Texture2D>("arrow");


            vector = new Vector2(120, 450);
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
            blockTexture.SetData(new[] { Color.Gray });
            surface = new Rectangle(0, 720, 1000, 20);


            gameOverScreen = new Gameover(_spriteBatch);
            mainScreen = new Main(_spriteBatch);
            wonScreen = new Won(_spriteBatch);
            arrowEnemy = new Enemy(_spriteBatch);

            blocks = new List<Block>() {
            new Block(100, 600, 100, 30,_spriteBatch),
            new Block(200, 220, 100, 30,_spriteBatch),
            new Block(400, 580, 100, 30,_spriteBatch),
            new Block(500, 200, 100, 30,_spriteBatch),
            new Block(250, 450, 100, 30,_spriteBatch),
            new Block(400, 350, 100, 30,_spriteBatch),
            };

            arrows = new List<Enemy>()
            {
                new Enemy(_spriteBatch),
                new Enemy(_spriteBatch),
                new Enemy(_spriteBatch),
                new Enemy(_spriteBatch),
                new Enemy(_spriteBatch),
                new Enemy(_spriteBatch),
                new Enemy(_spriteBatch),
            };

            person = new Character(texture, vector, _spriteBatch);

            person.Blocks = blocks;
            person.arrows = arrows;

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
                for (int i = 0; i < blocks.Count; i++)
                {
                    if(i != 0)
                    {
                        blocks[i].Move();
                    }
                    
                }
                coinFrame = coinFPS.Fps(gameTime, coinFrames);
                
                if (!person.Attack()) //Kan niet tegelijkertijd aanvallen en lopen
                {
                    if (person.Move(currentFrame, 4, surface))
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

                //_spriteBatch.Draw(blockTexture, person.hitbox.TrueHitbox, Color.Orange);

                //_spriteBatch.Draw(blockTexture, surface, Color.Black);

                _spriteBatch.DrawString(spriteFont, $"Level {currentLevel}", new Vector2(830, 50), Color.Black);


                foreach (var block in blocks)
                {
                    block.Draw(blockTexture);
                }
                person.feetBox.Draw(_spriteBatch, blockTexture);
                person.Draw(currentFrame);



                if (person.hitbox.TrueHitbox.Intersects(coinBox.TrueHitbox))
                {
                    won = true;
                } 
                
                DrawHealth();

                if (currentLevel == 2)
                {
                    for (int i = 0; i < arrows.Count; i++)
                    {
                        arrows[i].ArrowBox.Draw(_spriteBatch, blockTexture);
                        arrows[i].Draw(arrow);
                        arrows[i].Fall();
                    }

                }

                if (!won)
                {
                    //_spriteBatch.Draw(blockTexture, coinBox.TrueHitbox, Color.Orange);
                    _spriteBatch.Draw(coin, new Vector2(0, 0), coinFrame, Color.White, 0, new Vector2(0, 0), new Vector2(1, 1), 0, 0);


                }
                else
                {
                    wonScreen.Draw(spriteFont, wonPic, currentLevel);

                    

                    

                    if (Keyboard.GetState().IsKeyDown(Keys.R))
                    {
                        person.Restart();
                        won = false;
                    }

                    if(currentLevel == 1)
                    {
                        if (Keyboard.GetState().IsKeyDown(Keys.N))
                        {
                            currentLevel = 2;
                            person.Restart();
                            won = false;
                        }
                    }
                   
                }



               
                if (person.lost)
                {
                    _spriteBatch.Draw(blockTexture, new Rectangle(0, 0, 1000, 1000), Color.Black);
                    gameOverScreen.Draw(gameOver, spriteFont);

                    if (Keyboard.GetState().IsKeyDown(Keys.R))
                    {
                        person.Restart();
                    }
                }

                
                
            }else
            {
                mainScreen.Draw(background, spriteFont);

               
            }

            _spriteBatch.End();

            
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }



        public void DrawHealth()
        {
            int x = 770; 
            for(int i = 0; i < person.health; i++)
            {
                _spriteBatch.Draw(heart, new Rectangle(x, 0, heart.Width, heart.Height), Color.White);
                x += heart.Width;
            }
        }
    }
}