using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ShieldDefender.Screens
{
    public interface IScreen
    {
        /// <summary>
        /// Initialize the screen
        /// </summary>
        /// <param name="manager">The ScreenManager handling the screen</param>
        public abstract void Initialize(ScreenManager manager);

        /// <summary>
        /// Load the content for the screen
        /// </summary>
        /// <param name="content">The ContentManager</param>
        public abstract void Load(ContentManager content);

        /// <summary>
        /// Update the objects in the screen
        /// </summary>
        /// <param name="gameTime">The current game time</param>
        public abstract void Update(GameTime gameTime);

        /// <summary>
        /// Draw the objects onto the screen
        /// </summary>
        /// <param name="gameTime">The current game time</param>
        /// <param name="spriteBatch">The SpriteBatch in which to draw</param>
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
