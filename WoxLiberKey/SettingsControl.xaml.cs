using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

namespace WoxLiberKey
{
	/// <summary>
	/// Interaction logic for SettingsControl.xaml
	/// </summary>
	public partial class SettingsControl : System.Windows.Controls.UserControl
	{
		public Options Options;

		public SettingsControl(Options current)
		{
			InitializeComponent();

			Options = current;
			LiberKeyDbPathText.Text = Options.LiberKeyRootPath ?? "";
		}

		private void PickPath_Click(object sender, RoutedEventArgs e)
		{
			// Shows path selector dialog
			var fd = new FolderBrowserDialog
			{
				Description = "Select path to Steam installation folder",
				SelectedPath = Options.LiberKeyRootPath
			};

			// Stop if dialog was cancelled
			if (fd.ShowDialog() != DialogResult.OK) return;

			// Save selected path
			LiberKeyDbPathText.Text = Options.LiberKeyRootPath = fd.SelectedPath;
		}

		private void LiberKeyLocalappsDbXmlFileText_TextChanged(object sender, TextChangedEventArgs e)
		{
			// Only update options on valid paths
			if (!Directory.Exists(LiberKeyDbPathText.Text)) return;

			Options.LiberKeyRootPath = LiberKeyDbPathText.Text;
			Options.Save();
		}
	}
}
