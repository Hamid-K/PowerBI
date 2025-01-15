using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000095 RID: 149
	public sealed class PerspectiveColumnAnnotationCollection : NamedMetadataObjectCollection<Annotation, PerspectiveColumn>
	{
		// Token: 0x0600095B RID: 2395 RVA: 0x00050146 File Offset: 0x0004E346
		internal PerspectiveColumnAnnotationCollection(PerspectiveColumn parent, IEqualityComparer<string> comparer)
			: base(ObjectType.Annotation, parent, comparer, true)
		{
		}
	}
}
