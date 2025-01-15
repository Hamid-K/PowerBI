using System;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x02000360 RID: 864
	internal sealed class HtmlHeadElement : HtmlElement, IHtmlHeadElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x06001ABF RID: 6847 RVA: 0x000526D9 File Offset: 0x000508D9
		public HtmlHeadElement(Document owner, string prefix = null)
			: base(owner, TagNames.Head, prefix, NodeFlags.Special)
		{
		}
	}
}
