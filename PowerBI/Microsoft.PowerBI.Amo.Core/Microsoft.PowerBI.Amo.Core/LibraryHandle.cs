using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000045 RID: 69
	internal class LibraryHandle : SafeHandle
	{
		// Token: 0x06000325 RID: 805 RVA: 0x0000F6D9 File Offset: 0x0000D8D9
		protected LibraryHandle()
			: base(IntPtr.Zero, true)
		{
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000326 RID: 806 RVA: 0x0000F6E7 File Offset: 0x0000D8E7
		public override bool IsInvalid
		{
			get
			{
				return this.handle == IntPtr.Zero || base.IsClosed;
			}
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000F703 File Offset: 0x0000D903
		protected override bool ReleaseHandle()
		{
			return LibraryHandle.FreeLibrary(this.handle) != 0;
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000F713 File Offset: 0x0000D913
		private protected static void ThrowOnError()
		{
			throw new Win32Exception((int)LibraryHandle.GetLastError());
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000F720 File Offset: 0x0000D920
		private protected static IntPtr CheckEmptyHandle(IntPtr handle)
		{
			if (handle == IntPtr.Zero)
			{
				LibraryHandle.ThrowOnError();
			}
			return handle;
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000F735 File Offset: 0x0000D935
		private protected Delegate GetDelegate(string functionName, Type delegateType)
		{
			IntPtr procAddress = LibraryHandle.GetProcAddress(this, functionName);
			if (procAddress == IntPtr.Zero)
			{
				throw new Win32Exception((int)LibraryHandle.GetLastError());
			}
			return Marshal.GetDelegateForFunctionPointer(procAddress, delegateType);
		}

		// Token: 0x0600032B RID: 811
		[SuppressUnmanagedCodeSecurity]
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int FreeLibrary([In] IntPtr hModule);

		// Token: 0x0600032C RID: 812
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr GetProcAddress([In] LibraryHandle hModule, [MarshalAs(UnmanagedType.LPStr)] [In] string lpProcName);

		// Token: 0x0600032D RID: 813
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern ulong GetLastError();
	}
}
