using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using ShieldDefender.Screens._3DObjects;

namespace ShieldDefender.Screens
{
    class _3DBackgroundScreen : IScreen
    {
        CirclingCamera circlingCamera;

        Room room;

        Game game;

        public _3DBackgroundScreen (Game game)
        {
            this.game = game;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            room.Draw(circlingCamera);
        }

        public void Initialize(ScreenManager manager)
        {
            
        }

        public void Load(ContentManager content)
        {
            circlingCamera = new CirclingCamera(game, new Vector3(0f, 0f, 1f), .25f);
            room = new Room(game, Matrix.Identity);
        }

        public void Update(GameTime gameTime)
        {
            circlingCamera.Update(gameTime);
        }
    }
}
