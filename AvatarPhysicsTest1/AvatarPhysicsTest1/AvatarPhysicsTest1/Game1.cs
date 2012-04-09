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

namespace AvatarPhysicsTest1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        SpriteFont font = null;

        KeyboardState lastKey;
        KeyboardState currentKey;

        Texture2D player = null;
        Rectangle playerrect = Rectangle.Empty;
        Vector2 playerposition = Vector2.Zero;
        float fallTime = 0.0f;
        //bool Jumping = false;
        //bool Walking = false;
        //bool Falling = false;

        Texture2D platform1 = null;
        Rectangle platrect1 = Rectangle.Empty;
        Vector2 platposition1 = Vector2.Zero;

        Texture2D platform2 = null;
        Rectangle platrect2 = Rectangle.Empty;
        Vector2 platposition2 = Vector2.Zero;


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
            // TODO: Add your initialization logic here

            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            graphics.ApplyChanges();

            this.IsMouseVisible = true;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            font = this.Content.Load<SpriteFont>("font");

            player = this.Content.Load<Texture2D>("player");
            playerrect = new Rectangle((int)playerposition.X, (int)playerposition.Y, player.Width, player.Height);

            platform1 = this.Content.Load<Texture2D>("platform");
            platrect1 = new Rectangle((int)platposition1.X, (int)platposition1.Y, platform1.Width, platform1.Height);

            platform2 = this.Content.Load<Texture2D>("platform");
            platrect2 = new Rectangle((int)platposition2.X, (int)platposition2.Y, platform2.Width, platform2.Height);

            playerposition = new Vector2(this.Window.ClientBounds.Width / 5, this.Window.ClientBounds.Height / 2);

            platposition1 = new Vector2(0.0f, this.Window.ClientBounds.Height * 0.75f);
            platposition2 = new Vector2((this.Window.ClientBounds.Width) - (platform2.Width), this.Window.ClientBounds.Height * 0.75f);

            // TODO: use this.Content to load your game content here
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
            playerrect.X = (int)playerposition.X;
            playerrect.Y = (int)playerposition.Y;

            platrect1.X = (int)platposition1.X;
            platrect1.Y = (int)platposition1.Y;

            platrect2.X = (int)platposition2.X;
            platrect2.Y = (int)platposition2.Y;

            lastKey = currentKey;
            currentKey = Keyboard.GetState();

            // Allows the game to exit
            if (currentKey.IsKeyUp(Keys.Escape) && lastKey.IsKeyDown(Keys.Escape))
                this.Exit();

            if (playerposition.X < this.Window.ClientBounds.Width && playerposition.X > 0.0f &&
                playerposition.Y < this.Window.ClientBounds.Height && playerposition.Y > 0.0f)
            {
                if (!(playerrect.Intersects(platrect1) || playerrect.Intersects(platrect2)))
                {
                    fallTime += gameTime.ElapsedGameTime.Milliseconds;

                    playerposition.Y += ((fallTime / 100) * (fallTime / 100));
                }
                else if (playerrect.Intersects(platrect1) || playerrect.Intersects(platrect2))
                {
                    playerposition.Y = (platposition1.Y - (player.Height - 2));
                    fallTime = 0.0f;
                }

                if (currentKey.IsKeyDown(Keys.A))
                    playerposition.X -= gameTime.ElapsedGameTime.Milliseconds * 0.7f;
                if (currentKey.IsKeyDown(Keys.D))
                    playerposition.X += gameTime.ElapsedGameTime.Milliseconds * 0.7f;
                if (currentKey.IsKeyDown(Keys.W))
                    playerposition.Y -= 10;
            }
            else
            {
                playerposition = new Vector2(this.Window.ClientBounds.Width / 5, this.Window.ClientBounds.Height / 2);
            }

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

            spriteBatch.Draw(player, playerposition, playerrect, Color.White);
            spriteBatch.Draw(platform1, platposition1, platrect1, Color.White);
            spriteBatch.Draw(platform2, platposition2, platrect2, Color.White);

            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
