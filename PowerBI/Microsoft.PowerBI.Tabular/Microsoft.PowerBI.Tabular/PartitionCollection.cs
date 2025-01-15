using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x0200008F RID: 143
	public sealed class PartitionCollection : NamedMetadataObjectCollection<Partition, Table>
	{
		// Token: 0x060008F9 RID: 2297 RVA: 0x0004DEF2 File Offset: 0x0004C0F2
		internal PartitionCollection(Table parent, IEqualityComparer<string> comparer)
			: base(ObjectType.Partition, parent, comparer, true)
		{
		}
	}
}
