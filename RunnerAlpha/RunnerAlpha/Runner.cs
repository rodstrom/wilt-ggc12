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
using RunnerAlpha.Code.Input;
using RunnerAlpha.Code.States;
using RunnerAlpha.Code.Audio;

namespace RunnerAlpha
{
    public class Runner : Microsoft.Xna.Framework.Game
    {
        public const int WIDTH = 1920;
        public const int HEIGHT = 1200;

        //EntityManager entityManager;
        StateManager stateManager;

        AudioManager audioManager = null;

        //Timer timer = null;

        public GraphicsDeviceManager graphics;
        //SpriteBatch spriteBatch;

        //SpriteFont font = null;

        //Texture2D background;

        public AudioManager AudioManager
        {
            get { return audioManager; }
        }

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
            //spriteBatch = new SpriteBatch(GraphicsDevice);

            //entityManager = new EntityManager(this, spriteBatch);

            stateManager = new StateManager(this);

            audioManager = new AudioManager(this);
            audioManager.LoadNewEffect("Jump", @"Audio\Sound\Jump");
            audioManager.LoadNewMusic("Level1", @"Audio\Music\Level 1");
            audioManager.LoadNewMusic("Level3", @"Audio\Music\Level3");
            audioManager.LoadNewMusic("Menu", @"Audio\Music\Menu");
            audioManager.SetMusic("Level3");
            audioManager.PlayMusic();

            Resolution.Init(ref graphics);
            Resolution.SetResolution(1280, 800, false);

            //timer = new Timer(this);
            //timer.StartTimer();

            //font = this.Content.Load<SpriteFont>(@"Fonts\font");

            //background = this.Content.Load<Texture2D>(@"Graphics\Background");
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            stateManager.Update(gameTime);

            //entityManager.Update(gameTime);

            //timer.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            stateManager.Draw();

            base.Draw(gameTime);
        }
    }
}