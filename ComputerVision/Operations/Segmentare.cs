using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using ComputerVision.utils;

namespace ComputerVision.Operations
{
    public class Segmentare
    {
        private FastImage image;
        private readonly int panelWidth;
        private readonly int panelHeight;

        private int sumaIntensitati;
        private int contor;
        private double mediaIntensitati => contor == 0 ? 0 : sumaIntensitati / contor;
        private bool[,] matriceVizitate;
        private double prag = 70d;


        private FastImage canvas;
        private Queue<Point> pointsQueue = new Queue<Point>();

        public Segmentare(FastImage image, int panelWidth, int panelHeight)
        {
            this.image = image;
            this.panelWidth = panelWidth;
            this.panelHeight = panelHeight;
            this.canvas = new FastImage(new Bitmap(image.Width, image.Height));
            matriceVizitate = new bool[image.Width + 1, image.Height + 1];
        }

        public Bitmap Compute(Point point)
        {
            image.Lock();
            canvas.Lock();
            pointsQueue.Enqueue(point);
            sumaIntensitati += Utils.GetGrayscale(image.GetPixel(point));
            contor++;

            while (pointsQueue.Count > 0)
            {
                var selectedPoint = pointsQueue.Peek();

                if (matriceVizitate[selectedPoint.X, selectedPoint.Y])
                {
                    pointsQueue.Dequeue();
                    continue;
                }

                var limitXmin = selectedPoint.X == 0 ? selectedPoint.X : selectedPoint.X - 1;
                var limitXmax = selectedPoint.X == image.Width - 1 ? selectedPoint.X : selectedPoint.X + 1;
                var limitYmin = selectedPoint.Y == 0 ? 0 : selectedPoint.Y - 1;
                var limitYmax = selectedPoint.Y == image.Height - 1 ? selectedPoint.Y : selectedPoint.Y + 1;

                var pointA = new Point {X = limitXmin, Y = limitYmin};
                var pointB = new Point {X = limitXmax, Y = limitYmin};
                var pointC = new Point {X = limitXmin, Y = limitYmax};
                var pointD = new Point {X = limitXmax, Y = limitYmax};

                var culoareA = image.GetPixel(pointA);
                var culoareB = image.GetPixel(pointB);
                var culoareC = image.GetPixel(pointC);
                var culoareD = image.GetPixel(pointD);

                if (Math.Abs(Utils.GetGrayscale(culoareA) - mediaIntensitati) < prag)
                {
                    pointsQueue.Enqueue(pointA);
                }

                if (Math.Abs(Utils.GetGrayscale(culoareB) - mediaIntensitati) < prag)
                {
                    pointsQueue.Enqueue(pointB);
                }

                if (Math.Abs(Utils.GetGrayscale(culoareB) - mediaIntensitati) < prag)
                {
                    pointsQueue.Enqueue(pointC);
                }

                if (Math.Abs(Utils.GetGrayscale(culoareB) - mediaIntensitati) < prag)
                {
                    pointsQueue.Enqueue(pointD);
                }

                sumaIntensitati += Utils.GetGrayscale(image.GetPixel(point));
                contor++;
                matriceVizitate[selectedPoint.X, selectedPoint.Y] = true;
                canvas.SetPixel(selectedPoint, image.GetPixel(selectedPoint));
                pointsQueue.Dequeue();
            }

            image.Unlock();
            canvas.Unlock();

            return canvas.GetBitMap();
        }
    }
}