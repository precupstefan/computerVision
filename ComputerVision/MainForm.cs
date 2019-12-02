using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using ComputerVision.Operations;

namespace ComputerVision
{
    public partial class MainForm : Form
    {
        private string sSourceFileName = "";
        private FastImage workImage;
        private Bitmap image = null;

        public MainForm()
        {
            InitializeComponent();
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
            sSourceFileName = openFileDialog.FileName;
            panelSource.BackgroundImage = new Bitmap(sSourceFileName);
            image = new Bitmap(sSourceFileName);
            workImage = new FastImage(image);
        }

        private void buttonGrayscale_Click(object sender, EventArgs e)
        {
            Color color;

            workImage.Lock();
            for (int i = 0; i < workImage.Width; i++)
            {
                for (int j = 0; j < workImage.Height; j++)
                {
                    color = workImage.GetPixel(i, j);
                    byte R = color.R;
                    byte G = color.G;
                    byte B = color.B;

                    byte average = (byte) ((R + G + B) / 3);

                    color = Color.FromArgb(average, average, average);

                    workImage.SetPixel(i, j, color);
                }
            }

            panelDestination.BackgroundImage = null;
            panelDestination.BackgroundImage = workImage.GetBitMap();
            workImage.Unlock();
        }

        private void MarkovButton_Click(object sender, EventArgs e)
        {
            panelDestination.BackgroundImage = new Markov().Compute(workImage);
        }

        private void FtsButton_Click(object sender, EventArgs e)
        {
            panelDestination.BackgroundImage = new FTS().Compute(workImage);
        }

        private void UnsharpMaskingButton_Click(object sender, EventArgs e)
        {
            panelDestination.BackgroundImage = new UnsharpMasking().Compute(workImage);
        }

        private void Kirsch_Click(object sender, EventArgs e)
        {
            panelDestination.BackgroundImage = new Kirsch().Compute(workImage);
        }

        private void ConturlaPlaceButton_Click(object sender, EventArgs e)
        {
            panelDestination.BackgroundImage = new ConturLaPlace().Compute(workImage);
        }

        private void ConturRoberts_Click(object sender, EventArgs e)
        {
            panelDestination.BackgroundImage = new ConturRoberts().Compute(workImage);
        }

        private void PrewittButton_Click(object sender, EventArgs e)
        {
            panelDestination.BackgroundImage = new ConturPrewitt().Compute(workImage);
        }

        private void freiChenButton_Click(object sender, EventArgs e)
        {
            panelDestination.BackgroundImage = new ConturFreiChen().Compute(workImage);
        }

        private void GaborButton_Click(object sender, EventArgs e)
        {
            panelDestination.BackgroundImage = new ConturGabor().Compute(workImage);
        }

        private void panelSource_Click(object sender, EventArgs e)
        {
            var eventArgs = (MouseEventArgs) e;
            var point = new Point();
            point.X = eventArgs.X * workImage.Width / panelSource.Width;
            point.Y = eventArgs.Y * workImage.Height / panelSource.Height;
            panelDestination.BackgroundImage = new Segmentare(workImage,panelSource.Width,panelSource.Height).Compute(point);
        }
    }
}