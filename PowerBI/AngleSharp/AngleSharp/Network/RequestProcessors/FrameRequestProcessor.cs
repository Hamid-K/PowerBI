using System;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;

namespace AngleSharp.Network.RequestProcessors
{
	// Token: 0x020000A1 RID: 161
	internal sealed class FrameRequestProcessor : BaseRequestProcessor
	{
		// Token: 0x060004C3 RID: 1219 RVA: 0x0001EB71 File Offset: 0x0001CD71
		private FrameRequestProcessor(HtmlFrameElementBase element, IResourceLoader loader)
			: base(loader)
		{
			this._element = element;
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x0001EB84 File Offset: 0x0001CD84
		internal static FrameRequestProcessor Create(HtmlFrameElementBase element)
		{
			IResourceLoader loader = element.Owner.Loader;
			if (loader == null)
			{
				return null;
			}
			return new FrameRequestProcessor(element, loader);
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x0001EBA9 File Offset: 0x0001CDA9
		// (set) Token: 0x060004C6 RID: 1222 RVA: 0x0001EBB1 File Offset: 0x0001CDB1
		public IDocument Document { get; private set; }

		// Token: 0x060004C7 RID: 1223 RVA: 0x0001EBBC File Offset: 0x0001CDBC
		public override Task ProcessAsync(ResourceRequest request)
		{
			string contentHtml = this._element.GetContentHtml();
			if (contentHtml != null)
			{
				string documentUri = this._element.Owner.DocumentUri;
				return this.ProcessResponse(contentHtml, documentUri);
			}
			return base.ProcessAsync(request);
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x0001EBFC File Offset: 0x0001CDFC
		protected override Task ProcessResponseAsync(IResponse response)
		{
			CancellationToken none = CancellationToken.None;
			Task<IDocument> task = this._element.NestedContext.OpenAsync(response, none);
			return this.WaitResponse(task);
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x0001EC2C File Offset: 0x0001CE2C
		private Task ProcessResponse(string response, string referer)
		{
			CancellationToken none = CancellationToken.None;
			Task<IDocument> task = this._element.NestedContext.OpenAsync(delegate(VirtualResponse m)
			{
				m.Content(response).Address(referer);
			}, none);
			return this.WaitResponse(task);
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x0001EC78 File Offset: 0x0001CE78
		private async Task WaitResponse(Task<IDocument> task)
		{
			IDocument document = await task.ConfigureAwait(false);
			this.Document = document;
		}

		// Token: 0x040003C6 RID: 966
		private readonly HtmlFrameElementBase _element;
	}
}
