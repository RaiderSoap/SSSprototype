// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Iteration.cs" company="SSS">
//   MIT
// </copyright>
// <summary>
//   Defines the Iteration type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ducks.Components
{
    using System.Collections.Generic;
    using System.Diagnostics;

    using Microsoft.Xna.Framework;

    internal class Iteration
    {
        private readonly Entity avatar;

        private readonly LinkedList<Command> commands;

        private readonly int startingTick;

        private LinkedListNode<Command> currentNode;

        /// <inheritdoc />
        public Iteration(int tick, Entity avatar)
        {
            this.avatar = avatar;

            this.startingTick = tick;
            this.StartingPosition = avatar.Position;

            this.commands = new LinkedList<Command>();
            this.currentNode = this.commands.First;
        }

        public Vector2 StartingPosition { get; }

        public void AddCommand(Command cmd)
        {
            this.commands.AddLast(cmd);
        }

        public void Update(int currentTick)
        {
            while (this.currentNode != null)
            {
                var command = this.currentNode.Value;
                if (command.Tick == currentTick)
                {
                    command.Execute();
                    this.currentNode = this.currentNode.Next;
                }
                else if (command.Tick < currentTick)
                {
                    Debug.WriteLine("Should not happen");
                }
                else
                {
                    break;
                }
            }
        }
    }
}