using System;

namespace Microsoft.Cloud.Platform.MonitoredUtils
{
	// Token: 0x02000116 RID: 278
	public interface IAuditRequest
	{
		// Token: 0x17000142 RID: 322
		// (get) Token: 0x0600077D RID: 1917
		IAuditingPrincipal Principal { get; }

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600077E RID: 1918
		string AuditOperation { get; }

		// Token: 0x0600077F RID: 1919
		IAuditable AsAuditable();
	}
}
