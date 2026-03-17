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
      fbdCarmaPath = new FolderBrowserDialog();
      btnFontEditor = new Button();
      btnOpponentEditor = new Button();
      btnTwtEditor = new Button();
      btnRaceEditor = new Button();
      SuspendLayout();
      // 
      // fbdCarmaPath
      // 
      fbdCarmaPath.Description = "Select your Carmageddon 2 folder";
      fbdCarmaPath.RootFolder = Environment.SpecialFolder.MyComputer;
      fbdCarmaPath.ShowNewFolderButton = false;
      // 
      // btnFontEditor
      // 
      btnFontEditor.Location = new Point(342, 249);
      btnFontEditor.Name = "btnFontEditor";
      btnFontEditor.Size = new Size(116, 35);
      btnFontEditor.TabIndex = 0;
      btnFontEditor.Text = "Font Editor";
      btnFontEditor.UseVisualStyleBackColor = true;
      btnFontEditor.Click += btnFontEditing_Click;
      // 
      // btnOpponentEditor
      // 
      btnOpponentEditor.Location = new Point(342, 208);
      btnOpponentEditor.Name = "btnOpponentEditor";
      btnOpponentEditor.Size = new Size(116, 35);
      btnOpponentEditor.TabIndex = 1;
      btnOpponentEditor.Text = "Opponent Editor";
      btnOpponentEditor.UseVisualStyleBackColor = true;
      btnOpponentEditor.Click += btnOpponentEditor_Click;
      // 
      // btnTwtEditor
      // 
      btnTwtEditor.Location = new Point(342, 290);
      btnTwtEditor.Name = "btnTwtEditor";
      btnTwtEditor.Size = new Size(116, 35);
      btnTwtEditor.TabIndex = 2;
      btnTwtEditor.Text = "TWT Editor";
      btnTwtEditor.UseVisualStyleBackColor = true;
      btnTwtEditor.Click += btnTwtEditor_Click;
      // 
      // btnRaceEditor
      // 
      btnRaceEditor.Location = new Point(342, 167);
      btnRaceEditor.Name = "btnRaceEditor";
      btnRaceEditor.Size = new Size(116, 35);
      btnRaceEditor.TabIndex = 3;
      btnRaceEditor.Text = "Race Editor";
      btnRaceEditor.UseVisualStyleBackColor = true;
      btnRaceEditor.Click += btnRaceEditor_Click;
      // 
      // MainForm
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(800, 450);
      Controls.Add(btnRaceEditor);
      Controls.Add(btnTwtEditor);
      Controls.Add(btnOpponentEditor);
      Controls.Add(btnFontEditor);
      Name = "MainForm";
      Text = "Form1";
      Load += MainForm_Load;
      ResumeLayout(false);
    }

    #endregion

    private FolderBrowserDialog fbdCarmaPath;
    private Button btnFontEditor;
    private Button btnOpponentEditor;
    private Button btnTwtEditor;
    private Button btnRaceEditor;
  }
}
