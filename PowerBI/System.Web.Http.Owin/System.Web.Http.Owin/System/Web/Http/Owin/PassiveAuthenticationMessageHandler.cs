using System;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Owin.Properties;
using Microsoft.Owin.Security;

namespace System.Web.Http.Owin
{
	// Token: 0x02000014 RID: 20
	public class PassiveAuthenticationMessageHandler : DelegatingHandler
	{
		// Token: 0x060000AA RID: 170 RVA: 0x00003708 File Offset: 0x00001908
		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			IPrincipal previousPrincipal = PassiveAuthenticationMessageHandler.SetCurrentPrincipal(request, PassiveAuthenticationMessageHandler._anonymousPrincipal.Value);
			HttpResponseMessage httpResponseMessage;
			try
			{
				httpResponseMessage = await base.SendAsync(request, cancellationToken);
			}
			finally
			{
				PassiveAuthenticationMessageHandler.SetCurrentPrincipal(request, previousPrincipal);
			}
			PassiveAuthenticationMessageHandler.SuppressDefaultAuthenticationChallenges(request);
			return httpResponseMessage;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003760 File Offset: 0x00001960
		private static IPrincipal SetCurrentPrincipal(HttpRequestMessage request, IPrincipal principal)
		{
			HttpRequestContext requestContext = request.GetRequestContext();
			if (requestContext == null)
			{
				throw new ArgumentException(OwinResources.Request_RequestContextMustNotBeNull, "request");
			}
			IPrincipal principal2 = requestContext.Principal;
			requestContext.Principal = principal;
			return principal2;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003794 File Offset: 0x00001994
		private static void SuppressDefaultAuthenticationChallenges(HttpRequestMessage request)
		{
			IAuthenticationManager authenticationManager = request.GetAuthenticationManager();
			if (authenticationManager == null)
			{
				throw new InvalidOperationException(OwinResources.IAuthenticationManagerNotAvailable);
			}
			AuthenticationResponseChallenge authenticationResponseChallenge = authenticationManager.AuthenticationResponseChallenge;
			string[] array = new string[1];
			if (authenticationResponseChallenge == null)
			{
				authenticationManager.AuthenticationResponseChallenge = new AuthenticationResponseChallenge(array, new AuthenticationProperties());
				return;
			}
			if (authenticationResponseChallenge.AuthenticationTypes == null || authenticationResponseChallenge.AuthenticationTypes.Length == 0)
			{
				authenticationManager.AuthenticationResponseChallenge = new AuthenticationResponseChallenge(array, authenticationResponseChallenge.Properties);
			}
		}

		// Token: 0x04000030 RID: 48
		private static readonly Lazy<IPrincipal> _anonymousPrincipal = new Lazy<IPrincipal>(() => new ClaimsPrincipal(new ClaimsIdentity()), true);
	}
}
