using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Css
{
	// Token: 0x0200032E RID: 814
	[DomName("CSSStyleRule")]
	public interface ICssStyleRule : ICssRule, ICssNode, IStyleFormattable
	{
		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x060018F5 RID: 6389
		// (set) Token: 0x060018F6 RID: 6390
		[DomName("selectorText")]
		string SelectorText { get; set; }

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x060018F7 RID: 6391
		[DomName("style")]
		[DomPutForwards("cssText")]
		ICssStyleDeclaration Style { get; }

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x060018F8 RID: 6392
		// (set) Token: 0x060018F9 RID: 6393
		ISelector Selector { get; set; }
	}
}
