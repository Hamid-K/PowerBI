using System;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Extensions;
using AngleSharp.Html;

namespace AngleSharp.Network.RequestProcessors
{
	// Token: 0x0200009F RID: 159
	internal abstract class BaseRequestProcessor : IRequestProcessor
	{
		// Token: 0x060004B6 RID: 1206 RVA: 0x0001E9DF File Offset: 0x0001CBDF
		public BaseRequestProcessor(IResourceLoader loader)
		{
			this._loader = loader;
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x0001E9EE File Offset: 0x0001CBEE
		// (set) Token: 0x060004B8 RID: 1208 RVA: 0x0001E9F6 File Offset: 0x0001CBF6
		public IDownload Download { get; protected set; }

		// Token: 0x060004B9 RID: 1209 RVA: 0x0001E9FF File Offset: 0x0001CBFF
		public virtual Task ProcessAsync(ResourceRequest request)
		{
			if (this.IsDifferentToCurrentDownloadUrl(request.Target))
			{
				this.CancelDownload();
				this.Download = this._loader.DownloadAsync(request);
				return this.FinishDownloadAsync();
			}
			return null;
		}

		// Token: 0x060004BA RID: 1210
		protected abstract Task ProcessResponseAsync(IResponse response);

		// Token: 0x060004BB RID: 1211 RVA: 0x0001EA30 File Offset: 0x0001CC30
		protected async Task FinishDownloadAsync()
		{
			IDownload download = this.Download;
			IResponse response2 = await download.Task.ConfigureAwait(false);
			IResponse response = response2;
			EventTarget eventTarget = download.Originator as EventTarget;
			string eventName = EventNames.Error;
			if (response != null)
			{
				try
				{
					await this.ProcessResponseAsync(response).ConfigureAwait(false);
					eventName = EventNames.Load;
				}
				catch (Exception)
				{
				}
				finally
				{
					response.Dispose();
				}
			}
			EventTarget eventTarget2 = eventTarget;
			if (eventTarget2 != null)
			{
				eventTarget2.FireSimpleEvent(eventName, false, false);
			}
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0001EA78 File Offset: 0x0001CC78
		protected void CancelDownload()
		{
			IDownload download = this.Download;
			if (download != null && !download.IsCompleted)
			{
				download.Cancel();
			}
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0001EAA0 File Offset: 0x0001CCA0
		protected bool IsDifferentToCurrentDownloadUrl(Url target)
		{
			IDownload download = this.Download;
			return download == null || !target.Equals(download.Target);
		}

		// Token: 0x040003C1 RID: 961
		private readonly IResourceLoader _loader;
	}
}
