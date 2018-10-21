namespace Ducks.Components
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    class Entity
    {
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