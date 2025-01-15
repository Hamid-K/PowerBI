using System;

namespace Microsoft.ApplicationInsights.Extensibility
{
	// Token: 0x02000051 RID: 81
	public interface IApplicationIdProvider
	{
		// Token: 0x0600028C RID: 652
		bool TryGetApplicationId(string instrumentationKey, out string applicationId);
	}
}
