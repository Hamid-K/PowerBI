using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000A6 RID: 166
	public sealed class PerspectiveTableAnnotationCollection : NamedMetadataObjectCollection<Annotation, PerspectiveTable>
	{
		// Token: 0x06000A40 RID: 2624 RVA: 0x000552CB File Offset: 0x000534CB
		internal PerspectiveTableAnnotationCollection(PerspectiveTable parent, IEqualityComparer<string> comparer)
			: base(ObjectType.Annotation, parent, comparer, true)
		{
		}
	}
}
