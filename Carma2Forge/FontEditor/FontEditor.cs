using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Carma2ForgeLib.Modules;
using Carma2ForgeLib.Modules.FontModule;

namespace Carma2Forge {
  public partial class FontEditor : Form {
    private FontModule fontModule = new FontModule();

    private bool _hasChanges = false;
    private bool HasChanges {
      get {
        return _hasChanges;
      }
      set {
        _hasChanges = value;
        this.Text = "Font Editor" + (value ? " *" : "");
        btnSave.Enabled = value;
      }
    }

    public FontEditor(Carma2ForgeConfig config) {
      fontModule.Initialize(config);

      InitializeComponent();

      HasChanges = false;
    }

    Color LerpColor(Color a, Color b, float t) {
      int r = (int)(a.R + (b.R - a.R) * t);
      int g = (int)(a.G + (b.G - a.G) * t);
      int b2 = (int)(a.B + (b.B - a.B) * t);

      return Color.FromArgb(r, g, b2);
    }

    Bitmap CreateGradient(Color tl, Color tr, Color bl, Color br, int size = 32) {
      Bitmap bmp = new Bitmap(size, size);

      for (int y = 0; y < size; y++) {
        float v = (float)y / (size - 1);

        for (int x = 0; x < size; x++) {
          float u = (float)x / (size - 1);

          Color top = LerpColor(tl, tr, u);
          Color bottom = LerpColor(bl, br, u);

          Color final = LerpColor(top, bottom, v);

          bmp.SetPixel(x, y, final);
        }
      }

      return bmp;
    }

    private void lvFontColDefs_SelectedIndexChanged(object sender, EventArgs e) {
      if (lvFontColDefs.SelectedItems.Count == 0) {
        return;
      }
      ListViewItem selectedItem = lvFontColDefs.SelectedItems[0];
      if (selectedItem.Tag == null) {
        return;
      }

      FontColEntry selectedFcd = (FontColEntry)selectedItem.Tag;

      ceTopLeft.Color = selectedFcd.topLeft;
      ceTopRight.Color = selectedFcd.topRight;
      ceBottomLeft.Color = selectedFcd.bottomLeft;
      ceBottomRight.Color = selectedFcd.bottomRight;
    }

    private void UpdateFontColorDefImage(FontColEntry fcd) {
      Bitmap bmp = CreateGradient(
        fcd.topLeft,
        fcd.topRight,
        fcd.bottomLeft,
        fcd.bottomRight
      );

      ImageList? list = lvFontColDefs.SmallImageList;
      int index = list.Images.IndexOfKey(fcd.id.ToString());

      if (index >= 0) {
        list.Images[index] = bmp;
      }
    }

    private void RefreshFontColDefImages() {
      FontColEntry[] fcds = fontModule.GetFontColEntries();

      ImageList imageList = new ImageList();
      imageList.ImageSize = new Size(32, 32);

      foreach (FontColEntry fcd in fcds) {
        Bitmap bmp = CreateGradient(
          fcd.topLeft,
          fcd.topRight,
          fcd.bottomLeft,
          fcd.bottomRight
        );

        imageList.Images.Add(fcd.id.ToString(), bmp);
      }

      lvFontColDefs.SmallImageList = imageList;
      lvFontColDefs.LargeImageList = lvFontColDefs.SmallImageList;
    }

    private void FontForm_Load(object sender, EventArgs e) {
      FontColEntry[] fcds = fontModule.GetFontColEntries();

      RefreshFontColDefImages();

      foreach (FontColEntry fcd in fcds) {
        ListViewItem newListViewItem = new ListViewItem { Text = fcd.name, ImageKey = fcd.id.ToString() };
        newListViewItem.SubItems.Add(fcd.id.ToString());
        newListViewItem.Tag = fcd;
        lvFontColDefs.Items.Add(newListViewItem);
      }
    }

    private FontColEntry? GetSelectedFcd() {
      if (lvFontColDefs.SelectedItems.Count == 0) {
        return null;
      }
      ListViewItem selectedItem = lvFontColDefs.SelectedItems[0];
      if (selectedItem.Tag == null) {
        return null;
      }
      return (FontColEntry)selectedItem.Tag;
    }

    private void ceTopLeft_ColorChanged(object sender, Color e) {
      FontColEntry? selectedFcd = GetSelectedFcd();
      if (selectedFcd == null) {
        return;
      }
      selectedFcd.topLeft = e;

      UpdateFontColorDefImage(selectedFcd);

      lvFontColDefs.Invalidate();

      HasChanges = true;
    }

    private void ceTopRight_ColorChanged(object sender, Color e) {
      FontColEntry? selectedFcd = GetSelectedFcd();
      if (selectedFcd == null) {
        return;
      }
      selectedFcd.topRight = e;

      UpdateFontColorDefImage(selectedFcd);

      lvFontColDefs.Invalidate();

      HasChanges = true;
    }

    private void ceBottomLeft_ColorChanged(object sender, Color e) {
      FontColEntry? selectedFcd = GetSelectedFcd();
      if (selectedFcd == null) {
        return;
      }
      selectedFcd.bottomLeft = e;

      UpdateFontColorDefImage(selectedFcd);

      lvFontColDefs.Invalidate();

      HasChanges = true;
    }

    private void ceBottomRight_ColorChanged(object sender, Color e) {
      FontColEntry? selectedFcd = GetSelectedFcd();
      if (selectedFcd == null) {
        return;
      }
      selectedFcd.bottomRight = e;

      UpdateFontColorDefImage(selectedFcd);

      lvFontColDefs.Invalidate();

      HasChanges = true;
    }

    private void btnSave_Click(object sender, EventArgs e) {
      fontModule.SaveFontColEntries();

      HasChanges = false;
    }

    private void FontForm_FormClosing(object sender, FormClosingEventArgs e) {
      if (HasChanges) {
        DialogResult dlrSaveChanges = MessageBox.Show(
          "You have unsaved changes, save before closing?",
          "Unsaved changes",
          MessageBoxButtons.YesNoCancel,
          MessageBoxIcon.Warning
        );
        if (dlrSaveChanges == DialogResult.Yes) {
          fontModule.SaveFontColEntries();
        } else if (dlrSaveChanges == DialogResult.Cancel) {
          e.Cancel = true;
        }
      }
    }
  }
}
