using System;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000B3 RID: 179
	[OriginalName("CacheRefreshPlanSingle")]
	public class CacheRefreshPlanSingle : DataServiceQuerySingle<CacheRefreshPlan>
	{
		// Token: 0x06000777 RID: 1911 RVA: 0x0000F3E5 File Offset: 0x0000D5E5
		public CacheRefreshPlanSingle(DataServiceContext context, string path)
			: base(context, path)
		{
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x0000F3EF File Offset: 0x0000D5EF
		public CacheRefreshPlanSingle(DataServiceContext context, string path, bool isComposable)
			: base(context, path, isComposable)
		{
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x0000F3FA File Offset: 0x0000D5FA
		public CacheRefreshPlanSingle(DataServiceQuerySingle<CacheRefreshPlan> query)
			: base(query)
		{
		}
	}
}
