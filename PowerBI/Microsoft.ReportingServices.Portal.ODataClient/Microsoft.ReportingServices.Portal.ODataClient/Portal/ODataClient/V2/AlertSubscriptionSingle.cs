using System;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x0200000E RID: 14
	[OriginalName("AlertSubscriptionSingle")]
	public class AlertSubscriptionSingle : DataServiceQuerySingle<AlertSubscription>
	{
		// Token: 0x06000079 RID: 121 RVA: 0x00002BB6 File Offset: 0x00000DB6
		public AlertSubscriptionSingle(DataServiceContext context, string path)
			: base(context, path)
		{
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00002BC0 File Offset: 0x00000DC0
		public AlertSubscriptionSingle(DataServiceContext context, string path, bool isComposable)
			: base(context, path, isComposable)
		{
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00002BCB File Offset: 0x00000DCB
		public AlertSubscriptionSingle(DataServiceQuerySingle<AlertSubscription> query)
			: base(query)
		{
		}
	}
}
