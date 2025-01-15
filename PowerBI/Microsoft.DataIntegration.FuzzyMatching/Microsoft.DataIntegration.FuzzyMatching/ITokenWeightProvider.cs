using System;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x0200011A RID: 282
	public interface ITokenWeightProvider
	{
		// Token: 0x06000BBF RID: 3007
		int GetWeight(ITokenIdProvider tokenIdProvider, int tokenId);
	}
}
