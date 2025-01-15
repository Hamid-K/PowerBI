using System;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon
{
	// Token: 0x0200001A RID: 26
	public interface ISelfJoinEstimator
	{
		// Token: 0x0600006F RID: 111
		void Begin();

		// Token: 0x06000070 RID: 112
		void Add(int key);

		// Token: 0x06000071 RID: 113
		void End();

		// Token: 0x06000072 RID: 114
		long SelfJoinSize();

		// Token: 0x06000073 RID: 115
		void Clear();
	}
}
