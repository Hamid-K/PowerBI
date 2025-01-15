using System;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices.AzureClient
{
	// Token: 0x0200000A RID: 10
	[Guid("D9AC6A60-DC6E-414B-A698-15FB6225EE88")]
	[ComVisible(true)]
	public struct AadAuthenticationOptions
	{
		// Token: 0x04000021 RID: 33
		[MarshalAs(UnmanagedType.Bool)]
		public bool UseTokenCache;

		// Token: 0x04000022 RID: 34
		public SingleSignOnMode SsoMode;

		// Token: 0x04000023 RID: 35
		[MarshalAs(UnmanagedType.Bool)]
		public bool HasServicePrincipalProfile;
	}
}
