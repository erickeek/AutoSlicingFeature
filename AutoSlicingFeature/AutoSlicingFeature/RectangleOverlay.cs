using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AutoSlicingFeature
{
    public class RectangleOverlay
    {
        private readonly Game _game;
        private Texture2D _dummyTexture;
        private readonly Color _color;
        public Rectangle Rectangle;

        public RectangleOverlay(Rectangle rectangle, Color color, Game game)
        {
            Rectangle = rectangle;
            _color = color;
            _game = game;
        }

        public void LoadContent()
        {
            _dummyTexture = new Texture2D(_game.GraphicsDevice, 1, 1);
            _dummyTexture.SetData(new[] { Color.White });
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_dummyTexture, Rectangle, _color);
        }
    }
}