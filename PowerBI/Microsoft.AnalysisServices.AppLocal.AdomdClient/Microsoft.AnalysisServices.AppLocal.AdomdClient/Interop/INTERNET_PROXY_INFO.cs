using System;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices.AdomdClient.Interop
{
	// Token: 0x02000135 RID: 309
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct INTERNET_PROXY_INFO
	{
		// Token: 0x04000AC6 RID: 2758
		public uint dwAccessType;

		// Token: 0x04000AC7 RID: 2759
		[MarshalAs(UnmanagedType.LPWStr)]
		public string lpszProxy;

		// Token: 0x04000AC8 RID: 2760
		[MarshalAs(UnmanagedType.LPWStr)]
		public string lpszProxyBypass;
	}
}
