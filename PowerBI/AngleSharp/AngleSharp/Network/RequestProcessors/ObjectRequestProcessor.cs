using System;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Services;
using AngleSharp.Services.Media;

namespace AngleSharp.Network.RequestProcessors
{
	// Token: 0x020000A5 RID: 165
	internal sealed class ObjectRequestProcessor : ResourceRequestProcessor<IObjectInfo>
	{
		// Token: 0x060004D8 RID: 1240 RVA: 0x0001EE49 File Offset: 0x0001D049
		private ObjectRequestProcessor(IConfiguration options, IResourceLoader loader)
			: base(options, loader)
		{
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x0001EE54 File Offset: 0x0001D054
		internal static ObjectRequestProcessor Create(Element element)
		{
			Document owner = element.Owner;
			IConfiguration options = owner.Options;
			IResourceLoader loader = owner.Loader;
			if (options == null || loader == null)
			{
				return null;
			}
			return new ObjectRequestProcessor(options, loader);
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x0001EE83 File Offset: 0x0001D083
		public int Width
		{
			get
			{
				IObjectInfo resource = base.Resource;
				if (resource == null)
				{
					return 0;
				}
				return resource.Width;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x0001EE96 File Offset: 0x0001D096
		public int Height
		{
			get
			{
				IObjectInfo resource = base.Resource;
				if (resource == null)
				{
					return 0;
				}
				return resource.Height;
			}
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0001EEAC File Offset: 0x0001D0AC
		protected override async Task ProcessResponseAsync(IResponse response)
		{
			IResourceService<IObjectInfo> service = this.GetService(response);
			if (service != null)
			{
				CancellationToken none = CancellationToken.None;
				IObjectInfo objectInfo = await service.CreateAsync(response, none).ConfigureAwait(false);
				this.Resource = objectInfo;
			}
		}
	}
}
