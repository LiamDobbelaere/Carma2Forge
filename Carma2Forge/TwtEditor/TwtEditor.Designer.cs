namespace Carma2Forge {
  partial class TwtEditor {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      rtbTest = new RichTextBox();
      pictureBox1 = new PictureBox();
      ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
      SuspendLayout();
      // 
      // rtbTest
      // 
      rtbTest.Location = new Point(223, 118);
      rtbTest.Name = "rtbTest";
      rtbTest.Size = new Size(404, 267);
      rtbTest.TabIndex = 0;
      rtbTest.Text = "";
      // 
      // pictureBox1
      // 
      pictureBox1.Location = new Point(12, 118);
      pictureBox1.Name = "pictureBox1";
      pictureBox1.Size = new Size(145, 172);
      pictureBox1.TabIndex = 1;
      pictureBox1.TabStop = false;
      // 
      // TwtEditor
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(800, 450);
      Controls.Add(pictureBox1);
      Controls.Add(rtbTest);
      Name = "TwtEditor";
      Text = "TwtEditor";
      Load += TwtEditor_Load;
      ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
      ResumeLayout(false);
    }

    #endregion

    private RichTextBox rtbTest;
    private PictureBox pictureBox1;
  }
}