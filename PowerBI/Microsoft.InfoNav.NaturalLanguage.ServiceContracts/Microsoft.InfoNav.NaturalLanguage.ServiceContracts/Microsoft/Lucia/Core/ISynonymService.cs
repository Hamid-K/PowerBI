using System;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000E3 RID: 227
	public interface ISynonymService
	{
		// Token: 0x0600046E RID: 1134
		SynonymLookupResult LookupSynonyms(SynonymLookupRequest request);

		// Token: 0x0600046F RID: 1135
		SynonymLookupResult[] LookupSynonyms(SynonymLookupRequest[] requests);
	}
}
