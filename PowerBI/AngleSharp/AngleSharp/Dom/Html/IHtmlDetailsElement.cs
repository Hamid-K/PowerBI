using System;
using AngleSharp.Attributes;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003B9 RID: 953
	[DomName("HTMLDetailsElement")]
	public interface IHtmlDetailsElement : IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x06001E0E RID: 7694
		// (set) Token: 0x06001E0F RID: 7695
		[DomName("open")]
		bool IsOpen { get; set; }
	}
}
