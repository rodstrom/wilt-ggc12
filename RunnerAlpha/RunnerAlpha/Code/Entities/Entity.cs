using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using RunnerAlpha.Code.Graphics;
using Microsoft.Xna.Framework;

namespace RunnerAlpha.Code.Entities
{
    public class Entity : DrawableGameComponent
    {
        public Vector2 position;


        public Boolean KillMe
        {
            get;
            set;
        }

        protected Runner Runner
        {
            get;
            private set;
        }

        protected SpriteBatch SpriteBatch
        {
            get;
            private set;
        }

        public Vector2 Origin
        {
            get;
            set;
        }

        public float Rotation
        { 
            get;
            set;
        }

        //public Vector2 Position
        //{
        //    get { return position; }
        //    set { position = value; }
        //}

        public float Layer
        {
            get;
            set;
        }

        public Entity(Runner game, SpriteBatch spriteBatch)
            : base(game)
        {
            Runner = game;
            SpriteBatch = spriteBatch;
            position = Vector2.Zero;
            Origin = Vector2.Zero;
            Rotation = 0.0f;
            Layer = 1f;
            KillMe = false;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

    }
}
