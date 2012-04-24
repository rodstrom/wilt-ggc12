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
        String text;

        public HighScoreState(Runner game, String id)
            : base(game, id)
        {
            nextState = "PlayState";
        }

        public override void Terminate()
        {
            changeState = false;
        }

        public override void Update(GameTime gameTime)
        {
            inputManager.Update();

            if (inputManager.Pause)
            {
                changeState = true;
            }
        }

        public override void Draw()
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, null, null, null, null, Resolution.getTransformationMatrix());

            //spriteBatch.Draw(background, new Vector2(0, 0), Color.White);
            String time = "This is le highscore!";
            if (inputCode > 0)
            {
                text = "Congratulations, you won and it took you " + inputCode + " seconds!";
            }
            else
            {
                text = "Sorry bro, you suck...";
            }

            spriteBatch.DrawString(font, time, new Vector2(100, 100), Color.Red, 0f, Vector2.Zero, 3f, SpriteEffects.None, 1f);
            spriteBatch.DrawString(font, text, new Vector2(100, 300), Color.Red, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);

            spriteBatch.End();
        }
    }
}
