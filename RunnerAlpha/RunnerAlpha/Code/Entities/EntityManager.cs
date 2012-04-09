﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RunnerAlpha.Code.Entities
{
    class EntityManager
    {
        LinkedList<Entity> entities = new LinkedList<Entity>();
        LinkedListNode<Entity> player;

        public EntityManager(Runner game, SpriteBatch spriteBatch)
        {
            player = entities.AddLast(new Player(game, spriteBatch, "Guy", new Vector2(100f)));
            entities.AddLast(new Platform(game, spriteBatch, "platform", new Vector2(90f, 1200 * 0.75f)));
            entities.AddLast(new Platform(game, spriteBatch, "plattform2", new Vector2(800f, 1200 * 0.6f)));
            entities.AddLast(new Platform(game, spriteBatch, "plattform2", new Vector2(1850f, 1200 * 0.75f)));
        }

        public void Update(GameTime gameTime)
        {
            foreach (Entity e in entities)
            {
                e.Update(gameTime);
            }

            CollisionCheck();
        }

        public void Draw()
        {
            foreach (Entity e in entities)
            {
                e.Draw();
            }
        }

        private void CollisionCheck()
        {
            if (player.Value.position.X < Runner.WIDTH && player.Value.position.X > 0.0f &&
                player.Value.position.Y < Runner.HEIGHT && player.Value.position.Y > 0.0f)
            {
                Rectangle p = player.Value.rect;
                player.Value.falling = false;
                for (int i = 1; i < entities.Count; i++)
                {
                    if (!(p.Intersects(entities.ElementAt(i).rect)))
                    {
                        player.Value.falling = true;
                    }
                    else if (p.Intersects(entities.ElementAt(i).rect))
                    {
                        player.Value.position.Y = entities.ElementAt(i).rect.Y - p.Height / 2;
                        player.Value.falling = false;
                        return;
                    }
                }
            }
            else
            {
                PlayerOutOfBounds();
                //player.Value.position = new Vector2(100, 100);
            }
        }

        private void PlayerOutOfBounds()
        {
            if (player.Value.position.X + player.Value.rect.Width / 2 > Runner.WIDTH)
            {
                player.Value.position.X = Runner.WIDTH - player.Value.rect.Width / 2;
            }
            else if (player.Value.position.X - player.Value.rect.Width / 2 < 0f)
            {
                player.Value.position.X = player.Value.rect.Width / 2;
            }

            if (player.Value.position.Y + player.Value.rect.Height / 2 > Runner.HEIGHT)
            {
                player.Value.position.X = 100f;
                player.Value.position.Y = 100f;
            }
            else if (player.Value.position.Y - player.Value.rect.Height / 2 < 0f)
            {
                player.Value.position.Y = 0f;
            }
        }
    }
}
