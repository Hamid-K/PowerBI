using System;
using System.Runtime.InteropServices;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000346 RID: 838
	[StructLayout(LayoutKind.Sequential)]
	internal class SERVICE_SID_INFO
	{
		// Token: 0x040010C2 RID: 4290
		[MarshalAs(UnmanagedType.U4)]
		internal uint dwServiceSidType;
	}
}
