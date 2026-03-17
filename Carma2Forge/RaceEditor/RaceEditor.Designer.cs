
namespace Carma2Forge {
  partial class RaceEditor {
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
      lvRaces = new ListView();
      columnHeader1 = new ColumnHeader();
      colorDialog1 = new ColorDialog();
      SuspendLayout();
      // 
      // lvRaces
      // 
      lvRaces.AllowColumnReorder = true;
      lvRaces.Columns.AddRange(new ColumnHeader[] { columnHeader1 });
      lvRaces.Dock = DockStyle.Left;
      lvRaces.Location = new Point(0, 0);
      lvRaces.MultiSelect = false;
      lvRaces.Name = "lvRaces";
      lvRaces.Size = new Size(317, 450);
      lvRaces.TabIndex = 1;
      lvRaces.UseCompatibleStateImageBehavior = false;
      lvRaces.View = View.Details;
      // 
      // columnHeader1
      // 
      columnHeader1.Text = "Name";
      columnHeader1.Width = 256;
      // 
      // colorDialog1
      // 
      colorDialog1.AnyColor = true;
      colorDialog1.FullOpen = true;
      // 
      // RaceEditor
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(1140, 450);
      Controls.Add(lvRaces);
      Name = "RaceEditor";
      Text = "Race Editor";
      Load += RaceEditor_Load;
      ResumeLayout(false);
    }

    #endregion

    private ListView lvRaces;
    private ColumnHeader columnHeader1;
    private ColorDialog colorDialog1;
    private FontFrontEnd.ColorEditor ceTopLeft;
    private FontFrontEnd.ColorEditor ceTopRight;
    private FontFrontEnd.ColorEditor ceBottomLeft;
    private FontFrontEnd.ColorEditor ceBottomRight;
  }
}