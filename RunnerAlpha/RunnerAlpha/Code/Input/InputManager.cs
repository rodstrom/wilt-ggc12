﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace RunnerAlpha.Code.Input
{
    class InputManager
    {
        Runner Game;

        KeyboardState lastKey;
        KeyboardState currentKey;

        public float inputLock = 0.0f;

        MouseState currentMouseState, lastMouseState, originalMouseState;

        Vector2 mouseAbsolute = Vector2.Zero;
        Vector2 mouseRelative = Vector2.Zero;

        Keys up;
        Keys right;
        Keys left;

        Keys select;
        Keys pause;
        Keys space;

        public Vector2 MouseAbsolute
        {
            get { return mouseAbsolute; }
        }

        public Vector2 MouseRelative
        {
            get { return mouseRelative; }
        }

        public MouseState MouseOriginal
        {
            get { return originalMouseState; }
        }

        public MouseState CurrentMouse
        {
            get { return currentMouseState; }
            set { currentMouseState = value; }
        }

        public InputManager(Runner game)
        {
            Game = game; 
            Mouse.SetPosition(Game.Window.ClientBounds.Width / 2, Game.Window.ClientBounds.Height / 2);
            originalMouseState = Mouse.GetState();
            up = setKey(game.config.getValue("Controls", "Up"));
            right = setKey(game.config.getValue("Controls", "Right"));
            left = setKey(game.config.getValue("Controls", "Left"));
            select = setKey(game.config.getValue("Controls", "Select"));
            pause = setKey(game.config.getValue("Controls", "Pause"));
            space = setKey(game.config.getValue("Controls", "Space"));
        }

        private Keys setKey(String newKey)
        {
            try
            {
                return (Keys)Enum.Parse(typeof(Keys), newKey);
            }
            catch (Exception)
            {
                return Keys.F24;
            }
        }

        public void Update()
        {
            lastKey = currentKey;
            currentKey = Keyboard.GetState();

            lastMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
        }
        
        public bool Up
        {
            get
            {
                if (currentKey.IsKeyDown(up))
                {
                    return true;
                }
                return false;
            }
        }


        public bool Right
        {
            get
            {
                if (currentKey.IsKeyDown(right))
                {
                    return true;
                }
                return false;
            }
        }

        public bool RightOnce
        {
            get
            {
                if (currentKey.IsKeyUp(right) && lastKey.IsKeyDown(right))
                {
                    return true;
                }
                return false;
            }
        }

        public bool Left
        {
            get
            {
                if (currentKey.IsKeyDown(left))
                {
                    return true;
                }
                return false;
            }
        }

        public bool LeftOnce
        {
            get
            {
                if (currentKey.IsKeyUp(left) && lastKey.IsKeyDown(left))
                {
                    return true;
                }
                return false;
            }
        }

        public bool Select
        {
            get
            {
                if (currentKey.IsKeyUp(select) && lastKey.IsKeyDown(select))
                {
                    return true;
                }
                return false;
            }
        }

        public bool Pause
        {
            get
            {
                if (currentKey.IsKeyUp(pause) && lastKey.IsKeyDown(pause))
                {
                    return true;
                }
                return false;
            }
        }

        public bool Space
        {
            get
            {
                if (currentKey.IsKeyDown(space) && lastKey.IsKeyUp(space))
                {
                    return true;
                }
                return false;
            }
        }
    }
}
