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

        private Rectangle _hitRectangle;
        public Rectangle HitRectangle
        {
            get
            {
                Rectangle r = new Rectangle();
                r.X = Rectangle.Left + _hitRectangle.X;
                r.Y = Rectangle.Top + _hitRectangle.Y;
                r.Width = _hitRectangle.Width - _hitRectangle.X;
                r.Height = Rectangle.Bottom - r.Y;
                return r;
            }
        }

        public bool hitRect2Enabled;
        private Rectangle _hitRectangle2;
        public Rectangle HitRectangle2
        {
            get
            {
                if (hitRect2Enabled)
                {
                    Rectangle r = new Rectangle();
                    r.X = Rectangle.Left + _hitRectangle2.X;
                    r.Y = Rectangle.Top + _hitRectangle2.Y;
                    r.Width = _hitRectangle2.Width - _hitRectangle2.X;
                    r.Height = _hitRectangle2.Height;
                    return r;
                }
                return _hitRectangle2;
            }
        }

        public Platform(Runner game, SpriteBatch spriteBatch, string filename, Vector2 position, Rectangle hitRectangle)
            : base(spriteBatch, game)
        {
            this.position = position;
            this._filename = filename;
            this._hitRectangle = hitRectangle;
            this._hitRectangle2 = new Rectangle(0, 0, 0, 0);
            this.hitRect2Enabled = false;
        }

        public Platform(Runner game, SpriteBatch spriteBatch, string filename, Vector2 position, Rectangle[] hitRectangles)
            : base(spriteBatch, game)
        {
            this.position = position;
            this._filename = filename;
            this._hitRectangle = hitRectangles[0];
            this._hitRectangle2 = hitRectangles[1];
            this.hitRect2Enabled = true;
        }

        protected override void LoadContent()
        {
            SourceTexture = Game.Content.Load<Texture2D>(_filename);
            Origin = new Vector2(SourceRectangle.Left, SourceRectangle.Bottom);

            t = new Texture2D(GraphicsDevice, 1, 1);
            t.SetData(new[] { Color.White }); 
        }

        Texture2D t;
        Rectangle r;

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            if (bool.Parse(Runner.config.getValue("Debug", "Hitbox")))
            {
                r = HitRectangle;
                SpriteBatch.Draw(t, new Rectangle(r.Left, r.Top, 4, r.Height), Color.Red); // Left
                SpriteBatch.Draw(t, new Rectangle(r.Right, r.Top, 4, r.Height), Color.Red); // Right
                SpriteBatch.Draw(t, new Rectangle(r.Left, r.Top, r.Width, 4), Color.Red); // Top
                SpriteBatch.Draw(t, new Rectangle(r.Left, r.Bottom, r.Width, 4), Color.Red); // Bottom

                if (hitRect2Enabled)
                {
                    r = HitRectangle2;
                    SpriteBatch.Draw(t, new Rectangle(r.Left, r.Top, 4, r.Height), Color.Red); // Left
                    SpriteBatch.Draw(t, new Rectangle(r.Right, r.Top, 4, r.Height), Color.Red); // Right
                    SpriteBatch.Draw(t, new Rectangle(r.Left, r.Top, r.Width, 4), Color.Red); // Top
                    SpriteBatch.Draw(t, new Rectangle(r.Left, r.Bottom, r.Width, 4), Color.Red); // Bottom
                }
            }
        }
    }
}
