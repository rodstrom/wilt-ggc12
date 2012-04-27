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
        public LinkedList<Background> backgroundList = new LinkedList<Background>();
        List<String> platformFiles = new List<String>();
        Random random = new Random();

        public Platform platform = null;

        public EntityManager(Runner game, SpriteBatch spriteBatch)
        {
            this.game = game;
            this.spriteBatch = spriteBatch;

            player = new Player(game, spriteBatch, new Vector2(100f));
            player.Initialize();
            entityList.AddLast(player);

            platform = new Platform(game, spriteBatch, @"Graphics\Platforms\b1", new Vector2(-100f, Runner.HEIGHT));
            platform.Initialize();
            entityList.AddLast(platform);

            Background background = new Background(@"Graphics\Backgrounds\Background", spriteBatch, game, new Vector2(-Runner.WIDTH / 2, -Runner.HEIGHT));
            background.Initialize();
            backgroundList.AddLast(background);
            backgroundList.AddLast(addBackground());
            backgroundList.AddLast(addBackground());

            platformFiles.Add(@"Graphics\Platforms\1");
            platformFiles.Add(@"Graphics\Platforms\2 (2)");
            platformFiles.Add(@"Graphics\Platforms\3 (2)");
            platformFiles.Add(@"Graphics\Platforms\b1");
            platformFiles.Add(@"Graphics\Platforms\b4");
            platformFiles.Add(@"Graphics\Platforms\c2");
            platformFiles.Add(@"Graphics\Platforms\c3");

            for (int i = 0; i < 5; i++)
            {
                entityList.AddLast(addPlatform());
            }
        }

        private LinkedListNode<Entity> findFirstOfType(String type)
        {
            LinkedListNode<Entity> temp = entityList.First;
            bool foundFirstPlatform = false;
            do
            {
                if (temp.Value.GetType().Name.Equals(type))
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

        private LinkedListNode<Entity> findLastOfType(String type)
        {
            LinkedListNode<Entity> temp = entityList.Last;
            bool foundLastPlatform = false;
            do
            {
                if (temp.Value.GetType().Name.Equals(type))
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
            entityList.Remove(findFirstOfType("Platform"));
            entityList.AddLast(addPlatform());
        }

        private Platform addPlatform()
        {
            string filename = platformFiles[random.Next(platformFiles.Count)];
            Platform lastPlatform = (Platform)findLastOfType("Platform").Value;
            float posX = lastPlatform.position.X + lastPlatform.Rectangle.Width + 200f;
            Vector2 position = new Vector2(posX, Runner.HEIGHT);

            platform = new Platform(game, spriteBatch, filename, position);
            platform.Initialize();
            return platform;
        }

        private void refreshBackgrounds()
        {
            backgroundList.RemoveFirst();
            backgroundList.AddLast(addBackground());
        }

        private Background addBackground()
        {
            Vector2 position = new Vector2(backgroundList.Last.Value.Rectangle.Right, -Runner.HEIGHT);

            Background background = new Background(@"Graphics\Backgrounds\Background", spriteBatch, game, position);
            background.Initialize();
            return background;
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

            Platform tempPlatform = (Platform)findFirstOfType("Platform").Next.Value;
            if (tempPlatform.Rectangle.Right < game.Camera.Position.X - game.Camera.Center.X / 2)
            {
                refreshPlatforms();
            }

            if (backgroundList.First.Value.Rectangle.Right < game.Camera.Position.X - game.Camera.Center.X)
            {
                refreshBackgrounds();
            }

            CollisionCheck();
        }

        public void Draw(GameTime gameTime)
        {
            foreach (Background background in backgroundList)
            {
                background.Draw(gameTime);
            }

            foreach (Entity entity in entityList)
            {
                entity.Draw(gameTime);
            }

            LineBatch.DrawLine(spriteBatch, Color.Red,
                new Vector2(game.Camera.Position.X - game.Camera.Center.X, Runner.HEIGHT),
                new Vector2(game.Camera.Position.X + game.Camera.Center.X, Runner.HEIGHT));
        }

        private void CollisionCheck()
        {
            if (player.Rectangle.Bottom > Runner.HEIGHT)
            {
                player.lose = true;
            }

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
    }
}
