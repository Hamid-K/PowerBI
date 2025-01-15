using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x0200007B RID: 123
	public sealed class ModelAnnotationCollection : NamedMetadataObjectCollection<Annotation, Model>
	{
		// Token: 0x06000773 RID: 1907 RVA: 0x00040470 File Offset: 0x0003E670
		internal ModelAnnotationCollection(Model parent, IEqualityComparer<string> comparer)
			: base(ObjectType.Annotation, parent, comparer, true)
		{
		}
	}
}
