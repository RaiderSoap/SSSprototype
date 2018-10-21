// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="SSS">
//   MIT
// </copyright>
// <summary>
//   The main class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ducks
{
    using System;

#if WINDOWS || LINUX

    /// <summary>
    ///     The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            using (var game = new Game1())
            {
                game.Run();
            }
        }
    }
#endif
}