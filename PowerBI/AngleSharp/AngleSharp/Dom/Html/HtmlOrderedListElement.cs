using System;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Extensions;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x0200037D RID: 893
	internal sealed class HtmlOrderedListElement : HtmlElement, IHtmlOrderedListElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x06001C28 RID: 7208 RVA: 0x00054013 File Offset: 0x00052213
		public HtmlOrderedListElement(Document owner, string prefix = null)
			: base(owner, TagNames.Ol, prefix, NodeFlags.Special | NodeFlags.HtmlListScoped)
		{
		}

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x06001C29 RID: 7209 RVA: 0x00054027 File Offset: 0x00052227
		// (set) Token: 0x06001C2A RID: 7210 RVA: 0x00054034 File Offset: 0x00052234
		public bool IsReversed
		{
			get
			{
				return this.GetBoolAttribute(AttributeNames.Reversed);
			}
			set
			{
				this.SetBoolAttribute(AttributeNames.Reversed, value);
			}
		}

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x06001C2B RID: 7211 RVA: 0x00054042 File Offset: 0x00052242
		// (set) Token: 0x06001C2C RID: 7212 RVA: 0x00054055 File Offset: 0x00052255
		public int Start
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Start).ToInteger(1);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Start, value.ToString(), false);
			}
		}

		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x06001C2D RID: 7213 RVA: 0x00051A27 File Offset: 0x0004FC27
		// (set) Token: 0x06001C2E RID: 7214 RVA: 0x0004FF58 File Offset: 0x0004E158
		public string Type
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Type);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Type, value, false);
			}
		}
	}
}
