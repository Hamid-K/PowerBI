using System;
using System.Collections.Generic;
using Microsoft.DataIntegration.FuzzyMatchingCommon;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000093 RID: 147
	public class NullTokenIdProvider : ITokenIdProvider
	{
		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060005CB RID: 1483 RVA: 0x00019E1E File Offset: 0x0001801E
		// (set) Token: 0x060005CC RID: 1484 RVA: 0x00019E26 File Offset: 0x00018026
		public List<string> DomainNameList { get; set; }

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060005CD RID: 1485 RVA: 0x00019E2F File Offset: 0x0001802F
		public bool SupportsGetToken
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x00019E34 File Offset: 0x00018034
		public int GetDomainId(string domainName)
		{
			if (this.DomainNameList != null)
			{
				for (int i = 0; i < this.DomainNameList.Count; i++)
				{
					if (this.DomainNameList[i].Equals(domainName))
					{
						return i;
					}
				}
			}
			throw new Exception("Domain not found.");
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x00019E7F File Offset: 0x0001807F
		public string GetDomainName(int domainId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x00019E86 File Offset: 0x00018086
		public bool TryGetTokenId(StringExtent token, int domainId, out int tokenId)
		{
			tokenId = -1;
			return false;
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x00019E8C File Offset: 0x0001808C
		public int GetOrCreateTokenId(StringExtent token, int domainId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x00019E93 File Offset: 0x00018093
		public int CreateTokenId(int domainId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x00019E9A File Offset: 0x0001809A
		public StringExtent GetToken(int token)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x00019EA1 File Offset: 0x000180A1
		public bool TryGetToken(int tokenId, out StringExtent token)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x00019EA8 File Offset: 0x000180A8
		public int GetDomainId(int tokenId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x00019EAF File Offset: 0x000180AF
		public bool IsTemporary(int token)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060005D7 RID: 1495 RVA: 0x00019EB6 File Offset: 0x000180B6
		public IEnumerable<string> DomainNames
		{
			get
			{
				return this.DomainNameList;
			}
		}
	}
}
