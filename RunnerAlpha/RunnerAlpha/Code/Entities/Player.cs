using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using RunnerAlpha.Code.Input;
using RunnerAlpha.Code.Graphics;

namespace RunnerAlpha.Code.Entities
{
    class Player : Animation
    {
        InputManager input;

        Vector2 kinetics;

        float fallTime;
        float runTime;

        public bool falling;

        public bool win = false;
        public bool lose = false;

        public Player(Runner game, SpriteBatch spriteBatch, Vector2 position)
            : base(game, spriteBatch)
        {
            this.input = new InputManager(game);
            this.fallTime = 0f;
            this.runTime = 0f;
            this.position = position;
            kinetics = Vector2.Zero;
        }

        protected override void LoadContent()
        {
            AnimationStrip _runningAnim = new AnimationStrip();
            Texture2D _tmpSource = Game.Content.Load<Texture2D>(@"Graphics\Spritesheets\RunningSheet");

            for (int x = 0; x < 7; x++)
            {
                _runningAnim.AddFrame(new AnimationFrame(_tmpSource, new Rectangle(64 * x, 0, 64, 64)));
            }
            
            _runningAnim.TimeOnChange = 50;
            this.AddAnimation("Running", _runningAnim);
        }

        public override void Update(GameTime gameTime)
        {
            if (this.AnimationName != "Running")
            {
                this.AnimationName = "Running";
            }

            runTime += gameTime.ElapsedGameTime.Milliseconds;

            if (kinetics.X < 25)
            {
                kinetics.X = 25;
            }
            if (kinetics.X < 200f)
            {
                kinetics.X += (((runTime / 1000) * (runTime / 1000)) * 0.1f);
            }
            if (kinetics.X > 250f)
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
                kinetics.Y -= 80f;
            }
            if (input.Left)
            {
                kinetics.X -= 50f;
            }
            if (input.Space)
            {
                Runner.AudioManager.PlayEffect("Jump");
            }
        }
    }
}
