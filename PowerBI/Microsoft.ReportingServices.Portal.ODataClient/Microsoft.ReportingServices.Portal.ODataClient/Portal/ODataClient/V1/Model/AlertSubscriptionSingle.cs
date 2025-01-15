using System;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000F0 RID: 240
	[OriginalName("AlertSubscriptionSingle")]
	public class AlertSubscriptionSingle : DataServiceQuerySingle<AlertSubscription>
	{
		// Token: 0x06000AB3 RID: 2739 RVA: 0x00015329 File Offset: 0x00013529
		public AlertSubscriptionSingle(DataServiceContext context, string path)
			: base(context, path)
		{
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x00015333 File Offset: 0x00013533
		public AlertSubscriptionSingle(DataServiceContext context, string path, bool isComposable)
			: base(context, path, isComposable)
		{
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x0001533E File Offset: 0x0001353E
		public AlertSubscriptionSingle(DataServiceQuerySingle<AlertSubscription> query)
			: base(query)
		{
		}
	}
}
