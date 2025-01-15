using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000076 RID: 118
	public sealed class MeasureAnnotationCollection : NamedMetadataObjectCollection<Annotation, Measure>
	{
		// Token: 0x060006AF RID: 1711 RVA: 0x00034F07 File Offset: 0x00033107
		internal MeasureAnnotationCollection(Measure parent, IEqualityComparer<string> comparer)
			: base(ObjectType.Annotation, parent, comparer, true)
		{
		}
	}
}
