using System;
using System.Runtime.InteropServices;
namespace rclcs
{
    internal abstract class RCLBase : IDisposable
    {
        protected bool disposed = false;
        public abstract void Init(String[] args);

        public abstract rcl_context_t ZeroInitializedContext { get; }

        /// <summary>
        /// Releases all resource used by the <see cref="rclcs.RCL"/> object.
        /// </summary>
        /// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="rclcs.RCL"/>. The <see cref="Dispose"/>
        /// method leaves the <see cref="rclcs.RCL"/> in an unusable state. After calling <see cref="Dispose"/>, you must
        /// release all references to the <see cref="rclcs.RCL"/> so the garbage collector can reclaim the memory that the
        /// <see cref="rclcs.RCL"/> was occupying.</remarks>
        public void Dispose()
		{
			// Dispose of unmanaged resources.
			Dispose(true);
			// Suppress finalization.
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
		}
        ~RCLBase()
        {
            Dispose(false);
        }
	}
	public class RCL:IDisposable
	{
		bool disposed = false;

		RCLBase Impl;
		public RCL ()
		{
			if (Environment.OSVersion.Platform == PlatformID.Win32NT) {
                //FIXME: add windows impl
                //Impl = new RCLWindows();
                Impl = new RCLLinux();
            }
            else if (Environment.OSVersion.Platform == PlatformID.Unix) {
				Impl = new RCLLinux ();
			} else {
				throw new Exception("Operating system: " +Environment.OSVersion.Platform.ToString() + " not supported");
			}
		}
        ~RCL()
        {
            Dispose(false);
        }

		public void Init (String[] args)
		{
			Impl.Init(args);
		}


        public void Dispose()
		{
			// Dispose of unmanaged resources.
			Dispose(true);
			// Suppress finalization.
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposed)
				return;
			if (disposing) {

				// Free any other managed objects here.
				Impl.Dispose();
			}


			disposed = true;
		}

	}

			
}

