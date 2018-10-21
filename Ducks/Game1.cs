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
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;

    using Ducks.Components;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    /// <summary>
    ///     This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;

        private SpriteBatch spriteBatch;

        private uint gameTick = 0;

        /// <summary>
        /// Player Related code
        /// </summary>
        private Texture2D playerTexture2D;



        private List<Entity> playerStack;
        private List<LinkedList<Command>> commandStack;
        private List<LinkedListNode<Command>> currentNodeStack;
        private List<Vector2> posistionStack;
        private int level = 0;

        private bool replaying;

        private KeyboardState previousState;

        public Game1()
        {
            this.graphics = new GraphicsDeviceManager(this);
            this.Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // Screen and Resolution initialization
            this.graphics.PreferredBackBufferWidth = 1280;
            this.graphics.PreferredBackBufferHeight = 768;
            this.graphics.ApplyChanges();

            // Input initialization
            this.previousState = Keyboard.GetState();

            base.Initialize();
        }

        /// <summary>
        ///     This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.CornflowerBlue);

            this.spriteBatch.Begin();

            foreach (var entity in this.playerStack)
            {
                this.spriteBatch.Draw(entity.Texture, entity.Position, Color.White);
            }

            this.spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        ///     LoadContent will be called once per game and is the place to load
        ///     all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);

            this.playerTexture2D = this.Content.Load<Texture2D>("npc");

            // Setup new player
            var playerPosition = new Vector2(
                this.graphics.GraphicsDevice.Viewport.Width / 2.0f,
                this.graphics.GraphicsDevice.Viewport.Height / 2.0f);

            var newPlayer = new Entity(playerPosition);
            newPlayer.LoadTexture(this.playerTexture2D);

            // Setup stacks
            this.level = 0;
            this.posistionStack = new List<Vector2> { playerPosition };
            this.playerStack = new List<Entity> { newPlayer };

            this.commandStack = new List<LinkedList<Command>>();
            this.currentNodeStack = new List<LinkedListNode<Command>>();

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
            const float Speed = 100;

            var deltaPosition = Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            var currentPlayer = this.GetCurrentPlayer();
            var currrentCommands = this.commandStack[this.level];

            if (state.IsKeyDown(Keys.Up))
            {
                currrentCommands.AddLast(new Command(this.gameTick, currentPlayer, new Vector2(0.0f, -deltaPosition)));
            }

            if (state.IsKeyDown(Keys.Down))
            {
                currrentCommands.AddLast(new Command(this.gameTick, currentPlayer, new Vector2(0.0f, deltaPosition)));
            }

            if (state.IsKeyDown(Keys.Left))
            {
                currrentCommands.AddLast(new Command(this.gameTick, currentPlayer, new Vector2(-deltaPosition, 0.0f)));
            }

            if (state.IsKeyDown(Keys.Right))
            {
                currrentCommands.AddLast(new Command(this.gameTick, currentPlayer, new Vector2(deltaPosition, 0.0f)));
            }

            if (state.IsKeyDown(Keys.A) & !this.previousState.IsKeyDown(Keys.A))
            {
                this.replaying = !this.replaying;

                if (!this.replaying)
                {
                    currrentCommands.Clear();
                }
                else
                {
                    this.gameTick = 0;
                    // this.currentNode = this.commands.First;
                }

                Debug.WriteLine("replaying: " + this.replaying);
            }

            if (state.IsKeyDown(Keys.S) & !this.previousState.IsKeyDown(Keys.S))
            {
                this.EnterNextInteration();
            }

            // Excuting commands
            if (this.replaying)
            {
                this.UpdateCommandList(this.commandStack[this.level], this.currentNodeStack[this.level]);
            }
            else
            {
                this.ProcessNormalCommands(this.commandStack[this.level]);
            }

            base.Update(gameTime);
            this.previousState = state;
        }

        /// <summary>
        /// If not replaying, process player's commands as regular
        /// </summary>
        /// <param name="commands">
        /// The commands.
        /// </param>
        private void ProcessNormalCommands(LinkedList<Command> commands)
        {
            var currentNode = commands.Last;

            if (currentNode == null || currentNode.Value.Tick != this.gameTick)
            {
                return;
            }

            var preNode = currentNode.Previous;
            while (preNode != null && preNode.Value.Tick == this.gameTick)
            {
                currentNode = preNode;
                preNode = currentNode.Previous;
            }

            while (currentNode != null)
            {
                var command = currentNode.Value;
                command.Execute();

                currentNode = currentNode.Next;
            }
        }

        /// <summary>
        /// Replay the commands player did
        /// </summary>
        /// <param name="commands">
        /// The commands.
        /// </param>
        /// <param name="currentNode">
        /// The current Node.
        /// </param>
        private void UpdateCommandList(LinkedList<Command> commands, LinkedListNode<Command> currentNode)
        {

            while (currentNode != null)
            {
                var command = currentNode.Value;
                if (command.Tick == this.gameTick)
                {
                    command.Execute();
                    currentNode = currentNode.Next;
                }
                else
                {
                    break;
                }
            }
        }

        private Entity GetCurrentPlayer()
        {
            // Todo: do some checking
            return this.playerStack[this.level];
        }

        private void EnterNextInteration()
        {
            this.level++;

            // Clear current game tick
            this.gameTick = 0;

            // Making new player at
            var p = new Entity(this.posistionStack[this.level - 1]);
            p.LoadTexture(this.playerTexture2D);

            // Save starting position for this iteration
            this.playerStack.Add(p);
        }
    }
}