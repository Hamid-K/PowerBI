using System;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Extensions;
using AngleSharp.Html;
using AngleSharp.Network;
using AngleSharp.Network.RequestProcessors;

namespace AngleSharp.Dom.Html
{
	// Token: 0x02000355 RID: 853
	internal sealed class HtmlEmbedElement : HtmlElement, IHtmlEmbedElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers, ILoadableElement
	{
		// Token: 0x06001A43 RID: 6723 RVA: 0x000519DC File Offset: 0x0004FBDC
		public HtmlEmbedElement(Document owner, string prefix = null)
			: base(owner, TagNames.Embed, prefix, NodeFlags.SelfClosing | NodeFlags.Special)
		{
			this._request = ObjectRequestProcessor.Create(this);
		}

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06001A44 RID: 6724 RVA: 0x000519F8 File Offset: 0x0004FBF8
		public IDownload CurrentDownload
		{
			get
			{
				ObjectRequestProcessor request = this._request;
				if (request == null)
				{
					return null;
				}
				return request.Download;
			}
		}

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x06001A45 RID: 6725 RVA: 0x00051A0B File Offset: 0x0004FC0B
		// (set) Token: 0x06001A46 RID: 6726 RVA: 0x00051A18 File Offset: 0x0004FC18
		public string Source
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Src);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Src, value, false);
			}
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x06001A47 RID: 6727 RVA: 0x00051A27 File Offset: 0x0004FC27
		// (set) Token: 0x06001A48 RID: 6728 RVA: 0x0004FF58 File Offset: 0x0004E158
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

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x06001A49 RID: 6729 RVA: 0x00051A34 File Offset: 0x0004FC34
		// (set) Token: 0x06001A4A RID: 6730 RVA: 0x00051A41 File Offset: 0x0004FC41
		public string DisplayWidth
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

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x06001A4B RID: 6731 RVA: 0x00051A50 File Offset: 0x0004FC50
		// (set) Token: 0x06001A4C RID: 6732 RVA: 0x00051A5D File Offset: 0x0004FC5D
		public string DisplayHeight
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Height);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Height, value, false);
			}
		}

		// Token: 0x06001A4D RID: 6733 RVA: 0x00051A6C File Offset: 0x0004FC6C
		internal override void SetupElement()
		{
			base.SetupElement();
			string ownAttribute = this.GetOwnAttribute(AttributeNames.Src);
			if (ownAttribute != null)
			{
				this.UpdateSource(ownAttribute);
			}
		}

		// Token: 0x06001A4E RID: 6734 RVA: 0x00051A98 File Offset: 0x0004FC98
		internal void UpdateSource(string value)
		{
			Url url = new Url(this.Source);
			this.Process(this._request, url);
		}

		// Token: 0x04000CCA RID: 3274
		private readonly ObjectRequestProcessor _request;
	}
}
