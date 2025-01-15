using System;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000A2 RID: 162
	[Serializable]
	internal sealed class UnitRule
	{
		// Token: 0x06000657 RID: 1623 RVA: 0x0001BAB6 File Offset: 0x00019CB6
		internal void Reset(int token, int weight, int cluster)
		{
			this.token = token;
			this.weight = weight;
			this.tokenCluster = cluster;
		}

		// Token: 0x04000229 RID: 553
		internal int token;

		// Token: 0x0400022A RID: 554
		internal int weight;

		// Token: 0x0400022B RID: 555
		internal int tokenCluster;

		// Token: 0x0400022C RID: 556
		internal UnitRuleStatus status;

		// Token: 0x0400022D RID: 557
		internal int statusDim;
	}
}
