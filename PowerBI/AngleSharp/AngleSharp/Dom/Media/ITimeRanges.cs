using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Media
{
	// Token: 0x020001D6 RID: 470
	[DomName("TimeRanges")]
	public interface ITimeRanges
	{
		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000FC4 RID: 4036
		[DomName("length")]
		int Length { get; }

		// Token: 0x06000FC5 RID: 4037
		[DomName("start")]
		double Start(int index);

		// Token: 0x06000FC6 RID: 4038
		[DomName("end")]
		double End(int index);
	}
}
