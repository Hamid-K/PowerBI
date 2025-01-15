using System;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Extensions;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x0200033E RID: 830
	internal sealed class HtmlAnchorElement : HtmlUrlBaseElement, IHtmlAnchorElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers, IUrlUtilities
	{
		// Token: 0x0600191F RID: 6431 RVA: 0x0004FC7B File Offset: 0x0004DE7B
		public HtmlAnchorElement(Document owner, string prefix = null)
			: base(owner, TagNames.A, prefix, NodeFlags.HtmlFormatting)
		{
		}

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x06001920 RID: 6432 RVA: 0x0004FC8F File Offset: 0x0004DE8F
		// (set) Token: 0x06001921 RID: 6433 RVA: 0x0004FC9C File Offset: 0x0004DE9C
		public string Charset
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Charset);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Charset, value, false);
			}
		}

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x06001922 RID: 6434 RVA: 0x0004FCAB File Offset: 0x0004DEAB
		// (set) Token: 0x06001923 RID: 6435 RVA: 0x0004FCB8 File Offset: 0x0004DEB8
		public string Name
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Name);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Name, value, false);
			}
		}

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x06001924 RID: 6436 RVA: 0x0004FCC7 File Offset: 0x0004DEC7
		// (set) Token: 0x06001925 RID: 6437 RVA: 0x0004FCCF File Offset: 0x0004DECF
		public string Text
		{
			get
			{
				return this.TextContent;
			}
			set
			{
				this.TextContent = value;
			}
		}

		// Token: 0x06001926 RID: 6438 RVA: 0x0004FCD8 File Offset: 0x0004DED8
		public override void DoFocus()
		{
			if (this.HasOwnAttribute(AttributeNames.Href))
			{
				base.IsFocused = true;
			}
		}
	}
}
