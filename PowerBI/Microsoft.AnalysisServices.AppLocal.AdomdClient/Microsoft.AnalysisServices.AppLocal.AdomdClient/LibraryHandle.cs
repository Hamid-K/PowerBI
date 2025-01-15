using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200002C RID: 44
	internal class LibraryHandle : SafeHandle
	{
		// Token: 0x0600028F RID: 655 RVA: 0x0000C7B5 File Offset: 0x0000A9B5
		protected LibraryHandle()
			: base(IntPtr.Zero, true)
		{
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000290 RID: 656 RVA: 0x0000C7C3 File Offset: 0x0000A9C3
		public override bool IsInvalid
		{
			get
			{
				return this.handle == IntPtr.Zero || base.IsClosed;
			}
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000C7DF File Offset: 0x0000A9DF
		protected override bool ReleaseHandle()
		{
			return LibraryHandle.FreeLibrary(this.handle) != 0;
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000C7EF File Offset: 0x0000A9EF
		private protected static void ThrowOnError()
		{
			throw new Win32Exception((int)LibraryHandle.GetLastError());
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000C7FC File Offset: 0x0000A9FC
		private protected static IntPtr CheckEmptyHandle(IntPtr handle)
		{
			if (handle == IntPtr.Zero)
			{
				LibraryHandle.ThrowOnError();
			}
			return handle;
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000C811 File Offset: 0x0000AA11
		private protected Delegate GetDelegate(string functionName, Type delegateType)
		{
			IntPtr procAddress = LibraryHandle.GetProcAddress(this, functionName);
			if (procAddress == IntPtr.Zero)
			{
				throw new Win32Exception((int)LibraryHandle.GetLastError());
			}
			return Marshal.GetDelegateForFunctionPointer(procAddress, delegateType);
		}

		// Token: 0x06000295 RID: 661
		[SuppressUnmanagedCodeSecurity]
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int FreeLibrary([In] IntPtr hModule);

		// Token: 0x06000296 RID: 662
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr GetProcAddress([In] LibraryHandle hModule, [MarshalAs(UnmanagedType.LPStr)] [In] string lpProcName);

		// Token: 0x06000297 RID: 663
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern ulong GetLastError();
	}
}
