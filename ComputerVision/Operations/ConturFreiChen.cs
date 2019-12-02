using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ComputerVision.utils;

namespace ComputerVision.Operations
{
    public class ConturFreiChen
    {
        private FastImage image;

        public Bitmap Compute(FastImage workImage)
        {
            image = workImage;
            image.Lock();
            DoFreiChen();
            image.Unlock();
            return image.GetBitMap();
        }

        private void DoFreiChen()
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
            var f1 = GetF1(point);
            var f2 = GetF2(point);
            var f3 = GetF3(point);
            var f4 = GetF4(point);
            var f5 = GetF5(point);
            var f6 = GetF6(point);
            var f7 = GetF7(point);
            var f8 = GetF8(point);
            var f9 = GetF9(point);

            var ceva = new List<(double sR, double sG, double sB)> {f1, f2, f3, f4, f5, f6, f7, f8, f9};

            var suma4R = ceva.Take(4).Select((tuple, i) => tuple.sR * tuple.sR).Sum();
            var suma4G = ceva.Take(4).Select((tuple, i) => tuple.sG * tuple.sG).Sum();
            var suma4B = ceva.Take(4).Select((tuple, i) => tuple.sB * tuple.sB).Sum();
            var suma9R = ceva.Take(9).Select((tuple, i) => tuple.sR * tuple.sR).Sum();
            var suma9G = ceva.Take(9).Select((tuple, i) => tuple.sG * tuple.sG).Sum();
            var suma9B = ceva.Take(9).Select((tuple, i) => tuple.sB * tuple.sB).Sum();


            if (suma9R != 0)
            {
                var rR = Math.Sqrt(suma4R / suma9R) * 255;
                sR = Utils.ClampByte((int) rR);
            }
            else
            {
                sR = 0;
            }

            if (suma9G != 0)
            {
                var rG = Math.Sqrt(suma4G / suma9G) * 255;
                sG = Utils.ClampByte((int) rG);
            }
            else
            {
                sG = 0;
            }

            if (suma9B != 0d)
            {
                var rB = Math.Sqrt(suma4B / suma9B) * 255;
                sB = Utils.ClampByte((int) rB);
            }
            else
            {
                sB = 0;
            }

            return Color.FromArgb(sR, sG, sB);
        }

        private (double sR, double sG, double sB) GetF1(Point point)
        {
            var H = new double[,]
            {
                {1, Math.Sqrt(2), 1},
                {0, 0, 0},
                {-1, -Math.Sqrt(2), -1}
            };
            double sR = 0, sG = 0, sB = 0;
            for (var col = point.X - 1; col <= point.X + 1; col++)
            {
                for (var row = point.Y - 1; row <= point.Y + 1; row++)
                {
                    var _point = new Point {X = col, Y = row};
                    var color = image.GetPixel(_point);
                    sG += (color.G * H[row - point.Y + 1, col - point.X + 1]);
                    sR += (color.R * H[row - point.Y + 1, col - point.X + 1]);
                    sB += (color.B * H[row - point.Y + 1, col - point.X + 1]);
                }
            }

            return (sR: sR, sG: sG, sB: sB);
        }

        private (double sR, double sG, double sB) GetF2(Point point)
        {
            var H = new double[,]
            {
                {1, 0, -1},
                {Math.Sqrt(2), 0, -Math.Sqrt(2)},
                {1, 0, -1}
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

        private (double sR, double sG, double sB) GetF3(Point point)
        {
            var H = new double[,]
            {
                {0, -1, Math.Sqrt(2)},
                {1, 0, -1},
                {-Math.Sqrt(2), 1, 0}
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

        private (double sR, double sG, double sB) GetF4(Point point)
        {
            var H = new double[,]
            {
                {Math.Sqrt(2), -1, 0},
                {-1, 0, 1},
                {0, 1, -Math.Sqrt(2)}
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

        private (double sR, double sG, double sB) GetF5(Point point)
        {
            var H = new double[,]
            {
                {0, 1, 0},
                {-1, 0, -1},
                {0, 1, 0}
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

        private (double sR, double sG, double sB) GetF6(Point point)
        {
            var H = new double[,]
            {
                {-1, 0, 1},
                {0, 0, 0},
                {1, 0, -1}
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

        private (double sR, double sG, double sB) GetF7(Point point)
        {
            var H = new double[,]
            {
                {1, -2, 1},
                {-2, 4, -2},
                {1, -2, 1}
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

        private (double sR, double sG, double sB) GetF8(Point point)
        {
            var H = new double[,]
            {
                {-2, 1, -2},
                {1, 4, 1},
                {-2, 1, -2}
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

        private (double sR, double sG, double sB) GetF9(Point point)
        {
            var H = new double[,]
            {
                {1d / 9, 1d / 9, 1d / 9},
                {1d / 9, 1d / 9, 1d / 9},
                {1d / 9, 1d / 9, 1d / 9},
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