using System;
using System.ComponentModel;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000013 RID: 19
	internal sealed class SafeLibraryHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600004A RID: 74 RVA: 0x000026F7 File Offset: 0x000008F7
		private SafeLibraryHandle()
			: base(true)
		{
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002700 File Offset: 0x00000900
		internal static SafeLibraryHandle LoadLibrary(string libName)
		{
			SafeLibraryHandle safeLibraryHandle = NativeMethodsWrapper.LoadLibrary(libName);
			if (safeLibraryHandle.IsInvalid)
			{
				safeLibraryHandle.SetHandleAsInvalid();
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
			return safeLibraryHandle;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000272E File Offset: 0x0000092E
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected override bool ReleaseHandle()
		{
			return NativeMethodsWrapper.FreeLibrary(this.handle);
		}
	}
}
