using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RunnerAlpha.Code.Entities;

namespace RunnerAlpha.Code.Graphics
{
    class Animation : Entity
    {
        Dictionary<String, AnimationStrip> animationList = new Dictionary<String, AnimationStrip>();
        String currentAnimation = "";

        AnimationFrame currentFrame = null;

        public Rectangle Rectangle
        {
            get
            {
                int x = (int)(position.X - Origin.X);
                int y = (int)(position.Y - Origin.Y);
                return new Rectangle(x, y, currentFrame.SourceRectangle.Width, currentFrame.SourceRectangle.Height);
            }
        }

        public string AnimationName
        {
            get { return currentAnimation; }
            set
            {
                animationList[currentAnimation].Reset();
                currentAnimation = value;
            }
        }

        public Color _color = Color.White;
        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }

        public void AddAnimation(string id, AnimationStrip anim)
        {
            animationList[id] = anim;
            currentAnimation = id;
        }

        public Animation(Runner game, SpriteBatch spriteBatch)
            : base(game, spriteBatch)
        {
        }

        public override void Update(GameTime gameTime)
        {
            currentFrame = animationList[currentAnimation].getCurrentFrame(gameTime);

            Origin = new Vector2(currentFrame.SourceRectangle.Width * 0.5f, currentFrame.SourceRectangle.Height * 0.5f);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Draw(currentFrame.SourceTexture,
                                position,
                                currentFrame.SourceRectangle,
                                Color,
                                Rotation,
                                Origin,
                                1.0f,
                                SpriteEffects.None,
                                0.0f);
        }
    }
}
