using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000B9 RID: 185
	[CompatibilityRequirement(Pbi = "1400")]
	public sealed class SetCollection : NamedMetadataObjectCollection<Set, Table>
	{
		// Token: 0x06000B7F RID: 2943 RVA: 0x0005E741 File Offset: 0x0005C941
		internal SetCollection(Table parent, IEqualityComparer<string> comparer)
			: base(ObjectType.Set, parent, comparer, true)
		{
		}
	}
}
