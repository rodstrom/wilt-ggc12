using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RunnerAlpha.Code.Entities
{
    class EntityManager
    {
        public LinkedList<Entity> entityList = new LinkedList<Entity>();

        public Player player = null;
        public Platform platform = null;

        public EntityManager(Runner game, SpriteBatch spriteBatch)
        {
            player = new Player(game, spriteBatch, new Vector2(10, 1080));
            player.Initialize();
            player.position = new Vector2(20, 1080);
            entityList.AddLast(player);

            platform = new Platform(game, spriteBatch, @"Graphics\Start", new Vector2(90f, 1080));
            platform.Initialize();
            entityList.AddLast(platform);

            platform = new Platform(game, spriteBatch, @"Graphics\buildingStupranna", new Vector2(800f, 1000));
            platform.Initialize();
            entityList.AddLast(platform);

            platform = new Platform(game, spriteBatch, @"Graphics\buildingStupranna", new Vector2(800f, 1000));
            platform.Initialize();
            entityList.AddLast(platform);

            platform = new Platform(game, spriteBatch, @"Graphics\3", new Vector2(1300f, 1150));
            platform.Initialize();
            entityList.AddLast(platform);

            platform = new Platform(game, spriteBatch, @"Graphics\buildingDoor", new Vector2(1870f, 1100));
            platform.Initialize();
            entityList.AddLast(platform);
        }

        public void Terminate()
        {
            entityList.Clear();
        }

        public void Update(GameTime gameTime)
        {
            foreach (Entity entity in entityList)
            {
                entity.Update(gameTime);
            }

            CollisionCheck();
        }

        public void Draw(GameTime gameTime)
        {
            foreach (Entity entity in entityList)
            {
                entity.Draw(gameTime);
            }
        }

        private void CollisionCheck()
        {
            PlayerOutOfBoundsCheck();

            Rectangle p = player.Rectangle;
            player.falling = false;
            foreach(Entity entity in entityList)
            {
                if (entity.GetType().Name == "Platform")
                {
                    Platform tmpPlat = (Platform)entity;
                    if (!(p.Intersects(tmpPlat.Rectangle)))
                    {
                        player.falling = true;
                    }
                    else if (p.Intersects(tmpPlat.Rectangle))
                    {
                        player.position.Y = (tmpPlat.Rectangle.Y - p.Height / 2) + 2;
                        player.falling = false;
                        return;
                    }
                }
            }
        }

        private void PlayerOutOfBoundsCheck()
        {
            if (player.position.X + player.Rectangle.Width / 2 > Runner.WIDTH)
            {
                player.position.X = Runner.WIDTH - player.Rectangle.Width / 2;
            }
            if (player.position.X - player.Rectangle.Width / 2 < 0f)
            {
                player.position.X = player.Rectangle.Width / 2;
            }
            if (player.position.Y + player.Rectangle.Height / 2 > Runner.HEIGHT)
            {
                player.lose = true;
            }
            if (player.position.Y - player.Rectangle.Height / 2 < -100f)
            {
                player.position.Y = -100f;
            }
        }
    }
}
