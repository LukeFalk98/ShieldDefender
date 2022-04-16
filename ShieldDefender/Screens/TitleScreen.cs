using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShieldDefender.Screens
{
    public class TitleScreen : IScreen
    {
        private CursorArrow cursor;
        private Option[] Options;
        private Logo logo;
        private SpriteFont gravedigger;
        private ScreenManager screenManager;
        private _3DBackgroundScreen backgroundScreen;

        public TitleScreen (Game game)
        {
            backgroundScreen = new _3DBackgroundScreen(game);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            backgroundScreen.Draw(gameTime, spriteBatch);
            foreach (Option O in Options) O.Draw(spriteBatch);
            logo.Draw(gameTime, spriteBatch);
            spriteBatch.DrawString(gravedigger, "  Shield\nDefender", new Vector2(400, 20), Color.Red, 0f, new Vector2(110, 0), 1, SpriteEffects.None, 0);
            spriteBatch.DrawString(gravedigger, "Use mouse or left thumbstick to move\nA or click to select.", new Vector2(2, 425), Color.Red, 0, Vector2.Zero, .3f, SpriteEffects.None, 0);
            cursor.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }

        public void Initialize(ScreenManager screenManager)
        {
            cursor = new CursorArrow();
            Options = new Option[]
            {
                new Option(new Vector2(400, 200), "PlayButton", 172f, 64f),
                new Option(new Vector2(400, 300), "QuitButton", 172f, 64f),
            };
            logo = new Logo(new Vector2(480, 30));
            this.screenManager = screenManager;
            backgroundScreen.Initialize(screenManager);
        }

        public void Load(ContentManager content)
        {
            cursor.LoadContent(content);
            foreach (Option o in Options) o.LoadContent(content);
            gravedigger = content.Load<SpriteFont>("GraveDigger");
            logo.LoadContent(content);
            backgroundScreen.Load(content);
        }

        public void Update(GameTime gameTime)
        {
            bool select;
            cursor.Update(gameTime, out select);
            if (select)
            {
                foreach (Option o in Options)
                {
                    if (o.texturefile == "QuitButton" && cursor.Collideswith(o.bounds)) Exit();
                    if (o.texturefile == "PlayButton" && cursor.Collideswith(o.bounds))
                    {
                        var newScreen = new GameScreen();
                        screenManager.ChangeScreen(newScreen);
                    }
                }
            }
            backgroundScreen.Update(gameTime);
        }

        private void Exit()
        {
            screenManager.Exit();
        }
    }
}
