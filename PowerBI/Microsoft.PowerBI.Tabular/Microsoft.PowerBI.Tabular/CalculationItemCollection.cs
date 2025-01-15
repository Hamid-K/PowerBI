using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x0200003C RID: 60
	[CompatibilityRequirement("1470")]
	public sealed class CalculationItemCollection : NamedMetadataObjectCollection<CalculationItem, CalculationGroup>
	{
		// Token: 0x060001BD RID: 445 RVA: 0x0000D2CB File Offset: 0x0000B4CB
		internal CalculationItemCollection(CalculationGroup parent, IEqualityComparer<string> comparer)
			: base(ObjectType.CalculationItem, parent, comparer, true)
		{
		}
	}
}
