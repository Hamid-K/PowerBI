using System;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Internal;
using Microsoft.Identity.Client.Platforms.Shared.DefaultOSBrowser;
using Microsoft.Identity.Client.PlatformsCommon.Interfaces;
using Microsoft.Identity.Client.UI;

namespace Microsoft.Identity.Client.Platforms.Shared.Desktop.OsBrowser
{
	// Token: 0x02000183 RID: 387
	internal class DefaultOsBrowserWebUi : IWebUI
	{
		// Token: 0x060012AA RID: 4778 RVA: 0x0003F6C4 File Offset: 0x0003D8C4
		public DefaultOsBrowserWebUi(IPlatformProxy proxy, ILoggerAdapter logger, SystemWebViewOptions webViewOptions, IUriInterceptor uriInterceptor = null)
		{
			if (logger == null)
			{
				throw new ArgumentNullException("logger");
			}
			this._logger = logger;
			this._webViewOptions = webViewOptions;
			if (proxy == null)
			{
				throw new ArgumentNullException("proxy");
			}
			this._platformProxy = proxy;
			this._uriInterceptor = uriInterceptor ?? new HttpListenerInterceptor(this._logger);
		}

		// Token: 0x060012AB RID: 4779 RVA: 0x0003F724 File Offset: 0x0003D924
		public async Task<AuthorizationResult> AcquireAuthorizationAsync(Uri authorizationUri, Uri redirectUri, RequestContext requestContext, CancellationToken cancellationToken)
		{
			AuthorizationResult authorizationResult;
			try
			{
				Uri uri = await this.InterceptAuthorizationUriAsync(authorizationUri, redirectUri, requestContext.ServiceBundle.Config.IsBrokerEnabled, cancellationToken).ConfigureAwait(true);
				if (!uri.Authority.Equals(redirectUri.Authority, StringComparison.OrdinalIgnoreCase) || !uri.AbsolutePath.Equals(redirectUri.AbsolutePath))
				{
					throw new MsalClientException("loopback_response_uri_mismatch", MsalErrorMessage.RedirectUriMismatch(uri.AbsolutePath, redirectUri.AbsolutePath));
				}
				authorizationResult = AuthorizationResult.FromUri(uri.OriginalString);
			}
			catch (HttpListenerException)
			{
				cancellationToken.ThrowIfCancellationRequested();
				throw;
			}
			return authorizationResult;
		}

		// Token: 0x060012AC RID: 4780 RVA: 0x0003F788 File Offset: 0x0003D988
		public Uri UpdateRedirectUri(Uri redirectUri)
		{
			if (!redirectUri.IsLoopback)
			{
				throw new MsalClientException("loopback_redirect_uri", "Only loopback redirect uri is supported, but " + redirectUri.AbsoluteUri + " was found. Configure http://localhost or http://localhost:port both during app registration and when you create the PublicClientApplication object. See https://aka.ms/msal-net-os-browser for details");
			}
			if (redirectUri.Scheme != "http")
			{
				throw new MsalClientException("loopback_redirect_uri", "Only http uri scheme is supported, but " + redirectUri.Scheme + " was found. Configure http://localhost or http://localhost:port both during app registration and when you create the PublicClientApplication object. See https://aka.ms/msal-net-os-browser for details");
			}
			return DefaultOsBrowserWebUi.FindFreeLocalhostRedirectUri(redirectUri);
		}

		// Token: 0x060012AD RID: 4781 RVA: 0x0003F7F8 File Offset: 0x0003D9F8
		private static Uri FindFreeLocalhostRedirectUri(Uri redirectUri)
		{
			if (redirectUri.Port > 0 && redirectUri.Port != 80)
			{
				return redirectUri;
			}
			TcpListener tcpListener = new TcpListener(IPAddress.Loopback, 0);
			Uri uri;
			try
			{
				tcpListener.Start();
				uri = new Uri("http://localhost:" + ((IPEndPoint)tcpListener.LocalEndpoint).Port.ToString());
			}
			finally
			{
				if (tcpListener != null)
				{
					tcpListener.Stop();
				}
			}
			return uri;
		}

