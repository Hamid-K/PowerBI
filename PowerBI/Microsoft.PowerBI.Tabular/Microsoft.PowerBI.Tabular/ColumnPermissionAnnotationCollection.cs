using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000048 RID: 72
	[CompatibilityRequirement("1400")]
	public sealed class ColumnPermissionAnnotationCollection : NamedMetadataObjectCollection<Annotation, ColumnPermission>
	{
		// Token: 0x06000334 RID: 820 RVA: 0x0001967F File Offset: 0x0001787F
		internal ColumnPermissionAnnotationCollection(ColumnPermission parent, IEqualityComparer<string> comparer)
			: base(ObjectType.Annotation, parent, comparer, true)
		{
		}
	}
}
