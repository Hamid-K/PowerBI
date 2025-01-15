using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Html;
using AngleSharp.Network;
using AngleSharp.Network.Default;
using AngleSharp.Services;

namespace AngleSharp.Extensions
{
	// Token: 0x020000F3 RID: 243
	internal static class RequesterExtensions
	{
		// Token: 0x06000794 RID: 1940 RVA: 0x0003589F File Offset: 0x00033A9F
		public static bool IsRedirected(this HttpStatusCode status)
		{
			return status == HttpStatusCode.Found || status == HttpStatusCode.TemporaryRedirect || status == HttpStatusCode.SeeOther || status == HttpStatusCode.TemporaryRedirect || status == HttpStatusCode.MovedPermanently || status == HttpStatusCode.MultipleChoices;
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x000358D4 File Offset: 0x00033AD4
		public static IDownload FetchWithCors(this IResourceLoader loader, CorsRequest cors)
		{
			ResourceRequest request = cors.Request;
			CorsSetting setting = cors.Setting;
			Url target = request.Target;
			if (request.Origin == target.Origin || target.Scheme == ProtocolNames.Data || target.Href == "about:blank")
			{
				return loader.FetchFromSameOrigin(target, cors);
			}
			if (setting == CorsSetting.Anonymous || setting == CorsSetting.UseCredentials)
			{
				return loader.FetchFromDifferentOrigin(cors);
			}
			if (setting == CorsSetting.None)
			{
				return loader.FetchWithoutCors(request, cors.Behavior);
			}
			throw new DomException(DomError.Network);
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x00035960 File Offset: 0x00033B60
		private static IDownload FetchFromSameOrigin(this IResourceLoader loader, Url url, CorsRequest cors)
		{
			ResourceRequest request = cors.Request;
			IDownload download = loader.DownloadAsync(new ResourceRequest(request.Source, url)
			{
				Origin = request.Origin,
				IsManualRedirectDesired = true
			});
			return download.Wrap(delegate(IResponse response)
			{
				if (!response.IsRedirected())
				{
					return cors.CheckIntegrity(download);
				}
				url.Href = response.Headers.GetOrDefault(HeaderNames.Location, url.Href);
				if (!request.Origin.Is(url.Origin))
				{
					return loader.FetchFromSameOrigin(url, cors);
				}
				return loader.FetchWithCors(cors.RedirectTo(url));
			});
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x000359F0 File Offset: 0x00033BF0
		private static IDownload FetchFromDifferentOrigin(this IResourceLoader loader, CorsRequest cors)
		{
			ResourceRequest request = cors.Request;
			request.IsCredentialOmitted = cors.IsAnonymous();
			IDownload download = loader.DownloadAsync(request);
			return download.Wrap(delegate(IResponse response)
			{
				if (response == null || response.StatusCode != HttpStatusCode.OK)
				{
					if (response != null)
					{
						response.Dispose();
					}
					throw new DomException(DomError.Network);
				}
				return cors.CheckIntegrity(download);
			});
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x00035A4B File Offset: 0x00033C4B
		private static IDownload FetchWithoutCors(this IResourceLoader loader, ResourceRequest request, OriginBehavior behavior)
		{
			if (behavior == OriginBehavior.Fail)
			{
				throw new DomException(DomError.Network);
			}
			return loader.DownloadAsync(request);
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x00035A60 File Offset: 0x00033C60
		private static bool IsAnonymous(this CorsRequest cors)
		{
			return cors.Setting == CorsSetting.Anonymous;
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x00035A6C File Offset: 0x00033C6C
		private static IDownload Wrap(this IDownload download, Func<IResponse, IDownload> callback)
		{
			CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
			return new Download(download.Task.Wrap(callback), cancellationTokenSource, download.Target, download.Originator);
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x00035AA0 File Offset: 0x00033CA0
		private static IDownload Wrap(this IDownload download, IResponse response)
		{
			CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
			return new Download(TaskEx.FromResult<IResponse>(response), cancellationTokenSource, download.Target, download.Originator);
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x00035ACC File Offset: 0x00033CCC
		private static async Task<IResponse> Wrap(this Task<IResponse> task, Func<IResponse, IDownload> callback)
		{
			IResponse response = await task.ConfigureAwait(false);
			return await callback(response).Task.ConfigureAwait(false);
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x00035B19 File Offset: 0x00033D19
		private static bool IsRedirected(this IResponse response)
		{
			return ((response != null) ? response.StatusCode : HttpStatusCode.NotFound).IsRedirected();
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x00035B30 File Offset: 0x00033D30
		private static CorsRequest RedirectTo(this CorsRequest cors, Url url)
		{
			ResourceRequest request = cors.Request;
			return new CorsRequest(new ResourceRequest(request.Source, url)
			{
				IsCookieBlocked = request.IsCookieBlocked,
				IsSameOriginForced = request.IsSameOriginForced,
				Origin = request.Origin
			})
			{
				Setting = cors.Setting,
				Behavior = cors.Behavior,
				Integrity = cors.Integrity
			};
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x00035BA0 File Offset: 0x00033DA0
		private static IDownload CheckIntegrity(this CorsRequest cors, IDownload download)
		{
			IResponse result = download.Task.Result;
			IElement source = cors.Request.Source;
			string text = ((source != null) ? source.GetAttribute(AttributeNames.Integrity) : null);
			IIntegrityProvider integrity = cors.Integrity;
			if (string.IsNullOrEmpty(text) || integrity == null || result == null)
			{
				return download;
			}
			MemoryStream memoryStream = new MemoryStream();
			result.Content.CopyTo(memoryStream);
			memoryStream.Position = 0L;
			if (!integrity.IsSatisfied(memoryStream.ToArray(), text))
			{
				result.Dispose();
				throw new DomException(DomError.Security);
			}
			return download.Wrap(new Response
			{
				Address = result.Address,
				Content = memoryStream,
				Headers = result.Headers,
				StatusCode = result.StatusCode
			});
		}
	}
}
