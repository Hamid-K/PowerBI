using System;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Extensions;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x02000396 RID: 918
	internal sealed class HtmlTableCaptionElement : HtmlElement, IHtmlTableCaptionElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x06001CB1 RID: 7345 RVA: 0x00054BA2 File Offset: 0x00052DA2
		public HtmlTableCaptionElement(Document owner, string prefix = null)
			: base(owner, TagNames.Caption, prefix, NodeFlags.Special | NodeFlags.Scoped)
		{
		}

		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x06001CB2 RID: 7346 RVA: 0x00054BB3 File Offset: 0x00052DB3
		// (set) Token: 0x06001CB3 RID: 7347 RVA: 0x00054BC9 File Offset: 0x00052DC9
		public string Align
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Align) ?? Keywords.Top;
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Align, value.ToString(), false);
			}
		}
	}
}
