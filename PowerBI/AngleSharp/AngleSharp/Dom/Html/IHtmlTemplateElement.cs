using System;
using AngleSharp.Attributes;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003EE RID: 1006
	[DomName("HTMLTemplateElement")]
	public interface IHtmlTemplateElement : IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x06001FF6 RID: 8182
		[DomName("content")]
		IDocumentFragment Content { get; }
	}
}
