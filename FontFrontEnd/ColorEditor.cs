using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Carma2Forge.FontFrontEnd {
  public partial class ColorEditor : UserControl {
    public event EventHandler<Color> ColorChanged;

    public Color Color {
      get => GetColor();
      set {
        SetColor(value);
      }
    }

    public ColorEditor() {
      InitializeComponent();
    }

    private void trkRed_Scroll(object sender, EventArgs e) {
      SetColor(GetColor());
      ColorChanged?.Invoke(this, Color);
    }

    private void trkGreen_Scroll(object sender, EventArgs e) {
      SetColor(GetColor());
      ColorChanged?.Invoke(this, Color);
    }

    private void trkBlue_Scroll(object sender, EventArgs e) {
      SetColor(GetColor());
      ColorChanged?.Invoke(this, Color);
    }

    private void SetColor(Color col) {
      trkRed.Value = col.R;
      trkGreen.Value = col.G;
      trkBlue.Value = col.B;

      pnlColor.BackColor = col;

      lblRed.Text = $"Red: {col.R}";
      lblGreen.Text = $"Green: {col.G}";
      lblBlue.Text = $"Blue: {col.B}";
    }

    private Color GetColor() {
      return Color.FromArgb(trkRed.Value, trkGreen.Value, trkBlue.Value);
    }
  }
}
