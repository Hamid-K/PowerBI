using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000A2 RID: 162
	[CompatibilityRequirement(Pbi = "1400")]
	public sealed class PerspectiveSetAnnotationCollection : NamedMetadataObjectCollection<Annotation, PerspectiveSet>
	{
		// Token: 0x06000A03 RID: 2563 RVA: 0x0005366E File Offset: 0x0005186E
		internal PerspectiveSetAnnotationCollection(PerspectiveSet parent, IEqualityComparer<string> comparer)
			: base(ObjectType.Annotation, parent, comparer, true)
		{
		}
	}
}
