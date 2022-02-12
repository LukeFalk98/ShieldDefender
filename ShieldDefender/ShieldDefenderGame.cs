using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShieldDefender
{
    public class ShieldDefenderGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch spriteBatch;

        private CursorArrow cursor;
        private Option[] Options;
        private SpriteFont gravedigger;
        private Logo logo;

        private bool gameStart = false;
        private bool gameOver = false;
        private int score = 0;
        private Player player;
        private Arrow[] arrows;
        private float spawnTime;
        private System.Random rand;
        private float difficulty;

        public ShieldDefenderGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            cursor = new CursorArrow();
            Options = new Option[]
            {
                new Option(new Vector2(GraphicsDevice.Viewport.Width / 2, 200), "PlayButton", 172f, 64f),
                new Option(new Vector2(GraphicsDevice.Viewport.Width / 2, 300), "QuitButton", 172f, 64f),
            };
            logo = new Logo(new Vector2(480, 30));
            rand = new System.Random();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            cursor.LoadContent(this.Content);
            foreach (Option o in Options) o.LoadContent(Content);
            gravedigger = Content.Load<SpriteFont>("GraveDigger");
            logo.LoadContent(this.Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (gameStart && !gameOver)
            {
                spawnTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (spawnTime > 1 / difficulty)
                {
                    SpawnArrow();
                    score++;
                    spawnTime -= 1 / difficulty;
                    difficulty += .05f;
                }

                player.Update(gameTime, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
                foreach (Arrow a in arrows)
                {
                    if (a != null)
                    {
                        a.Update(gameTime, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
                        if (player.Collideswith(a.Bounds))
                        {
                            switch (player.Direction)
                            {
                                case Direction.down:
                                    if (!(a.Direction == Direction.up)) gameOver = true;
                                    break;
                                case Direction.up:
                                    if (!(a.Direction == Direction.down)) gameOver = true;
                                    break;
                                case Direction.left:
                                    if (!(a.Direction == Direction.right)) gameOver = true;
                                    break;
                                case Direction.right:
                                    if (!(a.Direction == Direction.left)) gameOver = true;
                                    break;
                            }
                            a.Active = false;
                        }
                        if (!a.Active) DespawnArrow(a);
                    }
                }

            }
            else if (gameOver)
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    gameOver = false;
                    gameStart = true;
                    score = 0;
                    foreach (Arrow a in arrows) if (a != null) DespawnArrow(a);
                }
            }
            else
            {
                bool select;
                // TODO: Add your update logic here
                cursor.Update(gameTime, out select);
                if (select)
                {
                    foreach (Option o in Options)
                    {
                        if (o.texturefile == "QuitButton" && cursor.Collideswith(o.bounds)) Exit();
                        if (o.texturefile == "PlayButton" && cursor.Collideswith(o.bounds))
                        {
                            gameStart = true;
                            gameOver = false;
                            difficulty = 0.5f;
                            LoadGame(gameTime);
                        }
                    }
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGray);
            // TODO: Add your drawing code here
            if (gameStart && !gameOver)
            {
                spriteBatch.Begin();
                foreach (Arrow a in arrows) if (a != null) a.Draw(spriteBatch);
                player.Draw(spriteBatch);
                spriteBatch.DrawString(gravedigger, "Score: " + score.ToString(), new Vector2(2, 2), Color.Red, 0f, new Vector2(0, 0), .75f, SpriteEffects.None, 0);
                spriteBatch.DrawString(gravedigger, "Use WASD, ARROW KEYS, or the LEFT ANALOG STICK to move.\nFacing an arrow will block the arrow", new Vector2(2, 435), Color.Red, 0f, new Vector2(0, 0), .25f, SpriteEffects.None, 0);
                spriteBatch.End();
            }
            else if (gameOver)
            {
                spriteBatch.Begin();
                foreach (Arrow a in arrows) if (a != null) a.Draw(spriteBatch);
                player.Draw(spriteBatch);
                spriteBatch.DrawString(gravedigger, "GAME OVER", new Vector2(GraphicsDevice.Viewport.Width / 2, 20), Color.Red, 0f, new Vector2(100, 0), 1f, SpriteEffects.None, 0);
                spriteBatch.DrawString(gravedigger, "Score: " + score.ToString(), new Vector2(GraphicsDevice.Viewport.Width / 2, 70), Color.Red, 0f, new Vector2(40, 0), .75f, SpriteEffects.None, 0);
                spriteBatch.DrawString(gravedigger, "To retry, press A or SPACE\nor press BACK or ESCAPE to quit.", new Vector2(GraphicsDevice.Viewport.Width / 2, 150), Color.Red, 0f, new Vector2(270, 0), .5f, SpriteEffects.None, 0);
                spriteBatch.End();
            }
            else
            {
                spriteBatch.Begin();
                foreach (Option O in Options) O.Draw(spriteBatch);
                logo.Draw(gameTime, spriteBatch);
                spriteBatch.DrawString(gravedigger, "  Shield\nDefender", new Vector2(GraphicsDevice.Viewport.Width / 2, 20), Color.Red, 0f, new Vector2(110, 0), 1, SpriteEffects.None, 0);
                spriteBatch.DrawString(gravedigger, "Use mouse or left thumbstick to move\nA or click to select.", new Vector2(2, GraphicsDevice.Viewport.Height - 45), Color.Red, 0, Vector2.Zero, .3f, SpriteEffects.None, 0);
                cursor.Draw(gameTime, spriteBatch);
                spriteBatch.End();
            }

            base.Draw(gameTime);
        }

        private void LoadGame(GameTime gameTime)
        {
            player = new Player(new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2));
            spawnTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            arrows = new Arrow[100];
            difficulty = .5f;
            player.LoadContent(Content);
        }

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
                            arrows[i] = new Arrow(direction, difficulty, new Vector2((float)rand.NextDouble() * GraphicsDevice.Viewport.Width, 0));
                            break;
                        case Direction.left:
                            arrows[i] = new Arrow(direction, difficulty, new Vector2(GraphicsDevice.Viewport.Width, (float)rand.NextDouble() * GraphicsDevice.Viewport.Height));
                            break;
                        case Direction.right:
                            arrows[i] = new Arrow(direction, difficulty, new Vector2(0, (float)rand.NextDouble() * GraphicsDevice.Viewport.Height));
                            break;
                        case Direction.up:
                            arrows[i] = new Arrow(direction, difficulty, new Vector2((float)rand.NextDouble() * GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height));
                            break;
                    }
                    arrows[i].LoadContent(Content);
                    break;
                }
            }
        }

        private void DespawnArrow(Arrow arrow)
        {
            for (int i = 0; i < 100; i++)
            {
                if (arrow == arrows[i]) arrows[i] = null;
            }
        }
    }
}
