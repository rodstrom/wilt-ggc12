 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RunnerAlpha.Code.Graphics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace RunnerAlpha.Code.Entities
{
    class Platform : Sprite
    {
        string _filename;

        public Platform(Runner game, SpriteBatch spriteBatch, string filename, Vector2 position)
            : base(spriteBatch, game)
        {
            this.position = position;
            _filename = filename;
        }

        protected override void LoadContent()
        {
            SourceTexture = Game.Content.Load<Texture2D>(_filename);
            Origin = new Vector2(0f, base.SourceRectangle.Height);
        }
    }
}
