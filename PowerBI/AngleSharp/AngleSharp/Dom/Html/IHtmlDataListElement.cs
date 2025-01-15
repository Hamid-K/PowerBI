using System;
using AngleSharp.Attributes;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003B8 RID: 952
	[DomName("HTMLDataListElement")]
	public interface IHtmlDataListElement : IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x06001E0D RID: 7693
		[DomName("options")]
		IHtmlCollection<IHtmlOptionElement> Options { get; }
	}
}
