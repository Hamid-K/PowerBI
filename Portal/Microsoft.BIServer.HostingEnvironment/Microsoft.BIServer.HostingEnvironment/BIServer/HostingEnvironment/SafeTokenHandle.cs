using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.BIServer.HostingEnvironment
{
	// Token: 0x02000019 RID: 25
	public sealed class SafeTokenHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060000AF RID: 175 RVA: 0x00003CD5 File Offset: 0x00001ED5
		private SafeTokenHandle()
			: base(true)
		{
		}

		// Token: 0x060000B0 RID: 176
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("kernel32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool CloseHandle(IntPtr handle);

		// Token: 0x060000B1 RID: 177 RVA: 0x00003CDE File Offset: 0x00001EDE
		protected override bool ReleaseHandle()
		{
			return SafeTokenHandle.CloseHandle(this.handle);
		}
	}
}
