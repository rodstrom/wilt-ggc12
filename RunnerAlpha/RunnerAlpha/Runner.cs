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

namespace RunnerAlpha
{
    public class Runner : Microsoft.Xna.Framework.Game
    {
        public const int WIDTH = 1920;
        public const int HEIGHT = 1200;

        InputManager input;
        EntityManager entities;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        SpriteFont font = null;

        //KeyboardState lastKey;
        //KeyboardState currentKey;

        //Texture2D player = null;
        //Rectangle playerrect = Rectangle.Empty;
        //Vector2 playerposition = Vector2.Zero;
        //float fallTime = 0.0f;

        //Texture2D platform1 = null;
        //Rectangle platrect1 = Rectangle.Empty;
        //Vector2 platposition1 = Vector2.Zero;

        //Texture2D platform2 = null;
        //Rectangle platrect2 = Rectangle.Empty;
        //Vector2 platposition2 = Vector2.Zero;

        Texture2D background;

        //LinkedList<Entity> entities = new LinkedList<Entity>();

        public Runner()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //graphics.IsFullScreen = true;
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

            font = this.Content.Load<SpriteFont>("font");

            //player = this.Content.Load<Texture2D>("Guy");
            //playerrect = new Rectangle((int)playerposition.X, (int)playerposition.Y, player.Width, player.Height);

            //platform1 = this.Content.Load<Texture2D>("platform");
            //platrect1 = new Rectangle((int)platposition1.X, (int)platposition1.Y, platform1.Width, platform1.Height);

            //platform2 = this.Content.Load<Texture2D>("platform");
            //platrect2 = new Rectangle((int)platposition2.X, (int)platposition2.Y, platform2.Width, platform2.Height);

            background = this.Content.Load<Texture2D>("Background");

            //playerposition = new Vector2(100, 100);

            //platposition1 = new Vector2(0.0f, 1200 * 0.75f);
            //platposition2 = new Vector2((1920) - (platform2.Width), 1200 * 0.75f);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            

            //playerrect.X = (int)playerposition.X;
            //playerrect.Y = (int)playerposition.Y;

            //platrect1.X = (int)platposition1.X;
            //platrect1.Y = (int)platposition1.Y;

            //platrect2.X = (int)platposition2.X;
            //platrect2.Y = (int)platposition2.Y;

            //lastKey = currentKey;
            //currentKey = Keyboard.GetState();

            //if (currentKey.IsKeyUp(Keys.Escape) && lastKey.IsKeyDown(Keys.Escape))
            //    this.Exit();

            //if (playerposition.X < this.Window.ClientBounds.Width && playerposition.X > 0.0f &&
            //    playerposition.Y < 1200 && playerposition.Y > 0.0f)
            //{
            //    if (!(playerrect.Intersects(platrect1) || playerrect.Intersects(platrect2)))
            //    {
            //        fallTime += gameTime.ElapsedGameTime.Milliseconds;

            //        playerposition.Y += ((fallTime / 100) * (fallTime / 100));
            //    }
            //    else if (playerrect.Intersects(platrect1) || playerrect.Intersects(platrect2))
            //    {
            //        playerposition.Y = (platposition1.Y - (player.Height - 2));
            //        fallTime = 0.0f;
            //    }

            //    if (currentKey.IsKeyDown(Keys.A))
            //        playerposition.X -= gameTime.ElapsedGameTime.Milliseconds * 0.7f;
            //    if (currentKey.IsKeyDown(Keys.D))
            //        playerposition.X += gameTime.ElapsedGameTime.Milliseconds * 0.7f;
            //    if (currentKey.IsKeyDown(Keys.W))
            //        playerposition.Y -= 10;
            //}
            //else
            //{
            //    playerposition = new Vector2(100, 100);
            //}

            entities.Update(gameTime);

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

            //spriteBatch.Begin();
            //spriteBatch.Begin(0, null, null, null, null, null, Resolution.getTransformationMatrix());
            spriteBatch.Begin(0, BlendState.Opaque, null, null, null, null, Resolution.getTransformationMatrix());

            spriteBatch.Draw(background, new Vector2(0, 0), Color.White);

            //foreach (Entity e in entities)
            //{
            //    e.Draw();
            //}

            //spriteBatch.Draw(player, playerposition, playerrect, Color.White);
            //spriteBatch.Draw(platform1, platposition1, platrect1, Color.White);
            //spriteBatch.Draw(platform2, platposition2, platrect2, Color.White);

            entities.Draw();

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}