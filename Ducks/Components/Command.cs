namespace Ducks.Components
{
    using Microsoft.Xna.Framework;

    class Command
    {
        private readonly Entity unit;

        public uint Tick { get; }

        private readonly Vector2 deltaPosition;

        /// <summary>
        /// Initializes a new instance of the <see cref="Command"/> class.
        /// </summary>
        /// <param name="unit">
        /// The unit.
        /// </param>
        /// <param name="deltaPosition">
        /// The delta Position.
        /// </param>
        public Command(uint tick, Entity unit, Vector2 deltaPosition)
        {
            this.Tick = tick;
            this.unit = unit;
            this.deltaPosition = deltaPosition;
        }

        public virtual void Execute()
        {
            this.unit.MoveTo(this.deltaPosition);
        }
    }
}