using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using ShieldDefender.Collisions;

namespace ShieldDefender
{
    /// <summary>
    /// A class representing the cursor arrow
    /// </summary>
    public class CursorArrow
    {
        private GamePadState gamePadState;

        private KeyboardState keyboardState;

        private Texture2D texture;

        private Vector2 position = new Vector2(200, 200);

        private BoundingCircle bounds = new BoundingCircle(new Vector2(225, 225), 3);

        private Vector2 direction;

        /// <summary>
        /// Loads the sprite texture using the provided ContentManager
        /// </summary>
        /// <param name="content">The ContentManager to load with</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Arrow");
        }

        public void Update(GameTime gameTime, out bool select)
        {
            gamePadState = GamePad.GetState(0);
            keyboardState = Keyboard.GetState();

            // Apply the gamepad movement
            direction = gamePadState.ThumbSticks.Left * new Vector2(100, -100) * (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            // Apply keyboard movement
            if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W)) direction += new Vector2(0, -100) * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S)) direction += new Vector2(0, 100) * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A)) direction += new Vector2(-100, 0) * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D)) direction += new Vector2(100, 0) * (float)gameTime.ElapsedGameTime.TotalSeconds;

            position += direction;
            bounds.Center = position + new Vector2(95, 41);

            if (keyboardState.IsKeyDown(Keys.Space) || gamePadState.IsButtonDown(Buttons.A)) select = true;
            else select = false;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, -.66f, new Vector2(16,16), 1, SpriteEffects.None, 0);
        }

        public bool Collideswith(BoundingRectangle other)
        {
            return CollisionHelper.Collides(this.bounds, other);
        }
    }
}
