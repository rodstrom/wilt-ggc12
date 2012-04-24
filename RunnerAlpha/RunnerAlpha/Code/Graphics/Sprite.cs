using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RunnerAlpha.Code.Camera;

namespace RunnerAlpha.Code.Graphics
{
    public class Sprite : DrawableGameComponent
    {
        private Texture2D texture;
        private SpriteBatch spriteBatch;
        private Runner runner;

        public Sprite(Runner game, SpriteBatch spriteBatch, string filename)
            : base(game)
        {
            this.spriteBatch = spriteBatch;
            runner = game;
            texture = Game.Content.Load<Texture2D>(filename);
        }

        public void Draw(Vector2 position, Rectangle rect, Color color, float rotation, Vector2 origin, float scale, SpriteEffects spriteEffects, float layer, Effect effect)
        {
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, null, null, null, null, runner.Camera.Transform);
            spriteBatch.Draw(texture, position, rect, color, rotation, origin, scale, spriteEffects, layer);
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, null, null, null, null, runner.Camera.Transform);
        }

        public Vector2 Dimension
        {
            get
            {
                return new Vector2((float)texture.Width, (float)texture.Height);
            }
        }
    }
}
