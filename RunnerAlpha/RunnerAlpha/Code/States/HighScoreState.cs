using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RunnerAlpha.Code.Graphics;

namespace RunnerAlpha.Code.States
{
    class HighScoreState : State
    {
        public HighScoreState(Runner game, String id)
            : base(game, id)
        {

        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw()
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, null, null, null, null, Resolution.getTransformationMatrix());

            //spriteBatch.Draw(background, new Vector2(0, 0), Color.White);
            String time = "This is le highscore!";
            spriteBatch.DrawString(font, time, new Vector2(100, 100), Color.Red, 0f, Vector2.Zero, 3f, SpriteEffects.None, 1f);

            spriteBatch.End();
        }
    }
}
