using System;
using Microsoft.DataIntegration.FuzzyMatchingCommon;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Diagnostics;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000090 RID: 144
	public interface IFuzzyLookupStateManager : IRecordContextCache, IInvertedIndexUpdate, IRecordWithIdUpdate, IRecordWithIdLookup, IInvertedIndexLookup, IMemoryUsage, IMemoryLimit
	{
		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060005C1 RID: 1473
		IStatistics Statistics { get; }
	}
}
