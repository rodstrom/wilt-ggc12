using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RunnerAlpha.Code.Lines;

namespace RunnerAlpha.Code.Entities
{
    class EntityManager
    {
        public Player player;
        LinkedList<Platform> platforms = new LinkedList<Platform>();
        Rectangle goal;

        LinkedList<GroundLine> groundLines = new LinkedList<GroundLine>();

        public EntityManager(Runner game, SpriteBatch spriteBatch)
        {
            player = new Player(game, spriteBatch, @"Graphics\running", new Vector2(100f));

            platforms.AddLast(new Platform(game, spriteBatch, @"Graphics\Start", new Vector2(90f, 1080)));
            platforms.AddLast(new Platform(game, spriteBatch, @"Graphics\buildingStupranna", new Vector2(800f, 1000)));
            platforms.AddLast(new Platform(game, spriteBatch, @"Graphics\3", new Vector2(1300f, 1150)));
            platforms.AddLast(new Platform(game, spriteBatch, @"Graphics\buildingDoor", new Vector2(1870f, 1100)));

            groundLines.AddLast(new GroundLine(game, spriteBatch, new Rectangle(500, 500, 1500, 800)));

            goal = new Rectangle(1850, 900, 200, 200);
        }

        public void Terminate()
        {
            player = null;
            platforms.Clear();
        }

        public void Update(GameTime gameTime)
        {
            player.Update(gameTime);

            foreach (Platform p in platforms)
            {
                p.Update(gameTime);
            }

            CollisionCheck();
        }

        public void Draw()
        {
            player.Draw();

            foreach (Platform p in platforms)
            {
                p.Draw();
            }

            foreach (GroundLine gl in groundLines)
            {
                gl.Draw();
            }
        }

        private void CollisionCheck()
        {
            PlayerOutOfBoundsCheck();

            Rectangle p = player.rect;
            player.falling = false;
            foreach(Platform pl in platforms)
            {
                if (!(p.Intersects(pl.rect)))
                {
                    player.falling = true;
                }
                else if (p.Intersects(pl.rect))
                {
                    player.position.Y = (pl.rect.Y - p.Height / 2) + 2;
                    player.falling = false;
                    return;
                }
            }

            //INSERT LINE DETECTION, REMOVE PLATFORMS

            if (p.Intersects(goal))
            {
                player.win = true;
            }
        }

        private void PlayerOutOfBoundsCheck()
        {
            if (player.position.X + player.rect.Width / 2 > Runner.WIDTH)
            {
                player.position.X = Runner.WIDTH - player.rect.Width / 2;
            }
            if (player.position.X - player.rect.Width / 2 < 0f)
            {
                player.position.X = player.rect.Width / 2;
            }
            if (player.position.Y + player.rect.Height / 2 > Runner.HEIGHT)
            {
                player.lose = true;
            }
            if (player.position.Y - player.rect.Height / 2 < -100f)
            {
                player.position.Y = -100f;
            }
        }
    }
}
