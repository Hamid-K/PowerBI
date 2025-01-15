using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000AB RID: 171
	[CompatibilityRequirement("1480")]
	public sealed class QueryGroupAnnotationCollection : NamedMetadataObjectCollection<Annotation, QueryGroup>
	{
		// Token: 0x06000A81 RID: 2689 RVA: 0x00056449 File Offset: 0x00054649
		internal QueryGroupAnnotationCollection(QueryGroup parent, IEqualityComparer<string> comparer)
			: base(ObjectType.Annotation, parent, comparer, true)
		{
		}
	}
}
