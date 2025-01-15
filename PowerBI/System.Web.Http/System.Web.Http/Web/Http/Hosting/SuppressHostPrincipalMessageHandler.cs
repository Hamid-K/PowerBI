using System;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Properties;

namespace System.Web.Http.Hosting
{
	// Token: 0x020000A7 RID: 167
	public class SuppressHostPrincipalMessageHandler : DelegatingHandler
	{
		// Token: 0x06000401 RID: 1025 RVA: 0x0000BC1C File Offset: 0x00009E1C
		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			IPrincipal previousPrincipal = SuppressHostPrincipalMessageHandler.SetCurrentPrincipal(request, SuppressHostPrincipalMessageHandler._anonymousPrincipal.Value);
			HttpResponseMessage httpResponseMessage;
			try
			{
				httpResponseMessage = await base.SendAsync(request, cancellationToken);
			}
			finally
			{
				SuppressHostPrincipalMessageHandler.SetCurrentPrincipal(request, previousPrincipal);
			}
			return httpResponseMessage;
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0000BC74 File Offset: 0x00009E74
		private static IPrincipal SetCurrentPrincipal(HttpRequestMessage request, IPrincipal principal)
		{
			HttpRequestContext requestContext = request.GetRequestContext();
			if (requestContext == null)
			{
				throw new ArgumentException(SRResources.Request_RequestContextMustNotBeNull, "request");
			}
			IPrincipal principal2 = requestContext.Principal;
			requestContext.Principal = principal;
			return principal2;
		}

		// Token: 0x040000EE RID: 238
		private static readonly Lazy<IPrincipal> _anonymousPrincipal = new Lazy<IPrincipal>(() => new ClaimsPrincipal(new ClaimsIdentity()), true);
	}
}
