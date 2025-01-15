using System;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000061 RID: 97
	public interface IDomainManager : IDomainIdProvider
	{
		// Token: 0x060003CE RID: 974
		IRecordTokenizer GetTokenizer(string domainName);

		// Token: 0x060003CF RID: 975
		ITransformationProvider GetLeftTransformationProvider(string domainName);

		// Token: 0x060003D0 RID: 976
		ITransformationProvider GetRightTransformationProvider(string domainName);

		// Token: 0x060003D1 RID: 977
		IPairSpecificTransformationProvider GetPairSpecificTransformationProvider(string domainName);

		// Token: 0x060003D2 RID: 978
		ITokenWeightProvider GetTokenWeightProvider(string domainName);
	}
}
