using System;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices.AdomdClient.Interop
{
	// Token: 0x02000135 RID: 309
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct INTERNET_PROXY_INFO
	{
		// Token: 0x04000AB9 RID: 2745
		public uint dwAccessType;

		// Token: 0x04000ABA RID: 2746
		[MarshalAs(UnmanagedType.LPWStr)]
		public string lpszProxy;

		// Token: 0x04000ABB RID: 2747
		[MarshalAs(UnmanagedType.LPWStr)]
		public string lpszProxyBypass;
	}
}
