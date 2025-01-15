using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Extensibility;
using Microsoft.Identity.Client.Http;
using Microsoft.Identity.Client.Internal;

namespace Microsoft.Identity.Client.UI
{
	// Token: 0x020001DA RID: 474
	internal class CustomWebUiHandler : IWebUI
	{
		// Token: 0x06001497 RID: 5271 RVA: 0x00045B3F File Offset: 0x00043D3F
		public CustomWebUiHandler(ICustomWebUi customWebUi)
		{
			this._customWebUi = customWebUi;
		}

		// Token: 0x06001498 RID: 5272 RVA: 0x00045B50 File Offset: 0x00043D50
		public async Task<AuthorizationResult> AcquireAuthorizationAsync(Uri authorizationUri, Uri redirectUri, RequestContext requestContext, CancellationToken cancellationToken)
		{
			requestContext.Logger.Info("Using CustomWebUi to acquire the authorization code");
			AuthorizationResult authorizationResult;
			try
			{
				requestContext.Logger.InfoPii(() => LogMessages.CustomWebUiCallingAcquireAuthorizationCodePii(authorizationUri, redirectUri), () => "Calling CustomWebUi.AcquireAuthorizationCode");
				Uri uri = await this._customWebUi.AcquireAuthorizationCodeAsync(authorizationUri, redirectUri, cancellationToken).ConfigureAwait(false);
				if (uri == null || string.IsNullOrWhiteSpace(uri.Query))
				{
					throw new MsalClientException("custom_webui_returned_invalid_uri", "ICustomWebUi returned an invalid URI - it is empty or has no query. ");
				}
				if (!uri.Authority.Equals(redirectUri.Authority, StringComparison.OrdinalIgnoreCase) || !uri.AbsolutePath.Equals(redirectUri.AbsolutePath))
				{
					throw new MsalClientException("custom_webui_invalid_mismatch", MsalErrorMessage.RedirectUriMismatch(uri.AbsolutePath, redirectUri.AbsolutePath));
				}
				requestContext.Logger.Info("Redirect Uri was matched.  Returning success from CustomWebUiHandler.");
				authorizationResult = AuthorizationResult.FromUri(uri.OriginalString);
			}
			catch (OperationCanceledException)
			{
				requestContext.Logger.Info("CustomWebUi AcquireAuthorizationCode was canceled");
				authorizationResult = AuthorizationResult.FromStatus(AuthorizationStatus.UserCancel);
			}
			catch (Exception ex)
			{
				requestContext.Logger.WarningPiiWithPrefix(ex, "CustomWebUi AcquireAuthorizationCode failed. ");
				throw;
			}
			return authorizationResult;
		}

		// Token: 0x06001499 RID: 5273 RVA: 0x00045BB4 File Offset: 0x00043DB4
		public Uri UpdateRedirectUri(Uri redirectUri)
		{
			RedirectUriHelper.Validate(redirectUri, false);
			return redirectUri;
		}

		// Token: 0x04000869 RID: 2153
		private readonly ICustomWebUi _customWebUi;
	}
}
