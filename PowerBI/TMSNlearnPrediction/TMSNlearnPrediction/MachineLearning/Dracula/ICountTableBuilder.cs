using System;

namespace Microsoft.MachineLearning.Dracula
{
	// Token: 0x0200040D RID: 1037
	public interface ICountTableBuilder
	{
		// Token: 0x060015BE RID: 5566
		double Increment(long key, long labelKey, double amount);

		// Token: 0x060015BF RID: 5567
		void InsertOrUpdateRawCounts(int hashId, long hashValue, float[] counts);

		// Token: 0x060015C0 RID: 5568
		ICountTable CreateCountTable();
	}
}
