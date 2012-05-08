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
        Dictionary<String, Rectangle> platformFiles = new Dictionary<String, Rectangle>();
        Random random = new Random();

        public Platform platform = null;
        
        public EntityManager(Runner game, SpriteBatch spriteBatch)
        {
            this.game = game;
            this.spriteBatch = spriteBatch;
        }

        public void Initialize()
        {
            player = new Player(game, spriteBatch, new Vector2(300f, -500f));
            player.Initialize();
            entityList.AddLast(player);

            platform = new Platform(game, spriteBatch, @"Graphics\Platforms\hus1", new Vector2(-100f, Runner.HEIGHT + 800f), new Rectangle(163, 216, 1030, 2484));
            platform.Initialize();
            entityList.AddLast(platform);

            Background background = new Background(@"Graphics\Backgrounds\Background", spriteBatch, game, new Vector2(-300f, Runner.HEIGHT));
            background.Initialize();
            backgroundList.AddLast(background);
            backgroundList.AddLast(addBackground());
            backgroundList.AddLast(addBackground());

            platformFiles.Add(@"Graphics\Platforms\hus1", new Rectangle(163, 216, 1030, 1000));
            platformFiles.Add(@"Graphics\Platforms\hus2", new Rectangle(10, 480, 1429, 1000));
            platformFiles.Add(@"Graphics\Platforms\hus3", new Rectangle(7, 116, 1997, 1000));
            platformFiles.Add(@"Graphics\Platforms\hus4", new Rectangle(3, 1331, 637, 1000));
            platformFiles.Add(@"Graphics\Platforms\hus5", new Rectangle(283, 266, 1149, 1000));
            platformFiles.Add(@"Graphics\Platforms\hus6", new Rectangle(115, 175, 984, 1000));
            platformFiles.Add(@"Graphics\Platforms\hus7", new Rectangle(20, 500, 2200, 1000));

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
            string filename = platformFiles.ElementAt(random.Next(platformFiles.Count)).Key;
            Rectangle rect = platformFiles.ElementAt(random.Next(platformFiles.Count)).Value;
            Platform lastPlatform = (Platform)findLastOfType("Platform").Value;
            float posX = lastPlatform.position.X + lastPlatform.Rectangle.Width + random.Next(200, 300);
            Vector2 position = new Vector2(posX, Runner.HEIGHT + 800f);

            platform = new Platform(game, spriteBatch, filename, position, rect);
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
            Vector2 position = new Vector2(backgroundList.Last.Value.Rectangle.Right, Runner.HEIGHT);

            Background background = new Background(@"Graphics\Backgrounds\Background", spriteBatch, game, position);
            background.Initialize();
            return background;
        }

        public void Terminate()
        {
            player = null;
            platform = null;
            entityList.Clear();
        }

        public void Update(GameTime gameTime)
        {
            foreach (Entity entity in entityList)
            {
                entity.Update(gameTime);
            }

            foreach (Background background in backgroundList)
            {
                background.Update(gameTime);
            }

            Platform tempPlatform = (Platform)findFirstOfType("Platform").Next.Value;
            if (tempPlatform.Rectangle.Right < player.position.X - Runner.WIDTH)
            {
                refreshPlatforms();
            }

            if (backgroundList.First.Next.Value.Rectangle.Right < player.position.X - Runner.WIDTH)
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

            player.Draw(gameTime);
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
                    if (!(p.Intersects(tmpPlat.HitRectangle)))
                    {
                        player.falling = true;
                    }
                    else if (p.Intersects(tmpPlat.HitRectangle))
                    {
                        SideCollided sides = GetSidesCollided(player.Rectangle, tmpPlat.HitRectangle);
                        if ((int)sides % 2 == (int)SideCollided.Top)
                        {
                            player.position.Y = (tmpPlat.HitRectangle.Top - p.Height / 2) - 2;
                            player.falling = false;
                        }
                        if (sides == SideCollided.Left)
                        {
                            player.position.X = (tmpPlat.HitRectangle.Left - p.Width / 2) - 2;
                            player.falling = true;
                        }
                        return;
                    }
                }
            }
        }

        public enum SideCollided
        {
            None = 0x00,
            Top = 0x01,
            Bottom = 0x02,
            Left = 0x04,
            Right = 0x08,
        }

        public static SideCollided GetSidesCollided(Rectangle sourceRect, Rectangle targetRect)
        {
            Vector2 targetCenter = new Vector2(targetRect.Center.X, targetRect.Center.Y);
            Vector2 sourceCenter = new Vector2(sourceRect.Center.X, sourceRect.Center.Y);

            SideCollided returnVal = SideCollided.None;

            // test left side  
            if (sourceRect.Right > targetRect.Left && sourceRect.Left < targetRect.Left &&
                sourceRect.Bottom > targetRect.Top && sourceRect.Top < targetRect.Bottom)
                returnVal = (returnVal | SideCollided.Left);

            // test top side  
            if (sourceRect.Center.X > targetRect.Left && sourceRect.Center.X < targetRect.Right &&
                sourceRect.Bottom > targetRect.Top && sourceRect.Top - 500 < targetRect.Top)
                returnVal = (returnVal | SideCollided.Top);

            //// test right side  
            //if (sourcePoint.X > centerLocation.X && sourcePoint.X < targetRect.Right &&
            //    sourcePoint.Y > targetRect.Top && sourcePoint.Y < targetRect.Bottom)
            //    returnVal = (returnVal | SideCollided.Right);

            //// test bottom side  
            //if (sourcePoint.X > targetRect.Left && sourcePoint.X < targetRect.Right &&
            //    sourcePoint.Y > centerLocation.Y && sourcePoint.Y < targetRect.Bottom)
            //    returnVal = (returnVal | SideCollided.Bottom);


            return returnVal;
        } 
    }
}
