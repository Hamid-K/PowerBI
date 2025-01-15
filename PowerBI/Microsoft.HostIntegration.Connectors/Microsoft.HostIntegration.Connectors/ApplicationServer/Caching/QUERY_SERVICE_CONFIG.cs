using System;
using System.Runtime.InteropServices;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000345 RID: 837
	[StructLayout(LayoutKind.Sequential)]
	internal class QUERY_SERVICE_CONFIG
	{
		// Token: 0x040010B9 RID: 4281
		[MarshalAs(UnmanagedType.U4)]
		internal uint dwServiceType;

		// Token: 0x040010BA RID: 4282
		[MarshalAs(UnmanagedType.U4)]
		internal uint dwStartType;

		// Token: 0x040010BB RID: 4283
		[MarshalAs(UnmanagedType.U4)]
		internal uint dwErrorControl;

		// Token: 0x040010BC RID: 4284
		[MarshalAs(UnmanagedType.LPWStr)]
		internal string lpBinaryPathName;

		// Token: 0x040010BD RID: 4285
		[MarshalAs(UnmanagedType.LPWStr)]
		internal string lpLoadOrderGroup;

		// Token: 0x040010BE RID: 4286
		[MarshalAs(UnmanagedType.U4)]
		internal uint dwTagID;

		// Token: 0x040010BF RID: 4287
		[MarshalAs(UnmanagedType.LPWStr)]
		internal string lpDependencies;

		// Token: 0x040010C0 RID: 4288
		[MarshalAs(UnmanagedType.LPWStr)]
		internal string lpServiceStartName;

		// Token: 0x040010C1 RID: 4289
		[MarshalAs(UnmanagedType.LPWStr)]
		internal string lpDisplayName;
	}
}
