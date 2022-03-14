using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using ShieldDefender.Collisions;

namespace ShieldDefender
{
    public class Bomb
    {
        private bool detonated = true;
        public bool IsDetonated { get => detonated; }

        private bool active = false;
        public bool IsActive { get => active; }

        private float animationSpeed = .75f;
        private float animationTimer;
        private int animationFrame;
        private float leftoverFuse;

        private Texture2D texture;

        private Vector2 position;

        public BoundingCircle Bounds;

        public Bomb()
        {

        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("BombSet");
        }

        public void Update(GameTime gameTime)
        {
            if (detonated) active = false;
            else
            {
                leftoverFuse -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (leftoverFuse < 0) detonated = true;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch) 
        {
            if (!detonated)
            {
                animationTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (animationTimer > animationSpeed)
                {
                    animationFrame++;
                    if (animationFrame > 3) animationFrame = 0;
                    animationTimer -= animationSpeed;
                }

                var source = new Rectangle((animationFrame % 2) * 32, (animationFrame / 2) * 32, 32, 32);
                spriteBatch.Draw(texture, position, source, Color.White);
            }
        }

        public void Reset(Vector2 position)
        {
            this.position = position;
            Bounds = new BoundingCircle(position, 80);
            leftoverFuse = animationSpeed * 4;
            animationFrame = 0;
            animationTimer = 0;
            detonated = false;
            active = true;
        }
    }
}
