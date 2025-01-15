using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000267 RID: 615
	internal interface IHierarchicalCachedItem : ICachedItem
	{
		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06001631 RID: 5681
		string ParentKey { get; }

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06001632 RID: 5682
		List<string> ChildKeys { get; }

		// Token: 0x06001633 RID: 5683
		void AddDependentKey(string parentKey);

		// Token: 0x06001634 RID: 5684
		void MakeDependentOn(IHierarchicalCachedItem parent);
	}
}
