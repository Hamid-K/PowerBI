using System;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Extensions;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x02000342 RID: 834
	internal sealed class HtmlBaseElement : HtmlElement, IHtmlBaseElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x06001931 RID: 6449 RVA: 0x0004FD81 File Offset: 0x0004DF81
		public HtmlBaseElement(Document owner, string prefix = null)
			: base(owner, TagNames.Base, prefix, NodeFlags.SelfClosing | NodeFlags.Special)
		{
		}

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x06001932 RID: 6450 RVA: 0x0004FD91 File Offset: 0x0004DF91
		// (set) Token: 0x06001933 RID: 6451 RVA: 0x0004FD9E File Offset: 0x0004DF9E
		public string Href
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Href);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Href, value, false);
			}
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06001934 RID: 6452 RVA: 0x0004FDAD File Offset: 0x0004DFAD
		// (set) Token: 0x06001935 RID: 6453 RVA: 0x0004FDBA File Offset: 0x0004DFBA
		public string Target
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Target);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Target, value, false);
			}
		}

		// Token: 0x06001936 RID: 6454 RVA: 0x0004FDCC File Offset: 0x0004DFCC
		internal override void SetupElement()
		{
			base.SetupElement();
			string ownAttribute = this.GetOwnAttribute(AttributeNames.Href);
			if (ownAttribute != null)
			{
				this.UpdateUrl(ownAttribute);
			}
		}

		// Token: 0x06001937 RID: 6455 RVA: 0x0004FDF5 File Offset: 0x0004DFF5
		internal void UpdateUrl(string url)
		{
			base.Owner.BaseUrl = new Url(base.Owner.DocumentUrl, url ?? string.Empty);
		}
	}
}
