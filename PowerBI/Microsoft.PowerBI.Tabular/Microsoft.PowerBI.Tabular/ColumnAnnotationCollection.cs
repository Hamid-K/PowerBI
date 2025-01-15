using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000043 RID: 67
	public sealed class ColumnAnnotationCollection : NamedMetadataObjectCollection<Annotation, Column>
	{
		// Token: 0x060002ED RID: 749 RVA: 0x00017958 File Offset: 0x00015B58
		internal ColumnAnnotationCollection(Column parent, IEqualityComparer<string> comparer)
			: base(ObjectType.Annotation, parent, comparer, true)
		{
		}
	}
}
