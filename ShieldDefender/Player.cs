using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ShieldDefender.Collisions;

namespace ShieldDefender
{
    public class Player
    {
        private GamePadState gamePadState;
        private KeyboardState keyboardState;

        private Vector2 position;
        public Vector2 Position { get; set; }

        private BoundingRectangle bounds;

        public Direction Direction;

        private Texture2D texture;

        public Player(Vector2 position)
        {
            this.position = position;
            bounds = new BoundingRectangle(position, 32, 38);
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("player");
        }

        public void Update(GameTime gameTime, int width, int height)
        {
            gamePadState = GamePad.GetState(0);
            keyboardState = Keyboard.GetState();

            Vector2 aim;
            aim = gamePadState.ThumbSticks.Left * new Vector2(100, -100) * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W)) aim += new Vector2(0, -100) * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S)) aim += new Vector2(0, 100) * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A)) aim += new Vector2(-100, 0) * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D)) aim += new Vector2(100, 0) * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Math.Abs(aim.X) > Math.Abs(aim.Y))
            {
                if (aim.X < 0) Direction = Direction.left;
                else if (aim.X > 0) Direction = Direction.right;
            }
            else
            {
                if (aim.Y < 0) Direction = Direction.up;
                else if (aim.Y > 0) Direction = Direction.down;
            }

            position += aim;
            if (position.X < 0) position.X = 0;
            else if (position.X > width - 32) position.X = width - 32;
            if (position.Y < 0) position.Y = 0;
            else if (position.Y > height - 38) position.Y = height - 38;

            bounds.X = position.X;
            bounds.Y = position.Y;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            SpriteEffects effects;
            if (Direction == Direction.left) effects = SpriteEffects.FlipHorizontally;
            else effects = SpriteEffects.None;
            int frame;
            switch (Direction)
            {
                case Direction.down:
                    frame = 0;
                    break;
                case Direction.up:
                    frame = 2;
                    break;
                default:
                    frame = 1;
                    break;
            }
            var source = new Rectangle(frame * 32, 0, 32, 38);
            spriteBatch.Draw(texture, position, source, Color.White, 0f, Vector2.Zero, 1f, effects, 0);
        }

        public bool Collideswith(BoundingCircle other)
        {
            return CollisionHelper.Collides(this.bounds, other);
        }
    }
}
