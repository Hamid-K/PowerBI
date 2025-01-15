using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000073 RID: 115
	public sealed class LinguisticMetadataAnnotationCollection : NamedMetadataObjectCollection<Annotation, LinguisticMetadata>
	{
		// Token: 0x06000657 RID: 1623 RVA: 0x00030B24 File Offset: 0x0002ED24
		internal LinguisticMetadataAnnotationCollection(LinguisticMetadata parent, IEqualityComparer<string> comparer)
			: base(ObjectType.Annotation, parent, comparer, true)
		{
		}
	}
}
