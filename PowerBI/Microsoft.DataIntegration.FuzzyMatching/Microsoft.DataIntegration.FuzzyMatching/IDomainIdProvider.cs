using System;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000060 RID: 96
	public interface IDomainIdProvider
	{
		// Token: 0x060003C8 RID: 968
		void AddDomain(string domainName, int domainId);

		// Token: 0x060003C9 RID: 969
		void DropDomain(string domainName);

		// Token: 0x060003CA RID: 970
		string GetDomainName(int id);

		// Token: 0x060003CB RID: 971
		int GetDomainId(string domainName);

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060003CC RID: 972
		IEnumerable<string> DomainNames { get; }

		// Token: 0x060003CD RID: 973
		bool DomainExists(string domainName);
	}
}
