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
        float fallTime;

        public bool falling = false;

        public Player(Runner game, SpriteBatch spriteBatch, string filename, Vector2 position)
            : base(game, spriteBatch, filename, position)
        {
            this.input = new InputManager();
            this.fallTime = 0f;
        }

        public override void Update(GameTime gameTime)
        {
            if (falling)
            {
                fallTime += gameTime.ElapsedGameTime.Milliseconds;
            }
            else
            {
                fallTime = 0f;
            }

            input.Update();
            Move(gameTime);
            base.Update(gameTime);
        }

        private void Move(GameTime gameTime)
        {
            if (falling)
            {
                position.Y += ((fallTime / 200) * (fallTime / 200));
            }

            if (input.Left)
            {
                position.X -= gameTime.ElapsedGameTime.Milliseconds * 0.7f;
            }
            if (input.Right)
            {
                position.X += gameTime.ElapsedGameTime.Milliseconds * 0.7f;
            }
            if (input.Up){
                position.Y -= 10;
            }
        }
    }
}
