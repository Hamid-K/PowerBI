using System;
using System.Collections;
using System.Collections.Generic;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Css
{
	// Token: 0x0200032C RID: 812
	[DomName("CSSRuleList")]
	public interface ICssRuleList : IEnumerable<ICssRule>, IEnumerable
	{
		// Token: 0x1700061C RID: 1564
		[DomName("item")]
		ICssRule this[int index] { get; }

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06001730 RID: 5936
		[DomName("length")]
		int Length { get; }
	}
}
