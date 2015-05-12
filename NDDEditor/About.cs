using System;
using Eto.Forms;
using NDDEditor.Desktop;
using Eto.Drawing;
using System.Reflection;

namespace NDDEditor
{
	public class About : Dialog
	{
		Label lblVersion;
		Label lblName;
		ImageView imgLogo;
		Label lblCopy0;
		Label lblCopy1;

		public About()
		{
			lblName = new Label ();
			lblVersion = new Label ();
			imgLogo = new ImageView ();
			lblCopy0 = new Label ();
			lblCopy1 = new Label ();
			BindSetup();
		}

		private void BindSetup()
		{
			this.Maximizable = false;
			this.Minimizable = false;
			this.Resizable = false;
			this.Topmost = true;
			this.MinimumSize = new Size (512, 256);
			Title = "About...";
			/*ClientSize = new Size (450, 350);
			Height = 350;
			Width = 450;*/

			this.lblName.Text = "NeoDigital Dreams Engine - Editor";
			lblName.Font = new Font (SystemFont.Bold);

			this.lblVersion.Text = string.Format("Version {0}", Assembly.GetExecutingAssembly().GetName().Version);

			this.imgLogo.Image = new Bitmap ("NDDEngine-logo.png");
			imgLogo.Height = 256;
			imgLogo.Width = 256;

			lblCopy0.Text = "Developed by Nourredine OCTEAU\n\n\n\n\n\n\n\n\n";
			lblCopy0.Font = new Font (SystemFont.Default, 8);
			lblCopy1.Text = "Copyright 2015 NeoDigital Dreams\nhttps://www.neodigital-dreams.net";

			Content = new TableLayout(
				new TableRow(imgLogo,
					new TableLayout(
						new TableRow(lblName),
						new TableRow(lblVersion),
						new TableRow(lblCopy0),
						new TableRow(lblCopy1)
					)
				)
			);
		}       
	}
}

