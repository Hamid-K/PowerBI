using System;

namespace Microsoft.AnalysisServices.Tabular.Metadata
{
	// Token: 0x02000205 RID: 517
	[Flags]
	internal enum RefreshTypeMask
	{
		// Token: 0x040006B6 RID: 1718
		None = 0,
		// Token: 0x040006B7 RID: 1719
		Full = 1,
		// Token: 0x040006B8 RID: 1720
		ClearValues = 2,
		// Token: 0x040006B9 RID: 1721
		Calculate = 4,
		// Token: 0x040006BA RID: 1722
		DataOnly = 8,
		// Token: 0x040006BB RID: 1723
		Automatic = 16,
		// Token: 0x040006BC RID: 1724
		Add = 32,
		// Token: 0x040006BD RID: 1725
		Defragment = 64
	}
}
