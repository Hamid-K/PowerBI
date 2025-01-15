using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Events
{
	// Token: 0x020001F7 RID: 503
	[DomName("TouchList")]
	public interface ITouchList
	{
		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06001110 RID: 4368
		[DomName("length")]
		int Length { get; }

		// Token: 0x170003BB RID: 955
		[DomAccessor(Accessors.Getter)]
		[DomName("item")]
		ITouchPoint this[int index] { get; }
	}
}
