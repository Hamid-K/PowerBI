using System;
using AngleSharp.Attributes;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003EB RID: 1003
	[DomName("HTMLTableHeaderCellElement")]
	public interface IHtmlTableHeaderCellElement : IHtmlTableCellElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x06001FEC RID: 8172
		// (set) Token: 0x06001FED RID: 8173
		[DomName("scope")]
		string Scope { get; set; }
	}
}
