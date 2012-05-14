using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RunnerAlpha.Code.Camera;
using RunnerAlpha.Code.Entities;

namespace RunnerAlpha.Code.Graphics
{
    public class Sprite : Entity
    {
        private Texture2D sourceTexture = null;
        private Rectangle sourceRectangle = Rectangle.Empty;
        //private Color[] colorData = null;
        //public Color color = Color.White;

        public Vector2 scale = new Vector2(1.0f, 1.0f);

        public Rectangle Rectangle
        {
            get
            {
                int x = (int)(position.X - Origin.X);
                int y = (int)(position.Y - Origin.Y);
                return new Rectangle(x, y, SourceTexture.Width, SourceTexture.Height);
            }
        }

        public Sprite(SpriteBatch spriteBatch, Runner game)
            : base(game, spriteBatch)
        {
            sourceTexture = null;
        }

        //public Color[] ColorData
        //{
        //    get { return colorData; }
        //}

        public Texture2D SourceTexture
        {
            get { return sourceTexture; }

            set
            {
                sourceTexture = value;

                if (sourceTexture != null)
                {
                    sourceRectangle.X = 0;
                    sourceRectangle.Y = 0;
                    sourceRectangle.Width = sourceTexture.Width;
                    sourceRectangle.Height = sourceTexture.Height;

                    //colorData = new Color[sourceRectangle.Width * sourceRectangle.Height];
                    //sourceTexture.GetData(colorData);
                }
            }
        }

        public Rectangle SourceRectangle
        {
            get { return sourceRectangle; }
            set
            {
                sourceRectangle = value;

                //if (sourceTexture != null)
                //{
                //    colorData = new Color[sourceRectangle.Width * sourceRectangle.Height];
                //    sourceTexture.GetData(colorData);
                //}
            }
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Draw(SourceTexture, position, SourceRectangle, Color.White, Rotation, Origin, scale, SpriteEffects.None, 0.0f);
        }
    }
}
