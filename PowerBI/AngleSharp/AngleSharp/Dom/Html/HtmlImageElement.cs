using System;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Extensions;
using AngleSharp.Html;
using AngleSharp.Network;
using AngleSharp.Network.RequestProcessors;

namespace AngleSharp.Dom.Html
{
	// Token: 0x02000365 RID: 869
	internal sealed class HtmlImageElement : HtmlElement, IHtmlImageElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers, ILoadableElement
	{
		// Token: 0x06001AD4 RID: 6868 RVA: 0x0005285C File Offset: 0x00050A5C
		public HtmlImageElement(Document owner, string prefix = null)
			: base(owner, TagNames.Img, prefix, NodeFlags.SelfClosing | NodeFlags.Special)
		{
			this._request = ImageRequestProcessor.Create(this);
		}

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x06001AD5 RID: 6869 RVA: 0x00052878 File Offset: 0x00050A78
		public IDownload CurrentDownload
		{
			get
			{
				ImageRequestProcessor request = this._request;
				if (request == null)
				{
					return null;
				}
				return request.Download;
			}
		}

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x06001AD6 RID: 6870 RVA: 0x0005288B File Offset: 0x00050A8B
		public string ActualSource
		{
			get
			{
				if (!this.IsCompleted)
				{
					return string.Empty;
				}
				return this._request.Source;
			}
		}

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x06001AD7 RID: 6871 RVA: 0x000528A6 File Offset: 0x00050AA6
		// (set) Token: 0x06001AD8 RID: 6872 RVA: 0x000528B3 File Offset: 0x00050AB3
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

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x06001AD9 RID: 6873 RVA: 0x000528C2 File Offset: 0x00050AC2
		// (set) Token: 0x06001ADA RID: 6874 RVA: 0x000528CF File Offset: 0x00050ACF
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

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x06001ADB RID: 6875 RVA: 0x000524DE File Offset: 0x000506DE
		// (set) Token: 0x06001ADC RID: 6876 RVA: 0x00051A18 File Offset: 0x0004FC18
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

		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x06001ADD RID: 6877 RVA: 0x0004FD0F File Offset: 0x0004DF0F
		// (set) Token: 0x06001ADE RID: 6878 RVA: 0x0004FD1C File Offset: 0x0004DF1C
		public string AlternativeText
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Alt);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Alt, value, false);
			}
		}

		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x06001ADF RID: 6879 RVA: 0x000528DE File Offset: 0x00050ADE
		// (set) Token: 0x06001AE0 RID: 6880 RVA: 0x000528EB File Offset: 0x00050AEB
		public string CrossOrigin
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.CrossOrigin);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.CrossOrigin, value, false);
			}
		}

		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x06001AE1 RID: 6881 RVA: 0x000528FA File Offset: 0x00050AFA
		// (set) Token: 0x06001AE2 RID: 6882 RVA: 0x00052907 File Offset: 0x00050B07
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

		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x06001AE3 RID: 6883 RVA: 0x00052916 File Offset: 0x00050B16
		// (set) Token: 0x06001AE4 RID: 6884 RVA: 0x000501DB File Offset: 0x0004E3DB
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

		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x06001AE5 RID: 6885 RVA: 0x0005292E File Offset: 0x00050B2E
		// (set) Token: 0x06001AE6 RID: 6886 RVA: 0x00050207 File Offset: 0x0004E407
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

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x06001AE7 RID: 6887 RVA: 0x00052946 File Offset: 0x00050B46
		public int OriginalWidth
		{
			get
			{
				ImageRequestProcessor request = this._request;
				if (request == null)
				{
					return 0;
				}
				return request.Width;
			}
		}

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x06001AE8 RID: 6888 RVA: 0x00052959 File Offset: 0x00050B59
		public int OriginalHeight
		{
			get
			{
				ImageRequestProcessor request = this._request;
				if (request == null)
				{
					return 0;
				}
				return request.Height;
			}
		}

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x06001AE9 RID: 6889 RVA: 0x0005296C File Offset: 0x00050B6C
		public bool IsCompleted
		{
			get
			{
				ImageRequestProcessor request = this._request;
				return request != null && request.IsReady;
			}
		}

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x06001AEA RID: 6890 RVA: 0x0005297F File Offset: 0x00050B7F
		// (set) Token: 0x06001AEB RID: 6891 RVA: 0x0005298C File Offset: 0x00050B8C
		public bool IsMap
		{
			get
			{
				return this.GetBoolAttribute(AttributeNames.IsMap);
			}
			set
			{
				this.SetBoolAttribute(AttributeNames.IsMap, value);
			}
		}

		// Token: 0x06001AEC RID: 6892 RVA: 0x0005299A File Offset: 0x00050B9A
		internal override void SetupElement()
		{
			base.SetupElement();
			this.UpdateSource();
		}

		// Token: 0x06001AED RID: 6893 RVA: 0x000529A8 File Offset: 0x00050BA8
		internal void UpdateSource()
		{
			Url imageCandidate = this.GetImageCandidate();
			this.Process(this._request, imageCandidate);
		}

		// Token: 0x04000CD6 RID: 3286
		private readonly ImageRequestProcessor _request;
	}
}
