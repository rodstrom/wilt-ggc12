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
        public Background(String filename, SpriteBatch spriteBatch, Runner game)
            : base(game, spriteBatch, filename)
        {
        }
    }
}
