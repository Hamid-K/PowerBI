using System;
using System.Collections.Generic;

namespace Microsoft.MachineLearning.Dracula
{
	// Token: 0x02000420 RID: 1056
	public interface ICountFeaturizer
	{
		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060015F9 RID: 5625
		int NumFeatures { get; }

		// Token: 0x060015FA RID: 5626
		IEnumerable<string> GetFeatureNames(string[] classNames = null);

		// Token: 0x060015FB RID: 5627
		void GetFeatures(long key, float[] features, int startIdx);
	}
}
