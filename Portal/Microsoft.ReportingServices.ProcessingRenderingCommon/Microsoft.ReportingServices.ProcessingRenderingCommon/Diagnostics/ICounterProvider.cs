using System;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000099 RID: 153
	internal interface ICounterProvider
	{
		// Token: 0x060004C4 RID: 1220
		ICounter GetCounterNumberOfItems(string categoryName, string counterName, bool resetCounter);

		// Token: 0x060004C5 RID: 1221
		ICounter GetCounterRatePerSecond(string categoryName, string counterNameTotal, string counterNamePerSecond, bool resetCounter);

		// Token: 0x060004C6 RID: 1222
		ICounter GetCounterAverageCount(string categoryName, string counterNameAverage, string counterNameBase, bool resetCounter);
	}
}
