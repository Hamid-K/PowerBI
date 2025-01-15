using System;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000009 RID: 9
	internal interface ITokenClusterMapComputer
	{
		// Token: 0x06000025 RID: 37
		void Reset();

		// Token: 0x06000026 RID: 38
		void AddRule(Transformation rule);

		// Token: 0x06000027 RID: 39
		void Cluster(ITokenIdProvider tokenIdProvider, int domainId, ITokenToClusterMap tokenClusterMap);
	}
}
