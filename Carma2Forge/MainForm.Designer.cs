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
      btnFontEditing = new Button();
      SuspendLayout();
      // 
      // fbdCarmaPath
      // 
      fbdCarmaPath.Description = "Select your Carmageddon 2 folder";
      fbdCarmaPath.RootFolder = Environment.SpecialFolder.MyComputer;
      fbdCarmaPath.ShowNewFolderButton = false;
      // 
      // btnFontEditing
      // 
      btnFontEditing.Location = new Point(331, 99);
      btnFontEditing.Name = "btnFontEditing";
      btnFontEditing.Size = new Size(116, 35);
      btnFontEditing.TabIndex = 0;
      btnFontEditing.Text = "Font Editor";
      btnFontEditing.UseVisualStyleBackColor = true;
      btnFontEditing.Click += btnFontEditing_Click;
      // 
      // MainForm
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(800, 450);
      Controls.Add(btnFontEditing);
      Name = "MainForm";
      Text = "Form1";
      Load += MainForm_Load;
      ResumeLayout(false);
    }

    #endregion

    private FolderBrowserDialog fbdCarmaPath;
    private Button btnFontEditing;
  }
}
