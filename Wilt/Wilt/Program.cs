using System;

namespace Wilt
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Wilt wilt = new Wilt())
            {
                wilt.Run();
            }
        }
    }
#endif
}

