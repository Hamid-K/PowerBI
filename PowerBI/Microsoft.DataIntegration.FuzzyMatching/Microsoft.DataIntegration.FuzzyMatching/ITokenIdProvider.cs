using System;
using Microsoft.DataIntegration.FuzzyMatchingCommon;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000092 RID: 146
	public interface ITokenIdProvider
	{
		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060005C3 RID: 1475
		bool SupportsGetToken { get; }

		// Token: 0x060005C4 RID: 1476
		bool TryGetTokenId(StringExtent token, int domainId, out int tokenId);

		// Token: 0x060005C5 RID: 1477
		int GetOrCreateTokenId(StringExtent token, int domainId);

		// Token: 0x060005C6 RID: 1478
		int CreateTokenId(int domainId);

		// Token: 0x060005C7 RID: 1479
		StringExtent GetToken(int token);

		// Token: 0x060005C8 RID: 1480
		bool TryGetToken(int tokenId, out StringExtent token);

		// Token: 0x060005C9 RID: 1481
		int GetDomainId(int tokenId);

		// Token: 0x060005CA RID: 1482
		bool IsTemporary(int token);
	}
}
