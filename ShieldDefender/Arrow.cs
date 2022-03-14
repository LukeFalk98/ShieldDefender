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
        private float vertVelocity = -6; // only when the arrow is deflected is this calculated
        const float vertAcceleration = 1;
        private float rotation;
        const float rotationSpeed = .45f;
        private Texture2D texture;

        private bool active = true;

        public BoundingCircle Bounds;
        public Direction Direction;

        /// <summary>
        /// whether or not the arrow is on screen
        /// </summary>
        public bool Active => active;
        /// <summary>
        /// Whether or not the arrow has been deflected by shield
        /// </summary>
        public bool Deflected = false;

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
            if (!Deflected)
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
            }
            else
            {
                if (Direction == Direction.left) rotation -= rotationSpeed;
                else rotation += rotationSpeed;

                Vector2 modPosition;
                vertVelocity += vertAcceleration;
                if (Direction == Direction.left ||
                    Direction == Direction.up)
                {
                    modPosition = new Vector2(5, vertVelocity);
                }
                else
                {
                    modPosition = new Vector2(-5, vertVelocity);
                }

                position += modPosition;
            }

            // check if the arrow is offscreen
            if (position.X > width || position.X < 0 ||
                position.Y > height || position.Y < 0) active = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Color color;
            if (Deflected) color = Color.Blue;
            else color = Color.White;
            spriteBatch.Draw(texture, position, null, color, (float)Math.PI * (int)Direction / 2 + rotation, new Vector2(16, 16), 1f, SpriteEffects.None, 0);
        }

        public bool Collideswith(BoundingRectangle other)
        {
            if (!Deflected)
            {
                return CollisionHelper.Collides(this.Bounds, other);
            }
            else return false; // the arrow has been deflected
        }
    }
}
