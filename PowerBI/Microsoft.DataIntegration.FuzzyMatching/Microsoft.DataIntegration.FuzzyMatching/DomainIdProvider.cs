using System;
using System.Collections.Generic;
using Microsoft.DataIntegration.FuzzyMatchingCommon;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000064 RID: 100
	[Serializable]
	public class DomainIdProvider : IDomainIdProvider, IMemoryUsage
	{
		// Token: 0x06000403 RID: 1027 RVA: 0x00012A52 File Offset: 0x00010C52
		public void AddDomain(string domainName, int domainId)
		{
			this.m_domainNameToId.Add(domainName, domainId);
			this.m_domainIdToDomainname.Add(domainId, domainName);
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00012A6E File Offset: 0x00010C6E
		public void DropDomain(string domainName)
		{
			this.m_domainIdToDomainname.Remove(this.GetDomainId(domainName));
			this.m_domainNameToId.Remove(domainName);
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x00012A90 File Offset: 0x00010C90
		public string GetDomainName(int domainId)
		{
			return this.m_domainIdToDomain[domainId];
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00012A9A File Offset: 0x00010C9A
		public int GetDomainId(string domainName)
		{
			return this.m_domainNameToId[domainName];
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x00012AA8 File Offset: 0x00010CA8
		public IEnumerable<string> DomainNames
		{
			get
			{
				return this.m_domainNameToId.Keys;
			}
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00012AB5 File Offset: 0x00010CB5
		public bool DomainExists(string domainName)
		{
			return this.m_domainNameToId.ContainsKey(domainName);
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x00012AC3 File Offset: 0x00010CC3
		public long MemoryUsage
		{
			get
			{
				return (long)(this.m_domainNameToId.Count * 256);
			}
		}

		// Token: 0x04000151 RID: 337
		private Dictionary<string, int> m_domainNameToId = new Dictionary<string, int>();

		// Token: 0x04000152 RID: 338
		private Dictionary<int, string> m_domainIdToDomainname = new Dictionary<int, string>();

		// Token: 0x04000153 RID: 339
		private string[] m_domainIdToDomain = new string[0];
	}
}
