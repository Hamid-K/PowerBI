using System;
using System.Collections.Generic;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000250 RID: 592
	internal interface IBatchOperation
	{
		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x060013CB RID: 5067
		List<string> RegionNames { get; }

		// Token: 0x060013CC RID: 5068
		List<string> GetContainedRegionsList(List<string> regions);

		// Token: 0x060013CD RID: 5069
		int GetItemCount(string regionName);

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x060013CE RID: 5070
		int Count { get; }

		// Token: 0x060013CF RID: 5071
		IEnumerator<EvictedElement> GetEnumerator(string regionName);

		// Token: 0x060013D0 RID: 5072
		IEnumerator<EvictedElement> GetEnumerator();
	}
}
