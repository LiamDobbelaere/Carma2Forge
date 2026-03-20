namespace Carma2Forge {
  partial class MapEditor {
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
      pbxMap = new PictureBox();
      ((System.ComponentModel.ISupportInitialize)pbxMap).BeginInit();
      SuspendLayout();
      // 
      // pbxMap
      // 
      pbxMap.Dock = DockStyle.Fill;
      pbxMap.Location = new Point(0, 0);
      pbxMap.Name = "pbxMap";
      pbxMap.Size = new Size(1362, 621);
      pbxMap.SizeMode = PictureBoxSizeMode.Zoom;
      pbxMap.TabIndex = 0;
      pbxMap.TabStop = false;
      // 
      // MapEditor
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(1362, 621);
      Controls.Add(pbxMap);
      Name = "MapEditor";
      Text = "Map Editor";
      ((System.ComponentModel.ISupportInitialize)pbxMap).EndInit();
      ResumeLayout(false);
    }

    #endregion

    private PictureBox pbxMap;
  }
}