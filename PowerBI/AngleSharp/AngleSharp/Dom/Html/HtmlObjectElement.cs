using System;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Extensions;
using AngleSharp.Html;
using AngleSharp.Network;
using AngleSharp.Network.RequestProcessors;

namespace AngleSharp.Dom.Html
{
	// Token: 0x0200037A RID: 890
	internal sealed class HtmlObjectElement : HtmlFormControlElement, IHtmlObjectElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers, IValidation, ILoadableElement
	{
		// Token: 0x06001BFF RID: 7167 RVA: 0x00053DF3 File Offset: 0x00051FF3
		public HtmlObjectElement(Document owner, string prefix = null)
			: base(owner, TagNames.Object, prefix, NodeFlags.Scoped)
		{
			this._request = ObjectRequestProcessor.Create(this);
		}

		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x06001C00 RID: 7168 RVA: 0x00053E10 File Offset: 0x00052010
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

		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x06001C01 RID: 7169 RVA: 0x00053E23 File Offset: 0x00052023
		// (set) Token: 0x06001C02 RID: 7170 RVA: 0x00053E30 File Offset: 0x00052030
		public string Source
		{
			get
			{
				return this.GetUrlAttribute(AttributeNames.Data);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Data, value, false);
			}
		}

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x06001C03 RID: 7171 RVA: 0x00051A27 File Offset: 0x0004FC27
		// (set) Token: 0x06001C04 RID: 7172 RVA: 0x0004FF58 File Offset: 0x0004E158
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

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x06001C05 RID: 7173 RVA: 0x00053E3F File Offset: 0x0005203F
		// (set) Token: 0x06001C06 RID: 7174 RVA: 0x00053E4C File Offset: 0x0005204C
		public bool TypeMustMatch
		{
			get
			{
				return this.GetBoolAttribute(AttributeNames.TypeMustMatch);
			}
			set
			{
				this.SetBoolAttribute(AttributeNames.TypeMustMatch, value);
			}
		}

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x06001C07 RID: 7175 RVA: 0x000528FA File Offset: 0x00050AFA
		// (set) Token: 0x06001C08 RID: 7176 RVA: 0x00052907 File Offset: 0x00050B07
		public string UseMap
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.UseMap);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.UseMap, value, false);
			}
		}

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x06001C09 RID: 7177 RVA: 0x00053E5A File Offset: 0x0005205A
		// (set) Token: 0x06001C0A RID: 7178 RVA: 0x000501DB File Offset: 0x0004E3DB
		public int DisplayWidth
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Width).ToInteger(this.OriginalWidth);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Width, value.ToString(), false);
			}
		}

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x06001C0B RID: 7179 RVA: 0x00053E72 File Offset: 0x00052072
		// (set) Token: 0x06001C0C RID: 7180 RVA: 0x00050207 File Offset: 0x0004E407
		public int DisplayHeight
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Height).ToInteger(this.OriginalHeight);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Height, value.ToString(), false);
			}
		}

		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x06001C0D RID: 7181 RVA: 0x00053E8A File Offset: 0x0005208A
		public int OriginalWidth
		{
			get
			{
				ObjectRequestProcessor request = this._request;
				if (request == null)
				{
					return 0;
				}
				return request.Width;
			}
		}

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x06001C0E RID: 7182 RVA: 0x00053E9D File Offset: 0x0005209D
		public int OriginalHeight
		{
			get
			{
				ObjectRequestProcessor request = this._request;
				if (request == null)
				{
					return 0;
				}
				return request.Height;
			}
		}

		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x06001C0F RID: 7183 RVA: 0x0000C295 File Offset: 0x0000A495
		public IDocument ContentDocument
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x06001C10 RID: 7184 RVA: 0x0000C295 File Offset: 0x0000A495
		public IWindow ContentWindow
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06001C11 RID: 7185 RVA: 0x0000EE9F File Offset: 0x0000D09F
		protected override bool CanBeValidated()
		{
			return false;
		}

		// Token: 0x06001C12 RID: 7186 RVA: 0x00053EB0 File Offset: 0x000520B0
		internal override void SetupElement()
		{
			base.SetupElement();
			string ownAttribute = this.GetOwnAttribute(AttributeNames.Data);
			if (ownAttribute != null)
			{
				this.UpdateSource(ownAttribute);
			}
		}

		// Token: 0x06001C13 RID: 7187 RVA: 0x00053EDC File Offset: 0x000520DC
		internal void UpdateSource(string value)
		{
			Url url = new Url(this.Source);
			this.Process(this._request, url);
		}

		// Token: 0x04000CEF RID: 3311
		private readonly ObjectRequestProcessor _request;
	}
}
