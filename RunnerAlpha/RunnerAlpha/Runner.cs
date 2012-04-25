﻿using System;
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
using RunnerAlpha.Code.Camera;
using RunnerAlpha.Code.Time;

namespace RunnerAlpha
{
    public class Runner : Microsoft.Xna.Framework.Game
    {
        public const int WIDTH = 1920;
        public const int HEIGHT = 1200;

        public InputFile config;

        StateManager stateManager;

        AudioManager audioManager = null;

        Timer timer;

        public GraphicsDeviceManager graphics;

        Camera2D camera = null;
        //SpriteBatch spriteBatch;

        //SpriteFont font = null;

        //Texture2D background;

        public Timer Timer
        {
            get { return timer; }
        }

        public Camera2D Camera
        {
            get { return camera; }
        }

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
            config = new InputFile(@"Content\Configs\config.ini");
            config.parse();

            timer = new Timer(this);

            audioManager = new AudioManager(this);
            audioManager.LoadNewEffect("Jump", @"Audio\Sound\Jump");
            audioManager.LoadNewMusic("Level1", @"Audio\Music\Level 1");
            audioManager.LoadNewMusic("Level3", @"Audio\Music\Level3");
            audioManager.LoadNewMusic("Menu", @"Audio\Music\Menu");
            audioManager.SetMusic("Level3");
            audioManager.PlayMusic();

            Resolution.Init(ref graphics);
            Resolution.SetResolution(
                int.Parse(config.getValue("Video", "Width")),
                int.Parse(config.getValue("Video", "Height")),
                bool.Parse(config.getValue("Video", "Fullscreen"))
                );

            Viewport view = new Viewport(0, 0, 1280, 800);
            camera = new Camera2D(view, 0.5f, 0f, this);

            LineBatch.Init(GraphicsDevice);

            stateManager = new StateManager(this);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            stateManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            stateManager.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}