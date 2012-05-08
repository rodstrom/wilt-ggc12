using System;
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

        public Background(String filename, SpriteBatch spriteBatch, Runner game, Vector2 position)
            : base(spriteBatch, game)
        {
            _filename = filename;
            base.position = position;
        }

        protected override void LoadContent()
        {
            SourceTexture = Game.Content.Load<Texture2D>(_filename);
        }

        public override void Update(GameTime gameTime)
        {
            position.X -= 1f;

            base.Update(gameTime);
        }
    }
}
