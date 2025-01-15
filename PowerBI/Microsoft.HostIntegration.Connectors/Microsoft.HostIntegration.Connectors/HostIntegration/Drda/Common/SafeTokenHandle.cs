using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x0200085F RID: 2143
	public class SafeTokenHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600443B RID: 17467 RVA: 0x000E5524 File Offset: 0x000E3724
		private SafeTokenHandle()
			: base(true)
		{
		}

		// Token: 0x0600443C RID: 17468
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool CloseHandle(IntPtr handle);

		// Token: 0x0600443D RID: 17469 RVA: 0x000E552D File Offset: 0x000E372D
		protected override bool ReleaseHandle()
		{
			return SafeTokenHandle.CloseHandle(this.handle);
		}
	}
}
