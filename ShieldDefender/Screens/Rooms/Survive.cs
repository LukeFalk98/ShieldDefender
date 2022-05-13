using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace ShieldDefender.Screens.Rooms
{
    public class Survive : IRoom
    {
        /// <summary>
        /// The player object
        /// </summary>
        private Player player;

        /// <summary>
        /// The difficulty of the room
        /// </summary>
        private float difficulty;

        /// <summary>
        /// TODO: input starting position
        /// </summary>
        public Vector2 StartPosition => new Vector2();

        public bool Freeze { get; set; }

        private float TimeRemaining;
        private BackgroundScreen bg;

        private float bombSpawnTime;
        private SoundEffect explosionSound;
        private Bomb bomb;
        private bool screenshake = false;
        private float shakeSpeed = .1f;
        private float shakeTimer;
        private float shakeSwapTimer;
        private Arrow[] arrows;
        private float arrowSpawnTime;
        private System.Random rand;

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
            bg.Draw(gameTime, spriteBatch);
            foreach (Arrow a in arrows) if (a != null) a.Draw(spriteBatch);
            if (bomb.IsActive) bomb.Draw(gameTime, spriteBatch);
            player.Draw(spriteBatch);
            spriteBatch.End();
            
        }

        public void Initialize(Player player, ScreenManager screenManager, float difficulty)
        {
            this.player = player;
            this.difficulty = difficulty;
            player.Position = StartPosition;
            TimeRemaining = 15 + (float)Math.Floor(difficulty/5)*5; // player will survive for time in intervals of 5
            bg = new BackgroundScreen();
            bg.Initialize(screenManager);
        }

        public void Load(ContentManager content)
        {
            bg.Load(content);
            bomb.LoadContent(content);
            rand = new Random();
        }

        public void Update(GameTime gameTime)
        {
            /*arrowSpawnTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

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
            }*/
        }
    }
}
