using System;
using System.CodeDom.Compiler;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000010 RID: 16
	[OriginalName("CacheRefreshPlanSingle")]
	public class CacheRefreshPlanSingle : DataServiceQuerySingle<CacheRefreshPlan>
	{
		// Token: 0x06000087 RID: 135 RVA: 0x00002CD5 File Offset: 0x00000ED5
		public CacheRefreshPlanSingle(DataServiceContext context, string path)
			: base(context, path)
		{
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00002CDF File Offset: 0x00000EDF
		public CacheRefreshPlanSingle(DataServiceContext context, string path, bool isComposable)
			: base(context, path, isComposable)
		{
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00002CEA File Offset: 0x00000EEA
		public CacheRefreshPlanSingle(DataServiceQuerySingle<CacheRefreshPlan> query)
			: base(query)
		{
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00002CF3 File Offset: 0x00000EF3
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("History")]
		public DataServiceQuery<SubscriptionHistory> History
		{
			get
			{
				if (!base.IsComposable)
				{
					throw new NotSupportedException("The previous function is not composable.");
				}
				if (this._History == null)
				{
					this._History = base.Context.CreateQuery<SubscriptionHistory>(base.GetPath("History"));
				}
				return this._History;
			}
		}

		// Token: 0x0400006C RID: 108
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<SubscriptionHistory> _History;
	}
}
