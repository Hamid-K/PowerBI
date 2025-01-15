using System;

namespace Microsoft.TMSN.TMSNlearn
{
	// Token: 0x02000497 RID: 1175
	public interface IEarlyStoppingCriterion
	{
		// Token: 0x0600188A RID: 6282
		bool CheckScore(float validationScore, float trainingScore, out bool isBestCandidate);
	}
}
