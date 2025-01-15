using System;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Services;
using AngleSharp.Services.Media;

namespace AngleSharp.Network.RequestProcessors
{
	// Token: 0x020000A2 RID: 162
	internal sealed class ImageRequestProcessor : ResourceRequestProcessor<IImageInfo>
	{
		// Token: 0x060004CB RID: 1227 RVA: 0x0001ECC5 File Offset: 0x0001CEC5
		private ImageRequestProcessor(IConfiguration options, IResourceLoader loader)
			: base(options, loader)
		{
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x0001ECD0 File Offset: 0x0001CED0
		internal static ImageRequestProcessor Create(Element element)
		{
			Document owner = element.Owner;
			IConfiguration options = owner.Options;
			IResourceLoader loader = owner.Loader;
			if (options == null || loader == null)
			{
				return null;
			}
			return new ImageRequestProcessor(options, loader);
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x0001ECFF File Offset: 0x0001CEFF
		public int Width
		{
			get
			{
				if (!base.IsReady)
				{
					return 0;
				}
				return base.Resource.Width;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x0001ED16 File Offset: 0x0001CF16
		public int Height
		{
			get
			{
				if (!base.IsReady)
				{
					return 0;
				}
				return base.Resource.Height;
			}
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x0001ED30 File Offset: 0x0001CF30
		protected override async Task ProcessResponseAsync(IResponse response)
		{
			IResourceService<IImageInfo> service = this.GetService(response);
			if (service != null)
			{
				CancellationToken none = CancellationToken.None;
				IImageInfo imageInfo = await service.CreateAsync(response, none).ConfigureAwait(false);
				this.Resource = imageInfo;
			}
		}
	}
}
