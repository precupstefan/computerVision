using System.Drawing;
using ComputerVision.utils;

namespace ComputerVision.Operations
{
    public class FTS
    {
        private FastImage image;

        private int[,] H = new int[3, 3]
        {
            {0, -1, 0},
            {-1, 5, -1},
            {0, -1, 0},
        };

        public Bitmap Compute(FastImage workImage)
        {
            image = workImage;
            image.Lock();
            Fts();
            image.Unlock();
            return image.GetBitMap();
        }

        private void Fts()
        {
            for (var row = 1; row <= image.Height - 2; row++)
            {
                for (var col = 1; col <= image.Width - 2; col++)
                {
                    int sR = 0, sG = 0, sB = 0;
                    Color color;
                    for (var _col = col - 1; _col <= col + 1; _col++)
                    {
                        for (var _row = row - 1; _row <= row + 1; _row++)
                        {
                            var _point = new Point {X = _col, Y = _row};
                            color = image.GetPixel(_point);
                            sR += (int) (color.R * H[_row - row + 1, _col - col + 1]);
                            sG += (int) (color.G * H[_row - row + 1, _col - col + 1]);
                            sB += (int) (color.B * H[_row - row + 1, _col - col + 1]);
                        }
                    }
                    var point = new Point {X = col, Y = row};
                    sR = Utils.ClampByte(sR);
                    sG = Utils.ClampByte(sG);
                    sB = Utils.ClampByte(sB);
                    color = Color.FromArgb(sR, sG, sB);
                    image.SetPixel(point, color);
                }
            }
        }
    }
}