using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000AC RID: 172
	[CompatibilityRequirement("1480")]
	public sealed class QueryGroupCollection : NamedMetadataObjectCollection<QueryGroup, Model>
	{
		// Token: 0x06000A82 RID: 2690 RVA: 0x00056456 File Offset: 0x00054656
		internal QueryGroupCollection(Model parent, IEqualityComparer<string> comparer)
			: base(ObjectType.QueryGroup, parent, comparer, true)
		{
		}
	}
}
