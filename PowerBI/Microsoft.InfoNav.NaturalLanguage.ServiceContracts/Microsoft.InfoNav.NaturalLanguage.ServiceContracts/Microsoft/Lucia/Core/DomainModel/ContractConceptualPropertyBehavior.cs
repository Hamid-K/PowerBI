using System;

namespace Microsoft.Lucia.Core.DomainModel
{
	// Token: 0x02000176 RID: 374
	[Flags]
	internal enum ContractConceptualPropertyBehavior
	{
		// Token: 0x040006C1 RID: 1729
		None = 0,
		// Token: 0x040006C2 RID: 1730
		Field = 1,
		// Token: 0x040006C3 RID: 1731
		Measure = 2,
		// Token: 0x040006C4 RID: 1732
		Hidden = 4,
		// Token: 0x040006C5 RID: 1733
		Kpi = 8
	}
}
