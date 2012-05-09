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
        Dictionary<String, Rectangle[]> platformFiles = new Dictionary<String, Rectangle[]>();
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
            
            Background background = new Background(@"Graphics\Backgrounds\Background", spriteBatch, game, new Vector2(-300f, Runner.HEIGHT));
            background.Initialize();
            backgroundList.AddLast(background);
            backgroundList.AddLast(addBackground());
            backgroundList.AddLast(addBackground());

            platform = new Platform(game, spriteBatch, @"Graphics\Platforms\hus1", new Vector2(-100f, Runner.HEIGHT + 600f), new Rectangle(163, 216, 1030, 1000));
            platform.Initialize();
            entityList.AddLast(platform);

            Rectangle[] r = new Rectangle[1];
            r[0] = new Rectangle(163, 216, 1030, 1000);
            platformFiles.Add(@"Graphics\Platforms\hus1", r);
            r[0] = new Rectangle(0, 112, 1190, 1000);
            platformFiles.Add(@"Graphics\Platforms\hus3", r);
            r[0] = new Rectangle(0, 1331, 634, 1000);
            platformFiles.Add(@"Graphics\Platforms\hus4", r);
            r[0] = new Rectangle(120, 175, 990, 1000);
            platformFiles.Add(@"Graphics\Platforms\hus6", r);
            r = new Rectangle[2];
            r[0] = new Rectangle(10, 480, 920, 1000);
            r[1] = new Rectangle(685, 5, 1429, 20);
            platformFiles.Add(@"Graphics\Platforms\hus2", r);
            r[0] = new Rectangle(0, 1020, 1429, 1000);
            r[1] = new Rectangle(280, 266, 1147, 20);
            platformFiles.Add(@"Graphics\Platforms\hus5", r);
            r[0] = new Rectangle(15, 495, 1125, 1000);
            r[1] = new Rectangle(1125, 470, 2160, 1000);
            platformFiles.Add(@"Graphics\Platforms\hus7", r);

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
            int rand = random.Next(platformFiles.Count);
            string filename = platformFiles.ElementAt(rand).Key;
            Rectangle[] rect = platformFiles.ElementAt(rand).Value;
            Platform lastPlatform = (Platform)findLastOfType("Platform").Value;
            float posX = lastPlatform.position.X + lastPlatform.Rectangle.Width + random.Next(0, 0);
            Vector2 position = new Vector2(posX, Runner.HEIGHT + 600f);
            if (rect.Length == 1)
            {
                platform = new Platform(game, spriteBatch, filename, position, rect[0]);
            }
            else
            {
                platform = new Platform(game, spriteBatch, filename, position, rect);
            }
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

            player.falling = false;
            foreach(Entity entity in entityList)
            {
                if (entity.GetType().Name == "Platform")
                {
                    Platform tmpPlat = (Platform)entity;
                    if (!(player.Rectangle.Intersects(tmpPlat.HitRectangle)))
                    {
                        player.falling = true;
                    }
                    else if (player.Rectangle.Intersects(tmpPlat.HitRectangle))
                    {
                        SideCollided sides = GetSidesCollided(player.Rectangle, tmpPlat.HitRectangle);
                        if ((int)sides % 2 == (int)SideCollided.Top)
                        {
                            player.position.Y = (tmpPlat.HitRectangle.Top - player.Rectangle.Height / 2) - 2;
                            player.falling = false;
                        }
                        if (sides == SideCollided.Left)
                        {
                            player.position.X = (tmpPlat.HitRectangle.Left - player.Rectangle.Width / 2) - 2;
                            player.falling = true;
                        }
                        return;
                    }
                    if(tmpPlat.hitRect2Enabled)
                    {
                        if (!(player.Rectangle.Intersects(tmpPlat.HitRectangle2)))
                        {
                            player.falling = true;
                        }
                        else if (player.Rectangle.Intersects(tmpPlat.HitRectangle2))
                        {
                            SideCollided sides = GetSidesCollided(player.Rectangle, tmpPlat.HitRectangle2);
                            if ((int)sides % 2 == (int)SideCollided.Top)
                            {
                                player.position.Y = (tmpPlat.HitRectangle2.Top - player.Rectangle.Height / 2) - 2;
                                player.falling = false;
                            }
                            return;
                        }
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
