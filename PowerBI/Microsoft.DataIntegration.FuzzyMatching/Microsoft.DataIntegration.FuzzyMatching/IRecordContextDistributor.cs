using System;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000077 RID: 119
	public interface IRecordContextDistributor
	{
		// Token: 0x060004CA RID: 1226
		void RequestRowset(string rowsetName, IRecordContextUpdate recordContextUpdate);
	}
}
