using System;
using System.Collections.Generic;

namespace Microsoft.MachineLearning.Dracula
{
	// Token: 0x0200040A RID: 1034
	public interface ICountTable
	{
		// Token: 0x060015A1 RID: 5537
		void GetCounts(long key, float[] counts);

		// Token: 0x060015A2 RID: 5538
		void GetRawCounts(RawCountKey key, float[] counts);

		// Token: 0x060015A3 RID: 5539
		IEnumerable<RawCountKey> AllRawCountKeys();

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060015A4 RID: 5540
		float GarbageThreshold { get; }

		// Token: 0x060015A5 RID: 5541
		void GetPriors(float[] priorCounts, float[] garbageCounts);
	}
}
