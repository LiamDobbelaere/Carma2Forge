namespace Carma2Forge {
  partial class OpponentEditor {
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
      lvOpponents = new ListView();
      columnHeader1 = new ColumnHeader();
      columnHeader2 = new ColumnHeader();
      pbxCar = new PictureBox();
      pbxCarInterface = new PictureBox();
      pbxDriver = new PictureBox();
      lblCarName = new Label();
      lblDriverName = new Label();
      lblCarStats = new Label();
      lblCarDescription = new Label();
      ((System.ComponentModel.ISupportInitialize)pbxCar).BeginInit();
      ((System.ComponentModel.ISupportInitialize)pbxCarInterface).BeginInit();
      ((System.ComponentModel.ISupportInitialize)pbxDriver).BeginInit();
      SuspendLayout();
      // 
      // lvOpponents
      // 
      lvOpponents.AllowColumnReorder = true;
      lvOpponents.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2 });
      lvOpponents.Dock = DockStyle.Left;
      lvOpponents.Location = new Point(0, 0);
      lvOpponents.MultiSelect = false;
      lvOpponents.Name = "lvOpponents";
      lvOpponents.Size = new Size(317, 500);
      lvOpponents.TabIndex = 2;
      lvOpponents.UseCompatibleStateImageBehavior = false;
      lvOpponents.View = View.Details;
      lvOpponents.SelectedIndexChanged += lvOpponents_SelectedIndexChanged;
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
      // pbxCar
      // 
      pbxCar.BackColor = Color.Transparent;
      pbxCar.BackgroundImageLayout = ImageLayout.Zoom;
      pbxCar.Location = new Point(370, 104);
      pbxCar.Name = "pbxCar";
      pbxCar.Size = new Size(192, 128);
      pbxCar.TabIndex = 3;
      pbxCar.TabStop = false;
      // 
      // pbxCarInterface
      // 
      pbxCarInterface.Location = new Point(323, 12);
      pbxCarInterface.Name = "pbxCarInterface";
      pbxCarInterface.Size = new Size(640, 480);
      pbxCarInterface.SizeMode = PictureBoxSizeMode.Zoom;
      pbxCarInterface.TabIndex = 4;
      pbxCarInterface.TabStop = false;
      // 
      // pbxDriver
      // 
      pbxDriver.BackColor = Color.Transparent;
      pbxDriver.BackgroundImageLayout = ImageLayout.Zoom;
      pbxDriver.Location = new Point(531, 201);
      pbxDriver.Name = "pbxDriver";
      pbxDriver.Size = new Size(64, 64);
      pbxDriver.TabIndex = 5;
      pbxDriver.TabStop = false;
      // 
      // lblCarName
      // 
      lblCarName.AutoSize = true;
      lblCarName.BackColor = Color.Transparent;
      lblCarName.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
      lblCarName.ForeColor = Color.FromArgb(255, 80, 80);
      lblCarName.Location = new Point(370, 49);
      lblCarName.Name = "lblCarName";
      lblCarName.Size = new Size(83, 32);
      lblCarName.TabIndex = 6;
      lblCarName.Text = "label1";
      // 
      // lblDriverName
      // 
      lblDriverName.AutoSize = true;
      lblDriverName.BackColor = Color.Transparent;
      lblDriverName.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
      lblDriverName.ForeColor = Color.FromArgb(255, 80, 80);
      lblDriverName.Location = new Point(370, 81);
      lblDriverName.Name = "lblDriverName";
      lblDriverName.Size = new Size(51, 20);
      lblDriverName.TabIndex = 7;
      lblDriverName.Text = "label1";
      // 
      // lblCarStats
      // 
      lblCarStats.BackColor = Color.Transparent;
      lblCarStats.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
      lblCarStats.ForeColor = Color.Lime;
      lblCarStats.Location = new Point(620, 104);
      lblCarStats.Name = "lblCarStats";
      lblCarStats.Size = new Size(188, 116);
      lblCarStats.TabIndex = 8;
      lblCarStats.Text = "label1";
      // 
      // lblCarDescription
      // 
      lblCarDescription.BackColor = Color.Transparent;
      lblCarDescription.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
      lblCarDescription.ForeColor = Color.Lime;
      lblCarDescription.Location = new Point(659, 332);
      lblCarDescription.Name = "lblCarDescription";
      lblCarDescription.Size = new Size(262, 103);
      lblCarDescription.TabIndex = 9;
      lblCarDescription.Text = "label1";
      // 
      // OpponentEditor
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(970, 500);
      Controls.Add(lblCarDescription);
      Controls.Add(lblCarStats);
      Controls.Add(lblDriverName);
      Controls.Add(lblCarName);
      Controls.Add(pbxDriver);
      Controls.Add(pbxCar);
      Controls.Add(pbxCarInterface);
      Controls.Add(lvOpponents);
      Name = "OpponentEditor";
      Text = "Opponent Editor";
      Load += OpponentEditor_Load;
      ((System.ComponentModel.ISupportInitialize)pbxCar).EndInit();
      ((System.ComponentModel.ISupportInitialize)pbxCarInterface).EndInit();
      ((System.ComponentModel.ISupportInitialize)pbxDriver).EndInit();
      ResumeLayout(false);
      PerformLayout();
    }

    #endregion

    private ListView lvOpponents;
    private ColumnHeader columnHeader1;
    private ColumnHeader columnHeader2;
    private PictureBox pbxCar;
    private PictureBox pbxCarInterface;
    private PictureBox pbxDriver;
    private Label lblCarName;
    private Label lblDriverName;
    private Label lblCarStats;
    private Label lblCarDescription;
  }
}