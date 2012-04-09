using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TimerTest1
{
    public class InputManager : GameComponent
    {
        private KeyboardState currentKeyState, lastKeyState;

        private MouseState currentMouseState, lastMouseState;

        private Vector2 mouseAbsolute = Vector2.Zero;
        private Vector2 mouseRelative = Vector2.Zero;

        public Vector2 MouseAbsolute
        {
            get { return mouseAbsolute; }
        }

        public Vector2 MouseRelative
        {
            get { return mouseRelative; }
        }

        public enum Buttons
        {
            Left,
            Right,
            Middle,
            xButton1,
            xButton2
        }

        public bool justPressed(Keys key)
        {
            return currentKeyState.IsKeyDown(key) && lastKeyState.IsKeyUp(key);
        }

        public bool justPressed(Buttons button)
        {
            return isDown(currentMouseState, button) == true && isDown(lastMouseState, button) == false;
        }

        public bool justReleased(Buttons button)
        {
            return isDown(currentMouseState, button) == false && isDown(lastMouseState, button) == true;
        }

        public bool justReleased(Keys key)
        {
            return currentKeyState.IsKeyUp(key) && lastKeyState.IsKeyDown(key);
        }

        public bool isDown(Keys key)
        {
            return currentKeyState.IsKeyDown(key);
        }

        public bool isDown(Buttons button)
        {
            switch (button)
            {
                case Buttons.Left:
                    return currentMouseState.LeftButton == ButtonState.Pressed ? true : false;
                case Buttons.Right:
                    return currentMouseState.RightButton == ButtonState.Pressed ? true : false;
                case Buttons.Middle:
                    return currentMouseState.MiddleButton == ButtonState.Pressed ? true : false;
                case Buttons.xButton1:
                    return currentMouseState.XButton1 == ButtonState.Pressed ? true : false;
                case Buttons.xButton2:
                    return currentMouseState.XButton2 == ButtonState.Pressed ? true : false;
                default:
                    return false;
            }
        }

        private bool isDown(MouseState state, Buttons button)
        {
            switch (button)
            {
                case Buttons.Left:
                    return state.LeftButton == ButtonState.Pressed ? true : false;
                case Buttons.Right:
                    return state.RightButton == ButtonState.Pressed ? true : false;
                case Buttons.Middle:
                    return state.MiddleButton == ButtonState.Pressed ? true : false;
                case Buttons.xButton1:
                    return state.XButton1 == ButtonState.Pressed ? true : false;
                case Buttons.xButton2:
                    return state.XButton2 == ButtonState.Pressed ? true : false;
                default:
                    return false;
            }
        }

        public InputManager(Game game) :
            base(game)
        {
        }

        public override void Update(GameTime gameTime)
        {
            lastKeyState = currentKeyState;
            currentKeyState = Keyboard.GetState();

            lastMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();

            mouseAbsolute = new Vector2(currentMouseState.X, currentMouseState.Y);
            mouseRelative = new Vector2(currentMouseState.X - lastMouseState.X, currentMouseState.Y - lastMouseState.Y);

            base.Update(gameTime);
        }
    }
}
