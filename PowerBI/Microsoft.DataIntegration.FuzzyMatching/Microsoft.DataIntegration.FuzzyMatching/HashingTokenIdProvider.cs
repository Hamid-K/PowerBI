using System;
using Microsoft.DataIntegration.FuzzyMatchingCommon;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000097 RID: 151
	[Serializable]
	public class HashingTokenIdProvider : ITokenIdProvider
	{
		// Token: 0x06000607 RID: 1543 RVA: 0x0001A462 File Offset: 0x00018662
		public bool IsTemporary(int tokenId)
		{
			return false;
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x0001A468 File Offset: 0x00018668
		public int GetOrCreateTokenId(StringExtent token, int domainId)
		{
			int num = Utilities.GetHashCode(domainId, Utilities.GetHashCode(token.Array, token.Offset, token.Length));
			if (num == 0)
			{
				num = 1;
			}
			num &= 16777215;
			return num | (domainId << 24);
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x0001A4AB File Offset: 0x000186AB
		public bool TryGetTokenId(StringExtent token, int domainId, out int tokenId)
		{
			tokenId = this.GetOrCreateTokenId(token, domainId);
			return true;
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x0001A4B8 File Offset: 0x000186B8
		public int GetDomainId(int tokenId)
		{
			return (int)((uint)(tokenId & -16777216) >> 24);
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x0001A4C4 File Offset: 0x000186C4
		public int CreateTokenId(int domainId)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x0001A4CB File Offset: 0x000186CB
		public bool TryGetToken(int tokenId, out StringExtent token)
		{
			token = default(StringExtent);
			return false;
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x0001A4D5 File Offset: 0x000186D5
		public StringExtent GetToken(int tokenId)
		{
			throw new NotSupportedException("Unable to map the TokenId back to String.");
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x0600060E RID: 1550 RVA: 0x0001A4E1 File Offset: 0x000186E1
		public bool SupportsGetToken
		{
			get
			{
				return false;
			}
		}
	}
}
