using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RunnerAlpha.Code.Graphics;

namespace RunnerAlpha.Code.Entities
{
    class EntityManager
    {
        Runner game;
        SpriteBatch spriteBatch;

        public Player player;
        public LinkedList<Entity> entityList = new LinkedList<Entity>();
        List<String> platformFiles = new List<String>();
        Random random = new Random();

        Background background;
        public Platform platform = null;

        public EntityManager(Runner game, SpriteBatch spriteBatch)
        {
            this.game = game;
            this.spriteBatch = spriteBatch;

            player = new Player(game, spriteBatch, new Vector2(10, 1080));
            player.Initialize();
            player.position = new Vector2(20, 1080);
            entityList.AddLast(player);

            platform = new Platform(game, spriteBatch, @"Graphics\Start", new Vector2(0f, 1200f));
            platform.Initialize();
            entityList.AddLast(platform);

            background = new Background(@"Graphics\Background", spriteBatch, game);
            background.Initialize();

            platformFiles.Add(@"Graphics\3");
            platformFiles.Add(@"Graphics\buildingDoor");
            platformFiles.Add(@"Graphics\buildingStupranna");
            platformFiles.Add(@"Graphics\Start");

            for (int i = 0; i < 9; i++)
            {
                entityList.AddLast(addPlatform());
            }
        }

        private LinkedListNode<Entity> findFirstPlatform()
        {
            LinkedListNode<Entity> temp = entityList.First;
            bool foundFirstPlatform = false;
            do
            {
                if (temp.Value.GetType().Name.Equals("Platform"))
                {
                    return temp;
                }
                else
                {
                    temp = temp.Next;
                }
            } while (!foundFirstPlatform);
            return null;
        }

        private LinkedListNode<Entity> findLastPlatform()
        {
            LinkedListNode<Entity> temp = entityList.Last;
            bool foundLastPlatform = false;
            do
            {
                if (temp.Value.GetType().Name.Equals("Platform"))
                {
                    return temp;
                }
                else
                {
                    temp = temp.Previous;
                }
            } while (!foundLastPlatform);
            return null;
        }

        private void refreshPlatforms()
        {
            entityList.Remove(findFirstPlatform());
            entityList.AddLast(addPlatform());
        }

        private Platform addPlatform()
        {
            string filename = platformFiles[random.Next(platformFiles.Count)];
            Platform lastPlatform = (Platform)findLastPlatform().Value;
            float posX = lastPlatform.position.X + lastPlatform.Rectangle.Width + 200f;
            Vector2 position = new Vector2(posX, 1200f);

            platform = new Platform(game, spriteBatch, filename, position);
            platform.Initialize();
            return platform;
        }

        public void Terminate()
        {
            entityList.Clear();
        }

        public void Update(GameTime gameTime)
        {
            background.position = new Vector2(game.Camera.Position.X - Runner.WIDTH / 2, 0f);

            foreach (Entity entity in entityList)
            {
                entity.Update(gameTime);
            }

            Platform temp = (Platform) findFirstPlatform().Value;
            if (temp.Rectangle.Right < game.Camera.Position.X - Runner.WIDTH / 2)
            {
                refreshPlatforms();
            }

            CollisionCheck();
        }

        public void Draw(GameTime gameTime)
        {
            background.Draw(gameTime);

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
                //player.position.X = Runner.WIDTH - player.Rectangle.Width / 2;
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
