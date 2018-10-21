// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Command.cs" company="SSS">
//   MIT
// </copyright>
// <summary>
//   Defines the Command type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ducks.Components
{
    using Microsoft.Xna.Framework;

    internal class Command
    {
        private readonly Vector2 deltaPosition;

        private readonly Entity unit;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Command" /> class.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <param name="deltaPosition">
        ///     The delta Position.
        /// </param>
        public Command(int tick, Entity unit, Vector2 deltaPosition)
        {
            this.Tick = tick;
            this.unit = unit;
            this.deltaPosition = deltaPosition;
        }

        public int Tick { get; }

        public virtual void Execute()
        {
            this.unit.MoveTo(this.deltaPosition);
        }
    }
}