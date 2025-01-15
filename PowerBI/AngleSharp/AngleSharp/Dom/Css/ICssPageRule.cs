using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000327 RID: 807
	[DomName("CSSPageRule")]
	public interface ICssPageRule : ICssRule, ICssNode, IStyleFormattable
	{
		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x0600171D RID: 5917
		// (set) Token: 0x0600171E RID: 5918
		[DomName("selectorText")]
		string SelectorText { get; set; }

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x0600171F RID: 5919
		[DomName("style")]
		[DomPutForwards("cssText")]
		ICssStyleDeclaration Style { get; }
	}
}
