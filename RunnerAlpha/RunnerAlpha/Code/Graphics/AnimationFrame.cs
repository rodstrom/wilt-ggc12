using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RunnerAlpha.Code.Graphics
{
    class AnimationFrame
    {
        public Texture2D SourceTexture
        {
            get;
            set;
        }

        public Rectangle SourceRectangle
        {
            get;
            set;
        }

        public AnimationFrame(Texture2D tex, Rectangle rect)
        {
            SourceTexture = tex;
            SourceRectangle = rect;
        }
    }
}
