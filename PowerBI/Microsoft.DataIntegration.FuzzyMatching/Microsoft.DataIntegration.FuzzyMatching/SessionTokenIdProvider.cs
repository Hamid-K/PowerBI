using System;
using System.Collections.Generic;
using Microsoft.DataIntegration.FuzzyMatchingCommon;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000094 RID: 148
	public class SessionTokenIdProvider : ITokenIdProvider, ITokenToClusterMap
	{
		// Token: 0x060005D9 RID: 1497 RVA: 0x00019EC8 File Offset: 0x000180C8
		public SessionTokenIdProvider(ITokenIdProvider primaryIdProvider)
		{
			this.m_primaryIdProvider = primaryIdProvider;
			this.m_tokenClusterMap = primaryIdProvider as ITokenToClusterMap;
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x00019F1B File Offset: 0x0001811B
		public bool SupportsGetToken
		{
			get
			{
				return this.m_primaryIdProvider.SupportsGetToken;
			}
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x00019F28 File Offset: 0x00018128
		public void AddTokenClusterMapping(int token, int cluster)
		{
			if (this.m_tokenClusterMap != null)
			{
				this.m_tokenClusterMap.AddTokenClusterMapping(token, cluster);
			}
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x00019F3F File Offset: 0x0001813F
		public int GetTokenClusterMapping(int token)
		{
			if (this.m_tokenClusterMap != null)
			{
				return this.m_tokenClusterMap.GetTokenClusterMapping(token);
			}
			return token;
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x00019F57 File Offset: 0x00018157
		public void Reset()
		{
			this.m_idToToken.Clear();
			this.m_tokenToId.Clear();
			this.m_nextId = 1;
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x00019F78 File Offset: 0x00018178
		public bool TryGetTokenId(StringExtent token, int domainId, out int tokenId)
		{
			if (this.m_primaryIdProvider.TryGetTokenId(token, domainId, out tokenId))
			{
				return true;
			}
			if (this.m_tokenToId.TryGetValue(new SessionTokenIdProvider.DomainToken
			{
				Token = token,
				DomainId = domainId
			}, ref tokenId))
			{
				return true;
			}
			tokenId = 0;
			return false;
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x00019FC4 File Offset: 0x000181C4
		public int GetOrCreateTokenId(StringExtent token, int domainId)
		{
			int num;
			if (!this.m_primaryIdProvider.TryGetTokenId(token, domainId, out num))
			{
				SessionTokenIdProvider.DomainToken domainToken = new SessionTokenIdProvider.DomainToken
				{
					Token = token,
					DomainId = domainId
				};
				if (!this.m_tokenToId.TryGetValue(domainToken, ref num))
				{
					num = this.CreateTokenId(domainId);
					token = (domainToken.Token = token.AllocClone(this.m_tokenAllocator));
					this.m_idToToken.Add(num, token);
					this.m_tokenToId.Add(domainToken, num);
				}
			}
			return num;
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x0001A048 File Offset: 0x00018248
		public int CreateTokenId(int domainId)
		{
			int nextId = this.m_nextId;
			this.m_nextId = nextId + 1;
			return -((nextId & 16777215) | (domainId << 24));
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x0001A072 File Offset: 0x00018272
		public bool TryGetToken(int tokenId, out StringExtent token)
		{
			if (tokenId > 0)
			{
				return this.m_primaryIdProvider.TryGetToken(tokenId, out token);
			}
			if (tokenId < 0)
			{
				token = this.m_idToToken[tokenId];
				return true;
			}
			throw new ArgumentException("TokenId is not valid.");
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0001A0A8 File Offset: 0x000182A8
		public StringExtent GetToken(int tokenId)
		{
			if (tokenId > 0)
			{
				return this.m_primaryIdProvider.GetToken(tokenId);
			}
			if (tokenId < 0)
			{
				return this.m_idToToken[tokenId];
			}
			throw new ArgumentException("TokenId is not valid.");
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x0001A0D6 File Offset: 0x000182D6
		public int GetDomainId(int tokenId)
		{
			if (tokenId > 0)
			{
				return this.m_primaryIdProvider.GetDomainId(tokenId);
			}
			if (tokenId < 0)
			{
				return (int)((uint)(-tokenId & -16777216) >> 24);
			}
			throw new ArgumentException("TokenId is not valid.");
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x0001A103 File Offset: 0x00018303
		public bool IsTemporary(int tokenId)
		{
			return tokenId < 0;
		}

		// Token: 0x040001F3 RID: 499
		private ITokenIdProvider m_primaryIdProvider;

		// Token: 0x040001F4 RID: 500
		private ITokenToClusterMap m_tokenClusterMap;

		// Token: 0x040001F5 RID: 501
		private Dictionary<int, StringExtent> m_idToToken = new Dictionary<int, StringExtent>();

		// Token: 0x040001F6 RID: 502
		private Dictionary<SessionTokenIdProvider.DomainToken, int> m_tokenToId = new Dictionary<SessionTokenIdProvider.DomainToken, int>(SessionTokenIdProvider.DomainTokenEqualityComparer.Instance);

		// Token: 0x040001F7 RID: 503
		private int m_nextId = 1;

		// Token: 0x040001F8 RID: 504
		private BlockedSegmentArray<char> m_tokenAllocator = new BlockedSegmentArray<char>();

		// Token: 0x0200016B RID: 363
		private struct DomainToken
		{
			// Token: 0x040005D3 RID: 1491
			public StringExtent Token;

			// Token: 0x040005D4 RID: 1492
			public int DomainId;
		}

		// Token: 0x0200016C RID: 364
		private class DomainTokenEqualityComparer : IEqualityComparer<SessionTokenIdProvider.DomainToken>
		{
			// Token: 0x06000CE8 RID: 3304 RVA: 0x00037515 File Offset: 0x00035715
			public bool Equals(SessionTokenIdProvider.DomainToken x, SessionTokenIdProvider.DomainToken y)
			{
				return x.DomainId == y.DomainId && x.Token.Equals(y.Token);
			}

			// Token: 0x06000CE9 RID: 3305 RVA: 0x00037539 File Offset: 0x00035739
			public int GetHashCode(SessionTokenIdProvider.DomainToken x)
			{
				return Utilities.GetHashCode(x.Token.GetHashCode(), x.DomainId);
			}

			// Token: 0x040005D5 RID: 1493
			public static readonly SessionTokenIdProvider.DomainTokenEqualityComparer Instance = new SessionTokenIdProvider.DomainTokenEqualityComparer();
		}
	}
}
