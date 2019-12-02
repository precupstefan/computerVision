using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ComputerVision.utils;

namespace ComputerVision.Operations
{
    public class ConturPrewitt
    {
        private FastImage image;

        public Bitmap Compute(FastImage workImage)
        {
            image = workImage;
            image.Lock();
            DoKirsch();
            image.Unlock();
            return image.GetBitMap();
        }

        private void DoKirsch()
        {
            for (var row = 1; row <= image.Height - 2; row++)
            {
                for (var col = 1; col <= image.Width - 2; col++)
                {
                    var point = new Point {X = col, Y = row};
                    var color = GetColorFromPoint(point);
                    image.SetPixel(point, color);
                }
            }
        }

        private Color GetColorFromPoint(Point point)
        {
            int sR = 0, sG = 0, sB = 0;
            var h1 = GetH1(point);
            var h2 = GetH2(point);

            var k = 1;
            sR = (int) (k * Math.Sqrt(h1.sR * h1.sR + h2.sR * h2.sR));
            sG = (int) (k * Math.Sqrt(h1.sG * h1.sG + h2.sG * h2.sG));
            sB = (int) (k * Math.Sqrt(h1.sB * h1.sB + h2.sB * h2.sB));

            sR = Utils.ClampByte(sR);
            sG = Utils.ClampByte(sG);
            sB = Utils.ClampByte(sB);

            return Color.FromArgb(sR, sG, sB);
        }

        private (int sR, int sG, int sB) GetMax((int sR, int sG, int sB) h1, (int sR, int sG, int sB) h2)
        {
            var list = new List<(int sR, int sG, int sB)> {h1, h2};

            var sR = list.Max(tuple => tuple.sR);
            var sB = list.Max(tuple => tuple.sB);
            var sG = list.Max(tuple => tuple.sG);

            return (sR, sG, sB);
        }


        private (int sR, int sG, int sB) GetH1(Point point)
        {
            var H = new int[,]
            {
                {-1, -1, -1},
                {0, 0, 0},
                {1, 1, 1}
            };
            int sR = 0, sG = 0, sB = 0;
            for (var col = point.X - 1; col <= point.X + 1; col++)
            {
                for (var row = point.Y - 1; row <= point.Y + 1; row++)
                {
                    var _point = new Point {X = col, Y = row};
                    var color = image.GetPixel(_point);
                    sG += (int) (color.G * H[row - point.Y + 1, col - point.X + 1]);
                    sR += (int) (color.R * H[row - point.Y + 1, col - point.X + 1]);
                    sB += (int) (color.B * H[row - point.Y + 1, col - point.X + 1]);
                }
            }

            return (sR: sR, sG: sG, sB: sB);
        }

        private (int sR, int sG, int sB) GetH2(Point point)
        {
            var H = new int[,]
            {
                {-1, 0, 1},
                {-1, 0, 1},
                {-1, 0, 1}
            };
            int sR = 0, sG = 0, sB = 0;
            for (var col = point.X - 1; col <= point.X + 1; col++)
            {
                for (var row = point.Y - 1; row <= point.Y + 1; row++)
                {
                    var _point = new Point {X = col, Y = row};
                    var color = image.GetPixel(_point);
                    sG += (int) (color.G * H[row - point.Y + 1, col - point.X + 1]);
                    sR += (int) (color.R * H[row - point.Y + 1, col - point.X + 1]);
                    sB += (int) (color.B * H[row - point.Y + 1, col - point.X + 1]);
                }
            }

            return (sR: sR, sG: sG, sB: sB);
        }
    }
}