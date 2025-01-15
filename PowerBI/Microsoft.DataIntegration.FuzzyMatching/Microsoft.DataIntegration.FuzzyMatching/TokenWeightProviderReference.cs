using System;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000025 RID: 37
	[Serializable]
	public class TokenWeightProviderReference : ProviderReference<ITokenWeightProvider>, ITokenWeightProvider
	{
		// Token: 0x06000110 RID: 272 RVA: 0x000053E6 File Offset: 0x000035E6
		public int GetWeight(ITokenIdProvider tokenIdProvider, int tokenId)
		{
			return this.m_interface.GetWeight(tokenIdProvider, tokenId);
		}
	}
}
