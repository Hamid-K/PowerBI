using System;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Extensions;
using AngleSharp.Services;

namespace AngleSharp.Network.RequestProcessors
{
	// Token: 0x020000A0 RID: 160
	internal sealed class DocumentRequestProcessor : BaseRequestProcessor
	{
		// Token: 0x060004BE RID: 1214 RVA: 0x0001EAC8 File Offset: 0x0001CCC8
		private DocumentRequestProcessor(IDocument document, IConfiguration configuration, IResourceLoader loader)
			: base(loader)
		{
			this._parentDocument = document;
			this._configuration = configuration;
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x0001EAE0 File Offset: 0x0001CCE0
		internal static DocumentRequestProcessor Create(Element element)
		{
			Document owner = element.Owner;
			IConfiguration options = owner.Options;
			IResourceLoader loader = owner.Loader;
			if (options == null || loader == null)
			{
				return null;
			}
			return new DocumentRequestProcessor(owner, options, loader);
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x0001EB12 File Offset: 0x0001CD12
		// (set) Token: 0x060004C1 RID: 1217 RVA: 0x0001EB1A File Offset: 0x0001CD1A
		public IDocument ChildDocument { get; private set; }

		// Token: 0x060004C2 RID: 1218 RVA: 0x0001EB24 File Offset: 0x0001CD24
		protected override async Task ProcessResponseAsync(IResponse response)
		{
			BrowsingContext browsingContext = new BrowsingContext(this._parentDocument.Context, Sandboxes.None);
			CreateDocumentOptions createDocumentOptions = new CreateDocumentOptions(response, this._configuration, this._parentDocument);
			IDocument document = await this._configuration.GetFactory<IDocumentFactory>().CreateAsync(browsingContext, createDocumentOptions, CancellationToken.None).ConfigureAwait(false);
			this.ChildDocument = document;
		}

		// Token: 0x040003C3 RID: 963
		private readonly IDocument _parentDocument;

		// Token: 0x040003C4 RID: 964
		private readonly IConfiguration _configuration;
	}
}
