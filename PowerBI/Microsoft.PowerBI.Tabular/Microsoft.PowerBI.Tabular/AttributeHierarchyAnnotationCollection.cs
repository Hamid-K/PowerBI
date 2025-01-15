using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x0200002E RID: 46
	public sealed class AttributeHierarchyAnnotationCollection : NamedMetadataObjectCollection<Annotation, AttributeHierarchy>
	{
		// Token: 0x060000A6 RID: 166 RVA: 0x00005D8D File Offset: 0x00003F8D
		internal AttributeHierarchyAnnotationCollection(AttributeHierarchy parent, IEqualityComparer<string> comparer)
			: base(ObjectType.Annotation, parent, comparer, true)
		{
		}
	}
}
