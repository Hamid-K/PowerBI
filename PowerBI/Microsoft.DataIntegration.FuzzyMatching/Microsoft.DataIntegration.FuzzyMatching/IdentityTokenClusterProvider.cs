using System;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000007 RID: 7
	[Serializable]
	public class IdentityTokenClusterProvider : ITokenToClusterMap
	{
		// Token: 0x06000020 RID: 32 RVA: 0x00002A8F File Offset: 0x00000C8F
		public void AddTokenClusterMapping(int token, int cluster)
		{
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002A91 File Offset: 0x00000C91
		public int GetTokenClusterMapping(int tokenId)
		{
			return tokenId;
		}
	}
}
