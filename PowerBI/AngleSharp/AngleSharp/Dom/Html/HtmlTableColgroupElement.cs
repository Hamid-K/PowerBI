using System;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Extensions;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x02000399 RID: 921
	internal sealed class HtmlTableColgroupElement : HtmlElement, IHtmlTableColumnElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x06001CDA RID: 7386 RVA: 0x00054DF7 File Offset: 0x00052FF7
		public HtmlTableColgroupElement(Document owner, string prefix = null)
			: base(owner, TagNames.Colgroup, prefix, NodeFlags.Special)
		{
		}

		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x06001CDB RID: 7387 RVA: 0x00054DBC File Offset: 0x00052FBC
		// (set) Token: 0x06001CDC RID: 7388 RVA: 0x00054155 File Offset: 0x00052355
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

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x06001CDD RID: 7389 RVA: 0x00054DCF File Offset: 0x00052FCF
		// (set) Token: 0x06001CDE RID: 7390 RVA: 0x00054DE2 File Offset: 0x00052FE2
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

		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x06001CDF RID: 7391 RVA: 0x00054C26 File Offset: 0x00052E26
		// (set) Token: 0x06001CE0 RID: 7392 RVA: 0x00054C39 File Offset: 0x00052E39
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

		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x06001CE1 RID: 7393 RVA: 0x00051A34 File Offset: 0x0004FC34
		// (set) Token: 0x06001CE2 RID: 7394 RVA: 0x00051A41 File Offset: 0x0004FC41
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
