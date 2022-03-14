using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShieldDefender.Screens
{
    public class BackgroundScreen : IScreen
    {
        private Texture2D image;

        /// <summary>
        /// Draws the background
        /// </summary>
        /// <param name="gameTime">The game time</param>
        /// <param name="spriteBatch">The spritebatch to draw the textures, NEEDS Spritebatch.Begin called!</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, new Vector2(-5, 0), Color.White);
        }

        public void Initialize(ScreenManager manager)
        {
            
        }

        public void Load(ContentManager content)
        {
            image = content.Load<Texture2D>("Background");
        }

        public void Update(GameTime gameTime)
        {
            
        }
    }
}
