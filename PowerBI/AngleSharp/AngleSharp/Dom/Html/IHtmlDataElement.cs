using System;
using AngleSharp.Attributes;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003B7 RID: 951
	[DomName("HTMLDataElement")]
	public interface IHtmlDataElement : IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x06001E0B RID: 7691
		// (set) Token: 0x06001E0C RID: 7692
		[DomName("value")]
		string Value { get; set; }
	}
}
