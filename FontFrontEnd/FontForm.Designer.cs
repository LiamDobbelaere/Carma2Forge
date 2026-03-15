
namespace Carma2Forge {
  partial class FontForm {
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
      lvFontColDefs = new ListView();
      columnHeader1 = new ColumnHeader();
      columnHeader2 = new ColumnHeader();
      colorDialog1 = new ColorDialog();
      ceTopLeft = new FontFrontEnd.ColorEditor();
      ceTopRight = new FontFrontEnd.ColorEditor();
      ceBottomLeft = new FontFrontEnd.ColorEditor();
      ceBottomRight = new FontFrontEnd.ColorEditor();
      btnSave = new Button();
      SuspendLayout();
      // 
      // lvFontColDefs
      // 
      lvFontColDefs.AllowColumnReorder = true;
      lvFontColDefs.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2 });
      lvFontColDefs.Dock = DockStyle.Left;
      lvFontColDefs.Location = new Point(0, 0);
      lvFontColDefs.MultiSelect = false;
      lvFontColDefs.Name = "lvFontColDefs";
      lvFontColDefs.Size = new Size(317, 450);
      lvFontColDefs.TabIndex = 1;
      lvFontColDefs.UseCompatibleStateImageBehavior = false;
      lvFontColDefs.View = View.Details;
      lvFontColDefs.SelectedIndexChanged += lvFontColDefs_SelectedIndexChanged;
      // 
      // columnHeader1
      // 
      columnHeader1.Text = "Name";
      columnHeader1.Width = 256;
      // 
      // columnHeader2
      // 
      columnHeader2.Text = "Id";
      // 
      // colorDialog1
      // 
      colorDialog1.AnyColor = true;
      colorDialog1.FullOpen = true;
      // 
      // ceTopLeft
      // 
      ceTopLeft.Color = Color.FromArgb(0, 0, 0);
      ceTopLeft.Location = new Point(323, 12);
      ceTopLeft.Name = "ceTopLeft";
      ceTopLeft.Size = new Size(407, 168);
      ceTopLeft.TabIndex = 2;
      ceTopLeft.ColorChanged += ceTopLeft_ColorChanged;
      // 
      // ceTopRight
      // 
      ceTopRight.Color = Color.FromArgb(0, 0, 0);
      ceTopRight.Location = new Point(736, 12);
      ceTopRight.Name = "ceTopRight";
      ceTopRight.Size = new Size(407, 168);
      ceTopRight.TabIndex = 3;
      ceTopRight.ColorChanged += ceTopRight_ColorChanged;
      // 
      // ceBottomLeft
      // 
      ceBottomLeft.Color = Color.FromArgb(0, 0, 0);
      ceBottomLeft.Location = new Point(323, 186);
      ceBottomLeft.Name = "ceBottomLeft";
      ceBottomLeft.Size = new Size(407, 168);
      ceBottomLeft.TabIndex = 4;
      ceBottomLeft.ColorChanged += ceBottomLeft_ColorChanged;
      // 
      // ceBottomRight
      // 
      ceBottomRight.Color = Color.FromArgb(0, 0, 0);
      ceBottomRight.Location = new Point(736, 186);
      ceBottomRight.Name = "ceBottomRight";
      ceBottomRight.Size = new Size(407, 168);
      ceBottomRight.TabIndex = 5;
      ceBottomRight.ColorChanged += ceBottomRight_ColorChanged;
      // 
      // btnSave
      // 
      btnSave.Location = new Point(1016, 406);
      btnSave.Name = "btnSave";
      btnSave.Size = new Size(112, 32);
      btnSave.TabIndex = 6;
      btnSave.Text = "Save changes";
      btnSave.UseVisualStyleBackColor = true;
      btnSave.Click += btnSave_Click;
      // 
      // FontForm
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(1140, 450);
      Controls.Add(btnSave);
      Controls.Add(ceBottomRight);
      Controls.Add(ceBottomLeft);
      Controls.Add(ceTopRight);
      Controls.Add(ceTopLeft);
      Controls.Add(lvFontColDefs);
      Name = "FontForm";
      Text = "FontForm";
      FormClosing += FontForm_FormClosing;
      Load += FontForm_Load;
      ResumeLayout(false);
    }

    #endregion

    private ListView lvFontColDefs;
    private ColumnHeader columnHeader1;
    private ColumnHeader columnHeader2;
    private ColorDialog colorDialog1;
    private FontFrontEnd.ColorEditor ceTopLeft;
    private FontFrontEnd.ColorEditor ceTopRight;
    private FontFrontEnd.ColorEditor ceBottomLeft;
    private FontFrontEnd.ColorEditor ceBottomRight;
    private Button btnSave;
  }
}