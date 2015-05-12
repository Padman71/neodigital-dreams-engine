using System;
using Eto.Forms;
using Eto.Drawing;
using Eto.Gl;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System.Reflection;
using NDDEditor.Desktop;
using Microsoft.Xna.Framework.Graphics;

namespace NDDEditor
{
	/// <summary>
	/// Your application's main form
	/// </summary>
	public class MainForm : Form
	{
		GLSurface glSurface;

		public MainForm ()
		{
			Icon = new Icon ("nddengine-icon.ico");
			Title = string.Format("NeoDigital Dreams Engine - Editor {0}", Assembly.GetExecutingAssembly().GetName().Version);
			ClientSize = new Size (1280, 800);
			Maximize ();

			ButtonMenuItem newNDDObjectButton = new ButtonMenuItem {
				Text = "New NDDObject..."
			};
			newNDDObjectButton.Click += (object sender, EventArgs e) => {
				//Create scene object with his associated class
			};

			//the main content
			Content = new Splitter {
				Panel1 = new Panel { 
					Width = 280, 
					Content = new Scrollable { 
						ExpandContentWidth = false,
						Content = new TreeView { 
							Width = 278
						} 
					}, 
					ContextMenu = new ContextMenu () 
				},
				Panel2 = new Splitter {
					Panel1 = new Splitter {
						Orientation = SplitterOrientation.Vertical,
						Panel1 = new NDDViewport(),//new Label { Height = 500, Width = 720, Text = "OpenGL View", TextAlignment = TextAlignment.Center, VerticalAlignment = VerticalAlignment.Center },
						Panel2 = new GridView()
					},
					Panel2 = new Panel()
				}
			};

			// create a few commands that can be used for the menu and toolbar
			var openNDDScene = new Command {
				MenuText = "Open scene...",
				Shortcut = Application.Instance.CommonModifier | Keys.O
			};
			openNDDScene.Executed += (sender, e) => {
				var openSceneDialog = new OpenFileDialog();
				openSceneDialog.Filters.Add(new FileDialogFilter("NDDScene File","*.nddscene"));
				openSceneDialog.MultiSelect = false;
				switch(openSceneDialog.ShowDialog(this))
				{
					case DialogResult.Ok:
						break;
					default:
						break;
				}
			};

			var saveAsNDDScene = new Command {
				MenuText = "Save scene as...",
				Shortcut = Application.Instance.CommonModifier | Keys.Shift | Keys.S
			};
			saveAsNDDScene.Executed += (sender, e) => {
				var saveSceneDialog = new SaveFileDialog();
				saveSceneDialog.Filters.Add(new FileDialogFilter("NDDScene File","*.nddscene"));
				switch(saveSceneDialog.ShowDialog(this))
				{
				case DialogResult.Ok:
					break;
				default:
					break;
				}
			};

			var quitCommand = new Command {
				MenuText = "Quit",
				Shortcut = Application.Instance.CommonModifier | Keys.Q
			};
			quitCommand.Executed += (sender, e) => Application.Instance.Quit ();

			var aboutCommand = new Command { MenuText = "About..." };
			aboutCommand.Executed += (sender, e) => {
				var about = new About ();
				about.ShowModal ();
			};

			// create menu
			Menu = new MenuBar {
				Items = {
					// File submenu
					new ButtonMenuItem { Text = "&File", Items = { openNDDScene, saveAsNDDScene } }
				},
				QuitItem = quitCommand,
				AboutItem = aboutCommand
			};



			// create toolbar			
			//ToolBar = new ToolBar { Items = { openNDDScene } };
		}
	}
}
