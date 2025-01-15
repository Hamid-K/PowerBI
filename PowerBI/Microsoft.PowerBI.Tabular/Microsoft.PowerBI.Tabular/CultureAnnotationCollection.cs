using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x0200004C RID: 76
	public sealed class CultureAnnotationCollection : NamedMetadataObjectCollection<Annotation, Culture>
	{
		// Token: 0x06000382 RID: 898 RVA: 0x0001C14D File Offset: 0x0001A34D
		internal CultureAnnotationCollection(Culture parent, IEqualityComparer<string> comparer)
			: base(ObjectType.Annotation, parent, comparer, true)
		{
		}
	}
}
