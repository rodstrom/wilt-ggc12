//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;

//namespace RunnerAlpha.Code.Graphics
//{
//    class Animation : DrawableGameComponent
//    {
//        Dictionary<String, AnimationStrip> animationList = new Dictionary<String, AnimationStrip>();
//        String currentAnimation = "";

//        AnimationFrame currentFrame = null;

//        public string AnimationName
//        {
//            get { return currentAnimation; }
//            set
//            {
//                animationList[currentAnimation].Reset();
//                currentAnimation = value;
//            }
//        }

//        public Vector2 Origin
//        {
//            get;
//            set;
//        }

//        public Color _color = Color.White;
//        public Color Color
//        {
//            get { return _color; }
//            set { _color = value; }
//        }
        
//        public void AddAnimation(string id, AnimationStrip anim)
//        {
//            animationList[id] = anim;
//            currentAnimation = id;
//        }

//        public Animation(Game game, SpriteBatch spriteBatch)
//            : base(game)
//        {
//        }

//        public override void Update(GameTime gameTime)
//        {
//            currentFrame = animationList[currentAnimation].getCurrentFrame(gameTime);

//            Origin = new Vector2(currentFrame.SourceRectangle.Width * 0.5f, currentFrame.SourceRectangle.Height * 0.5f);

//            base.Update(gameTime);
//        }

//        public override void Draw(GameTime gameTime)
//        {
//            SpriteBatch.Draw(currentFrame.SourceTexture,
//                                Position,
//                                currentFrame.SourceRectangle,
//                                Color,
//                                Rotation,
//                                Origin,
//                                1.0f,
//                                SpriteEffects.None,
//                                0.0f);
//        }
//    }
//}
