using System;
using Eto.Forms;
using Eto.Gl;
using swf = System.Windows.Forms;
using Eto.Drawing;
using Eto.WinForms;
using OpenTK;

namespace NDDEditor.Desktop
{
	public class Program
	{
		[STAThread]
		public static void Main (string[] args)
		{
			var generator = Eto.Platform.Detect;
			if (generator.IsWinForms) {
				generator.Add<GLSurface.IHandler> (() => new Eto.Gl.Windows.WinGLSurfaceHandler ());
			} else if (generator.IsGtk){
				Toolkit.Init (new ToolkitOptions {
					Backend = PlatformBackend.PreferNative
				});
				generator.Add<GLSurface.IHandler> (() => new Eto.Gl.Gtk.GtkGlSurfaceHandler ());
			}
			new Application (Eto.Platform.Detect).Run (new MainForm ());
		}
	}
}

