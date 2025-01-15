using System;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core.DomainModel
{
	// Token: 0x0200017C RID: 380
	public interface ILinguisticSchemaUpgradeLanguageService
	{
		// Token: 0x0600074A RID: 1866
		bool IsValid(LanguageIdentifier language);

		// Token: 0x0600074B RID: 1867
		string MakeFriendlyNameString(string name, LanguageIdentifier language);
	}
}
