using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using RunnerAlpha.Code.Input;

namespace RunnerAlpha.Code.Entities
{
    class Player : Entity
    {
        InputManager input;

        Vector2 kinetics;

        float fallTime;
        float runTime;

        public bool falling;

        public bool win = false;
        public bool lose = false;

        public Player(Runner game, SpriteBatch spriteBatch, string filename, Vector2 position)
            : base(game, spriteBatch, filename, position)
        {
            this.input = new InputManager(game);
            this.fallTime = 0f;
            this.runTime = 0f;
            kinetics = Vector2.Zero;
        }

        public override void Update(GameTime gameTime)
        {
            runTime += gameTime.ElapsedGameTime.Milliseconds;

            if (kinetics.X < 25)
            {
                kinetics.X = 25;
            }
            if (kinetics.X < 490f)
            {
                kinetics.X += (((runTime / 1000) * (runTime / 1000)) * 0.1f);
            }
            if (kinetics.X > 510f)
            {
                kinetics.X -= (((runTime / 1000) * (runTime / 1000)) * 0.1f);
            }

            if (falling)
            {
                fallTime += gameTime.ElapsedGameTime.Milliseconds;
                kinetics.Y += ((fallTime / 1000) * (fallTime / 1000) * 100);
            }
            else
            {
                fallTime = 0;
            }
            
            input.Update();
            Move(gameTime);
            base.Update(gameTime);
        }

        private void Move(GameTime gameTime)
        {
            position.Y += (kinetics.Y / 10);
            position.X += (kinetics.X / 10);

            if (input.Up && !falling)
            {
                kinetics.Y -= 200f;
            }
            if (input.Left)
            {
                kinetics.X -= 50f;
            }
            if (input.Space)
            {
                game.AudioManager.PlayEffect("Jump");
            }
        }
    }
}
