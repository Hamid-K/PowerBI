using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000CC RID: 204
	[CompatibilityRequirement(Pbi = "1200", Box = "1400", Excel = "1400")]
	public sealed class VariationCollection : NamedMetadataObjectCollection<Variation, Column>
	{
		// Token: 0x06000CF3 RID: 3315 RVA: 0x0006C394 File Offset: 0x0006A594
		internal VariationCollection(Column parent, IEqualityComparer<string> comparer)
			: base(ObjectType.Variation, parent, comparer, true)
		{
		}
	}
}
