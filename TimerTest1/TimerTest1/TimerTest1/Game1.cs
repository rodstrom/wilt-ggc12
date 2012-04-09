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

namespace TimerTest1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Timer timerTest = null;
        SpriteFont font = null;
        InputManager inputManager = null;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            timerTest = new Timer(this);
            timerTest.Initialize();

            inputManager = new InputManager(this);

            IsMouseVisible = true;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            font = this.Content.Load<SpriteFont>("font");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            inputManager.Update(gameTime);

            timerTest.Update(gameTime);

            if(inputManager.justReleased(Keys.Escape))
                this.Exit();

            if (inputManager.justReleased(Keys.Q))
                timerTest.StartTimer();

            if (inputManager.justReleased(Keys.W))
                timerTest.StopTimer();

            if (inputManager.justReleased(Keys.R))
                timerTest.ResetTimer();

            if (inputManager.justReleased(Keys.D1))
            {
                if (timerTest.timerList.Count < 1)
                {
                    timerTest.CreateSubTimer();
                    return;
                }
                if (!timerTest.timerList[0].active)
                {
                    timerTest.StartSubTimer(0);
                }
                else
                {
                    timerTest.StopSubTimer(0);
                }
            }

            if (inputManager.justReleased(Keys.D2))
            {
                if (timerTest.timerList.Count < 2)
                {
                    timerTest.CreateSubTimer();
                    return;
                }
                if (!timerTest.timerList[1].active)
                {
                    timerTest.StartSubTimer(1);
                }
                else
                {
                    timerTest.StopSubTimer(1);
                }
            }

            if (inputManager.justReleased(Keys.Z))
            {
                timerTest.ResetSubTimer(5);
            }

            if (inputManager.justReleased(Keys.V))
                timerTest.RemoveSubTimer(0);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            spriteBatch.DrawString(font, TimeSpan.FromMilliseconds(timerTest.MainEvent.currentTime).ToString(), new Vector2((this.Window.ClientBounds.Width / 2), (this.Window.ClientBounds.Height / 2)), Color.Red);

            for (int x = 0; x < timerTest.timerList.Count; x++ )
            {
                spriteBatch.DrawString(font, TimeSpan.FromMilliseconds(timerTest.timerList[x].currentTime).ToString(), new Vector2((this.Window.ClientBounds.Width / 2), (this.Window.ClientBounds.Height / 2) + ((x + 1) * 50)), Color.Orange);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
