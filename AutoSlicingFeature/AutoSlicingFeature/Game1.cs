using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AutoSlicingFeature
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private readonly RectangleOverlay _overlay;
        private readonly List<Rectangle> _sources;

        private Texture2D _texture;
        private Color[] _pixelColours;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 800,
                PreferredBackBufferHeight = 600
            };
            Content.RootDirectory = "Content";

            _overlay = new RectangleOverlay(Rectangle.Empty, Color.Red * 0.5f, this);
            _sources = new List<Rectangle>();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _texture = Content.Load<Texture2D>("item");
            _pixelColours = _texture.GetPixels();
            _overlay.LoadContent();

            var stopwatch = Stopwatch.StartNew();
            GenerateSources();
            stopwatch.Stop();
            Debug.WriteLine(stopwatch.Elapsed);
        }

        private void GenerateSources()
        {
            for (var y = 0; y < _texture.Height; y++)
            {
                for (var x = 0; x < _texture.Width; x++)
                {
                    if (_pixelColours.IsTransparent(x, y, _texture.Width))
                        continue;

                    if (_sources.Any(r => r.Contains(new Point(x, y))))
                        continue;

                    _sources.Add(new SourceRectangle(_texture, _pixelColours, x, y).Generate());
                }
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(_texture, Vector2.Zero, null, Color.White);

            foreach (var r in _sources)
            {
                _overlay.Rectangle = r;
                _overlay.Draw(_spriteBatch);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}