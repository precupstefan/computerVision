using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace ComputerVision
{
    public class FastImage
    {
        public int Height = 0;
        public int Width = 0;
        private Bitmap image = null;
        private Bitmap shadowCopy = null;
        private Rectangle rectangle;
        private BitmapData bitmapData = null;
        private BitmapData shadowCopybitmapData = null;
        private Color color;
        private Point size;
        private int currentBitmapWidth = 0;

        struct PixelData
        {
            public byte red, green, blue;
        }

        public FastImage(Bitmap bitmap)
        {
            image = bitmap;
            Width = image.Width;
            Height = image.Height;
            shadowCopy = bitmap.Clone(new Rectangle(0, 0, Width, Height), bitmap.PixelFormat);
            size = new Point(image.Size);
            currentBitmapWidth = size.X;
        }

        public void Lock()
        {
            // Rectangle For Locking The Bitmap In Memory
            rectangle = new Rectangle(0, 0, image.Width, image.Height);
            // Get The Bitmap's Pixel Data From The Locked Bitmap
            bitmapData = image.LockBits(rectangle, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            shadowCopybitmapData = shadowCopy.LockBits(rectangle, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
        }

        public void Unlock()
        {
            image.UnlockBits(bitmapData);
            shadowCopy.UnlockBits(shadowCopybitmapData);
        }

        public Color GetPixel(int col, int row)
        {
            unsafe
            {
                PixelData* pBase = (PixelData*) shadowCopybitmapData.Scan0;
                PixelData* pPixel = pBase + row * currentBitmapWidth + col;
                color = Color.FromArgb(pPixel->red, pPixel->green, pPixel->blue);
            }

            return color;
        }

        public Color GetPixel(Point point)
        {
            unsafe
            {
                PixelData* pBase = (PixelData*) shadowCopybitmapData.Scan0;
                PixelData* pPixel = pBase + point.Y * currentBitmapWidth + point.X;
                color = Color.FromArgb(pPixel->red, pPixel->green, pPixel->blue);
            }

            return color;
        }

        public void SetPixel(int col, int row, Color c)
        {
            unsafe
            {
                PixelData* pBase = (PixelData*) bitmapData.Scan0;
                PixelData* pPixel = pBase + row * currentBitmapWidth + col;
                pPixel->red = c.R;
                pPixel->green = c.G;
                pPixel->blue = c.B;
            }
        }

        public void SetPixel(Point point, Color c)
        {
            unsafe
            {
                PixelData* pBase = (PixelData*) bitmapData.Scan0;
                PixelData* pPixel = pBase + point.Y * currentBitmapWidth + point.X;
                pPixel->red = c.R;
                pPixel->green = c.G;
                pPixel->blue = c.B;
            }
        }

        public Bitmap GetBitMap()
        {
            return image;
        }
    }
}