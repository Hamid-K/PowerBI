using System;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using AngleSharp.Extensions;
using AngleSharp.Network;
using AngleSharp.Network.RequestProcessors;

namespace AngleSharp.Html.LinkRels
{
	// Token: 0x020000CE RID: 206
	internal class StyleSheetLinkRelation : BaseLinkRelation
	{
		// Token: 0x060005FC RID: 1532 RVA: 0x0002F01D File Offset: 0x0002D21D
		public StyleSheetLinkRelation(HtmlLinkElement link)
			: base(link, StyleSheetRequestProcessor.Create(link))
		{
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060005FD RID: 1533 RVA: 0x0002F02C File Offset: 0x0002D22C
		public IStyleSheet Sheet
		{
			get
			{
				StyleSheetRequestProcessor styleSheetRequestProcessor = base.Processor as StyleSheetRequestProcessor;
				if (styleSheetRequestProcessor == null)
				{
					return null;
				}
				return styleSheetRequestProcessor.Sheet;
			}
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x0002F044 File Offset: 0x0002D244
		public override Task LoadAsync()
		{
			ResourceRequest resourceRequest = base.Link.CreateRequestFor(base.Url);
			IRequestProcessor processor = base.Processor;
			if (processor == null)
			{
				return null;
			}
			return processor.ProcessAsync(resourceRequest);
		}
	}
}
