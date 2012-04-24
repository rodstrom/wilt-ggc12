using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RunnerAlpha.Code.Lines;
using RunnerAlpha.Code.Graphics;

namespace RunnerAlpha.Code.Entities
{
    class EntityManager
    {
        Runner game;
        SpriteBatch spriteBatch;

        public Player player;
        LinkedList<Platform> platforms = new LinkedList<Platform>();
        List<String> platformFiles = new List<String>();
        Random random = new Random();

        Background background;

        //LinkedList<GroundLine> groundLines = new LinkedList<GroundLine>();

        public EntityManager(Runner game, SpriteBatch spriteBatch)
        {
            this.game = game;
            this.spriteBatch = spriteBatch;

            player = new Player(game, spriteBatch, @"Graphics\running", new Vector2(100f, 900));

            background = new Background(@"Graphics\Background", spriteBatch, game);

            platformFiles.Add(@"Graphics\3");
            platformFiles.Add(@"Graphics\buildingDoor");
            platformFiles.Add(@"Graphics\buildingStupranna");
            platformFiles.Add(@"Graphics\Start");

            platforms.AddLast(new Platform(game, spriteBatch, @"Graphics\3", new Vector2(0f, 1200f)));
            for (int i = 0; i < 9; i++)
            {
                platforms.AddLast( addPlatform() );
            }
        }

        private void refreshPlatforms()
        {
            platforms.RemoveFirst();
            platforms.AddLast( addPlatform() );
        }

        private Platform addPlatform()
        {
            string filename = platformFiles[random.Next(platformFiles.Count)];
            Platform lastPlatform = platforms.Last.Value;
            float posX = lastPlatform.position.X + lastPlatform.rect.Width + 0f;
            Vector2 position = new Vector2(posX, 1200f);

            return new Platform(game, spriteBatch, filename, position);
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

            if (platforms.First.Value.rect.Right < game.Camera.Position.X - Runner.WIDTH / 2)
            {
                refreshPlatforms();
            }

            CollisionCheck();
        }

        public void Draw()
        {
            background.Draw(new Vector2(game.Camera.Position.X - Runner.WIDTH / 2, 0f));

            player.Draw();

            foreach (Platform p in platforms)
            {
                p.Draw();
            }
        }

        private void CollisionCheck()
        {
            PlayerOutOfBoundsCheck();

            Rectangle p = player.rect;
            player.falling = false;
            foreach (Platform pl in platforms)
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
        }

        private void PlayerOutOfBoundsCheck()
        {
            if (player.position.X + player.rect.Width / 2 > Runner.WIDTH)
            {
                //player.position.X = Runner.WIDTH - player.rect.Width / 2;
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
