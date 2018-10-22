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
            this.currentNode = null;
        }

        public Vector2 StartingPosition { get; }

        public void AddCommand(Command cmd)
        {
            this.commands.AddLast(cmd);
        }

        public void Update(int currentTick)
        {
            if (this.currentNode == null)
            {
                this.currentNode = this.commands.First;
            }

            while (this.currentNode != null)
            {
                if (this.currentNode == this.commands.Last)
                {
                    return;
                }

                var command = this.currentNode.Value;
                if (command.Tick == currentTick)
                {
                    command.Execute();
                    this.currentNode = this.currentNode.Next;
                }
                else if (command.Tick < currentTick)
                {
                    Debug.WriteLine("Should not happen");
                    return;
                }
                else
                {
                    break;
                }
            }
        }

        public void Reset()
        {
            this.currentNode = this.commands.First;
            this.avatar.SetPosition(this.StartingPosition);
        }
    }
}