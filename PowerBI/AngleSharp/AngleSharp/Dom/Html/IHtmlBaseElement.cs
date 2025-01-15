using System;
using AngleSharp.Attributes;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003B1 RID: 945
	[DomName("HTMLBaseElement")]
	public interface IHtmlBaseElement : IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x06001DDB RID: 7643
		// (set) Token: 0x06001DDC RID: 7644
		[DomName("href")]
		string Href { get; set; }

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x06001DDD RID: 7645
		// (set) Token: 0x06001DDE RID: 7646
		[DomName("target")]
		string Target { get; set; }
	}
}
