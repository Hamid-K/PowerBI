using System;
using AngleSharp.Attributes;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003E8 RID: 1000
	[DomName("HTMLTableColElement")]
	public interface IHtmlTableColumnElement : IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x06001FD7 RID: 8151
		// (set) Token: 0x06001FD8 RID: 8152
		[DomName("span")]
		int Span { get; set; }
	}
}
