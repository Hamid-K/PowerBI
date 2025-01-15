using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000064 RID: 100
	public sealed class HierarchyAnnotationCollection : NamedMetadataObjectCollection<Annotation, Hierarchy>
	{
		// Token: 0x06000581 RID: 1409 RVA: 0x0002AA3D File Offset: 0x00028C3D
		internal HierarchyAnnotationCollection(Hierarchy parent, IEqualityComparer<string> comparer)
			: base(ObjectType.Annotation, parent, comparer, true)
		{
		}
	}
}
