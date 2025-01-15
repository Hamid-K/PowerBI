using System;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x0200039C RID: 924
	internal sealed class HtmlTableHeaderCellElement : HtmlTableCellElement, IHtmlTableHeaderCellElement, IHtmlTableCellElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x06001D09 RID: 7433 RVA: 0x00055281 File Offset: 0x00053481
		public HtmlTableHeaderCellElement(Document owner, string prefix = null)
			: base(owner, TagNames.Th, prefix)
		{
		}
	}
}
