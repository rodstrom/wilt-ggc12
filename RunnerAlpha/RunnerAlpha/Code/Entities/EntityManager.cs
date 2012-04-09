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
        LinkedList<Entity> entities = new LinkedList<Entity>();
        LinkedListNode<Entity> player;

        public EntityManager(Runner game, SpriteBatch spriteBatch)
        {
            player = entities.AddLast(new Player(game, spriteBatch, "running", new Vector2(100f)));
            entities.AddLast(new Platform(game, spriteBatch, "Start", new Vector2(90f, 1080)));
            entities.AddLast(new Platform(game, spriteBatch, "buildingStupranna", new Vector2(800f, 1000)));
            entities.AddLast(new Platform(game, spriteBatch, "3", new Vector2(1300f, 1150)));
            entities.AddLast(new Platform(game, spriteBatch, "buildingDoor", new Vector2(1870f, 1100)));
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
            PlayerOutOfBoundsCheck();

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

        private void PlayerOutOfBoundsCheck()
        {
            if (player.Value.position.X + player.Value.rect.Width / 2 > Runner.WIDTH)
            {
                player.Value.position.X = Runner.WIDTH - player.Value.rect.Width / 2;
            }
            if (player.Value.position.X - player.Value.rect.Width / 2 < 0f)
            {
                player.Value.position.X = player.Value.rect.Width / 2;
            }
            if (player.Value.position.Y + player.Value.rect.Height / 2 > Runner.HEIGHT)
            {
                player.Value.position.X = 100f;
                player.Value.position.Y = 100f;
            }
            if (player.Value.position.Y - player.Value.rect.Height / 2 < -100f)
            {
                player.Value.position.Y = -100f;
            }
        }
    }
}
