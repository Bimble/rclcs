﻿using System;
using System.Runtime.InteropServices;
namespace rclcs
{
	/// <summary>
	/// The RCL class handles the initialisation of the ros client librarie and wraps the functions defined in the rcl/rcl.h.
	/// It furthermore defines the paths to the rcl and rmw libs that are used in the DllImport statement for native interop.
	/// This class implements IDisposable.
	/// </summary>
	internal class RCLLinux:RCLBase, IDisposable
	{
		bool disposed = false;

		/// <summary>
		/// This method does the initilisation of the ros client lib.
		/// <remarks>Call this method before you do any other calls to ros</remarks>
		/// </summary>
		/// <param name="args">Commandline arguments</param>
		/// <exception cref="RCLAlreadInitExcption">In case rcl was alread initialised</exception>
		public override void Init(String[] args)
		{
			if (args == null)
				throw new ArgumentNullException ();
			RCLReturnValues retVal = (RCLReturnValues)rcl_init (args.Length, args, Allocator.rcl_get_default_allocator ());
			switch (retVal) {
			case RCLReturnValues.RCL_RET_OK:

				break;
			case RCLReturnValues.RCL_RET_ALREADY_INIT:
				throw new RCLAlreadyInitExcption ();
			case RCLReturnValues.RCL_RET_BAD_ALLOC:
				throw new RCLBadAllocException ();
			case RCLReturnValues.RCL_RET_ERROR:
				throw new RCLErrorException (RCLErrorHandling.GetRMWErrorState());
			default:
				break;
			}

		}
		/// <summary>
		/// This method does the initilisation of the ros client lib
		/// <remarks>Call this method before you do any other calls to ros
		/// You can specify a custom memory allocator for ros but I wouldn't recommend doing this at the moment. </remarks>
		/// </summary>
		/// <param name="args">Arguments.</param>
		/// <param name="custom_allocator">Custom allocator.</param>
		public  override void Init(String[] args, rcl_allocator_t custom_allocator)
		{
			if (args == null)
				throw new ArgumentNullException ();
			RCLReturnValues retVal = (RCLReturnValues)rcl_init (args.Length, args, custom_allocator);
			switch (retVal) {
			case RCLReturnValues.RCL_RET_OK:

				break;
			case RCLReturnValues.RCL_RET_ALREADY_INIT:
				throw new RCLAlreadyInitExcption ();
			case RCLReturnValues.RCL_RET_BAD_ALLOC:
				throw new RCLBadAllocException ();
			case RCLReturnValues.RCL_RET_ERROR:
				throw new RCLErrorException (RCLErrorHandling.GetRMWErrorState());
			default:
				break;
			}

		}
		/// <summary>
		/// Gets a value indicating whether there was a Init call in the past.
		/// </summary>
		/// <value><c>true</c> if this instance is init; otherwise, <c>false</c>.</value>
		public override  bool IsInit
		{
			get{ return rcl_ok(); }

		}


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
		/// <summary>
		/// Implementation of IDisposable
		/// </summary>
		/// <param name="disposing">If set to <c>true</c> disposing.</param>
		protected virtual void Dispose(bool disposing)
		{
			if (disposed)
				return;
			if (disposing) {

				// Free any other managed objects here.
			}
			// Free any unmanaged objects here.
			RCLReturnValues retVal = (RCLReturnValues)rcl_shutdown ();
			switch (retVal) {
			case RCLReturnValues.RCL_RET_OK:
				break;
			case RCLReturnValues.RCL_RET_NOT_INIT:
				//throw new RCLNotInitException ();
				break;
			case RCLReturnValues.RCL_RET_ERROR:
				//throw new RCLErrorException (RCLErrorHandling.GetRMWErrorState());
				break;
			default:
				break;
			}
			disposed = true;
		}
		/// <summary>
		/// Releases unmanaged resources and performs other cleanup operations before the <see cref="rclcs.RCL"/> is reclaimed
		/// by garbage collection.
		/// </summary>
		~RCLLinux()
		{
			Dispose (false);
		}
		//Native methods

		[DllImport("librcl.so")]
		static extern int rcl_init(int argc, [In, Out] String[] argv, rcl_allocator_t allocator);

		[DllImport("librcl.so")]
		static extern int rcl_shutdown ();

		[DllImport("librcl.so")]
		static extern UInt64 rcl_get_instance_id ();

		[DllImport("librcl.so")]
		static extern bool rcl_ok ();


	}

}
