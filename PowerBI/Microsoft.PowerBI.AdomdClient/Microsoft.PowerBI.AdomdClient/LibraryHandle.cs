using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200002C RID: 44
	internal class LibraryHandle : SafeHandle
	{
		// Token: 0x06000282 RID: 642 RVA: 0x0000C485 File Offset: 0x0000A685
		protected LibraryHandle()
			: base(IntPtr.Zero, true)
		{
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000283 RID: 643 RVA: 0x0000C493 File Offset: 0x0000A693
		public override bool IsInvalid
		{
			get
			{
				return this.handle == IntPtr.Zero || base.IsClosed;
			}
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000C4AF File Offset: 0x0000A6AF
		protected override bool ReleaseHandle()
		{
			return LibraryHandle.FreeLibrary(this.handle) != 0;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000C4BF File Offset: 0x0000A6BF
		private protected static void ThrowOnError()
		{
			throw new Win32Exception((int)LibraryHandle.GetLastError());
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000C4CC File Offset: 0x0000A6CC
		private protected static IntPtr CheckEmptyHandle(IntPtr handle)
		{
			if (handle == IntPtr.Zero)
			{
				LibraryHandle.ThrowOnError();
			}
			return handle;
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000C4E1 File Offset: 0x0000A6E1
		private protected Delegate GetDelegate(string functionName, Type delegateType)
		{
			IntPtr procAddress = LibraryHandle.GetProcAddress(this, functionName);
			if (procAddress == IntPtr.Zero)
			{
				throw new Win32Exception((int)LibraryHandle.GetLastError());
			}
			return Marshal.GetDelegateForFunctionPointer(procAddress, delegateType);
		}

		// Token: 0x06000288 RID: 648
		[SuppressUnmanagedCodeSecurity]
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int FreeLibrary([In] IntPtr hModule);

		// Token: 0x06000289 RID: 649
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr GetProcAddress([In] LibraryHandle hModule, [MarshalAs(UnmanagedType.LPStr)] [In] string lpProcName);

		// Token: 0x0600028A RID: 650
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern ulong GetLastError();
	}
}
