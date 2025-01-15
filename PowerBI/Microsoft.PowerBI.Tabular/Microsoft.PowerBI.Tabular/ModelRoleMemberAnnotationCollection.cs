using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000083 RID: 131
	public sealed class ModelRoleMemberAnnotationCollection : NamedMetadataObjectCollection<Annotation, ModelRoleMember>
	{
		// Token: 0x060007E1 RID: 2017 RVA: 0x00042F4C File Offset: 0x0004114C
		internal ModelRoleMemberAnnotationCollection(ModelRoleMember parent, IEqualityComparer<string> comparer)
			: base(ObjectType.Annotation, parent, comparer, true)
		{
		}
	}
}
