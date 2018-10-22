// --------------------------------------------------------------------------------------------------------------------
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
    ///     The entity.
    /// </summary>
    public class Entity
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Entity" /> class.
        /// </summary>
        /// <param name="initPosition">
        ///     The init position.
        /// </param>
        public Entity(Vector2 initPosition)
        {
            this.Position = initPosition;
        }

        public Vector2 Position { get; private set; }

        public Texture2D Texture { get; private set; }

        public float alpha = 1.0f;

        public void LoadTexture(Texture2D texture)
        {
            this.Texture = texture;
        }

        public void MoveBy(Vector2 deltaPosition)
        {
            this.Position += deltaPosition;
        }

        public void SetPosition(Vector2 position)
        {
            this.Position = position;
        }

        public void Update()
        {

        }
    }
}