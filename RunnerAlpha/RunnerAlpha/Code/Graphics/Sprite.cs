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
        private Color[] colorData = null;
        public Color color = Color.White;

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

        public Color[] ColorData
        {
            get { return colorData; }
        }

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

                    colorData = new Color[sourceRectangle.Width * sourceRectangle.Height];
                    sourceTexture.GetData(colorData);
                }
            }
        }

        public Rectangle SourceRectangle
        {
            get { return sourceRectangle; }
            set
            {
                sourceRectangle = value;

                if (sourceTexture != null)
                {
                    colorData = new Color[sourceRectangle.Width * sourceRectangle.Height];
                    sourceTexture.GetData(colorData);
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Draw(SourceTexture, position, SourceRectangle, color, Rotation, Origin, scale, SpriteEffects.None, 0.0f);
        }
    }
    //public class Sprite : DrawableGameComponent
    //{
    //    private Texture2D texture;
    //    private SpriteBatch spriteBatch;
    //    private Runner runner;

    //    public Sprite(Runner game, SpriteBatch spriteBatch, string filename)
    //        : base(game)
    //    {
    //        this.spriteBatch = spriteBatch;
    //        runner = game;
    //        texture = Game.Content.Load<Texture2D>(filename);
    //    }

    //    public void Draw(Vector2 position, Rectangle rect, Color color, float rotation, Vector2 origin, float scale, SpriteEffects spriteEffects, float layer, Effect effect)
    //    {
    //        spriteBatch.End();
    //        spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, null, null, null, null, runner.Camera.Transform);
    //        spriteBatch.Draw(texture, position, rect, color, rotation, origin, scale, spriteEffects, layer);
    //        spriteBatch.End();
    //        spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, null, null, null, null, runner.Camera.Transform);
    //    }

    //    public Vector2 Dimension
    //    {
    //        get
    //        {
    //            return new Vector2((float)texture.Width, (float)texture.Height);
    //        }
    //    }
    //}
}
