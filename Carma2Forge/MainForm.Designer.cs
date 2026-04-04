namespace Carma2Forge
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
      fbdCarmaPath = new FolderBrowserDialog();
      btnTwtEditor = new Button();
      btnRaceEditor = new Button();
      btnOpponentEditor = new Button();
      btnFontEditor = new Button();
      SuspendLayout();
      // 
      // fbdCarmaPath
      // 
      fbdCarmaPath.Description = "Select your Carmageddon 2 folder";
      fbdCarmaPath.RootFolder = Environment.SpecialFolder.MyComputer;
      fbdCarmaPath.ShowNewFolderButton = false;
      // 
      // btnTwtEditor
      // 
      btnTwtEditor.Location = new Point(672, 403);
      btnTwtEditor.Name = "btnTwtEditor";
      btnTwtEditor.Size = new Size(116, 35);
      btnTwtEditor.TabIndex = 2;
      btnTwtEditor.Text = "TWT Editor";
      btnTwtEditor.UseVisualStyleBackColor = true;
      btnTwtEditor.Click += btnTwtEditor_Click;
      // 
      // btnRaceEditor
      // 
      btnRaceEditor.BackColor = Color.FromArgb(216, 0, 0, 0);
      btnRaceEditor.FlatStyle = FlatStyle.Flat;
      btnRaceEditor.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
      btnRaceEditor.ForeColor = Color.Lime;
      btnRaceEditor.Image = Properties.Resources.list_2_32;
      btnRaceEditor.ImageAlign = ContentAlignment.TopCenter;
      btnRaceEditor.Location = new Point(12, 12);
      btnRaceEditor.Name = "btnRaceEditor";
      btnRaceEditor.Padding = new Padding(8, 16, 8, 8);
      btnRaceEditor.Size = new Size(126, 119);
      btnRaceEditor.TabIndex = 3;
      btnRaceEditor.Text = "Race Editor";
      btnRaceEditor.TextAlign = ContentAlignment.BottomCenter;
      btnRaceEditor.UseVisualStyleBackColor = false;
      btnRaceEditor.Click += btnRaceEditor_Click;
      // 
      // btnOpponentEditor
      // 
      btnOpponentEditor.BackColor = Color.FromArgb(216, 0, 0, 0);
      btnOpponentEditor.FlatStyle = FlatStyle.Flat;
      btnOpponentEditor.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
      btnOpponentEditor.ForeColor = Color.Lime;
      btnOpponentEditor.Image = Properties.Resources.car_23_32;
      btnOpponentEditor.ImageAlign = ContentAlignment.TopCenter;
      btnOpponentEditor.Location = new Point(144, 12);
      btnOpponentEditor.Name = "btnOpponentEditor";
      btnOpponentEditor.Padding = new Padding(8, 16, 8, 8);
      btnOpponentEditor.Size = new Size(126, 119);
      btnOpponentEditor.TabIndex = 4;
      btnOpponentEditor.Text = "Opponent Editor";
      btnOpponentEditor.TextAlign = ContentAlignment.BottomCenter;
      btnOpponentEditor.UseVisualStyleBackColor = false;
      btnOpponentEditor.Click += btnOpponentEditor_Click;
      // 
      // btnFontEditor
      // 
      btnFontEditor.BackColor = Color.FromArgb(216, 0, 0, 0);
      btnFontEditor.FlatStyle = FlatStyle.Flat;
      btnFontEditor.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
      btnFontEditor.ForeColor = Color.Lime;
      btnFontEditor.Image = Properties.Resources.generic_text_32;
      btnFontEditor.ImageAlign = ContentAlignment.TopCenter;
      btnFontEditor.Location = new Point(276, 12);
      btnFontEditor.Name = "btnFontEditor";
      btnFontEditor.Padding = new Padding(8, 16, 8, 8);
      btnFontEditor.Size = new Size(126, 119);
      btnFontEditor.TabIndex = 5;
      btnFontEditor.Text = "Font Editor";
      btnFontEditor.TextAlign = ContentAlignment.BottomCenter;
      btnFontEditor.UseVisualStyleBackColor = false;
      btnFontEditor.Click += btnFontEditor_Click;
      // 
      // MainForm
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      BackgroundImage = Properties.Resources.main;
      BackgroundImageLayout = ImageLayout.Stretch;
      ClientSize = new Size(800, 450);
      Controls.Add(btnFontEditor);
      Controls.Add(btnOpponentEditor);
      Controls.Add(btnRaceEditor);
      Controls.Add(btnTwtEditor);
      DoubleBuffered = true;
      FormBorderStyle = FormBorderStyle.FixedSingle;
      Icon = (Icon)resources.GetObject("$this.Icon");
      Name = "MainForm";
      Text = "Carma 2 Forge";
      Load += MainForm_Load;
      ResumeLayout(false);
    }

    #endregion

    private FolderBrowserDialog fbdCarmaPath;
    private Button btnTwtEditor;
    private Button btnRaceEditor;
    private Button btnOpponentEditor;
    private Button btnFontEditor;
  }
}
