using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom
{
	// Token: 0x02000172 RID: 370
	[DomName("Range")]
	public enum RangeType : byte
	{
		// Token: 0x040009DC RID: 2524
		[DomName("START_TO_START")]
		StartToStart,
		// Token: 0x040009DD RID: 2525
		[DomName("START_TO_END")]
		StartToEnd,
		// Token: 0x040009DE RID: 2526
		[DomName("END_TO_END")]
		EndToEnd,
		// Token: 0x040009DF RID: 2527
		[DomName("END_TO_START")]
		EndToStart
	}
}
