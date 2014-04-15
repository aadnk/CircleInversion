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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CircleInvertor));
            this.PictureContainer = new System.Windows.Forms.PictureBox();
            this.buttonInvert = new System.Windows.Forms.Button();
            this.buttonImport = new System.Windows.Forms.Button();
            this.openImageFile = new System.Windows.Forms.OpenFileDialog();
            this.buttonClear = new System.Windows.Forms.Button();
            this.circleSizeSelector = new System.Windows.Forms.NumericUpDown();
            this.labelCircleSize = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolPencil = new System.Windows.Forms.ToolStripButton();
            this.toolLine = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.PictureContainer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.circleSizeSelector)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // PictureContainer
            // 
            this.PictureContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PictureContainer.BackColor = System.Drawing.Color.White;
            this.PictureContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PictureContainer.Location = new System.Drawing.Point(3, 28);
            this.PictureContainer.Name = "PictureContainer";
            this.PictureContainer.Size = new System.Drawing.Size(867, 447);
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
            // buttonClear
            // 
            this.buttonClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClear.Location = new System.Drawing.Point(593, 481);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(84, 26);
            this.buttonClear.TabIndex = 3;
            this.buttonClear.Text = "Clear Image";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // circleSizeSelector
            // 
            this.circleSizeSelector.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.circleSizeSelector.DecimalPlaces = 3;
            this.circleSizeSelector.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.circleSizeSelector.Location = new System.Drawing.Point(87, 486);
            this.circleSizeSelector.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.circleSizeSelector.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.circleSizeSelector.Name = "circleSizeSelector";
            this.circleSizeSelector.Size = new System.Drawing.Size(59, 20);
            this.circleSizeSelector.TabIndex = 4;
            this.circleSizeSelector.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.circleSizeSelector.ValueChanged += new System.EventHandler(this.circleSizeSelector_ValueChanged);
            // 
            // labelCircleSize
            // 
            this.labelCircleSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelCircleSize.AutoSize = true;
            this.labelCircleSize.Location = new System.Drawing.Point(12, 488);
            this.labelCircleSize.Name = "labelCircleSize";
            this.labelCircleSize.Size = new System.Drawing.Size(59, 13);
            this.labelCircleSize.TabIndex = 5;
            this.labelCircleSize.Text = "Circle Size:";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolPencil,
            this.toolLine});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(871, 25);
            this.toolStrip1.TabIndex = 0;
            // 
            // toolPencil
            // 
            this.toolPencil.Checked = true;
            this.toolPencil.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolPencil.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolPencil.Image = ((System.Drawing.Image)(resources.GetObject("toolPencil.Image")));
            this.toolPencil.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolPencil.Name = "toolPencil";
            this.toolPencil.Size = new System.Drawing.Size(23, 22);
            this.toolPencil.Text = "Draw Pencil";
            this.toolPencil.Click += new System.EventHandler(this.toolPencil_Click);
            // 
            // toolLine
            // 
            this.toolLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolLine.Image = ((System.Drawing.Image)(resources.GetObject("toolLine.Image")));
            this.toolLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolLine.Name = "toolLine";
            this.toolLine.Size = new System.Drawing.Size(23, 22);
            this.toolLine.Text = "Draw Line";
            this.toolLine.Click += new System.EventHandler(this.toolLine_Click);
            // 
            // CircleInvertor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(871, 514);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.labelCircleSize);
            this.Controls.Add(this.circleSizeSelector);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonImport);
            this.Controls.Add(this.buttonInvert);
            this.Controls.Add(this.PictureContainer);
            this.Name = "CircleInvertor";
            this.Text = "Circle Invertor";
            this.ResizeEnd += new System.EventHandler(this.CircleInvertor_ResizeEnd);
            this.SizeChanged += new System.EventHandler(this.CircleInvertor_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.PictureContainer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.circleSizeSelector)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox PictureContainer;
        private System.Windows.Forms.Button buttonInvert;
        private System.Windows.Forms.Button buttonImport;
        private System.Windows.Forms.OpenFileDialog openImageFile;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.NumericUpDown circleSizeSelector;
        private System.Windows.Forms.Label labelCircleSize;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolPencil;
        private System.Windows.Forms.ToolStripButton toolLine;
    }
}

