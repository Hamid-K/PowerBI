using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000BF RID: 191
	public sealed class TableAnnotationCollection : NamedMetadataObjectCollection<Annotation, Table>
	{
		// Token: 0x06000C2B RID: 3115 RVA: 0x00066F54 File Offset: 0x00065154
		internal TableAnnotationCollection(Table parent, IEqualityComparer<string> comparer)
			: base(ObjectType.Annotation, parent, comparer, true)
		{
		}
	}
}
