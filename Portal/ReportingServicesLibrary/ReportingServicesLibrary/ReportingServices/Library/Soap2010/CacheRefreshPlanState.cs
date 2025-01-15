using System;

namespace Microsoft.ReportingServices.Library.Soap2010
{
	// Token: 0x020002F3 RID: 755
	public class CacheRefreshPlanState
	{
		// Token: 0x06001AE8 RID: 6888 RVA: 0x000025F4 File Offset: 0x000007F4
		public CacheRefreshPlanState()
		{
		}

		// Token: 0x06001AE9 RID: 6889 RVA: 0x0006CE19 File Offset: 0x0006B019
		internal CacheRefreshPlanState(ActiveState state)
		{
			this.MissingParameterValue = state.MissingParameterValue;
			this.InvalidParameterValue = state.InvalidParameterValue;
			this.UnknownItemParameter = state.UnknownItemParameter;
			this.CachingNotEnabledOnItem = state.CachingNotEnabledOnItem;
		}

		// Token: 0x040009E7 RID: 2535
		public bool MissingParameterValue;

		// Token: 0x040009E8 RID: 2536
		public bool InvalidParameterValue;

		// Token: 0x040009E9 RID: 2537
		public bool UnknownItemParameter;

		// Token: 0x040009EA RID: 2538
		public bool CachingNotEnabledOnItem;
	}
}
