using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x0200008E RID: 142
	public sealed class PartitionAnnotationCollection : NamedMetadataObjectCollection<Annotation, Partition>
	{
		// Token: 0x060008F8 RID: 2296 RVA: 0x0004DEE5 File Offset: 0x0004C0E5
		internal PartitionAnnotationCollection(Partition parent, IEqualityComparer<string> comparer)
			: base(ObjectType.Annotation, parent, comparer, true)
		{
		}
	}
}
