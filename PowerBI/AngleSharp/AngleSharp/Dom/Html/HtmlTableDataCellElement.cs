using System;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x0200039A RID: 922
	internal sealed class HtmlTableDataCellElement : HtmlTableCellElement, IHtmlTableDataCellElement, IHtmlTableCellElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x06001CE3 RID: 7395 RVA: 0x00054E07 File Offset: 0x00053007
		public HtmlTableDataCellElement(Document owner, string prefix = null)
			: base(owner, TagNames.Td, prefix)
		{
		}
	}
}
