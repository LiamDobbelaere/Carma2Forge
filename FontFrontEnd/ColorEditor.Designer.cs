namespace Carma2Forge.FontFrontEnd {
  partial class ColorEditor {
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      pnlColor = new Panel();
      trkRed = new TrackBar();
      lblRed = new Label();
      lblGreen = new Label();
      trkGreen = new TrackBar();
      lblBlue = new Label();
      trkBlue = new TrackBar();
      ((System.ComponentModel.ISupportInitialize)trkRed).BeginInit();
      ((System.ComponentModel.ISupportInitialize)trkGreen).BeginInit();
      ((System.ComponentModel.ISupportInitialize)trkBlue).BeginInit();
      SuspendLayout();
      // 
      // pnlColor
      // 
      pnlColor.Location = new Point(3, 3);
      pnlColor.Name = "pnlColor";
      pnlColor.Size = new Size(88, 161);
      pnlColor.TabIndex = 0;
      // 
      // trkRed
      // 
      trkRed.Location = new Point(97, 21);
      trkRed.Maximum = 255;
      trkRed.Name = "trkRed";
      trkRed.Size = new Size(296, 45);
      trkRed.TabIndex = 1;
      trkRed.Scroll += trkRed_Scroll;
      // 
      // lblRed
      // 
      lblRed.AutoSize = true;
      lblRed.Location = new Point(97, 3);
      lblRed.Name = "lblRed";
      lblRed.Size = new Size(27, 15);
      lblRed.TabIndex = 2;
      lblRed.Text = "Red";
      // 
      // lblGreen
      // 
      lblGreen.AutoSize = true;
      lblGreen.Location = new Point(97, 51);
      lblGreen.Name = "lblGreen";
      lblGreen.Size = new Size(38, 15);
      lblGreen.TabIndex = 4;
      lblGreen.Text = "Green";
      // 
      // trkGreen
      // 
      trkGreen.Location = new Point(97, 69);
      trkGreen.Maximum = 255;
      trkGreen.Name = "trkGreen";
      trkGreen.Size = new Size(296, 45);
      trkGreen.TabIndex = 3;
      trkGreen.Scroll += trkGreen_Scroll;
      // 
      // lblBlue
      // 
      lblBlue.AutoSize = true;
      lblBlue.Location = new Point(97, 102);
      lblBlue.Name = "lblBlue";
      lblBlue.Size = new Size(30, 15);
      lblBlue.TabIndex = 6;
      lblBlue.Text = "Blue";
      // 
      // trkBlue
      // 
      trkBlue.Location = new Point(97, 120);
      trkBlue.Maximum = 255;
      trkBlue.Name = "trkBlue";
      trkBlue.Size = new Size(296, 45);
      trkBlue.TabIndex = 5;
      trkBlue.Scroll += trkBlue_Scroll;
      // 
      // ColorEditor
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      Controls.Add(lblBlue);
      Controls.Add(trkBlue);
      Controls.Add(lblGreen);
      Controls.Add(trkGreen);
      Controls.Add(lblRed);
      Controls.Add(trkRed);
      Controls.Add(pnlColor);
      Name = "ColorEditor";
      Size = new Size(396, 167);
      ((System.ComponentModel.ISupportInitialize)trkRed).EndInit();
      ((System.ComponentModel.ISupportInitialize)trkGreen).EndInit();
      ((System.ComponentModel.ISupportInitialize)trkBlue).EndInit();
      ResumeLayout(false);
      PerformLayout();
    }

    #endregion

    private Panel pnlColor;
    private TrackBar trkRed;
    private Label lblRed;
    private Label lblGreen;
    private TrackBar trkGreen;
    private Label lblBlue;
    private TrackBar trkBlue;
  }
}
