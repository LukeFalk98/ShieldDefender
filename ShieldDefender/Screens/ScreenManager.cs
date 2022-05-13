using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShieldDefender.Screens
{
    public class ScreenManager
    {
        private IScreen frontScreen;
        private ContentManager content;
        private ShieldDefenderGame parent;


        public ScreenManager(ShieldDefenderGame game)
        {
            frontScreen = new TitleScreen(game);
            parent = game;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            frontScreen.Draw(gameTime, spriteBatch);
        }

        public void Initialize()
        {
            frontScreen.Initialize(this);
        }

        public void Load(ContentManager content)
        {
            frontScreen.Load(content);
            this.content = content;
        }

        public void Update(GameTime gameTime)
        {
            frontScreen.Update(gameTime);
        }

        public void ChangeScreen(IScreen screen)
        {
            frontScreen = screen;
            frontScreen.Initialize(this);
            frontScreen.Load(content);
        }

        public void Exit()
        {
            parent.Exit();
        }

        public void GenerateExplosion(Vector2 position)
        {
            parent.GenerateExplosion(position);
        }
    }
}
