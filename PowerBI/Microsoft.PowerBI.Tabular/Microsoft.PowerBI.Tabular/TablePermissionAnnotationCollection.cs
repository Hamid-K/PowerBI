using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000C5 RID: 197
	public sealed class TablePermissionAnnotationCollection : NamedMetadataObjectCollection<Annotation, TablePermission>
	{
		// Token: 0x06000C7A RID: 3194 RVA: 0x0006926C File Offset: 0x0006746C
		internal TablePermissionAnnotationCollection(TablePermission parent, IEqualityComparer<string> comparer)
			: base(ObjectType.Annotation, parent, comparer, true)
		{
		}
	}
}
