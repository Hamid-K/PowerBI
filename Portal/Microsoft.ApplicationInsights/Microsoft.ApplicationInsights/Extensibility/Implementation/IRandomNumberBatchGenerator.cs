using System;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation
{
	// Token: 0x0200006F RID: 111
	internal interface IRandomNumberBatchGenerator
	{
		// Token: 0x0600035E RID: 862
		void NextBatch(ulong[] buffer, int index, int count);
	}
}
