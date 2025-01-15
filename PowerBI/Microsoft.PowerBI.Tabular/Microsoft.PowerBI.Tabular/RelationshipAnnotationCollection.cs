using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000B2 RID: 178
	public sealed class RelationshipAnnotationCollection : NamedMetadataObjectCollection<Annotation, Relationship>
	{
		// Token: 0x06000B38 RID: 2872 RVA: 0x0005C3EC File Offset: 0x0005A5EC
		internal RelationshipAnnotationCollection(Relationship parent, IEqualityComparer<string> comparer)
			: base(ObjectType.Annotation, parent, comparer, true)
		{
		}
	}
}
