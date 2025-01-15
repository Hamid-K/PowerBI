using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000332 RID: 818
	[DomName("ElementCSSInlineStyle")]
	[DomNoInterfaceObject]
	public interface IElementCssInlineStyle
	{
		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x06001903 RID: 6403
		[DomName("style")]
		[DomPutForwards("cssText")]
		ICssStyleDeclaration Style { get; }
	}
}
