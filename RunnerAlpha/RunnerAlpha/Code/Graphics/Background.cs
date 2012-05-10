﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace RunnerAlpha.Code.Graphics
{
    class Background : Sprite
    {
        string _filename;
        public int velocityX = 0;

        public Background(String filename, SpriteBatch spriteBatch, Runner game, Vector2 position)
            : base(spriteBatch, game)
        {
            _filename = filename;
            base.position = position;
        }

        public Background(String filename, SpriteBatch spriteBatch, Runner game, Vector2 position, int velocityX)
            : base(spriteBatch, game)
        {
            _filename = filename;
            this.velocityX = velocityX;
            base.position = position;
        }

        protected override void LoadContent()
        {
            SourceTexture = Game.Content.Load<Texture2D>(_filename);
            Origin = new Vector2(SourceRectangle.Left, SourceRectangle.Center.Y * 1.5f);
            scale *= 2f;
        }

        public override void Update(GameTime gameTime)
        {
            position.X -= velocityX;
            base.Update(gameTime);
        }
    }
}
