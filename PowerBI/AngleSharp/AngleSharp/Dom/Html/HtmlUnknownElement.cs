using System;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003A7 RID: 935
	internal sealed class HtmlUnknownElement : HtmlElement, IHtmlUnknownElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x06001D69 RID: 7529 RVA: 0x000559C2 File Offset: 0x00053BC2
		public HtmlUnknownElement(Document owner, string localName, string prefix = null)
			: base(owner, localName, prefix, NodeFlags.None)
		{
		}
	}
}
