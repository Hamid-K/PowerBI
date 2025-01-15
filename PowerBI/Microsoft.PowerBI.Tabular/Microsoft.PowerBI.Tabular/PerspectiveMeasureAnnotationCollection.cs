using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x0200009E RID: 158
	public sealed class PerspectiveMeasureAnnotationCollection : NamedMetadataObjectCollection<Annotation, PerspectiveMeasure>
	{
		// Token: 0x060009CB RID: 2507 RVA: 0x000523F6 File Offset: 0x000505F6
		internal PerspectiveMeasureAnnotationCollection(PerspectiveMeasure parent, IEqualityComparer<string> comparer)
			: base(ObjectType.Annotation, parent, comparer, true)
		{
		}
	}
}
