using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ShieldDefender.Screens;
using ShieldDefender.Particles;

namespace ShieldDefender
{
    public class ShieldDefenderGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch spriteBatch;
        private ScreenManager manager;
        private ExplosionParticleSystem explosions;

        public ShieldDefenderGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
            manager = new ScreenManager(this);
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            explosions = new ExplosionParticleSystem(this, 20);
            Components.Add(explosions);
            manager.Initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
            manager.Load(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            manager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGray);
            
            manager.Draw(gameTime, spriteBatch);
            base.Draw(gameTime);
        }
        
        public void GenerateExplosion(Vector2 position)
        {
            explosions.PlaceExplosion(position);
        }
    }
}
