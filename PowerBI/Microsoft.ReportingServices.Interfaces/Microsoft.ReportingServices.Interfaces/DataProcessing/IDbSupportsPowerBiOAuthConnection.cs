using System;

namespace Microsoft.ReportingServices.DataProcessing
{
	// Token: 0x02000009 RID: 9
	public interface IDbSupportsPowerBiOAuthConnection
	{
		// Token: 0x17000009 RID: 9
		// (set) Token: 0x06000010 RID: 16
		string ServiceToServiceToken { set; }

		// Token: 0x1700000A RID: 10
		// (set) Token: 0x06000011 RID: 17
		string IdentityProvider { set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000012 RID: 18
		// (set) Token: 0x06000013 RID: 19
		IntendedConnectionUsage IntendedUsage { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000014 RID: 20
		// (set) Token: 0x06000015 RID: 21
		bool BypassBuildPermission { get; set; }
	}
}
