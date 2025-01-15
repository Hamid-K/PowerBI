using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000029 RID: 41
	[CompatibilityRequirement("1460")]
	public sealed class AlternateOfAnnotationCollection : NamedMetadataObjectCollection<Annotation, AlternateOf>
	{
		// Token: 0x06000032 RID: 50 RVA: 0x00003339 File Offset: 0x00001539
		internal AlternateOfAnnotationCollection(AlternateOf parent, IEqualityComparer<string> comparer)
			: base(ObjectType.Annotation, parent, comparer, true)
		{
		}
	}
}
