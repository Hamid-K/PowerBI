using System;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Extensions;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x0200037F RID: 895
	internal sealed class HtmlParagraphElement : HtmlElement, IHtmlParagraphElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x06001C3C RID: 7228 RVA: 0x00054131 File Offset: 0x00052331
		public HtmlParagraphElement(Document owner, string prefix = null)
			: base(owner, TagNames.P, prefix, NodeFlags.Special | NodeFlags.ImplicitelyClosed | NodeFlags.ImpliedEnd)
		{
		}

		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x06001C3D RID: 7229 RVA: 0x00054142 File Offset: 0x00052342
		// (set) Token: 0x06001C3E RID: 7230 RVA: 0x00054155 File Offset: 0x00052355
		public HorizontalAlignment Align
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Align).ToEnum(HorizontalAlignment.Left);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Align, value.ToString(), false);
			}
		}
	}
}
