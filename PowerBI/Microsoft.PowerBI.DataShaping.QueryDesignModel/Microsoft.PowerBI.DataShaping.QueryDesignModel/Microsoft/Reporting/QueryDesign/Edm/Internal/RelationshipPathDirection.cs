using System;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x020001F6 RID: 502
	[Flags]
	internal enum RelationshipPathDirection
	{
		// Token: 0x04000CCD RID: 3277
		None = 0,
		// Token: 0x04000CCE RID: 3278
		Higher = 2,
		// Token: 0x04000CCF RID: 3279
		Lower = 4,
		// Token: 0x04000CD0 RID: 3280
		Current = 8,
		// Token: 0x04000CD1 RID: 3281
		CurrentOrLower = 12,
		// Token: 0x04000CD2 RID: 3282
		All = 14
	}
}
