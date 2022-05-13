using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShieldDefender.Screens.Rooms
{
    public class RoomManager : IScreen
    {

        private SpriteFont gravedigger;
        private uint score;
        private float difficulty;
        private IRoom currentRoom;
        private IRoom nextRoom;
        private Player player;

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            spriteBatch.Begin();
            spriteBatch.DrawString(gravedigger, "Score: " + score.ToString(), new Vector2(2, 2), Color.Red, 0f, new Vector2(0, 0), .75f, SpriteEffects.None, 0);
            spriteBatch.DrawString(gravedigger, "Use WASD, ARROW KEYS, or the LEFT ANALOG STICK to move.\nFacing an arrow will block the arrow, but not block bombs.", new Vector2(2, 435), Color.Red, 0f, new Vector2(0, 0), .25f, SpriteEffects.None, 0);
            spriteBatch.End();
        }

        public void Initialize(ScreenManager manager)
        {
            player = new Player(new Vector2(400, 240));
            currentRoom = new Survive();
            currentRoom.Initialize(player, manager, 0);
        }

        public void Load(ContentManager content)
        {
            throw new NotImplementedException();
        }

        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
