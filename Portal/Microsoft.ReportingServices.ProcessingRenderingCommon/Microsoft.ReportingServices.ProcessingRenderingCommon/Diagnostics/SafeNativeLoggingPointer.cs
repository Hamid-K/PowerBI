using System;
using System.Runtime.ConstrainedExecution;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000024 RID: 36
	internal class SafeNativeLoggingPointer : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06000117 RID: 279 RVA: 0x000056C8 File Offset: 0x000038C8
		private SafeNativeLoggingPointer()
			: base(true)
		{
		}

		// Token: 0x06000118 RID: 280 RVA: 0x000056D1 File Offset: 0x000038D1
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		protected override bool ReleaseHandle()
		{
			NativeLoggingMethods.ReleaseNativeLoggingObject(this.handle);
			return true;
		}
	}
}
