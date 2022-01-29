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

        public ShieldDefenderGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
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
            bool select;
            // TODO: Add your update logic here
            cursor.Update(gameTime, out select);
            if (select)
            {
                foreach(Option o in Options)
                {
                    if (o.texturefile == "QuitButton" && cursor.Collideswith(o.bounds)) Exit();
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGray);
            // TODO: Add your drawing code here
            spriteBatch.Begin();
            foreach (Option O in Options) O.Draw(spriteBatch);
            logo.Draw(gameTime, spriteBatch);
            spriteBatch.DrawString(gravedigger, "  Shield\nDefender", new Vector2(GraphicsDevice.Viewport.Width / 2, 20), Color.Red, 0f, new Vector2(110, 0), 1, SpriteEffects.None, 0);
            cursor.Draw(gameTime, spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
