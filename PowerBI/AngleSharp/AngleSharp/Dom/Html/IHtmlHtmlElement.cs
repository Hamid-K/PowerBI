using System;
using AngleSharp.Attributes;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003C5 RID: 965
	[DomName("HTMLHtmlElement")]
	public interface IHtmlHtmlElement : IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x06001E64 RID: 7780
		// (set) Token: 0x06001E65 RID: 7781
		[DomName("manifest")]
		string Manifest { get; set; }
	}
}
