using System;
using Microsoft.InfoNav.Experimental.Insights.ServiceContracts.Internal;

namespace Microsoft.PowerBI.ExploreHost.Insights
{
	// Token: 0x02000081 RID: 129
	public interface IInsightsSessionStorage : IStorageService, IAsyncStorageService
	{
		// Token: 0x0600036F RID: 879
		void Clear();
	}
}
