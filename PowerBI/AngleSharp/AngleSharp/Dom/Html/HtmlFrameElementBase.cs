using System;
using AngleSharp.Extensions;
using AngleSharp.Html;
using AngleSharp.Network;
using AngleSharp.Network.RequestProcessors;

namespace AngleSharp.Dom.Html
{
	// Token: 0x0200035D RID: 861
	internal abstract class HtmlFrameElementBase : HtmlFrameOwnerElement
	{
		// Token: 0x06001A9E RID: 6814 RVA: 0x000524B0 File Offset: 0x000506B0
		public HtmlFrameElementBase(Document owner, string name, string prefix, NodeFlags flags = NodeFlags.None)
			: base(owner, name, prefix, flags | NodeFlags.Special)
		{
			this._request = FrameRequestProcessor.Create(this);
		}

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x06001A9F RID: 6815 RVA: 0x000524CB File Offset: 0x000506CB
		public IDownload CurrentDownload
		{
			get
			{
				FrameRequestProcessor request = this._request;
				if (request == null)
				{
					return null;
				}
				return request.Download;
			}
		}

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x06001AA0 RID: 6816 RVA: 0x0004FCAB File Offset: 0x0004DEAB
		// (set) Token: 0x06001AA1 RID: 6817 RVA: 0x0004FCB8 File Offset: 0x0004DEB8
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

		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x06001AA2 RID: 6818 RVA: 0x000524DE File Offset: 0x000506DE
		// (set) Token: 0x06001AA3 RID: 6819 RVA: 0x00051A18 File Offset: 0x0004FC18
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

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x06001AA4 RID: 6820 RVA: 0x000524EB File Offset: 0x000506EB
		// (set) Token: 0x06001AA5 RID: 6821 RVA: 0x000524F8 File Offset: 0x000506F8
		public string Scrolling
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Scrolling);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Scrolling, value, false);
			}
		}

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x06001AA6 RID: 6822 RVA: 0x00052507 File Offset: 0x00050707
		public IDocument ContentDocument
		{
			get
			{
				FrameRequestProcessor request = this._request;
				if (request == null)
				{
					return null;
				}
				return request.Document;
			}
		}

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x06001AA7 RID: 6823 RVA: 0x0005251A File Offset: 0x0005071A
		// (set) Token: 0x06001AA8 RID: 6824 RVA: 0x00052527 File Offset: 0x00050727
		public string LongDesc
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.LongDesc);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.LongDesc, value, false);
			}
		}

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x06001AA9 RID: 6825 RVA: 0x00052536 File Offset: 0x00050736
		// (set) Token: 0x06001AAA RID: 6826 RVA: 0x00052543 File Offset: 0x00050743
		public string FrameBorder
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.FrameBorder);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.FrameBorder, value, false);
			}
		}

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x06001AAB RID: 6827 RVA: 0x00052554 File Offset: 0x00050754
		public IBrowsingContext NestedContext
		{
			get
			{
				IBrowsingContext browsingContext;
				if ((browsingContext = this._context) == null)
				{
					browsingContext = (this._context = base.Owner.NewChildContext(Sandboxes.None));
				}
				return browsingContext;
			}
		}

		// Token: 0x06001AAC RID: 6828 RVA: 0x0000C295 File Offset: 0x0000A495
		internal virtual string GetContentHtml()
		{
			return null;
		}

		// Token: 0x06001AAD RID: 6829 RVA: 0x00052580 File Offset: 0x00050780
		internal override void SetupElement()
		{
			base.SetupElement();
			if (this.GetOwnAttribute(AttributeNames.Src) != null)
			{
				this.UpdateSource();
			}
		}

		// Token: 0x06001AAE RID: 6830 RVA: 0x0005259C File Offset: 0x0005079C
		internal void UpdateSource()
		{
			string contentHtml = this.GetContentHtml();
			string source = this.Source;
			if ((source != null && source != base.Owner.DocumentUri) || contentHtml != null)
			{
				Url url = this.HyperReference(source);
				this.Process(this._request, url);
			}
		}

		// Token: 0x04000CD2 RID: 3282
		private IBrowsingContext _context;

		// Token: 0x04000CD3 RID: 3283
		private FrameRequestProcessor _request;
	}
}
