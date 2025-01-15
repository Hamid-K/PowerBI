using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x0200009A RID: 154
	public sealed class PerspectiveHierarchyAnnotationCollection : NamedMetadataObjectCollection<Annotation, PerspectiveHierarchy>
	{
		// Token: 0x06000994 RID: 2452 RVA: 0x000512BE File Offset: 0x0004F4BE
		internal PerspectiveHierarchyAnnotationCollection(PerspectiveHierarchy parent, IEqualityComparer<string> comparer)
			: base(ObjectType.Annotation, parent, comparer, true)
		{
		}
	}
}
