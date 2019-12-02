using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ComputerVision.utils;

namespace ComputerVision.Operations
{
    public class ConturGabor
    {
        private FastImage image;

        private int[,] P =
        {
            {1, 1, 1},
            {0, 0, 0},
            {-1, -1, -1}
        };

        private int[,] Q =
        {
            {-1, 0, 1},
            {-1, 0, 1},
            {-1, 0, 1},
        };

        public Bitmap Compute(FastImage workImage)
        {
            image = workImage;
            image.Lock();
            DoGabor();
            image.Unlock();
            return image.GetBitMap();
        }

        private void DoGabor()
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
            var p = GetP(point);
            var q = GetQ(point);

            sR = (int) ComputePixelR(p.sR, q.sR, point);
            sG = (int) ComputePixelG(p.sG, q.sG, point);
            sB = (int) ComputePixelB(p.sB, q.sB, point);

            sR = Utils.ClampByte(sR);
            sG = Utils.ClampByte(sG);
            sB = Utils.ClampByte(sB);
            return Color.FromArgb(sR, sG, sB);
        }

        private double ComputePixelR(double pS, double qS, Point point)
        {
            double u;
            if (qS == 0)
            {
                u = pS > 0 ? Math.PI / 2 : -Math.PI / 2;
            }
            else
            {
                u = Math.Atan(pS / qS);
                if (qS < 0)
                {
                    u += Math.PI;
                }
            }

            u += Math.PI / 2;
            var suma = 0d;
            var sigma = 0.66;
            var omega = 1.5;
            var colorPoint = image.GetPixel(point);
            for (var col = point.X - 1; col <= point.X + 1; col++)
            {
                for (var row = point.Y - 1; row <= point.Y + 1; row++)
                {
                    var _point = new Point {X = col, Y = row};
                    var color = image.GetPixel(_point);
                    var pozr = -point.Y - row + 1;
                    var pozc = -point.X - col + 1;
                    var scale = Math.Pow(Math.E, -(pozr * pozr + pozc * pozc) / (2 * sigma * sigma));
                    scale = scale * Math.Sin(omega * (pozr * Math.Cos(u) + pozc * Math.Sin(u)));
                    suma += scale * colorPoint.R;
                }
            }

            return suma;
        }
        
        private double ComputePixelG(double pS, double qS, Point point)
        {
            double u;
            if (qS == 0)
            {
                u = pS > 0 ? Math.PI / 2 : -Math.PI / 2;
            }
            else
            {
                u = Math.Atan(pS / qS);
                if (qS < 0)
                {
                    u += Math.PI;
                }
            }

            u += Math.PI / 2;
            var suma = 0d;
            var sigma = 0.66;
            var omega = 1.5;
            var colorPoint = image.GetPixel(point);
            for (var col = point.X - 1; col <= point.X + 1; col++)
            {
                for (var row = point.Y - 1; row <= point.Y + 1; row++)
                {
                    var _point = new Point {X = col, Y = row};
                    var color = image.GetPixel(_point);
                    var pozr = -point.Y - row + 1;
                    var pozc = -point.X - col + 1;
                    var scale = Math.Pow(Math.E, -(pozr * pozr + pozc * pozc) / (2 * sigma * sigma));
                    scale = scale * Math.Sin(omega * (pozr * Math.Cos(u) + pozc * Math.Sin(u)));
                    suma += scale * colorPoint.G;
                }
            }

            return suma;
        }

        private double ComputePixelB(double pS, double qS, Point point)
        {
            double u;
            if (qS == 0)
            {
                u = pS > 0 ? Math.PI / 2 : -Math.PI / 2;
            }
            else
            {
                u = Math.Atan(pS / qS);
                if (qS < 0)
                {
                    u += Math.PI;
                }
            }

            u += Math.PI / 2;
            var suma = 0d;
            var sigma = 0.66;
            var omega = 1.5;
            var colorPoint = image.GetPixel(point);
            for (var col = point.X - 1; col <= point.X + 1; col++)
            {
                for (var row = point.Y - 1; row <= point.Y + 1; row++)
                {
                    var _point = new Point {X = col, Y = row};
                    var color = image.GetPixel(_point);
                    var pozr = -point.Y - row + 1;
                    var pozc = -point.X - col + 1;
                    var scale = Math.Pow(Math.E, -(pozr * pozr + pozc * pozc) / (2 * sigma * sigma));
                    scale = scale * Math.Sin(omega * (pozr * Math.Cos(u) + pozc * Math.Sin(u)));
                    suma += scale * colorPoint.B;
                }
            }

            return suma;
        }
        private (double sR, double sG, double sB) GetP(Point point)
        {
            var H = P;
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

        private (double sR, double sG, double sB) GetQ(Point point)
        {
            var H = Q;
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