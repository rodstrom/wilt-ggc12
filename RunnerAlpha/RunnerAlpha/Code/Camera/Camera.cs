using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RunnerAlpha.Code.Entities;
using RunnerAlpha.Code.Graphics;

namespace RunnerAlpha.Code.Camera
{
    public class Camera2D : GameComponent
    {
        public Viewport View
        {
            get;
            set;
        }

        public Vector2 Position
        {
            get;
            set;
        }

        public Vector2 Center
        {
            get;
            set;
        }

        public float Zoom
        {
            get;
            set;
        }

        public float Rotation
        {
            get;
            set;
        }

        public Matrix Transform
        {
            get;
            set;
        }

        public Entity Focus
        {
            get;
            set;
        }

        public Camera2D(Viewport view, Game game) : base(game)
        {
            View = view;
            Zoom = 1.0f;
            Rotation = 0.0f;
            Center = new Vector2(View.Width / 2, View.Height / 2);
        }

        public Camera2D(Viewport view, float zoom, float rotation, Game game) : base(game)
        {
            View = view;
            Zoom = zoom;
            Rotation = rotation;
            Center = new Vector2(View.X / 2, View.Y / 2);
        }

        public override void Update(GameTime gameTime)
        {
            if (Focus != null)
            {
                Position = Focus.position;
            }
            Transform = Resolution.getTransformationMatrix();
            Transform = Matrix.Identity *
                Matrix.CreateTranslation(-Position.X, -Position.Y, 0) *
                Matrix.CreateRotationZ(Rotation) *
                Matrix.CreateTranslation(Center.X, Center.Y, 0) *
                Matrix.CreateScale(new Vector3(Zoom, Zoom, Zoom));
        }
    }
}
