using System;
using System.Threading.Tasks;
using AngleSharp.Dom.Html;
using AngleSharp.Network.RequestProcessors;

namespace AngleSharp.Html.LinkRels
{
	// Token: 0x020000CC RID: 204
	internal abstract class BaseLinkRelation
	{
		// Token: 0x060005F1 RID: 1521 RVA: 0x0002EEDA File Offset: 0x0002D0DA
		public BaseLinkRelation(HtmlLinkElement link, IRequestProcessor processor)
		{
			this._link = link;
			this._processor = processor;
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060005F2 RID: 1522 RVA: 0x0002EEF0 File Offset: 0x0002D0F0
		public IRequestProcessor Processor
		{
			get
			{
				return this._processor;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060005F3 RID: 1523 RVA: 0x0002EEF8 File Offset: 0x0002D0F8
		public HtmlLinkElement Link
		{
			get
			{
				return this._link;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060005F4 RID: 1524 RVA: 0x0002EF00 File Offset: 0x0002D100
		public Url Url
		{
			get
			{
				return new Url(this._link.Href);
			}
		}

		// Token: 0x060005F5 RID: 1525
		public abstract Task LoadAsync();

		// Token: 0x040005F6 RID: 1526
		private readonly HtmlLinkElement _link;

		// Token: 0x040005F7 RID: 1527
		private readonly IRequestProcessor _processor;
	}
}
