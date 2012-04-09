using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using RunnerAlpha.Code.Graphics;
using Microsoft.Xna.Framework;

namespace RunnerAlpha.Code.Entities
{
    class Entity
    {
        public Runner game;
        public Sprite sprite;
        public Vector2 position;
        public Rectangle rect;
        public Vector2 origin;
        public float layer;

        public bool falling = false;

        public Entity(Runner game, SpriteBatch spriteBatch, string filename, Vector2 position)
        {
            this.game = game;
            this.sprite = new Sprite(game, spriteBatch, filename);
            this.position = position;

            this.rect = new Rectangle((int)this.position.X, (int)this.position.Y, (int)this.sprite.Dimension.X, (int)this.sprite.Dimension.Y);
            this.origin = this.sprite.Dimension / 2;
            this.layer = 1f;
        }

        public virtual void Update(GameTime gameTime)
        {
            this.sprite.Update(gameTime);
            this.rect.X = (int)this.position.X - (int)this.sprite.Dimension.X / 2;
            this.rect.Y = (int)this.position.Y - (int)this.sprite.Dimension.Y / 2;
        }

        public virtual void Draw()
        {
            this.sprite.Draw(this.position, this.rect, Color.White, 0f, this.origin, 1f, SpriteEffects.None, this.layer, null);
        }

    }
}
