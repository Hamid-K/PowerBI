using System;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003A8 RID: 936
	internal sealed class HtmlUnorderedListElement : HtmlElement, IHtmlUnorderedListElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x06001D6A RID: 7530 RVA: 0x000559CE File Offset: 0x00053BCE
		public HtmlUnorderedListElement(Document owner, string prefix = null)
			: base(owner, TagNames.Ul, prefix, NodeFlags.Special | NodeFlags.HtmlListScoped)
		{
		}
	}
}
