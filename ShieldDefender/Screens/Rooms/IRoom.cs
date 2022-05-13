using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ShieldDefender.Collisions;

namespace ShieldDefender.Screens.Rooms
{
    public interface IRoom
    {
        /// <summary>
        /// The starting position of the player
        /// </summary>
        Vector2 StartPosition { get; }

        /// <summary>
        /// Whether or not to freeze the scene
        /// </summary>
        bool Freeze { set; }

        /// <summary>
        /// Initialize/Reset all pieces of the scene
        /// </summary>
        /// <param name="player">The current player</param>
        public void Initialize(Player player, ScreenManager manager, float difficulty);

        /// <summary>
        /// Load the initial 
        /// </summary>
        /// <param name="content">The content manager to load assets</param>
        public void Load(ContentManager content);

        /// <summary>
        /// Draws the scene
        /// </summary>
        /// <param name="gameTime">The current gametime</param>
        public void Draw(GameTime gameTime, SpriteBatch spritebatch);

        /// <summary>
        /// Updates the logic of the scene
        /// </summary>
        /// <param name="gameTime">the current gametime</param>
        public void Update(GameTime gameTime);
    }
}
