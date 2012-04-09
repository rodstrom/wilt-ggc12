using System;

namespace RunnerAlpha
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Runner game = new Runner())
            {
                game.Run();
            }
        }
    }
#endif
}

