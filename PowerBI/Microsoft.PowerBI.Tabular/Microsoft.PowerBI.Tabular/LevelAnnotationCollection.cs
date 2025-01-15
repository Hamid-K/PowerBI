using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x0200006E RID: 110
	public sealed class LevelAnnotationCollection : NamedMetadataObjectCollection<Annotation, Level>
	{
		// Token: 0x06000615 RID: 1557 RVA: 0x0002F3BB File Offset: 0x0002D5BB
		internal LevelAnnotationCollection(Level parent, IEqualityComparer<string> comparer)
			: base(ObjectType.Annotation, parent, comparer, true)
		{
		}
	}
}
