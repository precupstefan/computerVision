using System.Drawing;
using ComputerVision.utils;

namespace ComputerVision.Operations
{
    public class UnsharpMasking
    {
        private FastImage image;
        private double c = 0.6;

        public Bitmap Compute(FastImage workImage)
        {
            image = workImage;
            image.Lock();
            DOSmth();
            image.Unlock();
            return image.GetBitMap();
        }

        private void DOSmth()
        {
            for (var row = 1; row <= image.Height - 2; row++)
            {
                for (var col = 1; col <= image.Width - 2; col++)
                {
                    var point = new Point {X = col, Y = row};
                    var colorFtj = getFTJ(point);
                    var color = image.GetPixel(point);
                    var r = ComputeNewPixelColor(color.R, colorFtj.R);
                    var g = ComputeNewPixelColor(color.G, colorFtj.G);
                    var b = ComputeNewPixelColor(color.B, colorFtj.B);
                    var newColor = Color.FromArgb(r, g, b);
//                    var newColor = Color.FromArgb(colorFtj.R, colorFtj.G, colorFtj.B);
                    image.SetPixel(point, newColor);
                }
            }
        }

        private int ComputeNewPixelColor(int value, int colorFtj)
        {
            var primaFractie = c / (2 * c - 1);
            var doiFractie = (1 - c) / (2 * c - 1);

            var newValue = (primaFractie * value) - (doiFractie * colorFtj);
            return Utils.ClampByte((int) newValue);
        }

        private Color getFTJ(Point point)
        {
            var H = new double[,]
            {
//                {1 / 9, 1 / 9, 1 / 9},
//                {1 / 9, 1 / 9, 1 / 9},
//                {1 / 9, 1 / 9, 1 / 9}
                {1,1,1},
                {1,1,1},
                {1,1,1}
            };

            double sR = 0, sG = 0, sB = 0;
            Color color;
            for (var col = point.X; col <= point.X + 1; col++)
            {
                for (var row = point.Y - 1; row <= point.Y + 1; row++)
                {
                    var _point = new Point {X = col, Y = row};
                    color = image.GetPixel(_point);
                    sR += (color.R * H[row - point.Y + 1, col - point.X + 1]);
                    sG += (color.G * H[row - point.Y + 1, col - point.X + 1]);
                    sB += (color.B * H[row - point.Y + 1, col - point.X + 1]);
                }
            }

            var R = Utils.ClampByte((int) (sR / 9));
            var G = Utils.ClampByte((int) (sG / 9));
            var B = Utils.ClampByte((int) (sB / 9));
            color = Color.FromArgb(R, G, B);
            return color;
        }
    }
}