using System;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000012 RID: 18
	[OriginalName("SubscriptionHistorySingle")]
	public class SubscriptionHistorySingle : DataServiceQuerySingle<SubscriptionHistory>
	{
		// Token: 0x060000AB RID: 171 RVA: 0x00002FBA File Offset: 0x000011BA
		public SubscriptionHistorySingle(DataServiceContext context, string path)
			: base(context, path)
		{
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00002FC4 File Offset: 0x000011C4
		public SubscriptionHistorySingle(DataServiceContext context, string path, bool isComposable)
			: base(context, path, isComposable)
		{
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00002FCF File Offset: 0x000011CF
		public SubscriptionHistorySingle(DataServiceQuerySingle<SubscriptionHistory> query)
			: base(query)
		{
		}
	}
}
