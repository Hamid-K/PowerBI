using System;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Extensions;
using AngleSharp.Network;
using AngleSharp.Services;

namespace AngleSharp
{
	// Token: 0x02000008 RID: 8
	public static class BrowsingContextExtensions
	{
		// Token: 0x06000039 RID: 57 RVA: 0x00002664 File Offset: 0x00000864
		public static Task<IDocument> OpenNewAsync(this IBrowsingContext context, string url = null)
		{
			return context.OpenAsync(delegate(VirtualResponse m)
			{
				m.Address(url);
			});
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002690 File Offset: 0x00000890
		public static Task<IDocument> OpenAsync(this IBrowsingContext context, IResponse response, CancellationToken cancel)
		{
			if (response == null)
			{
				throw new ArgumentNullException("response");
			}
			if (context == null)
			{
				context = BrowsingContext.New(null);
			}
			CreateDocumentOptions createDocumentOptions = new CreateDocumentOptions(response, context.Configuration, null);
			return context.Configuration.GetFactory<IDocumentFactory>().CreateAsync(context, createDocumentOptions, cancel);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000026D8 File Offset: 0x000008D8
		public static async Task<IDocument> OpenAsync(this IBrowsingContext context, DocumentRequest request, CancellationToken cancel)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			IDocumentLoader loader = context.Loader;
			if (loader != null)
			{
				IDownload download = loader.DownloadAsync(request);
				cancel.Register(new Action(download.Cancel));
				IResponse response2 = await download.Task.ConfigureAwait(false);
				using (IResponse response = response2)
				{
					if (response != null)
					{
						return await context.OpenAsync(response, cancel).ConfigureAwait(false);
					}
				}
				IResponse response = null;
			}
			return await context.OpenNewAsync(request.Target.Href).ConfigureAwait(false);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002730 File Offset: 0x00000930
		public static Task<IDocument> OpenAsync(this IBrowsingContext context, Url url, CancellationToken cancel)
		{
			if (url == null)
			{
				throw new ArgumentNullException("url");
			}
			DocumentRequest documentRequest = DocumentRequest.Get(url, null, null);
			if (context != null && context.Active != null)
			{
				documentRequest.Referer = context.Active.DocumentUri;
			}
			return context.OpenAsync(documentRequest, cancel);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002778 File Offset: 0x00000978
		public static async Task<IDocument> OpenAsync(this IBrowsingContext context, Action<VirtualResponse> request, CancellationToken cancel)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			IDocument document;
			using (IResponse response = VirtualResponse.Create(request))
			{
				document = await context.OpenAsync(response, cancel).ConfigureAwait(false);
			}
			return document;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000027CD File Offset: 0x000009CD
		public static Task<IDocument> OpenAsync(this IBrowsingContext context, Action<VirtualResponse> request)
		{
			return context.OpenAsync(request, CancellationToken.None);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000027DB File Offset: 0x000009DB
		public static Task<IDocument> OpenAsync(this IBrowsingContext context, Url url)
		{
			return context.OpenAsync(url, CancellationToken.None);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000027E9 File Offset: 0x000009E9
		public static Task<IDocument> OpenAsync(this IBrowsingContext context, string address)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return context.OpenAsync(Url.Create(address), CancellationToken.None);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x0000280A File Offset: 0x00000A0A
		public static void NavigateTo(this IBrowsingContext context, IDocument document)
		{
			IHistory sessionHistory = context.SessionHistory;
			if (sessionHistory != null)
			{
				sessionHistory.PushState(document, document.Title, document.Url);
			}
			context.Active = document;
		}
	}
}
