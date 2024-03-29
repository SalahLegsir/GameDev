﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyGame.Gameplay;
using MyGame.Start;
using SharpDX.Direct2D1.Effects;
using SharpDX.Direct3D9;
using SharpDX.XAudio2;
using System.Collections.Generic;
using System.Diagnostics;
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
        private Gameover.Gameover gameOverScreen;
        private Won.Won wonScreen;
        private Main mainScreen;

        private List<Rectangle> coinFrames = new List<Rectangle>();
        private Sprites coins = new Sprites(200, 200);
        private FPS coinFPS = new FPS();
        private Rectangle coinFrame;
        private Hitbox coinBox = new Hitbox(15, 28, 175, 170);
        private Texture2D heart;
        private bool won = false;
        private Enemy arrowEnemy;
        private Texture2D arrow;
        private int currentLevel;
        private List<Enemy> arrows;
        private List<Collectable> collectCoins;
        private Texture2D dataBlock;
        private Texture2D startButton;
        private Texture2D restartButton;
        private Texture2D homeButton;
        private Texture2D nextButton;
        private Texture2D block;
        private bool paused;
        private Texture2D pauseButton;

        private KeyboardState lastState;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            // Set Resolution https://www.industrian.net/tutorials/changing-display-resolution/#:~:text=MonoGame's%20default%20resolution%20is%20800x480,resolution%20of%20a%20MonoGame%20project.

            Content.RootDirectory = "Content";
            IsMouseVisible = true;

        }

        protected override void Initialize()
        {

            currentLevel = 1;

            background = Content.Load<Texture2D>("Screens/Start/Background");
            gameOver = Content.Load<Texture2D>("Screens/Gameover/GameOver");
            texture = Content.Load<Texture2D>("Screens/Game/Fighter");
            spriteFont = Content.Load<SpriteFont>("Fonts/start");
            coin = Content.Load<Texture2D>("Screens/Game/coin");
            heart = Content.Load<Texture2D>("Screens/Game/heart48");
            wonPic = Content.Load<Texture2D>("Screens/Won/win");
            arrow = Content.Load<Texture2D>("Screens/Game/arrow");
            startButton = Content.Load<Texture2D>("Buttons/StartButton");
            restartButton = Content.Load<Texture2D>("Buttons/RestartButton");
            nextButton = Content.Load<Texture2D>("Buttons/NextButton");
            homeButton = Content.Load<Texture2D>("Buttons/HomeButton");
            block = Content.Load<Texture2D>("Screens/Game/Block");
            pauseButton = Content.Load<Texture2D>("Buttons/PauseButton");



            vector = new Vector2(120, 450);
            fpsIdle = new FPS();
            fpsWalk = new FPS();
            fpsPowerShot = new FPS();
            sprite = new Sprites(64, 64);
            movements = sprite.MakeDictionary();
            paused = false;


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

            dataBlock = new Texture2D(GraphicsDevice, 1, 1);
            dataBlock.SetData(new[] { Color.BurlyWood });

            surface = new Rectangle(0, 720, 1000, 20);


            gameOverScreen = new Gameover.Gameover(_spriteBatch, restartButton);
            mainScreen = new Main(_spriteBatch, startButton);
            wonScreen = new Won.Won(_spriteBatch, restartButton, homeButton, nextButton);
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

            collectCoins = new List<Collectable>()
            {
                new Collectable(coin,_spriteBatch),
                new Collectable(coin,_spriteBatch),
                new Collectable(coin,_spriteBatch),
                new Collectable(coin,_spriteBatch),
                new Collectable(coin,_spriteBatch),
                new Collectable(coin,_spriteBatch),
                new Collectable(coin,_spriteBatch),
                new Collectable(coin,_spriteBatch),
            };

            person = new Character(texture, vector, _spriteBatch);

            person.Blocks = blocks;
            person.arrows = arrows;


        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Escape) && lastState.IsKeyUp(Keys.Escape))
            {
                paused = !paused;
            }


            lastState = state;





            if (mainScreen.StartButton.pressed)
            {
                if (!paused)
                {
                    for (int i = 0; i < blocks.Count; i++)
                    {
                        if (i != 0)
                        {
                            blocks[i].Move();
                        }

                    }
                    coinFrame = coinFPS.Fps(gameTime, coinFrames);

                    if (!person.Attack()) //Kan niet tegelijkertijd aanvallen en lopen
                    {
                        if (!won && !person.lost)
                        {
                            if (person.Move(currentFrame, 4, surface, currentLevel))
                            {
                                currentFrame = fpsWalk.Fps(gameTime, movements["walk"]);
                            }
                            else
                            {
                                currentFrame = fpsIdle.Fps(gameTime, movements["idle"]);
                            }
                        }

                    }
                    else
                    {
                        currentFrame = fpsPowerShot.Fps(gameTime, movements["powerShot"]);
                    }

                    if (won)
                    {
                        wonScreen.RestartButton.Update();

                        if (currentLevel == 1)
                        {
                            wonScreen.NextButton.Update();
                        }
                        else
                        {
                            wonScreen.HomeButton.Update();
                        }
                    }


                    if (person.lost)
                    {
                        gameOverScreen.RestartButton.Update();
                    }

                }
            }
            else
            {
                mainScreen.StartButton.Update();
            }



            base.Update(gameTime);




        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            if (mainScreen.StartButton.pressed)
            {
                _spriteBatch.Draw(background, new Rectangle(0, 0, background.Width, background.Height), Color.White);



                _spriteBatch.Draw(dataBlock, new Rectangle(740, 10, 180, 90), Color.White);
                _spriteBatch.DrawString(spriteFont, $"Level {currentLevel}", new Vector2(810, 70), Color.Black);
                _spriteBatch.DrawString(spriteFont, $"X{person.coins}", new Vector2(770, 70), Color.Black);

                _spriteBatch.Draw(coin, new Vector2(740, 65), coinFrame, Color.White, 0, new Vector2(0, 0), 0.15f, SpriteEffects.None, 0);



                foreach (var block in blocks)
                {
                    block.Draw(this.block);
                }

                foreach (var coin in collectCoins)
                {

                    if (person.hitbox.TrueHitbox.Intersects(coin.hitBox.TrueHitbox) && !coin.collected)
                    {
                        coin.collected = true;
                        person.coins++;
                    }

                    if (!coin.collected)
                    {

                        coin.Draw(coinFrame);
                    }
                }




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

                        arrows[i].Draw(arrow);
                        if (!paused)
                        {
                            arrows[i].Fall();
                        }

                    }

                }

                if (!won)
                {

                    _spriteBatch.Draw(coin, new Vector2(0, 0), coinFrame, Color.White, 0, new Vector2(0, 0), new Vector2(1, 1), 0, 0);


                }
                else
                {
                    _spriteBatch.Draw(blockTexture, new Rectangle(0, 0, 1000, 1000), Color.Black);
                    wonScreen.Draw(wonPic, currentLevel);





                    if (wonScreen.RestartButton.pressed)
                    {
                        ResetLevel();
                        won = false;
                        wonScreen.RestartButton.pressed = false;

                    }

                    if (currentLevel == 1)
                    {
                        if (wonScreen.NextButton.pressed)
                        {
                            currentLevel = 2;
                            ResetLevel();
                            won = false;
                            wonScreen.NextButton.pressed = false;
                        }
                    }
                    else
                    {
                        if (wonScreen.HomeButton.pressed)
                        {
                            currentLevel = 1;
                            ResetLevel();
                            won = false;
                            mainScreen.StartButton.pressed = false;
                            wonScreen.HomeButton.pressed = false;
                        }
                    }

                }




                if (person.lost)
                {
                    _spriteBatch.Draw(blockTexture, new Rectangle(0, 0, 1000, 1000), Color.Black);
                    gameOverScreen.Draw(gameOver);

                    if (gameOverScreen.RestartButton.pressed)
                    {
                        ResetLevel();
                        gameOverScreen.RestartButton.pressed = false;
                    }
                }

                if (paused)
                {
                    _spriteBatch.Draw(blockTexture, new Rectangle(0, 0, background.Width, background.Height), Color.Black * 0.5f);
                    _spriteBatch.Draw(pauseButton, new Vector2((background.Width / 2) - 138, (background.Height / 2) - 158), Color.White);

                }



            }
            else
            {

                mainScreen.Draw(background);



            }

            _spriteBatch.End();




            base.Draw(gameTime);
        }



        public void DrawHealth()
        {
            int x = 770;
            for (int i = 0; i < person.health; i++)
            {
                _spriteBatch.Draw(heart, new Rectangle(x - 10, 20, heart.Width, heart.Height), Color.White);
                x += heart.Width;
            }
        }

        public void ResetLevel()
        {
            foreach (var coin in collectCoins)
            {
                coin.Reset();
            }
            person.Restart();
        }
    }
}