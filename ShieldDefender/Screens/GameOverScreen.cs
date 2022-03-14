using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShieldDefender.Screens
{
    public class GameOverScreen : IScreen
    {
        private SpriteFont gravedigger;

        /// <summary>
        /// The screen to overlay the game over on
        /// </summary>
        public IScreen backgroundScreen;

        /// <summary>
        /// The player's end score
        /// </summary>
        public int score;
        
        private ScreenManager screenManager;

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            backgroundScreen.Draw(gameTime, spriteBatch);
            spriteBatch.Begin();
            spriteBatch.DrawString(gravedigger, "GAME OVER", new Vector2(370, 20), Color.Red, 0f, new Vector2(100, 0), 1f, SpriteEffects.None, 0);
            spriteBatch.DrawString(gravedigger, "Score: " + score.ToString(), new Vector2(370, 70), Color.Red, 0f, new Vector2(40, 0), .75f, SpriteEffects.None, 0);
            spriteBatch.DrawString(gravedigger, "To retry, press A or SPACE\nor press BACK or ESCAPE to quit.", new Vector2(370, 150), Color.Red, 0f, new Vector2(270, 0), .5f, SpriteEffects.None, 0);
            spriteBatch.End();
        }

        public void Initialize(ScreenManager manager)
        {
            screenManager = manager;
        }

        public void Load(ContentManager content)
        {
            gravedigger = content.Load<SpriteFont>("GraveDigger");
        }

        public void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                if (backgroundScreen is GameScreen gs)
                {
                    gs.Reset();
                }
                screenManager.ChangeScreen(backgroundScreen);
            }
            else if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                screenManager.Exit();
            }
        }
    }
}
