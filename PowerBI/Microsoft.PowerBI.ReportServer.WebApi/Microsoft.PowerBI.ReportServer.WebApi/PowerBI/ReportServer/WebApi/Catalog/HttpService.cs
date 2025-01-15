using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.BIServer.HostingEnvironment.Request;
using Newtonsoft.Json;

namespace Microsoft.PowerBI.ReportServer.WebApi.Catalog
{
	// Token: 0x02000040 RID: 64
	internal sealed class HttpService : IHttpService
	{
		// Token: 0x0600011C RID: 284 RVA: 0x000074A8 File Offset: 0x000056A8
		private HttpService()
		{
			ServicePointManager.DefaultConnectionLimit = 300;
			this._httpClientHandler = new HttpClientHandler
			{
				UseDefaultCredentials = true,
				PreAuthenticate = true,
				UseCookies = false
			};
			this._httpClient = new HttpClient(this._httpClientHandler);
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600011D RID: 285 RVA: 0x000074F6 File Offset: 0x000056F6
		public static HttpService Instance
		{
			get
			{
				return HttpService._lazy.Value;
			}
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00007504 File Offset: 0x00005704
		public async Task<HttpResponseMessage> InvokeApi(IPrincipal userPrincipal, Uri uriToInvoke)
		{
			HttpService.<>c__DisplayClass6_0 CS$<>8__locals1 = new HttpService.<>c__DisplayClass6_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.request = new HttpRequestMessage
			{
				Method = HttpMethod.Get,
				RequestUri = uriToInvoke
			};
			RequestContext.PassOnCurrentRequestContext(CS$<>8__locals1.request);
			return await this.ExecuteWithCorrespondingAuthMechanism<Task<HttpResponseMessage>>(CS$<>8__locals1.request, userPrincipal, delegate
			{
				HttpService.<>c__DisplayClass6_0.<<InvokeApi>b__0>d <<InvokeApi>b__0>d;
				<<InvokeApi>b__0>d.<>4__this = CS$<>8__locals1;
				<<InvokeApi>b__0>d.<>t__builder = AsyncTaskMethodBuilder<HttpResponseMessage>.Create();
				<<InvokeApi>b__0>d.<>1__state = -1;
				AsyncTaskMethodBuilder<HttpResponseMessage> <>t__builder = <<InvokeApi>b__0>d.<>t__builder;
				<>t__builder.Start<HttpService.<>c__DisplayClass6_0.<<InvokeApi>b__0>d>(ref <<InvokeApi>b__0>d);
				return <<InvokeApi>b__0>d.<>t__builder.Task;
			});
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000755C File Offset: 0x0000575C
		public async Task<HttpResponseMessage> InvokeApiWithTrustedProcessToken(IPrincipal userPrincipal, Uri catalogItemContentUrl)
		{
			HttpService.<>c__DisplayClass7_0 CS$<>8__locals1 = new HttpService.<>c__DisplayClass7_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.request = this.CreateRequestMessageWithTrustedProcessToken(userPrincipal, catalogItemContentUrl);
			RequestContext.PassOnCurrentRequestContext(CS$<>8__locals1.request);
			return await this.ExecuteWithCorrespondingAuthMechanism<Task<HttpResponseMessage>>(CS$<>8__locals1.request, userPrincipal, delegate
			{
				HttpService.<>c__DisplayClass7_0.<<InvokeApiWithTrustedProcessToken>b__0>d <<InvokeApiWithTrustedProcessToken>b__0>d;
				<<InvokeApiWithTrustedProcessToken>b__0>d.<>4__this = CS$<>8__locals1;
				<<InvokeApiWithTrustedProcessToken>b__0>d.<>t__builder = AsyncTaskMethodBuilder<HttpResponseMessage>.Create();
				<<InvokeApiWithTrustedProcessToken>b__0>d.<>1__state = -1;
				AsyncTaskMethodBuilder<HttpResponseMessage> <>t__builder = <<InvokeApiWithTrustedProcessToken>b__0>d.<>t__builder;
				<>t__builder.Start<HttpService.<>c__DisplayClass7_0.<<InvokeApiWithTrustedProcessToken>b__0>d>(ref <<InvokeApiWithTrustedProcessToken>b__0>d);
				return <<InvokeApiWithTrustedProcessToken>b__0>d.<>t__builder.Task;
			});
		}

		// Token: 0x06000120 RID: 288 RVA: 0x000075B4 File Offset: 0x000057B4
		internal HttpRequestMessage CreateRequestMessageWithTrustedProcessToken(IPrincipal userPrincipal, Uri catalogItemContentUrl)
		{
			var <>f__AnonymousType = new
			{
				TrustedProcessToken = TrustedProcessToken.CreateToken(userPrincipal)
			};
			return new HttpRequestMessage
			{
				Method = HttpMethod.Post,
				RequestUri = catalogItemContentUrl,
				Content = this.SerializeContentToJson(<>f__AnonymousType)
			};
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000075F1 File Offset: 0x000057F1
		internal StringContent SerializeContentToJson(object content)
		{
			return new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00007608 File Offset: 0x00005808
		private TReturn ExecuteWithCorrespondingAuthMechanism<TReturn>(HttpRequestMessage request, IPrincipal userPrincipal, Func<TReturn> func)
		{
			request.Headers.Add("RSTrustedServiceToken", TrustedProcessToken.CreateToken(userPrincipal));
			if (userPrincipal.Identity is WindowsIdentity)
			{
				return this.ExecuteWithWindowsAuth<TReturn>(request, userPrincipal, func);
			}
			if (userPrincipal.Identity.AuthenticationType.Equals("Basic", StringComparison.OrdinalIgnoreCase))
			{
				return this.ExecuteWithBasicAuth<TReturn>(request, userPrincipal, func);
			}
			if (userPrincipal.Identity is IUserContextContainer || userPrincipal.Identity is ClaimsIdentity)
			{
				return this.ExecuteWithCustomAuth<TReturn>(request, userPrincipal, func);
			}
			throw new NotImplementedException(string.Format("AuthenticationType {0} not supported in SOAP proxy!", userPrincipal.Identity.AuthenticationType));
		}

		// Token: 0x06000123 RID: 291 RVA: 0x000076A4 File Offset: 0x000058A4
		private void SetRequestProperties(HttpRequestMessage request, IUserContextContainer userContextContainer)
		{
			if (userContextContainer != null && userContextContainer.UserCookies != null && userContextContainer.UserCookies.Any<KeyValuePair<string, string>>())
			{
				CookieContainer cookieContainer = new CookieContainer();
				foreach (KeyValuePair<string, string> keyValuePair in userContextContainer.UserCookies)
				{
					cookieContainer.Add(request.RequestUri, new Cookie(keyValuePair.Key, keyValuePair.Value, "/", request.RequestUri.Host));
				}
				request.Headers.TryAddWithoutValidation("Cookie", cookieContainer.GetCookieHeader(request.RequestUri));
			}
		}

		// Token: 0x06000124 RID: 292 RVA: 0x0000775C File Offset: 0x0000595C
		private TReturn ExecuteWithWindowsAuth<TReturn>(HttpRequestMessage request, IPrincipal userPrincipal, Func<TReturn> func)
		{
			TReturn treturn;
			using (((WindowsIdentity)userPrincipal.Identity).Impersonate())
			{
				treturn = func();
			}
			return treturn;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x000077A0 File Offset: 0x000059A0
		private TReturn ExecuteWithCustomAuth<TReturn>(HttpRequestMessage request, IPrincipal userPrincipal, Func<TReturn> func)
		{
			IUserContextContainer userContextContainer = userPrincipal.Identity as IUserContextContainer;
			this.SetRequestProperties(request, userContextContainer);
			return func();
		}

		// Token: 0x06000126 RID: 294 RVA: 0x000077C8 File Offset: 0x000059C8
		private TReturn ExecuteWithBasicAuth<TReturn>(HttpRequestMessage request, IPrincipal userPrincipal, Func<TReturn> func)
		{
			HttpListenerBasicIdentity httpListenerBasicIdentity = userPrincipal.Identity as HttpListenerBasicIdentity;
			if (httpListenerBasicIdentity == null)
			{
				throw new InvalidOperationException("A server configured with Basic Authentication is running with non-BasicIdentity");
			}
			string text = Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", httpListenerBasicIdentity.Name, httpListenerBasicIdentity.Password)));
			request.Headers.Add("Authorization", "Basic " + text);
			return func();
		}

		// Token: 0x040000BD RID: 189
		private static readonly Lazy<HttpService> _lazy = new Lazy<HttpService>(() => new HttpService());

		// Token: 0x040000BE RID: 190
		private readonly HttpClient _httpClient;

		// Token: 0x040000BF RID: 191
		private readonly HttpClientHandler _httpClientHandler;
	}
}
