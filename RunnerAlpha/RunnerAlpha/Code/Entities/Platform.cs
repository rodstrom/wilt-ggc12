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
                Rectangle r = Rectangle;
                r.X = r.Left + _hitRectangle.X;
                r.Y = r.Top + _hitRectangle.Y;
                r.Width = r.Left + _hitRectangle.Width - r.X;
                r.Height = r.Bottom;
                return r;
            }
            set
            {
                _hitRectangle = value;
            }
        }

        public Platform(Runner game, SpriteBatch spriteBatch, string filename, Vector2 position, Rectangle hitRectangle)
            : base(spriteBatch, game)
        {
            this.position = position;
            this._filename = filename;
            this._hitRectangle = hitRectangle;
        }

        protected override void LoadContent()
        {
            SourceTexture = Game.Content.Load<Texture2D>(_filename);
            Origin = new Vector2(SourceRectangle.Left, SourceRectangle.Bottom);



            TEMPrect = new Texture2D(Runner.graphics.GraphicsDevice, HitRectangle.Width, HitRectangle.Height);
            TEMPdata = new Color[HitRectangle.Width * HitRectangle.Height];
            for (int i = 0; i < TEMPdata.Length; ++i) TEMPdata[i] = Color.Red;
            TEMPrect.SetData(TEMPdata);
        }

        private Color[] TEMPdata;
        private Texture2D TEMPrect;

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            Vector2 coor = new Vector2(HitRectangle.Left, HitRectangle.Top);
            SpriteBatch.Draw(TEMPrect, coor, Color.White);
        }
    }
}
