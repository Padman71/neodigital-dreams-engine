using System;
using Microsoft.Xna.Framework.Graphics;
using OpenTK;
using Microsoft.Xna.Framework;
using OpenTK.Graphics.OpenGL;
using Eto.Gl;
using Eto.Forms;
using Eto.Drawing;
using System.Drawing;

namespace NDDEditor.Desktop
{
    public abstract class GraphicsDeviceControl : GLSurface
    {
        #region Fields

        private bool _designMode;

        Form _mainForm;

        GraphicsDeviceService _deviceService;

        ServiceContainer _services = new ServiceContainer();

        #endregion

        #region Properties

        public Form MainForm
        {
            get { return _mainForm; }
            internal set { _mainForm = value; }
        }

        public GraphicsDevice GraphicsDevice
        {
            get { return _deviceService.GraphicsDevice; }
        }

        public GraphicsDeviceService GraphicsDeviceService
        {
            get { return _deviceService; }
        }

        public ServiceContainer Services
        {
            get { return _services; }
        }

        #endregion

        #region Events

        public event EventHandler<EventArgs> ControlInitialized;
        public event EventHandler<EventArgs> ControlInitializing;

        #endregion

        #region Initialization

        protected GraphicsDeviceControl ()
        {
            _designMode = DesignMode;
        }

		protected override void OnLoadComplete (EventArgs e)
    	{
			if (!DesignMode) {
				_deviceService = GraphicsDeviceService.AddRef(this.NativeHandle, GLSize.Width, GLSize.Height);

				_services.AddService<IGraphicsDeviceService>(_deviceService);

				if (ControlInitializing != null) {
					ControlInitializing(this, EventArgs.Empty);
				}

				Initialize();

				if (ControlInitialized != null) {
					ControlInitialized(this, EventArgs.Empty);
				}
			}
			this.DrawNow += GraphicsDeviceControl_DrawNow;
    		base.OnLoadComplete (e);
    	}

		void GraphicsDeviceControl_DrawNow (object sender, EventArgs e)
		{
			BeginDraw();
			Draw();
			EndDraw();
		}


        protected override void Dispose (bool disposing)
        {
            if (_deviceService != null) {
                try {
                    _deviceService.Release();
                }
                catch { }

                _deviceService = null;
            }

            base.Dispose(disposing);
        }

        protected new bool DesignMode
        {
            get { return _designMode; }
        }

        #endregion

        #region Paint

        private string BeginDraw ()
        {
            if (_deviceService == null) {
				return GetType().ToString();
            }

            string deviceResetError = HandleDeviceReset();

            if (!string.IsNullOrEmpty(deviceResetError)) {
                return deviceResetError;
            }
				
			this.MakeCurrent ();
			_deviceService.GraphicsDevice.PresentationParameters.BackBufferHeight = GLSize.Height;
			_deviceService.GraphicsDevice.PresentationParameters.BackBufferWidth = GLSize.Width;

            Viewport viewport = new Viewport();

            viewport.X = 0;
            viewport.Y = 0;

			viewport.Width = GLSize.Width;
			viewport.Height = GLSize.Height;

            viewport.MinDepth = 0;
            viewport.MaxDepth = 1;

            if (GraphicsDevice.Viewport.Equals(viewport) == false)
                GraphicsDevice.Viewport = viewport;

            return null;
        }

        private static Random rand = new Random();

        private void EndDraw ()
        {
            try {
                SwapBuffers();
            }
            catch {
            }
        }

        private string HandleDeviceReset ()
        {
            bool needsReset = false;

            switch (GraphicsDevice.GraphicsDeviceStatus) {
                case GraphicsDeviceStatus.Lost:
                    return "Graphics device lost";

                case GraphicsDeviceStatus.NotReset:
                    needsReset = true;
                    break;

                default:
                    PresentationParameters pp = GraphicsDevice.PresentationParameters;
					needsReset = (GLSize.Width > pp.BackBufferWidth) || (GLSize.Height > pp.BackBufferHeight);
                    break;
            }

            if (needsReset) {
                try {
					_deviceService.ResetDevice(GLSize.Width, GLSize.Height);
                }
                catch (Exception e) {
                    return "Graphics device reset failed\n\n" + e;
                }
            }

            return null;
        }

        #endregion

        #region Abstract Methods

        protected abstract void Initialize ();
        protected abstract void Draw ();

        #endregion
    }
}