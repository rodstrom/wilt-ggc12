using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace RunnerAlpha.Code.Input
{
    class InputManager
    {
        KeyboardState lastKey;
        KeyboardState currentKey;

        Keys up = Keys.W;
        Keys right = Keys.D;
        Keys left = Keys.A;

        Keys select = Keys.Enter;
        Keys pause = Keys.Escape;

        Keys space = Keys.Space;

        public InputManager()
        {

        }

        public void Update()
        {
            lastKey = currentKey;
            currentKey = Keyboard.GetState();
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
