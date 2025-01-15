using System;
using AngleSharp.Attributes;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003F1 RID: 1009
	[DomName("HTMLTitleElement")]
	public interface IHtmlTitleElement : IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x0600201E RID: 8222
		// (set) Token: 0x0600201F RID: 8223
		[DomName("text")]
		string Text { get; set; }
	}
}
