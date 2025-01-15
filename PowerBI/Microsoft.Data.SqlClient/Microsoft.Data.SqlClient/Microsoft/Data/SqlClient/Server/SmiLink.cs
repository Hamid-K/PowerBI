using System;

namespace Microsoft.Data.SqlClient.Server
{
	// Token: 0x0200014D RID: 333
	internal abstract class SmiLink
	{
		// Token: 0x060019B7 RID: 6583
		internal abstract ulong NegotiateVersion(ulong requestedVersion);

		// Token: 0x060019B8 RID: 6584
		internal abstract object GetCurrentContext(SmiEventSink eventSink);

		// Token: 0x040009E1 RID: 2529
		internal const ulong InterfaceVersion = 210UL;
	}
}
