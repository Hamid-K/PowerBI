using System;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Dom.Media;
using AngleSharp.Services;
using AngleSharp.Services.Media;

namespace AngleSharp.Network.RequestProcessors
{
	// Token: 0x020000A4 RID: 164
	internal sealed class MediaRequestProcessor<TMediaInfo> : ResourceRequestProcessor<TMediaInfo> where TMediaInfo : IMediaInfo
	{
		// Token: 0x060004D2 RID: 1234 RVA: 0x0001ED7D File Offset: 0x0001CF7D
		private MediaRequestProcessor(IConfiguration options, IResourceLoader loader)
			: base(options, loader)
		{
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x0001ED88 File Offset: 0x0001CF88
		internal static MediaRequestProcessor<TMediaInfo> Create(Element element)
		{
			Document owner = element.Owner;
			IConfiguration options = owner.Options;
			IResourceLoader loader = owner.Loader;
			if (options == null || loader == null)
			{
				return null;
			}
			return new MediaRequestProcessor<TMediaInfo>(options, loader);
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x0001EDB7 File Offset: 0x0001CFB7
		// (set) Token: 0x060004D5 RID: 1237 RVA: 0x0001EDBF File Offset: 0x0001CFBF
		public TMediaInfo Media { get; private set; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x0001EDC8 File Offset: 0x0001CFC8
		public MediaNetworkState NetworkState
		{
			get
			{
				IDownload download = base.Download;
				if (download != null)
				{
					if (download.IsRunning)
					{
						return MediaNetworkState.Loading;
					}
					if (base.Resource == null)
					{
						return MediaNetworkState.NoSource;
					}
				}
				return MediaNetworkState.Idle;
			}
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0001EDFC File Offset: 0x0001CFFC
		protected override async Task ProcessResponseAsync(IResponse response)
		{
			IResourceService<TMediaInfo> service = this.GetService(response);
			if (service != null)
			{
				CancellationToken none = CancellationToken.None;
				TMediaInfo tmediaInfo = await service.CreateAsync(response, none).ConfigureAwait(false);
				this.Media = tmediaInfo;
			}
		}
	}
}
