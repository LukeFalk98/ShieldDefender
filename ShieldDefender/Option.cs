using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using ShieldDefender.Collisions;

namespace ShieldDefender
{
    /// <summary>
    /// This represents an option on the menu
    /// </summary>
    public class Option
    {
        private Vector2 position;

        /// <summary>
        /// the name of the file used for the texture
        /// </summary>
        public string texturefile;

        private Texture2D texture;

        /// <summary>
        /// The bounding box of the option button
        /// </summary>
        public BoundingRectangle bounds;

        /// <summary>
        /// The bounding rectangle of the button
        /// </summary>
        public BoundingRectangle Bounds => bounds;

        /// <summary>
        /// Creates a new button
        /// </summary>
        /// <param name="position">The location to make the button</param>
        /// <param name="fn">the filename of the texture</param>
        public Option(Vector2 position, string fn, float width, float height)
        {
            this.position = position;
            this.texturefile = fn;
            this.bounds = new BoundingRectangle(position, width, height);
        }

        /// <summary>
        /// Loads the required textures for the button
        /// </summary>
        /// <param name="content">the ContentManager to load the texture</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>(texturefile);
        }

        /// <summary>
        /// Draws the button
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch to draw the button with</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0f, new Vector2(86,32), 1f, SpriteEffects.None, 0);
        }
    }
}
