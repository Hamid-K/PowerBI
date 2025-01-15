using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Web.Services.Protocols;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.BIServer.Owin.Common;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Portal.Services.Extensions;

namespace Microsoft.ReportingServices.Portal.Services.SoapProxy
{
	// Token: 0x02000029 RID: 41
	public static class SoapAuthenticationHelper
	{
		// Token: 0x060001DA RID: 474 RVA: 0x0000D108 File Offset: 0x0000B308
		public static TReturn ExecuteWithCorrespondingAuthMechanism<TReturn>(SoapHttpClientProtocol soapClient, IPrincipal userPrincipal, Func<TReturn> func)
		{
			if (userPrincipal.Identity is WindowsIdentity)
			{
				return SoapAuthenticationHelper.ExecuteWithWindowsAuth<TReturn>(soapClient, userPrincipal, func);
			}
			if (userPrincipal.Identity.AuthenticationType.Equals("Basic", StringComparison.OrdinalIgnoreCase))
			{
				return SoapAuthenticationHelper.ExecuteWithBasicAuth<TReturn>(soapClient, userPrincipal, func);
			}
			if (userPrincipal.Identity is IUserContextContainer || userPrincipal.Identity is ClaimsIdentity)
			{
				return SoapAuthenticationHelper.ExecuteWithCustomAuth<TReturn>(soapClient, userPrincipal, func);
			}
			throw new NotImplementedException(string.Format("AuthenticationType {0} not supported in SOAP proxy!", userPrincipal.Identity.AuthenticationType));
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000D18C File Offset: 0x0000B38C
		public static void ExecuteWithCorrespondingAuthMechanism(SoapHttpClientProtocol soapClient, IPrincipal userPrincipal, Action action)
		{
			SoapAuthenticationHelper.ExecuteWithCorrespondingAuthMechanism<int>(soapClient, userPrincipal, delegate
			{
				action();
				return 0;
			});
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000D1BC File Offset: 0x0000B3BC
		public static void SetRequestProperties(HttpWebRequest request, IUserContextContainer userContextContainer, string reportServerHostName)
		{
			if (userContextContainer.UserCookies.Any<KeyValuePair<string, string>>())
			{
				request.CookieContainer = new CookieContainer();
				foreach (KeyValuePair<string, string> keyValuePair in userContextContainer.UserCookies)
				{
					request.CookieContainer.Add(new Cookie(keyValuePair.Key, keyValuePair.Value, "/", reportServerHostName));
				}
			}
			request.Headers.Add(WebRequestUtil.ClientHostHeaderName, reportServerHostName);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000D250 File Offset: 0x0000B450
		public static WebRequest PrepareWebRequest(HttpWebRequest request, IIdentity userContext, string reportServerHostName)
		{
			if (request != null)
			{
				request.Headers.Add("Accept-Language", SynchronizationContext.Current.GetAcceptLanguage());
				if (userContext != null && userContext.IsOAuthAuthenticated())
				{
					if (!string.IsNullOrEmpty(SynchronizationContext.Current.GetAuthenticationHeader()))
					{
						request.Headers.Add("Authorization", SynchronizationContext.Current.GetAuthenticationHeader());
					}
					KeyValuePair<string, string> oauthSessionCookie = SynchronizationContext.Current.GetOAuthSessionCookie();
					if (!string.IsNullOrEmpty(oauthSessionCookie.Key))
					{
						request.CookieContainer = new CookieContainer();
						request.CookieContainer.Add(new Cookie(oauthSessionCookie.Key, oauthSessionCookie.Value, "/", reportServerHostName));
					}
				}
				if (!WebRequestUtil.IsClientLocal())
				{
					request.Headers.Add("RSClientNotLocalHeader", "true");
				}
				request.Headers.Add("RSViaWebApp", "true");
				if (userContext is IUserContextContainer)
				{
					IUserContextContainer userContextContainer = userContext as IUserContextContainer;
					SoapAuthenticationHelper.SetRequestProperties(request, userContextContainer, reportServerHostName);
				}
			}
			return request;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000D344 File Offset: 0x0000B544
		public static HttpWebRequest PrepareWebRequestWithCorrespondingAuthMechanism(HttpWebRequest request, IPrincipal userPrincipal, string reportServerHostName)
		{
			SoapAuthenticationHelper.PrepareWebRequest(request, userPrincipal.Identity, reportServerHostName);
			if (userPrincipal.Identity is WindowsIdentity)
			{
				request.UseDefaultCredentials = true;
			}
			else if (userPrincipal.Identity.AuthenticationType.Equals("Basic", StringComparison.OrdinalIgnoreCase))
			{
				request.UseDefaultCredentials = false;
				HttpListenerBasicIdentity httpListenerBasicIdentity = (HttpListenerBasicIdentity)userPrincipal.Identity;
				request.Credentials = new NetworkCredential(httpListenerBasicIdentity.Name, httpListenerBasicIdentity.Password);
			}
			else
			{
				if (!(userPrincipal.Identity is IUserContextContainer) && !(userPrincipal.Identity is ClaimsIdentity))
				{
					throw new NotImplementedException(string.Format("AuthenticationType {0} not supported !", userPrincipal.Identity.AuthenticationType));
				}
				request.UseDefaultCredentials = true;
				request.Credentials = CredentialCache.DefaultNetworkCredentials;
			}
			return request;
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000D408 File Offset: 0x0000B608
		private static TReturn ExecuteWithWindowsAuth<TReturn>(SoapHttpClientProtocol soapClient, IPrincipal userPrincipal, Func<TReturn> func)
		{
			soapClient.UseDefaultCredentials = true;
			TReturn treturn;
			using (((WindowsIdentity)userPrincipal.Identity).Impersonate())
			{
				treturn = func();
			}
			return treturn;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000D454 File Offset: 0x0000B654
		private static TReturn ExecuteWithCustomAuth<TReturn>(SoapHttpClientProtocol soapClient, IPrincipal userPrincipal, Func<TReturn> func)
		{
			soapClient.UseDefaultCredentials = true;
			soapClient.Credentials = CredentialCache.DefaultNetworkCredentials;
			return func();
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000D470 File Offset: 0x0000B670
		private static TReturn ExecuteWithBasicAuth<TReturn>(SoapHttpClientProtocol soapClient, IPrincipal principal, Func<TReturn> func)
		{
			soapClient.UseDefaultCredentials = false;
			HttpListenerBasicIdentity httpListenerBasicIdentity = (HttpListenerBasicIdentity)principal.Identity;
			soapClient.Credentials = new NetworkCredential(httpListenerBasicIdentity.Name, httpListenerBasicIdentity.Password);
			return func();
		}
	}
}
