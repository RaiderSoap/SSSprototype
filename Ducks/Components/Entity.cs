﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Entity.cs" company="SSS">
//   MIT
// </copyright>
// <summary>
//   Defines the Entity type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ducks.Components
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// The entity. 
    /// </summary>
    internal class Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Entity"/> class.
        /// </summary>
        /// <param name="initPosition">
        /// The init position.
        /// </param>
        public Entity(Vector2 initPosition)
        {
            this.Position = initPosition;
        }

        public Vector2 Position { get; private set; }

        public Texture2D Texture { get; private set; }

        public void LoadTexture(Texture2D texture)
        {
            this.Texture = texture;
        }

        public void MoveTo(Vector2 deltaPosition)
        {
            this.Position += deltaPosition;
        }
    }
}