using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using RunnerAlpha.Code.Graphics;
using RunnerAlpha.Code.Camera;

namespace RunnerAlpha.Code.Graphics
{
    class Background
    {
        public Game Runner
        {
            get;
            set;
        }
        public SpriteBatch SpriteBatch
        {
            get;
            set;
        }

        public Background()
        {
        }

        public Vector2 Position = new Vector2(0, 0);
        private Texture2D spriteTexture;
        public string assetName;
        public Rectangle size;
        private float scale = 1.0f;

        public float Scale
        {
            get { return scale; }
            set
            {
                scale = value;
                size = new Rectangle(0, 0, (int)(spriteTexture.Width * Scale), (int)(spriteTexture.Height * Scale));
            }
        }

        public void LoadContent(string theAssetName, Game game)
        {
            Runner = game;
            spriteTexture = Runner.Content.Load<Texture2D>(theAssetName);
            assetName = theAssetName;
            size = new Rectangle(0, 0, (int)(spriteTexture.Width * Scale), (int)(spriteTexture.Height * Scale));
        }

        public void Update(GameTime gameTime, Vector2 theSpeed, Vector2 theDirection)
        {
            Position += theDirection * theSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            SpriteBatch = spriteBatch;
            SpriteBatch.Draw(spriteTexture, Position,
                new Rectangle(0, 0, spriteTexture.Width, spriteTexture.Height),
                Color.White, 0.0f, Vector2.Zero, Scale, SpriteEffects.None, 0);
        }
    }
}

class Scrolling
{
    public Game Runner
    {
        get;
        set;
    }
    public SpriteBatch SpriteBatch
    {
        get;
        set;
    }

    public Camera2D Camera
    {
        get;
        set;
    }

    List<Background> backgrounds;

    Background mostRightSprite;
    Background mostLeftSprite;

    //Viewport viewport;

    public enum scrollDirection
    {
        Left,
        Right
    }

    public Scrolling(Viewport theViewport, Game game, SpriteBatch spriteBatch)
    {
        Runner = game;
        SpriteBatch = spriteBatch;
        backgrounds = new List<Background>();
        mostRightSprite = null;
        mostLeftSprite = null;
        //viewport = theViewport;
    }

    public void LoadContent()
    {
        mostRightSprite = null;
        mostLeftSprite = null;

        float width = 0;

        foreach (Background backgroundSprite in backgrounds)
        {
            backgroundSprite.LoadContent(backgroundSprite.assetName, Runner);
            backgroundSprite.Scale = Camera.View.Height / backgroundSprite.size.Height;

            if (mostRightSprite == null)
            {
                backgroundSprite.Position = new Vector2(Camera.View.X, Camera.View.Y);
                mostLeftSprite = backgroundSprite;
            }
            else
            {
                backgroundSprite.Position = new Vector2(mostRightSprite.Position.X + mostRightSprite.size.Width, Camera.View.Y);
            }

            mostRightSprite = backgroundSprite;

            width += backgroundSprite.size.Width;
        }

        int index = 0;
        if (backgrounds.Count > 0 && width < Camera.View.Width * 2)
        {
            do
            {
                Background aBackgroundSprite = new Background();
                aBackgroundSprite.assetName = backgrounds[index].assetName;
                aBackgroundSprite.LoadContent(aBackgroundSprite.assetName, Runner);
                aBackgroundSprite.Scale = Camera.View.Height / aBackgroundSprite.size.Height;
                aBackgroundSprite.Position = new Vector2(mostRightSprite.Position.X + mostRightSprite.size.Width, Camera.View.Y);
                backgrounds.Add(aBackgroundSprite);
                mostRightSprite = aBackgroundSprite;

                width += aBackgroundSprite.size.Width;

                index += 1;
                if (index > backgrounds.Count - 1)
                {
                    index = 0;
                }

            } while (width < Camera.View.Width * 2);
        }
    }

    public void AddBackground(string theAssetName)
    {
        Background aBackgroundSprite = new Background();
        aBackgroundSprite.assetName = theAssetName;

        backgrounds.Add(aBackgroundSprite);
    }

    public void Update(GameTime theGameTime, int theSpeed, scrollDirection theDirection)
    {
        if (theDirection == scrollDirection.Left)
        {
            foreach (Background aBackgroundSprite in backgrounds)
            {
                if (aBackgroundSprite.Position.X < Camera.View.X - aBackgroundSprite.size.Width)
                {
                    aBackgroundSprite.Position = new Vector2(mostRightSprite.Position.X + mostRightSprite.size.Width, Camera.View.Y);
                    mostRightSprite = aBackgroundSprite;
                }
            }
        }
        else if (theDirection == scrollDirection.Right)
        {
            foreach (Background aBackgroundSprite in backgrounds)
            {
                if (aBackgroundSprite.Position.X > Camera.View.X + Camera.View.Width)
                {
                    aBackgroundSprite.Position = new Vector2(mostLeftSprite.Position.X - mostLeftSprite.size.Width, Camera.View.Y);
                    mostLeftSprite = aBackgroundSprite;
                }
            }
        }

        Vector2 aDirection = Vector2.Zero;
        if (theDirection == scrollDirection.Left)
        {
            aDirection.X = -1;
        }
        else if (theDirection == scrollDirection.Right)
        {
            aDirection.X = 1;
        }

        foreach (Background aBackgroundSprite in backgrounds)
        {
            aBackgroundSprite.Update(theGameTime, new Vector2(theSpeed, 0), aDirection);
        }
    }

    public void Draw(GameTime gameTime)
    {
        foreach (Background aBackgroundSprite in backgrounds)
        {
            aBackgroundSprite.Draw(gameTime, SpriteBatch);
        }
    }
}