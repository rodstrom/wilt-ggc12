using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RunnerAlpha.Code.Input;

namespace RunnerAlpha.Code.States
{
    class State
    {
        protected Runner game;
        String id;

        protected InputManager inputManager;

        protected SpriteBatch spriteBatch;
        protected SpriteFont font;

        public State(Runner game, String id)
        {
            this.id = id;
            this.game = game;

            LoadContent();
        }

        protected void LoadContent()
        {
            inputManager = new InputManager();

            spriteBatch = new SpriteBatch(game.GraphicsDevice);

            font = game.Content.Load<SpriteFont>(@"Fonts\font");
        }

        public virtual void Update(GameTime gameTime)
        {
            inputManager.Update();

            //if (inputManager.Quit)
            //{
            //    game.Exit();
            //}
        }

        public virtual void Draw()
        {

        }

        public String ID
        {
            get
            {
                return id;
            }
        }
    }
}