		// Token: 0x060012AE RID: 4782 RVA: 0x0003F874 File Offset: 0x0003DA74
		private async Task<Uri> InterceptAuthorizationUriAsync(Uri authorizationUri, Uri redirectUri, bool isBrokerConfigured, CancellationToken cancellationToken)
		{
			Func<Uri, Task> func = (Uri u) => this._platformProxy.StartDefaultOsBrowserAsync(u.AbsoluteUri, isBrokerConfigured);
			SystemWebViewOptions webViewOptions = this._webViewOptions;
			Func<Uri, Task> func2 = ((webViewOptions != null) ? webViewOptions.OpenBrowserAsync : null) ?? func;
			cancellationToken.ThrowIfCancellationRequested();
			await func2(authorizationUri).ConfigureAwait(false);
			cancellationToken.ThrowIfCancellationRequested();
			return await this._uriInterceptor.ListenToSingleRequestAndRespondAsync(redirectUri.Port, redirectUri.AbsolutePath, new Func<Uri, MessageAndHttpCode>(this.GetResponseMessage), cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x060012AF RID: 4783 RVA: 0x0003F8D8 File Offset: 0x0003DAD8
		internal MessageAndHttpCode GetResponseMessage(Uri authCodeUri)
		{
			AuthorizationResult authorizationResult = AuthorizationResult.FromUri(authCodeUri.OriginalString);
			if (!string.IsNullOrEmpty(authorizationResult.Error))
			{
				this._logger.Warning("Default OS Browser intercepted an Uri with an error: " + authorizationResult.Error + " " + authorizationResult.ErrorDescription);
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				SystemWebViewOptions webViewOptions = this._webViewOptions;
				string text = string.Format(invariantCulture, ((webViewOptions != null) ? webViewOptions.HtmlMessageError : null) ?? "<html>\r\n  <head><title>Authentication Failed</title></head>\r\n  <body>\r\n    Authentication failed. You can return to the application. Feel free to close this browser tab.\r\n</br></br></br></br>\r\n    Error details: error {0} error_description: {1}\r\n  </body>\r\n</html>", authorizationResult.Error, authorizationResult.ErrorDescription);
				SystemWebViewOptions webViewOptions2 = this._webViewOptions;
				return DefaultOsBrowserWebUi.GetMessage((webViewOptions2 != null) ? webViewOptions2.BrowserRedirectError : null, text);
			}
			SystemWebViewOptions webViewOptions3 = this._webViewOptions;
			Uri uri = ((webViewOptions3 != null) ? webViewOptions3.BrowserRedirectSuccess : null);
			SystemWebViewOptions webViewOptions4 = this._webViewOptions;
			return DefaultOsBrowserWebUi.GetMessage(uri, ((webViewOptions4 != null) ? webViewOptions4.HtmlMessageSuccess : null) ?? "<html>\r\n  <head><title>Authentication Complete</title></head>\r\n  <body>\r\n    Authentication complete. You can return to the application. Feel free to close this browser tab.\r\n  </body>\r\n</html>");
		}

		// Token: 0x060012B0 RID: 4784 RVA: 0x0003F9A1 File Offset: 0x0003DBA1
		private static MessageAndHttpCode GetMessage(Uri redirectUri, string message)
		{
			if (redirectUri != null)
			{
				return new MessageAndHttpCode(HttpStatusCode.Found, redirectUri.ToString());
			}
			return new MessageAndHttpCode(HttpStatusCode.OK, message);
		}

		// Token: 0x040006F3 RID: 1779
		internal const string DefaultSuccessHtml = "<html>\r\n  <head><title>Authentication Complete</title></head>\r\n  <body>\r\n    Authentication complete. You can return to the application. Feel free to close this browser tab.\r\n  </body>\r\n</html>";

		// Token: 0x040006F4 RID: 1780
		internal const string DefaultFailureHtml = "<html>\r\n  <head><title>Authentication Failed</title></head>\r\n  <body>\r\n    Authentication failed. You can return to the application. Feel free to close this browser tab.\r\n</br></br></br></br>\r\n    Error details: error {0} error_description: {1}\r\n  </body>\r\n</html>";

		// Token: 0x040006F5 RID: 1781
		private readonly IUriInterceptor _uriInterceptor;

		// Token: 0x040006F6 RID: 1782
		private readonly ILoggerAdapter _logger;

		// Token: 0x040006F7 RID: 1783
		private readonly SystemWebViewOptions _webViewOptions;

		// Token: 0x040006F8 RID: 1784
		private readonly IPlatformProxy _platformProxy;
	}
}
