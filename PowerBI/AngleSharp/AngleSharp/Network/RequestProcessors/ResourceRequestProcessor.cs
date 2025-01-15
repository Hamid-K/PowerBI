using System;
using System.Threading.Tasks;
using AngleSharp.Extensions;
using AngleSharp.Services;
using AngleSharp.Services.Media;

namespace AngleSharp.Network.RequestProcessors
{
	// Token: 0x020000A6 RID: 166
	internal abstract class ResourceRequestProcessor<TResource> : BaseRequestProcessor where TResource : IResourceInfo
	{
		// Token: 0x060004DD RID: 1245 RVA: 0x0001EEF9 File Offset: 0x0001D0F9
		public ResourceRequestProcessor(IConfiguration options, IResourceLoader loader)
			: base(loader)
		{
			this._options = options;
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060004DE RID: 1246 RVA: 0x0001EF0C File Offset: 0x0001D10C
		public string Source
		{
			get
			{
				TResource resource = this.Resource;
				ref TResource ptr = ref resource;
				TResource tresource = default(TResource);
				string text;
				if (tresource == null)
				{
					tresource = resource;
					ptr = ref tresource;
					if (tresource == null)
					{
						text = null;
						goto IL_003D;
					}
				}
				text = ptr.Source.Href;
				IL_003D:
				return text ?? string.Empty;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x0001EF5F File Offset: 0x0001D15F
		public bool IsReady
		{
			get
			{
				return this.Resource != null;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060004E0 RID: 1248 RVA: 0x0001EF6F File Offset: 0x0001D16F
		// (set) Token: 0x060004E1 RID: 1249 RVA: 0x0001EF77 File Offset: 0x0001D177
		public TResource Resource { get; protected set; }

		// Token: 0x060004E2 RID: 1250 RVA: 0x0001EF80 File Offset: 0x0001D180
		public override Task ProcessAsync(ResourceRequest request)
		{
			if (this.IsDifferentToCurrentResourceUrl(request.Target))
			{
				return base.ProcessAsync(request);
			}
			return null;
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x0001EF9C File Offset: 0x0001D19C
		protected IResourceService<TResource> GetService(IResponse response)
		{
			MimeType contentType = response.GetContentType();
			return this._options.GetResourceService(contentType.Content);
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x0001EFC4 File Offset: 0x0001D1C4
		private bool IsDifferentToCurrentResourceUrl(Url target)
		{
			TResource resource = this.Resource;
			return resource == null || !target.Equals(resource.Source);
		}

		// Token: 0x040003C9 RID: 969
		private readonly IConfiguration _options;
	}
}
