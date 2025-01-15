using System;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x0200003B RID: 59
	[OriginalName("NotificationSingle")]
	public class NotificationSingle : DataServiceQuerySingle<Notification>
	{
		// Token: 0x06000285 RID: 645 RVA: 0x0000669E File Offset: 0x0000489E
		public NotificationSingle(DataServiceContext context, string path)
			: base(context, path)
		{
		}

		// Token: 0x06000286 RID: 646 RVA: 0x000066A8 File Offset: 0x000048A8
		public NotificationSingle(DataServiceContext context, string path, bool isComposable)
			: base(context, path, isComposable)
		{
		}

		// Token: 0x06000287 RID: 647 RVA: 0x000066B3 File Offset: 0x000048B3
		public NotificationSingle(DataServiceQuerySingle<Notification> query)
			: base(query)
		{
		}
	}
}
