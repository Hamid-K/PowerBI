using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x0200007F RID: 127
	public sealed class ModelRoleAnnotationCollection : NamedMetadataObjectCollection<Annotation, ModelRole>
	{
		// Token: 0x060007A6 RID: 1958 RVA: 0x00041AA5 File Offset: 0x0003FCA5
		internal ModelRoleAnnotationCollection(ModelRole parent, IEqualityComparer<string> comparer)
			: base(ObjectType.Annotation, parent, comparer, true)
		{
		}
	}
}
