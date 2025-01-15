using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x0200006B RID: 107
	public sealed class KPIAnnotationCollection : NamedMetadataObjectCollection<Annotation, KPI>
	{
		// Token: 0x060005D7 RID: 1495 RVA: 0x0002D199 File Offset: 0x0002B399
		internal KPIAnnotationCollection(KPI parent, IEqualityComparer<string> comparer)
			: base(ObjectType.Annotation, parent, comparer, true)
		{
		}
	}
}
