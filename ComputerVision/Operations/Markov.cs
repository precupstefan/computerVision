using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ComputerVision.Operations
{
    public class Markov
    {
        public int SR => 4;
        public int CS => 3;
        public int T => 500;
        public FastImage image = null;

        public Bitmap Compute(FastImage workImage)
        {
            image = workImage;
            image.Lock();
            CBPF();
            image.Unlock();
            return image.GetBitMap();
        }

        private Color CBP(Point point)
        {
            var Q = new Dictionary<Color, int>();
            for (var y = point.Y - SR; y <= point.Y + SR; y++)
            {
                if (!(0 <= y && y < image.Width)) continue;
                for (var x = point.X - SR; x <= point.X + SR; x++)
                {
                    if (!(0 <= x && x < image.Height)) continue;
                    if (x == point.X && y == point.Y) continue;
                    var currentPoint = new Point {X = x, Y = y};
                    if (SAD(point, currentPoint) < T && !SaltAndPepper(currentPoint))
                    {
                        var color = image.GetPixel(currentPoint);
                        if (Q.ContainsKey(color))
                        {
                            Q[color]++;
                        }
                        else
                        {
                            Q.Add(color, 1);
                        }
                    }
                }
            }

            var max = Q.Max(pair => pair.Value);
            return Q.Where(pair => pair.Value == max).Select(pair => pair.Key).ToList()[0];
        }

        private int SAD(Point point1, Point point2)
        {
            var sum = 0;
            for (var y = -CS / 2; y <= -CS / 2; y++)
            {
                if (!(0 <= y + point1.Y && y + point1.Y < image.Width)) continue;
                if (!(0 <= y + point2.Y && y + point2.Y < image.Width)) continue;
                for (var x = -CS / 2; x <= CS / 2; x++)
                {
                    if (!(0 <= x + point1.X && x + point1.X < image.Height)) continue;
                    if (!(0 <= x + point2.X && x + point2.X < image.Height)) continue;
                    if (x == 0 && y == 0) continue;
                    var pointA = new Point {X = x + point1.X, Y = y + point1.Y};
                    var pointB = new Point {X = x + point2.X, Y = y + point2.Y};
                    var color1 = image.GetPixel(pointA);
                    var color2 = image.GetPixel(pointB);
                    sum += color1.R - color2.R;
                }
            }

            return sum;
        }

        private void CBPF()
        {
            for (int y = 0; y < image.Width; y++)
            {
                for (int x = 0; x < image.Height; x++)
                {
                    var point = new Point {X = x, Y = y};
                    if (SaltAndPepper(point))
                    {
                        var result = CBP(point);
                        image.SetPixel(point, result);
                    }
                }
            }
        }

        private bool SaltAndPepper(Point point)
        {
            var pixel = image.GetPixel(point);
            return pixel == Color.FromArgb(0, 0, 0) ||
                   pixel == Color.FromArgb(255, 255, 255);
        }
    }
}