using System;
using System.Collections.Generic;
using Microsoft.DataIntegration.FuzzyMatchingCommon;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000B7 RID: 183
	[Serializable]
	internal class ComparisonProviderInfo
	{
		// Token: 0x060006FC RID: 1788 RVA: 0x0001F2F2 File Offset: 0x0001D4F2
		public ComparisonProviderInfo(IDomainManager domainManager, ITokenIdProvider tokenIdProvider, RecordBinding bindings, IEnumerable<string> domains, IEnumerable<string> exactMatchDomains, JoinSide joinSide)
		{
			this.Prepare(domainManager, tokenIdProvider, bindings, domains, exactMatchDomains, joinSide);
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x0001F320 File Offset: 0x0001D520
		public void ResetSession()
		{
			foreach (DomainProviderInfo domainProviderInfo in this.m_domainProviderInfo)
			{
				domainProviderInfo.ResetSession();
			}
			foreach (DomainProviderInfo domainProviderInfo2 in this.m_exactMatchDomainProviderInfo)
			{
				domainProviderInfo2.ResetSession();
			}
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x0001F3B0 File Offset: 0x0001D5B0
		public void Prepare(IDomainManager domainManager, ITokenIdProvider tokenIdProvider, RecordBinding binding, IEnumerable<string> domains, IEnumerable<string> exactMatchDomains, JoinSide joinSide)
		{
			this.TokenIdProvider = tokenIdProvider;
			this.m_domainProviderInfo.Clear();
			bool flag = false;
			foreach (string text in domains)
			{
				if (!flag)
				{
					flag = binding.GetDomainBinding(text).Columns.Count > 0;
				}
				DomainProviderInfo domainProviderInfo = new DomainProviderInfo(domainManager, text, domainManager.GetDomainId(text), joinSide);
				domainProviderInfo.Tokenizer.Prepare(binding.Schema, binding.GetDomainBinding(text), out domainProviderInfo.TokenizerContext);
				this.m_domainProviderInfo.Add(domainProviderInfo);
			}
			if (!flag)
			{
				throw new InvalidOperationException(string.Format("Invalid RecordBinding.  There were no columns bound for any of following domains: {0}.", Utilities.ToString<string>(domains, ", ")));
			}
			this.m_exactMatchDomainProviderInfo.Clear();
			if (exactMatchDomains != null)
			{
				foreach (string text2 in exactMatchDomains)
				{
					DomainProviderInfo domainProviderInfo2 = new DomainProviderInfo(domainManager, text2, domainManager.GetDomainId(text2), joinSide);
					domainProviderInfo2.Tokenizer.Prepare(binding.Schema, binding.GetDomainBinding(text2), out domainProviderInfo2.TokenizerContext);
					this.m_exactMatchDomainProviderInfo.Add(domainProviderInfo2);
				}
			}
		}

		// Token: 0x040002AC RID: 684
		public ITokenIdProvider TokenIdProvider;

		// Token: 0x040002AD RID: 685
		public List<DomainProviderInfo> m_domainProviderInfo = new List<DomainProviderInfo>();

		// Token: 0x040002AE RID: 686
		public List<DomainProviderInfo> m_exactMatchDomainProviderInfo = new List<DomainProviderInfo>();
	}
}
