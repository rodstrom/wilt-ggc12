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

        Keys quit = Keys.Escape;

        public InputManager()
        {

        }

        public void Update()
        {
            lastKey = currentKey;
            currentKey = Keyboard.GetState();
        }

        //public bool Up
        //{
        //    get
        //    {
        //        if (currentKey.IsKeyUp(up) && lastKey.IsKeyDown(up))
        //        {
        //            return true;
        //        }
        //        return false;
        //    }
        //}

        //public bool Right
        //{
        //    get
        //    {
        //        if (currentKey.IsKeyUp(right) && lastKey.IsKeyDown(right))
        //        {
        //            return true;
        //        }
        //        return false;
        //    }
        //}

        //public bool Left
        //{
        //    get
        //    {
        //        if (currentKey.IsKeyUp(left) && lastKey.IsKeyDown(left))
        //        {
        //            return true;
        //        }
        //        return false;
        //    }
        //}
        
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

        public bool Quit
        {
            get
            {
                if (currentKey.IsKeyUp(quit) && lastKey.IsKeyDown(quit))
                {
                    return true;
                }
                return false;
            }
        }
    }
}
