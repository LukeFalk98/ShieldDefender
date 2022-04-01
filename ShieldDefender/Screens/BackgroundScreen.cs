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
        private Tilemap _tileMap;

        /// <summary>
        /// Draws the background
        /// </summary>
        /// <param name="gameTime">The game time</param>
        /// <param name="spriteBatch">The spritebatch to draw the textures, NEEDS Spritebatch.Begin called!</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(image, new Vector2(-5, 0), Color.White);
            _tileMap.Draw(gameTime, spriteBatch);
        }

        public void Initialize(ScreenManager manager)
        {
            _tileMap = new Tilemap("map.txt");
        }

        public void Load(ContentManager content)
        {
            image = content.Load<Texture2D>("Background");
            _tileMap.LoadContent(content);
        }

        public void Update(GameTime gameTime)
        {
            
        }
    }
}
