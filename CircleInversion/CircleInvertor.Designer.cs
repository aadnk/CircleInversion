namespace CircleInversion
{
    partial class CircleInvertor
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
            this.PictureContainer = new System.Windows.Forms.PictureBox();
            this.buttonInvert = new System.Windows.Forms.Button();
            this.buttonImport = new System.Windows.Forms.Button();
            this.openImageFile = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.PictureContainer)).BeginInit();
            this.SuspendLayout();
            // 
            // PictureContainer
            // 
            this.PictureContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PictureContainer.Location = new System.Drawing.Point(3, 2);
            this.PictureContainer.Name = "PictureContainer";
            this.PictureContainer.Size = new System.Drawing.Size(867, 473);
            this.PictureContainer.TabIndex = 0;
            this.PictureContainer.TabStop = false;
            this.PictureContainer.WaitOnLoad = true;
            this.PictureContainer.Paint += new System.Windows.Forms.PaintEventHandler(this.PictureContainer_Paint);
            this.PictureContainer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureContainer_MouseDown);
            this.PictureContainer.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureContainer_MouseMove);
            this.PictureContainer.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PictureContainer_MouseUp);
            // 
            // buttonInvert
            // 
            this.buttonInvert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonInvert.Location = new System.Drawing.Point(773, 481);
            this.buttonInvert.Name = "buttonInvert";
            this.buttonInvert.Size = new System.Drawing.Size(86, 26);
            this.buttonInvert.TabIndex = 1;
            this.buttonInvert.Text = "Invert";
            this.buttonInvert.UseVisualStyleBackColor = true;
            this.buttonInvert.Click += new System.EventHandler(this.buttonInvert_Click);
            // 
            // buttonImport
            // 
            this.buttonImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonImport.Location = new System.Drawing.Point(683, 481);
            this.buttonImport.Name = "buttonImport";
            this.buttonImport.Size = new System.Drawing.Size(84, 26);
            this.buttonImport.TabIndex = 2;
            this.buttonImport.Text = "Import Image";
            this.buttonImport.UseVisualStyleBackColor = true;
            this.buttonImport.Click += new System.EventHandler(this.buttonImport_Click);
            // 
            // openImageFile
            // 
            this.openImageFile.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif;" +
    " *.png";
            // 
            // CircleInvertor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(871, 514);
            this.Controls.Add(this.buttonImport);
            this.Controls.Add(this.buttonInvert);
            this.Controls.Add(this.PictureContainer);
            this.Name = "CircleInvertor";
            this.Text = "Circle Invertor";
            this.ResizeEnd += new System.EventHandler(this.CircleInvertor_ResizeEnd);
            this.SizeChanged += new System.EventHandler(this.CircleInvertor_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.PictureContainer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox PictureContainer;
        private System.Windows.Forms.Button buttonInvert;
        private System.Windows.Forms.Button buttonImport;
        private System.Windows.Forms.OpenFileDialog openImageFile;
    }
}

