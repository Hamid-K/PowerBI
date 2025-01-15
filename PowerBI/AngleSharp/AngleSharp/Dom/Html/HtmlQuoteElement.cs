using System;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Extensions;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x02000385 RID: 901
	internal sealed class HtmlQuoteElement : HtmlElement, IHtmlQuoteElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x06001C4F RID: 7247 RVA: 0x0005425C File Offset: 0x0005245C
		public HtmlQuoteElement(Document owner, string name = null, string prefix = null)
			: base(owner, name ?? TagNames.Quote, prefix, name.Is(TagNames.BlockQuote) ? NodeFlags.Special : NodeFlags.None)
		{
		}

		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x06001C50 RID: 7248 RVA: 0x00053D77 File Offset: 0x00051F77
		// (set) Token: 0x06001C51 RID: 7249 RVA: 0x00053D84 File Offset: 0x00051F84
		public string Citation
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Cite);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Cite, value, false);
			}
		}
	}
}
