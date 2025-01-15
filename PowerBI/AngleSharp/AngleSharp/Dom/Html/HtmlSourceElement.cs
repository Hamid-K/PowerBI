using System;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Extensions;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x02000390 RID: 912
	internal sealed class HtmlSourceElement : HtmlElement, IHtmlSourceElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x06001C91 RID: 7313 RVA: 0x000549E5 File Offset: 0x00052BE5
		public HtmlSourceElement(Document owner, string prefix = null)
			: base(owner, TagNames.Source, prefix, NodeFlags.SelfClosing | NodeFlags.Special)
		{
		}

		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x06001C92 RID: 7314 RVA: 0x000524DE File Offset: 0x000506DE
		// (set) Token: 0x06001C93 RID: 7315 RVA: 0x00051A18 File Offset: 0x0004FC18
		public string Source
		{
			get
			{
				return this.GetUrlAttribute(AttributeNames.Src);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Src, value, false);
			}
		}

		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x06001C94 RID: 7316 RVA: 0x0005326D File Offset: 0x0005146D
		// (set) Token: 0x06001C95 RID: 7317 RVA: 0x0005327A File Offset: 0x0005147A
		public string Media
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Media);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Media, value, false);
			}
		}

		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x06001C96 RID: 7318 RVA: 0x00051A27 File Offset: 0x0004FC27
		// (set) Token: 0x06001C97 RID: 7319 RVA: 0x0004FF58 File Offset: 0x0004E158
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

		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x06001C98 RID: 7320 RVA: 0x000528A6 File Offset: 0x00050AA6
		// (set) Token: 0x06001C99 RID: 7321 RVA: 0x000528B3 File Offset: 0x00050AB3
		public string SourceSet
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.SrcSet);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.SrcSet, value, false);
			}
		}

		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x06001C9A RID: 7322 RVA: 0x000528C2 File Offset: 0x00050AC2
		// (set) Token: 0x06001C9B RID: 7323 RVA: 0x000528CF File Offset: 0x00050ACF
		public string Sizes
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Sizes);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Sizes, value, false);
			}
		}
	}
}
