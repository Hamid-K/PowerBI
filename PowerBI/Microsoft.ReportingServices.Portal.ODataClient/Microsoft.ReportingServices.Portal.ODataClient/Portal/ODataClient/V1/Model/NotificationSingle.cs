using System;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000E1 RID: 225
	[OriginalName("NotificationSingle")]
	public class NotificationSingle : DataServiceQuerySingle<Notification>
	{
		// Token: 0x06000A15 RID: 2581 RVA: 0x000145A9 File Offset: 0x000127A9
		public NotificationSingle(DataServiceContext context, string path)
			: base(context, path)
		{
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x000145B3 File Offset: 0x000127B3
		public NotificationSingle(DataServiceContext context, string path, bool isComposable)
			: base(context, path, isComposable)
		{
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x000145BE File Offset: 0x000127BE
		public NotificationSingle(DataServiceQuerySingle<Notification> query)
			: base(query)
		{
		}
	}
}
