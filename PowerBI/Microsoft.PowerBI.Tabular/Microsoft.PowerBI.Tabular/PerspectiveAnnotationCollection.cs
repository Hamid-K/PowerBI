using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000092 RID: 146
	public sealed class PerspectiveAnnotationCollection : NamedMetadataObjectCollection<Annotation, Perspective>
	{
		// Token: 0x06000927 RID: 2343 RVA: 0x0004F045 File Offset: 0x0004D245
		internal PerspectiveAnnotationCollection(Perspective parent, IEqualityComparer<string> comparer)
			: base(ObjectType.Annotation, parent, comparer, true)
		{
		}
	}
}
