using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x020000B2 RID: 178
	internal enum LimitOperatorType
	{
		// Token: 0x040001C0 RID: 448
		First,
		// Token: 0x040001C1 RID: 449
		Last,
		// Token: 0x040001C2 RID: 450
		Top,
		// Token: 0x040001C3 RID: 451
		Sample,
		// Token: 0x040001C4 RID: 452
		Bottom,
		// Token: 0x040001C5 RID: 453
		BinnedLineSample,
		// Token: 0x040001C6 RID: 454
		OverlappingPointsSample,
		// Token: 0x040001C7 RID: 455
		TopNPerLevel,
		// Token: 0x040001C8 RID: 456
		Window
	}
}
