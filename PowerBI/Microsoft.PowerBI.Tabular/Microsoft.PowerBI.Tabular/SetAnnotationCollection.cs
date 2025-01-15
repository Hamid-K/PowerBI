using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000B8 RID: 184
	[CompatibilityRequirement(Pbi = "1400")]
	public sealed class SetAnnotationCollection : NamedMetadataObjectCollection<Annotation, Set>
	{
		// Token: 0x06000B7E RID: 2942 RVA: 0x0005E734 File Offset: 0x0005C934
		internal SetAnnotationCollection(Set parent, IEqualityComparer<string> comparer)
			: base(ObjectType.Annotation, parent, comparer, true)
		{
		}
	}
}
