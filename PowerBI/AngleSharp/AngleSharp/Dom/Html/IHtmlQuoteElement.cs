using System;
using AngleSharp.Attributes;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003DF RID: 991
	[DomName("HTMLQuoteElement")]
	public interface IHtmlQuoteElement : IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x06001F90 RID: 8080
		// (set) Token: 0x06001F91 RID: 8081
		[DomName("cite")]
		string Citation { get; set; }
	}
}
