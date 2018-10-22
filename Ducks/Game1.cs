// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Game1.cs" company="SSS">
//   MIT
// </copyright>
// <summary>
//   This is the main type for your game.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ducks
{
    using System.Collections.Generic;

    using Ducks.Components;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    /// <summary>
    ///     This is the main type for your game.
    /// </summary>
    public class Game1 : GameBase
    {
        private List<Command> executingCommands;

        private int gameTick;

        private List<Iteration> iterations;

        private int level;

        private Vector2 spawnPosition;

        /// <summary>
        ///     LoadContent will be called once per game and is the place to load
        ///     all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            base.LoadContent();

            this.spawnPosition = new Vector2(
                this.graphics.GraphicsDevice.Viewport.Width / 2.0f,
                this.graphics.GraphicsDevice.Viewport.Height / 2.0f);

            // Make a player
            var player = new Entity(this.spawnPosition);
            this.entities.Add(player);

            // Making an container for iterations
            this.level = 0;
            this.iterations = new List<Iteration> { new Iteration(this.gameTick, player) };

            this.executingCommands = new List<Command>();
        }

        /// <summary>
        ///     Allows the game to run logic such as updating the world,
        ///     checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Update tick counter
            this.gameTick++;

            // Inputs
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            var state = Keyboard.GetState();
            const float Speed = 250;

            var deltaPosition = Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            var currentPlayer = this.GetCurrentPlayer();

            if (state.IsKeyDown(Keys.Up))
            {
                this.executingCommands.Add(
                    new Command(this.gameTick, currentPlayer, new Vector2(0.0f, -deltaPosition)));
            }

            if (state.IsKeyDown(Keys.Down))
            {
                this.executingCommands.Add(new Command(this.gameTick, currentPlayer, new Vector2(0.0f, deltaPosition)));
            }

            if (state.IsKeyDown(Keys.Left))
            {
                this.executingCommands.Add(
                    new Command(this.gameTick, currentPlayer, new Vector2(-deltaPosition, 0.0f)));
            }

            if (state.IsKeyDown(Keys.Right))
            {
                this.executingCommands.Add(new Command(this.gameTick, currentPlayer, new Vector2(deltaPosition, 0.0f)));
            }

            if (state.IsKeyDown(Keys.S) & !this.previousState.IsKeyDown(Keys.S))
            {
                this.EndCurrentIteration();
            }

            // Process regular commands and store them to current Iteration.
            var currentIteration = this.GetCurrentIteration();
            foreach (var cmd in this.executingCommands)
            {
                cmd.Execute();
                currentIteration.AddCommand(cmd);
            }

            this.executingCommands.Clear();

            // Update all iterations, ignore the top most iteration
            for (int i = 0; i < this.iterations.Count - 1; i++)
            {
                this.iterations[i].Update(this.gameTick);
            }

            base.Update(gameTime);
            this.previousState = state;
        }

        private void EndCurrentIteration()
        {
            // First, make an new player instance at the spawn point
            var player = new Entity(this.spawnPosition);
            this.entities.Add(player);

            // Second, reset all previous iterations to original state
            this.iterations.ForEach(it => it.Reset());

            // Third, create the last iteration
            var newIteration = new Iteration(this.gameTick, player);
            this.iterations.Add(newIteration);

            // Finally, reset the game tick count and progress the level
            this.gameTick = 0;
            this.level++;
        }

        private Iteration GetCurrentIteration()
        {
            return this.iterations[this.level];
        }

        private Entity GetCurrentPlayer()
        {
            return this.entities[this.level];
        }
    }
}