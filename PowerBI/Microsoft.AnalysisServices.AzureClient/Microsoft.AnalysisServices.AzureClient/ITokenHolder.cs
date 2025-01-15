using System;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices.AzureClient
{
	// Token: 0x0200000E RID: 14
	[Guid("EB1A4741-E3F1-468D-B963-C8A969F551AA")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true)]
	public interface ITokenHolder
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001D RID: 29
		string TenantId { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001E RID: 30
		string IdentityProvider { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001F RID: 31
		string AuthenticationScheme { get; }

		// Token: 0x06000020 RID: 32
		string GetValidAccessToken();

		// Token: 0x06000021 RID: 33
		long GetReAcquireOn();
	}
}
