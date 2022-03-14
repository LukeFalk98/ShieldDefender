using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShieldDefender.Screens
{
    public class GameScreen : IScreen
    {
        private int score = 0;

        private SpriteFont gravedigger;
        private IScreen background;

        private Player player;
        private Arrow[] arrows;

        private float arrowSpawnTime;
        private System.Random rand;
        private float difficulty;

        private float bombSpawnTime;
        private SoundEffect explosionSound;
        private Bomb bomb;
        private bool screenshake = false;
        private float shakeSpeed = .1f;
        private float shakeTimer;
        private float shakeSwapTimer;

        private ContentManager content;
        private ScreenManager screenManager;

        private SoundEffect[] deflectSounds;
        private SoundEffect deathSound;
        private Song backgroundMusic;

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (screenshake)
            {
                shakeTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                shakeSwapTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (shakeTimer > 1.5f)
                {
                    screenshake = false;
                    shakeTimer -= 1.5f;
                }

                int x;
                if (shakeSwapTimer > shakeSpeed)
                {
                    x = -5;
                    if (shakeSwapTimer > shakeSpeed * 2) shakeSwapTimer -= shakeSpeed * 2;
                }
                else x = 5;

                var transform = Matrix.CreateTranslation(x, 0, 0);

                spriteBatch.Begin(transformMatrix: transform);
            }
            else
            {
                spriteBatch.Begin();
            }
            background.Draw(gameTime, spriteBatch);
            foreach (Arrow a in arrows) if (a != null) a.Draw(spriteBatch);
            if (bomb.IsActive) bomb.Draw(gameTime, spriteBatch);
            player.Draw(spriteBatch);
            spriteBatch.End();
            spriteBatch.Begin();
            spriteBatch.DrawString(gravedigger, "Score: " + score.ToString(), new Vector2(2, 2), Color.Red, 0f, new Vector2(0, 0), .75f, SpriteEffects.None, 0);
            spriteBatch.DrawString(gravedigger, "Use WASD, ARROW KEYS, or the LEFT ANALOG STICK to move.\nFacing an arrow will block the arrow, but not block bombs.", new Vector2(2, 435), Color.Red, 0f, new Vector2(0, 0), .25f, SpriteEffects.None, 0);
            spriteBatch.End();
        }

        public void Initialize(ScreenManager manager)
        {
            background = new BackgroundScreen();
            screenManager = manager;
            rand = new Random(); 
            player = new Player(new Vector2(400, 240));
            arrows = new Arrow[100];
            deflectSounds = new SoundEffect[4];
            bomb = new Bomb();
            difficulty = .5f;
        }

        public void Load(ContentManager content)
        {
            this.content = content;
            background.Load(content);
            gravedigger = content.Load<SpriteFont>("GraveDigger");
            player.LoadContent(content);
            for (int i = 0; i < deflectSounds.Length; i++) 
            {
                deflectSounds[i] = content.Load<SoundEffect>("block" + i.ToString());
            }
            deathSound = content.Load<SoundEffect>("Hurt");
            explosionSound = content.Load<SoundEffect>("ExplosionSound");
            backgroundMusic = content.Load<Song>("mixkit-infected-vibes-157");
            bomb.LoadContent(content);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(backgroundMusic);
        }

        public void Update(GameTime gameTime)
        {
            arrowSpawnTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (arrowSpawnTime > 1 / difficulty)
            {
                SpawnArrow();
                score++;
                arrowSpawnTime -= 1 / difficulty;
                difficulty += .05f;
            }

            player.Update(gameTime, 800, 480);
            foreach (Arrow a in arrows)
            {
                if (a != null)
                {
                    a.Update(gameTime, 800, 480);
                    if (!a.Deflected && player.Collideswith(a.Bounds))
                    {
                        switch (player.Direction) // This is all checking if the shield blocks the arrow
                        {
                            case Direction.down:
                                if (!(a.Direction == Direction.up)) GameOver();
                                else
                                {
                                    deflectSounds[rand.Next(4)].Play();
                                    score++;
                                }
                                break;
                            case Direction.up:
                                if (!(a.Direction == Direction.down)) GameOver();
                                else
                                {
                                    deflectSounds[rand.Next(4)].Play();
                                    score++;
                                }
                                break;
                            case Direction.left:
                                if (!(a.Direction == Direction.right)) GameOver();
                                else
                                {
                                    deflectSounds[rand.Next(4)].Play();
                                    score++;
                                }
                                break;
                            case Direction.right:
                                if (!(a.Direction == Direction.left)) GameOver();
                                else
                                {
                                    deflectSounds[rand.Next(4)].Play();
                                    score++;
                                }
                                break;
                        }
                        a.Deflected = true;
                    }
                    if (!a.Active) DespawnArrow(a);
                }
            }

            if (!bomb.IsActive)
            {
                bombSpawnTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (bombSpawnTime > 10)
                {
                    bomb.Reset(new Vector2((float)rand.Next(768), (float)rand.Next(448)));
                    bombSpawnTime -= 10;
                }
            }
            else
            {
                bomb.Update(gameTime);
                if (bomb.IsDetonated)
                {
                    explosionSound.Play();
                    screenshake = true;
                    screenManager.GenerateExplosion(bomb.Bounds.Center);
                    if (player.Collideswith(bomb.Bounds)) GameOver();
                    else score += 5;
                }
            }
        }

        /// <summary>
        /// This is called to reset the game screen for another game
        /// </summary>
        public void Reset()
        {
            player = new Player(new Vector2(400, 240));
            arrows = new Arrow[100];
            difficulty = .5f;
            score = 0;
        }

        /// <summary>
        /// This is executed whenever game over occurs
        /// </summary>
        private void GameOver()
        {
            MediaPlayer.Stop();
            deathSound.Play();
            var newScreen = new GameOverScreen();
            newScreen.score = score;
            newScreen.backgroundScreen = this;
            screenManager.ChangeScreen(newScreen);
        }

        /// <summary>
        /// This spawns another arrow
        /// </summary>
        private void SpawnArrow()
        {
            for (int i = 0; i < 100; i++)
            {
                if (arrows[i] == null)
                {
                    Direction direction = (Direction)rand.Next(0, 4);
                    switch (direction)
                    {
                        case Direction.down:
                            arrows[i] = new Arrow(direction, difficulty, new Vector2((float)rand.NextDouble() * 800, 0));
                            break;
                        case Direction.left:
                            arrows[i] = new Arrow(direction, difficulty, new Vector2(800, (float)rand.NextDouble() * 480));
                            break;
                        case Direction.right:
                            arrows[i] = new Arrow(direction, difficulty, new Vector2(0, (float)rand.NextDouble() * 480));
                            break;
                        case Direction.up:
                            arrows[i] = new Arrow(direction, difficulty, new Vector2((float)rand.NextDouble() * 800, 480));
                            break;
                    }
                    arrows[i].LoadContent(content);
                    break;
                }
            }
        }

        /// <summary>
        /// This removes an arrow from play
        /// </summary>
        /// <param name="arrow">the arrow to remove</param>
        private void DespawnArrow(Arrow arrow)
        {
            for (int i = 0; i < 100; i++)
            {
                if (arrow == arrows[i]) arrows[i] = null;
            }
        }
    }
}
