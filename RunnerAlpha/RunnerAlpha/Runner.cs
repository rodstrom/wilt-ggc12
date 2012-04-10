using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using RunnerAlpha.Code.Graphics;
using RunnerAlpha.Code.Entities;
using RunnerAlpha.Code.Input;
using RunnerAlpha.Code.Timer;

namespace RunnerAlpha
{
    public class Runner : Microsoft.Xna.Framework.Game
    {
        public const int WIDTH = 1920;
        public const int HEIGHT = 1200;

        InputManager input;
        EntityManager entities;

        Timer timer = null;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        SpriteFont font = null;

        Texture2D background;


        public Runner()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            this.IsMouseVisible = true;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            entities = new EntityManager(this, spriteBatch);
            input = new InputManager();

            Resolution.Init(ref graphics);
            Resolution.SetResolution(1280, 800, false);

            timer = new Timer(this);
            timer.StartTimer();

            font = this.Content.Load<SpriteFont>(@"Fonts\font");

            background = this.Content.Load<Texture2D>(@"Graphics\Background");
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            entities.Update(gameTime);

            timer.Update(gameTime);

            input.Update();
            if (input.Quit)
            {
                this.Exit();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, null, null, null, null, Resolution.getTransformationMatrix());
            
            spriteBatch.Draw(background, new Vector2(0, 0), Color.White);
            int time = (int)timer.MainEvent.currentTime;
            spriteBatch.DrawString(font, (time / 1000).ToString(), new Vector2(100, 100), Color.Red);

            entities.Draw();

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}