using System;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x02000391 RID: 913
	internal sealed class HtmlSpanElement : HtmlElement, IHtmlSpanElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x06001C9C RID: 7324 RVA: 0x000549F5 File Offset: 0x00052BF5
		public HtmlSpanElement(Document owner, string prefix = null)
			: base(owner, TagNames.Span, prefix, NodeFlags.None)
		{
		}
	}
}
