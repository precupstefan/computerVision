namespace ComputerVision
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelSource = new System.Windows.Forms.Panel();
            this.panelDestination = new System.Windows.Forms.Panel();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.FtsButton = new System.Windows.Forms.Button();
            this.MarkovButton = new System.Windows.Forms.Button();
            this.buttonGrayscale = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.UnsharpMaskingButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSource
            // 
            this.panelSource.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelSource.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelSource.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.panelSource.Location = new System.Drawing.Point(14, 14);
            this.panelSource.Name = "panelSource";
            this.panelSource.Size = new System.Drawing.Size(373, 276);
            this.panelSource.TabIndex = 0;
            // 
            // panelDestination
            // 
            this.panelDestination.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelDestination.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelDestination.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.panelDestination.Location = new System.Drawing.Point(406, 14);
            this.panelDestination.Name = "panelDestination";
            this.panelDestination.Size = new System.Drawing.Size(373, 276);
            this.panelDestination.TabIndex = 1;
            // 
            // buttonLoad
            // 
            this.buttonLoad.Location = new System.Drawing.Point(14, 507);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(87, 27);
            this.buttonLoad.TabIndex = 2;
            this.buttonLoad.Text = "Load";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.UnsharpMaskingButton);
            this.panel1.Controls.Add(this.FtsButton);
            this.panel1.Controls.Add(this.MarkovButton);
            this.panel1.Controls.Add(this.buttonGrayscale);
            this.panel1.Location = new System.Drawing.Point(406, 313);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(373, 219);
            this.panel1.TabIndex = 3;
            // 
            // FtsButton
            // 
            this.FtsButton.Location = new System.Drawing.Point(8, 112);
            this.FtsButton.Name = "FtsButton";
            this.FtsButton.Size = new System.Drawing.Size(86, 27);
            this.FtsButton.TabIndex = 14;
            this.FtsButton.Text = "FTS";
            this.FtsButton.UseVisualStyleBackColor = true;
            this.FtsButton.Click += new System.EventHandler(this.FtsButton_Click);
            // 
            // MarkovButton
            // 
            this.MarkovButton.Location = new System.Drawing.Point(8, 145);
            this.MarkovButton.Name = "MarkovButton";
            this.MarkovButton.Size = new System.Drawing.Size(87, 27);
            this.MarkovButton.TabIndex = 4;
            this.MarkovButton.Text = "Markov";
            this.MarkovButton.UseVisualStyleBackColor = true;
            this.MarkovButton.Click += new System.EventHandler(this.MarkovButton_Click);
            // 
            // buttonGrayscale
            // 
            this.buttonGrayscale.Location = new System.Drawing.Point(8, 179);
            this.buttonGrayscale.Name = "buttonGrayscale";
            this.buttonGrayscale.Size = new System.Drawing.Size(87, 27);
            this.buttonGrayscale.TabIndex = 13;
            this.buttonGrayscale.Text = "Grayscale";
            this.buttonGrayscale.UseVisualStyleBackColor = true;
            this.buttonGrayscale.Click += new System.EventHandler(this.buttonGrayscale_Click);
            // 
            // UnsharpMaskingButton
            // 
            this.UnsharpMaskingButton.Location = new System.Drawing.Point(11, 80);
            this.UnsharpMaskingButton.Name = "UnsharpMaskingButton";
            this.UnsharpMaskingButton.Size = new System.Drawing.Size(83, 21);
            this.UnsharpMaskingButton.TabIndex = 15;
            this.UnsharpMaskingButton.Text = "UnsharpMasking";
            this.UnsharpMaskingButton.UseVisualStyleBackColor = true;
            this.UnsharpMaskingButton.Click += new System.EventHandler(this.UnsharpMaskingButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(793, 546);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buttonLoad);
            this.Controls.Add(this.panelDestination);
            this.Controls.Add(this.panelSource);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelSource;
        private System.Windows.Forms.Panel panelDestination;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonGrayscale;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button MarkovButton;
        private System.Windows.Forms.Button FtsButton;
        private System.Windows.Forms.Button UnsharpMaskingButton;
    }
}

