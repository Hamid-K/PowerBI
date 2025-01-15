using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Dom.Events;
using AngleSharp.Extensions;

namespace AngleSharp.Network.Default
{
	// Token: 0x020000A9 RID: 169
	public abstract class BaseLoader : ILoader
	{
		// Token: 0x060004FC RID: 1276 RVA: 0x0001F3DC File Offset: 0x0001D5DC
		public BaseLoader(IBrowsingContext context, Predicate<IRequest> filter)
		{
			this._context = context;
			Predicate<IRequest> predicate = filter;
			if (filter == null && (predicate = BaseLoader.<>c.<>9__3_0) == null)
			{
				predicate = (BaseLoader.<>c.<>9__3_0 = (IRequest _) => true);
			}
			this._filter = predicate;
			this._downloads = new List<IDownload>();
			this.MaxRedirects = 50;
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x0001F433 File Offset: 0x0001D633
		// (set) Token: 0x060004FE RID: 1278 RVA: 0x0001F43B File Offset: 0x0001D63B
		public int MaxRedirects { get; protected set; }

		// Token: 0x060004FF RID: 1279 RVA: 0x0001F444 File Offset: 0x0001D644
		protected virtual void Add(IDownload download)
		{
			lock (this)
			{
				this._downloads.Add(download);
			}
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0001F488 File Offset: 0x0001D688
		protected virtual void Remove(IDownload download)
		{
			lock (this)
			{
				this._downloads.Remove(download);
			}
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0001F4CC File Offset: 0x0001D6CC
		protected virtual string GetCookie(Url url)
		{
			return this._context.Configuration.GetCookie(url.Origin);
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0001F4E4 File Offset: 0x0001D6E4
		protected virtual void SetCookie(Url url, string value)
		{
			this._context.Configuration.SetCookie(url.Origin, value);
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0001F500 File Offset: 0x0001D700
		protected virtual IDownload DownloadAsync(Request request, INode originator)
		{
			CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
			if (this._filter(request))
			{
				Task<IResponse> task = this.LoadAsync(request, cancellationTokenSource.Token);
				Download download = new Download(task, cancellationTokenSource, request.Address, originator);
				this.Add(download);
				task.ContinueWith(delegate(Task<IResponse> m)
				{
					this.Remove(download);
				});
				return download;
			}
			return new Download(TaskEx.FromResult<IResponse>(null), cancellationTokenSource, request.Address, originator);
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0001F58C File Offset: 0x0001D78C
		public IEnumerable<IDownload> GetDownloads()
		{
			IEnumerable<IDownload> enumerable;
			lock (this)
			{
				enumerable = this._downloads.ToArray();
			}
			return enumerable;
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0001F5D0 File Offset: 0x0001D7D0
		protected async Task<IResponse> LoadAsync(Request request, CancellationToken cancel)
		{
			IEnumerable<IRequester> requesters = this._context.Configuration.GetServices<IRequester>();
			IResponse response = null;
			int redirectCount = 0;
			this.AppendCookieTo(request);
			do
			{
				if (response != null)
				{
					int num = redirectCount;
					redirectCount = num + 1;
					this.ExtractCookieFrom(response);
					request = BaseLoader.CreateNewRequest(request, response);
					this.AppendCookieTo(request);
				}
				foreach (IRequester requester in requesters)
				{
					if (requester.SupportsProtocol(request.Address.Scheme))
					{
						this._context.Fire(new RequestEvent(request, null));
						response = await requester.RequestAsync(request, cancel).ConfigureAwait(false);
						this._context.Fire(new RequestEvent(request, response));
						break;
					}
				}
				IEnumerator<IRequester> enumerator = null;
			}
			while (response != null && response.StatusCode.IsRedirected() && redirectCount < this.MaxRedirects);
			return response;
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0001F628 File Offset: 0x0001D828
		protected static Request CreateNewRequest(IRequest request, IResponse response)
		{
			HttpMethod httpMethod = request.Method;
			Stream stream = request.Content;
			Dictionary<string, string> dictionary = new Dictionary<string, string>(request.Headers);
			string text = response.Headers[HeaderNames.Location];
			if (response.StatusCode == HttpStatusCode.Found || response.StatusCode == HttpStatusCode.SeeOther)
			{
				httpMethod = HttpMethod.Get;
				stream = Stream.Null;
			}
			else if (stream.Length > 0L)
			{
				stream.Position = 0L;
			}
			dictionary.Remove(HeaderNames.Cookie);
			return new Request
			{
				Address = new Url(request.Address, text),
				Method = httpMethod,
				Content = stream,
				Headers = dictionary
			};
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x0001F6D0 File Offset: 0x0001D8D0
		private void AppendCookieTo(Request request)
		{
			string cookie = this.GetCookie(request.Address);
			if (cookie != null)
			{
				request.Headers[HeaderNames.Cookie] = cookie;
			}
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0001F700 File Offset: 0x0001D900
		private void ExtractCookieFrom(IResponse response)
		{
			string orDefault = response.Headers.GetOrDefault(HeaderNames.SetCookie, null);
			if (orDefault != null)
			{
				this.SetCookie(response.Address, orDefault);
			}
		}

		// Token: 0x040003D6 RID: 982
		private readonly IBrowsingContext _context;

		// Token: 0x040003D7 RID: 983
		private readonly Predicate<IRequest> _filter;

		// Token: 0x040003D8 RID: 984
		private readonly List<IDownload> _downloads;
	}
}
