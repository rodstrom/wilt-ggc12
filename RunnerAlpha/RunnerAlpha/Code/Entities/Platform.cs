 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RunnerAlpha.Code.Graphics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace RunnerAlpha.Code.Entities
{
    class Platform : Entity
    {
        public Platform(Runner game, SpriteBatch spriteBatch, string filename, Vector2 position)
            : base(game, spriteBatch, filename, position)
        {
            origin = new Vector2(0f, sprite.Dimension.Y);
        }
    }
}
