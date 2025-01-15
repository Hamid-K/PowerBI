using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Css
{
	// Token: 0x0200032A RID: 810
	[DomName("CSSRule")]
	public interface ICssRule : ICssNode, IStyleFormattable
	{
		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x06001729 RID: 5929
		[DomName("type")]
		CssRuleType Type { get; }

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x0600172A RID: 5930
		// (set) Token: 0x0600172B RID: 5931
		[DomName("cssText")]
		string CssText { get; set; }

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x0600172C RID: 5932
		[DomName("parentRule")]
		ICssRule Parent { get; }

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x0600172D RID: 5933
		[DomName("parentStyleSheet")]
		ICssStyleSheet Owner { get; }
	}
}
