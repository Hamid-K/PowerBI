using System;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000008 RID: 8
	public interface ITokenToClusterMap
	{
		// Token: 0x06000023 RID: 35
		void AddTokenClusterMapping(int token, int cluster);

		// Token: 0x06000024 RID: 36
		int GetTokenClusterMapping(int token);
	}
}
