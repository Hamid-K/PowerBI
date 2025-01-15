using System;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x02000348 RID: 840
	internal sealed class HtmlBreakRowElement : HtmlElement, IHtmlBreakRowElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x06001961 RID: 6497 RVA: 0x0004FF1D File Offset: 0x0004E11D
		public HtmlBreakRowElement(Document owner, string prefix = null)
			: base(owner, TagNames.Br, prefix, NodeFlags.SelfClosing | NodeFlags.Special)
		{
		}
	}
}
