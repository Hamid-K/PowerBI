using System;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices.Interop
{
	// Token: 0x0200012A RID: 298
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct INTERNET_PROXY_INFO
	{
		// Token: 0x04000A7F RID: 2687
		public uint dwAccessType;

		// Token: 0x04000A80 RID: 2688
		[MarshalAs(UnmanagedType.LPWStr)]
		public string lpszProxy;

		// Token: 0x04000A81 RID: 2689
		[MarshalAs(UnmanagedType.LPWStr)]
		public string lpszProxyBypass;
	}
}
