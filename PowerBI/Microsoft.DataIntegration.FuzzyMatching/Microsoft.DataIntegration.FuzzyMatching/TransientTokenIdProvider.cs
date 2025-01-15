using System;
using System.Collections.Generic;
using Microsoft.DataIntegration.FuzzyMatchingCommon;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000095 RID: 149
	[Serializable]
	public class TransientTokenIdProvider : ITokenIdProvider, ITokenToClusterMap, IReset
	{
		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060005E5 RID: 1509 RVA: 0x0001A109 File Offset: 0x00018309
		// (set) Token: 0x060005E6 RID: 1510 RVA: 0x0001A111 File Offset: 0x00018311
		public bool CloneTokens { get; set; }

		// Token: 0x060005E7 RID: 1511 RVA: 0x0001A11A File Offset: 0x0001831A
		public TransientTokenIdProvider()
		{
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x0001A138 File Offset: 0x00018338
		public TransientTokenIdProvider(ITokenIdProvider primaryIdProvider)
		{
			this.SetPrimaryTokenIdProvider(primaryIdProvider);
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060005E9 RID: 1513 RVA: 0x0001A15D File Offset: 0x0001835D
		public bool SupportsGetToken
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x0001A160 File Offset: 0x00018360
		public void SetPrimaryTokenIdProvider(ITokenIdProvider primaryIdProvider)
		{
			this.Reset();
			this.m_primaryIdProvider = primaryIdProvider;
			this.m_tokenClusterMap = primaryIdProvider as ITokenToClusterMap;
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x0001A17B File Offset: 0x0001837B
		public void AddTokenClusterMapping(int token, int cluster)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x0001A182 File Offset: 0x00018382
		public int GetTokenClusterMapping(int token)
		{
			if (this.m_tokenClusterMap != null)
			{
				return this.m_tokenClusterMap.GetTokenClusterMapping(token);
			}
			return token;
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x0001A19A File Offset: 0x0001839A
		public void Reset()
		{
			this.m_idToToken.Clear();
			this.m_tokenAllocator.Reset();
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x0001A1B2 File Offset: 0x000183B2
		public bool TryGetTokenId(StringExtent token, int domainId, out int tokenId)
		{
			bool flag = this.m_primaryIdProvider.TryGetTokenId(token, domainId, out tokenId);
			if (flag)
			{
				this.Cache(tokenId, token);
			}
			return flag;
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x0001A1CE File Offset: 0x000183CE
		private void Cache(int tokenId, StringExtent token)
		{
			if (!this.m_idToToken.ContainsKey(tokenId))
			{
				this.m_idToToken.Add(tokenId, token.AllocClone(this.m_tokenAllocator));
			}
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x0001A1F8 File Offset: 0x000183F8
		public int GetOrCreateTokenId(StringExtent token, int domainId)
		{
			int orCreateTokenId = this.m_primaryIdProvider.GetOrCreateTokenId(token, domainId);
			this.Cache(orCreateTokenId, token);
			return orCreateTokenId;
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x0001A21C File Offset: 0x0001841C
		public int CreateTokenId(int domainId)
		{
			return this.m_primaryIdProvider.CreateTokenId(domainId);
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x0001A22A File Offset: 0x0001842A
		public bool TryGetToken(int tokenId, out StringExtent token)
		{
			if (this.m_idToToken.TryGetValue(tokenId, ref token))
			{
				if (this.CloneTokens)
				{
					token = token.AllocClone(HeapSegmentAllocator<char>.Instance);
				}
				return true;
			}
			return this.m_primaryIdProvider.TryGetToken(tokenId, out token);
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x0001A264 File Offset: 0x00018464
		public StringExtent GetToken(int tokenId)
		{
			StringExtent stringExtent;
			if (this.m_idToToken.TryGetValue(tokenId, ref stringExtent))
			{
				if (this.CloneTokens)
				{
					stringExtent = stringExtent.AllocClone(HeapSegmentAllocator<char>.Instance);
				}
				return stringExtent;
			}
			return this.m_primaryIdProvider.GetToken(tokenId);
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x0001A2A4 File Offset: 0x000184A4
		public int GetDomainId(int tokenId)
		{
			return this.m_primaryIdProvider.GetDomainId(tokenId);
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x0001A2B2 File Offset: 0x000184B2
		public bool IsTemporary(int tokenId)
		{
			return this.m_primaryIdProvider.IsTemporary(tokenId);
		}

		// Token: 0x040001F9 RID: 505
		private ITokenIdProvider m_primaryIdProvider;

		// Token: 0x040001FA RID: 506
		private ITokenToClusterMap m_tokenClusterMap;

		// Token: 0x040001FB RID: 507
		private Dictionary<int, StringExtent> m_idToToken = new Dictionary<int, StringExtent>();

		// Token: 0x040001FC RID: 508
		private BlockedSegmentArray<char> m_tokenAllocator = new BlockedSegmentArray<char>();
	}
}
