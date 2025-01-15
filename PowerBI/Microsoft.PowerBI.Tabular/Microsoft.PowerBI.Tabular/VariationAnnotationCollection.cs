using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000CB RID: 203
	[CompatibilityRequirement(Pbi = "1200", Box = "1400", Excel = "1400")]
	public sealed class VariationAnnotationCollection : NamedMetadataObjectCollection<Annotation, Variation>
	{
		// Token: 0x06000CF2 RID: 3314 RVA: 0x0006C387 File Offset: 0x0006A587
		internal VariationAnnotationCollection(Variation parent, IEqualityComparer<string> comparer)
			: base(ObjectType.Annotation, parent, comparer, true)
		{
		}
	}
}
