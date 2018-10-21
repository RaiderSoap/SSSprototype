// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameBase.cs" company="SSS">
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
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    /// <summary>
    ///     This is the main type for your game.
    /// </summary>
    public class GameBase : Game
    {
        protected List<Entity> entities;

        protected GraphicsDeviceManager graphics;

        protected Texture2D playerTexture2D;

        protected KeyboardState previousState;

        private SpriteBatch spriteBatch;

        public GameBase()
        {
            this.graphics = new GraphicsDeviceManager(this);
            this.Content.RootDirectory = "Content";
            this.entities = new List<Entity>();
        }

        /// <summary>
        ///     This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.CornflowerBlue);

            this.spriteBatch.Begin();

            foreach (var entity in this.entities)
            {
                this.spriteBatch.Draw(this.playerTexture2D, entity.Position, Color.White);
            }

            this.spriteBatch.End();

            base.Draw(gameTime);
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
        ///     LoadContent will be called once per game and is the place to load
        ///     all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);

            this.playerTexture2D = this.Content.Load<Texture2D>("npc");
        }
    }
}