using System;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using AngleSharp.Extensions;
using AngleSharp.Services;
using AngleSharp.Services.Styling;

namespace AngleSharp.Network.RequestProcessors
{
	// Token: 0x020000A8 RID: 168
	internal sealed class StyleSheetRequestProcessor : BaseRequestProcessor
	{
		// Token: 0x060004F3 RID: 1267 RVA: 0x0001F25B File Offset: 0x0001D45B
		private StyleSheetRequestProcessor(HtmlLinkElement link, Document document, IResourceLoader loader)
			: base(loader)
		{
			this._link = link;
			this._document = document;
			this._loader = loader;
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0001F27C File Offset: 0x0001D47C
		internal static StyleSheetRequestProcessor Create(HtmlLinkElement element)
		{
			Document owner = element.Owner;
			IResourceLoader loader = owner.Loader;
			if (loader == null)
			{
				return null;
			}
			return new StyleSheetRequestProcessor(element, owner, loader);
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x0001F2A4 File Offset: 0x0001D4A4
		// (set) Token: 0x060004F6 RID: 1270 RVA: 0x0001F2AC File Offset: 0x0001D4AC
		public IStyleSheet Sheet { get; private set; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060004F7 RID: 1271 RVA: 0x0001F2B8 File Offset: 0x0001D4B8
		public IStyleEngine Engine
		{
			get
			{
				IStyleEngine styleEngine;
				if ((styleEngine = this._engine) == null)
				{
					styleEngine = (this._engine = this._document.Options.GetStyleEngine(this.LinkType));
				}
				return styleEngine;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060004F8 RID: 1272 RVA: 0x0001F2EE File Offset: 0x0001D4EE
		public string LinkType
		{
			get
			{
				return this._link.Type ?? MimeTypeNames.Css;
			}
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0001F304 File Offset: 0x0001D504
		public override Task ProcessAsync(ResourceRequest request)
		{
			if (this.Engine != null && base.IsDifferentToCurrentDownloadUrl(request.Target))
			{
				base.CancelDownload();
				base.Download = this.DownloadWithCors(request);
				return base.FinishDownloadAsync();
			}
			return null;
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0001F338 File Offset: 0x0001D538
		protected override async Task ProcessResponseAsync(IResponse response)
		{
			CancellationToken none = CancellationToken.None;
			StyleOptions styleOptions = new StyleOptions(this._document.Context)
			{
				Element = this._link,
				IsDisabled = this._link.IsDisabled,
				IsAlternate = this._link.RelationList.Contains(Keywords.Alternate)
			};
			IStyleSheet styleSheet = await this._engine.ParseStylesheetAsync(response, styleOptions, none).ConfigureAwait(false);
			styleSheet.Media.MediaText = this._link.Media ?? string.Empty;
			this.Sheet = styleSheet;
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0001F388 File Offset: 0x0001D588
		private IDownload DownloadWithCors(ResourceRequest request)
		{
			return this._loader.FetchWithCors(new CorsRequest(request)
			{
				Setting = this._link.CrossOrigin.ToEnum(CorsSetting.None),
				Behavior = OriginBehavior.Taint,
				Integrity = this._document.Options.GetProvider<IIntegrityProvider>()
			});
		}

		// Token: 0x040003D1 RID: 977
		private readonly HtmlLinkElement _link;

		// Token: 0x040003D2 RID: 978
		private readonly Document _document;

		// Token: 0x040003D3 RID: 979
		private readonly IResourceLoader _loader;

		// Token: 0x040003D4 RID: 980
		private IStyleEngine _engine;
	}
}
