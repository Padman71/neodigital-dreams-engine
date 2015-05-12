using System;
using Eto.Forms;
using OpenTK;
using System.ComponentModel;

namespace NDDEditor.Desktop
{
	public class NDDEditorApp : Application
	{
		protected override void OnInitialized(EventArgs e)
		{
			try
			{
				Toolkit.Init();
			}
			catch( Exception ex )
			{
				Console.WriteLine(string.Format("Got an exception while initializing OpenTK : {0}", ex.Message));
				Instance.Quit();
			}

			Style = "application";

			this.MainForm = new MainForm();

			base.OnInitialized(e);

			// show the main form
			MainForm.Show();
		}

		protected override void OnTerminating(CancelEventArgs e)
		{
			base.OnTerminating(e);

			var result = MessageBox.Show(MainForm, "Are you sure you want to quit?", MessageBoxButtons.YesNo, MessageBoxType.Question);
			if( result == DialogResult.No )
				e.Cancel = true;
		}
	}
}

