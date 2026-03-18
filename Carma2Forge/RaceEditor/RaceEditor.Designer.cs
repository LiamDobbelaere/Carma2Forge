
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
      pictureBox1 = new PictureBox();
      btnEditMap = new Button();
      ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
      SuspendLayout();
      // 
      // lvRaces
      // 
      lvRaces.AllowColumnReorder = true;
      lvRaces.Columns.AddRange(new ColumnHeader[] { columnHeader1 });
      lvRaces.Dock = DockStyle.Left;
      lvRaces.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
      lvRaces.Location = new Point(0, 0);
      lvRaces.MultiSelect = false;
      lvRaces.Name = "lvRaces";
      lvRaces.Size = new Size(317, 450);
      lvRaces.TabIndex = 1;
      lvRaces.UseCompatibleStateImageBehavior = false;
      lvRaces.View = View.Details;
      lvRaces.SelectedIndexChanged += lvRaces_SelectedIndexChanged;
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
      // pictureBox1
      // 
      pictureBox1.Location = new Point(323, 12);
      pictureBox1.Name = "pictureBox1";
      pictureBox1.Size = new Size(565, 426);
      pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
      pictureBox1.TabIndex = 2;
      pictureBox1.TabStop = false;
      // 
      // btnEditMap
      // 
      btnEditMap.Location = new Point(894, 415);
      btnEditMap.Name = "btnEditMap";
      btnEditMap.Size = new Size(75, 23);
      btnEditMap.TabIndex = 3;
      btnEditMap.Text = "Edit Map";
      btnEditMap.UseVisualStyleBackColor = true;
      btnEditMap.Click += btnEditMap_Click;
      // 
      // RaceEditor
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(1140, 450);
      Controls.Add(btnEditMap);
      Controls.Add(pictureBox1);
      Controls.Add(lvRaces);
      Name = "RaceEditor";
      Text = "Race Editor";
      Load += RaceEditor_Load;
      ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
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
    private PictureBox pictureBox1;
    private Button btnEditMap;
  }
}