using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AutoSlicingFeature
{
    public class SourceRectangle
    {
        private readonly Texture2D _texture;
        private readonly Color[] _pixelColours;
        private readonly int _x;
        private readonly int _y;
        private readonly Queue<Point> _queue;
        private readonly HashSet<Point> _points;

        public SourceRectangle(Texture2D texture, Color[] pixelColours, int x, int y)
        {
            _texture = texture;
            _pixelColours = pixelColours;
            _x = x;
            _y = y;
            _queue = new Queue<Point>();
            _points = new HashSet<Point>();
        }

        public Rectangle Generate()
        {
            _queue.Enqueue(new Point(_x, _y));

            while (_queue.Any())
            {
                var p = _queue.Dequeue();

                if (_points.Contains(p))
                    continue;

                if (p.X < 0 || p.Y < 0 || p.X >= _texture.Width || p.Y >= _texture.Height)
                    continue;

                if (_pixelColours.IsTransparent(p.X, p.Y, _texture.Width))
                    continue;

                _points.Add(p);

                // UP
                _queue.Enqueue(new Point(p.X, p.Y + 1));
                // UP-RIGHT
                _queue.Enqueue(new Point(p.X + 1, p.Y + 1));
                // RIGHT
                _queue.Enqueue(new Point(p.X + 1, p.Y));
                // RIGHT-DOWN
                _queue.Enqueue(new Point(p.X + 1, p.Y - 1));
                // DOWN
                _queue.Enqueue(new Point(p.X, p.Y - 1));
                // DOWN-LEFT
                _queue.Enqueue(new Point(p.X - 1, p.Y - 1));
                // LEFT
                _queue.Enqueue(new Point(p.X - 1, p.Y));
                // LEFT-UP
                _queue.Enqueue(new Point(p.X - 1, p.Y + 1));
            }

            var left = _points.Min(r => r.X);
            var top = _points.Min(r => r.Y);
            var right = _points.Max(r => r.X);
            var bottom = _points.Max(r => r.Y);
            var width = right - left + 1;
            var height = bottom - top + 1;

            return new Rectangle(left, top, width, height);
        }
    }
}