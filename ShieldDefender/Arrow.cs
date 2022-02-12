using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using ShieldDefender.Collisions;

namespace ShieldDefender
{
    public enum Direction
    {
        up = 2,
        down = 0,
        left = 1,
        right = 3
    }

    public class Arrow
    {
        private Vector2 position;
        private float speed;
        private Texture2D texture;

        public BoundingCircle Bounds;
        public Direction Direction;

        /// <summary>
        /// whether or not the arrow is on screen
        /// </summary>
        public bool Active = true;

        public Arrow(Direction direction, float speed, Vector2 position)
        {
            this.Direction = direction;
            this.speed = speed;
            this.position = position;
            Bounds = new BoundingCircle(position, 1);
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Arrow");
        }

        public void Update(GameTime gameTime, int width, int height)
        {
            switch (Direction)
            {
                case Direction.down:
                    position += new Vector2(0, 100) * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    Bounds.Center = position + new Vector2(0, 16);
                    break;
                case Direction.up:
                    position += new Vector2(0, -100) * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    Bounds.Center = position + new Vector2(0, -16);
                    break;
                case Direction.left:
                    position += new Vector2(-100, 0) * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    Bounds.Center = position + new Vector2(-16, 0);
                    break;
                case Direction.right:
                    position += new Vector2(100, 0) * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    Bounds.Center = position + new Vector2(16, 0);
                    break;
            }

            // check if the arrow is offscreen
            if (position.X > width || position.X < 0 ||
                position.Y > height || position.Y < 0) Active = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, (float)Math.PI * (int)Direction / 2, new Vector2(16, 16), 1f, SpriteEffects.None, 0);
        }

        public bool Collideswith(BoundingRectangle other)
        {
            return CollisionHelper.Collides(this.Bounds, other);
        }
    }
}
