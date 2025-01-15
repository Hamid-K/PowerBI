using System;
using AngleSharp.Attributes;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003D2 RID: 978
	[DomName("HTMLMetaElement")]
	public interface IHtmlMetaElement : IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x06001F37 RID: 7991
		// (set) Token: 0x06001F38 RID: 7992
		[DomName("name")]
		string Name { get; set; }

		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x06001F39 RID: 7993
		// (set) Token: 0x06001F3A RID: 7994
		[DomName("httpEquiv")]
		string HttpEquivalent { get; set; }

		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x06001F3B RID: 7995
		// (set) Token: 0x06001F3C RID: 7996
		[DomName("content")]
		string Content { get; set; }
	}
}
