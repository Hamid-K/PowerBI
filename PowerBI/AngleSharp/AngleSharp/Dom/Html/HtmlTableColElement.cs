using System;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Extensions;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x02000398 RID: 920
	internal sealed class HtmlTableColElement : HtmlElement, IHtmlTableColumnElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x06001CD1 RID: 7377 RVA: 0x00054DAC File Offset: 0x00052FAC
		public HtmlTableColElement(Document owner, string prefix = null)
			: base(owner, TagNames.Col, prefix, NodeFlags.SelfClosing | NodeFlags.Special)
		{
		}

		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x06001CD2 RID: 7378 RVA: 0x00054DBC File Offset: 0x00052FBC
		// (set) Token: 0x06001CD3 RID: 7379 RVA: 0x00054155 File Offset: 0x00052355
		public HorizontalAlignment Align
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Align).ToEnum(HorizontalAlignment.Center);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Align, value.ToString(), false);
			}
		}

		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x06001CD4 RID: 7380 RVA: 0x00054DCF File Offset: 0x00052FCF
		// (set) Token: 0x06001CD5 RID: 7381 RVA: 0x00054DE2 File Offset: 0x00052FE2
		public int Span
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Span).ToInteger(0);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Span, value.ToString(), false);
			}
		}

		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x06001CD6 RID: 7382 RVA: 0x00054C26 File Offset: 0x00052E26
		// (set) Token: 0x06001CD7 RID: 7383 RVA: 0x00054C39 File Offset: 0x00052E39
		public VerticalAlignment VAlign
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Valign).ToEnum(VerticalAlignment.Middle);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Valign, value.ToString(), false);
			}
		}

		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x06001CD8 RID: 7384 RVA: 0x00051A34 File Offset: 0x0004FC34
		// (set) Token: 0x06001CD9 RID: 7385 RVA: 0x00051A41 File Offset: 0x0004FC41
		public string Width
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Width);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Width, value, false);
			}
		}
	}
}
