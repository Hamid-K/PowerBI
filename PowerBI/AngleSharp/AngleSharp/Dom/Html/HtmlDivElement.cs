using System;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x02000352 RID: 850
	internal sealed class HtmlDivElement : HtmlElement, IHtmlDivElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x06001997 RID: 6551 RVA: 0x000504DD File Offset: 0x0004E6DD
		public HtmlDivElement(Document owner, string prefix = null)
			: base(owner, TagNames.Div, prefix, NodeFlags.Special)
		{
		}
	}
}
