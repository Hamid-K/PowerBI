using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Identity.Client.Http;
using Microsoft.Identity.Client.ManagedIdentity;
using Microsoft.Identity.Client.OAuth2;
using Microsoft.Identity.Client.Utils;

namespace Microsoft.Identity.Client
{
	// Token: 0x02000171 RID: 369
	internal class MsalServiceExceptionFactory
	{
		// Token: 0x0600123F RID: 4671 RVA: 0x0003E454 File Offset: 0x0003C654
		internal static MsalServiceException FromHttpResponse(string errorCode, string errorMessage, HttpResponse httpResponse, Exception innerException = null)
		{
			MsalServiceException ex = null;
			OAuth2ResponseBase oauth2ResponseBase = JsonHelper.TryToDeserializeFromJson<OAuth2ResponseBase>((httpResponse != null) ? httpResponse.Body : null, null);
			if (MsalServiceExceptionFactory.IsInvalidGrant((oauth2ResponseBase != null) ? oauth2ResponseBase.Error : null, (oauth2ResponseBase != null) ? oauth2ResponseBase.SubError : null) || MsalServiceExceptionFactory.IsInteractionRequired((oauth2ResponseBase != null) ? oauth2ResponseBase.Error : null))
			{
				string text;
				if (MsalServiceExceptionFactory.IsThrottled(oauth2ResponseBase))
				{
					text = "Your app has been throttled by AAD due to too many requests. To avoid this, cache your tokens see https://aka.ms/msal-net-throttling.";
				}
				else
				{
					text = errorMessage;
				}
				if (oauth2ResponseBase.Claims == null)
				{
					ex = new MsalUiRequiredException(errorCode, text, innerException);
				}
				else
				{
					text += " The returned error contains a claims challenge. For additional info on how to handle claims related to multifactor authentication, Conditional Access, and incremental consent, see https://aka.ms/msal-conditional-access-claims. If you are using the On-Behalf-Of flow, see https://aka.ms/msal-conditional-access-claims-obo for details.";
					ex = new MsalClaimsChallengeException(errorCode, text, innerException);
				}
			}
			if (string.Equals((oauth2ResponseBase != null) ? oauth2ResponseBase.Error : null, "invalid_client", StringComparison.OrdinalIgnoreCase))
			{
				ex = new MsalServiceException("invalid_client", "A configuration issue is preventing authentication - check the error message from the server for details. You can modify the configuration in the application registration portal. See https://aka.ms/msal-net-invalid-client for details.  Original exception: " + ((oauth2ResponseBase != null) ? oauth2ResponseBase.ErrorDescription : null), innerException);
			}
			if (ex == null)
			{
				ex = new MsalServiceException(errorCode, errorMessage, innerException);
			}
			MsalServiceExceptionFactory.SetHttpExceptionData(ex, httpResponse);
			ex.Claims = ((oauth2ResponseBase != null) ? oauth2ResponseBase.Claims : null);
			ex.CorrelationId = ((oauth2ResponseBase != null) ? oauth2ResponseBase.CorrelationId : null);
			ex.SubError = ((oauth2ResponseBase != null) ? oauth2ResponseBase.SubError : null);
			ex.ErrorCodes = ((oauth2ResponseBase != null) ? oauth2ResponseBase.ErrorCodes : null);
			return ex;
		}

		// Token: 0x06001240 RID: 4672 RVA: 0x0003E57B File Offset: 0x0003C77B
		private static bool IsThrottled(OAuth2ResponseBase oAuth2Response)
		{
			return oAuth2Response.ErrorDescription != null && oAuth2Response.ErrorDescription.StartsWith("AADSTS50196");
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x0003E598 File Offset: 0x0003C798
		internal static MsalServiceException FromBrokerResponse(MsalTokenResponse msalTokenResponse, string errorMessage)
		{
			string error = msalTokenResponse.Error;
			string correlationId = msalTokenResponse.CorrelationId;
			string text = (string.IsNullOrEmpty(msalTokenResponse.SubError) ? "unknown_broker_error" : msalTokenResponse.SubError);
			HttpResponse httpResponse = msalTokenResponse.HttpResponse;
			MsalServiceException ex = null;
			if (MsalServiceExceptionFactory.IsAppProtectionPolicyRequired(error, text))
			{
				ex = new IntuneAppProtectionPolicyRequiredException(error, text)
				{
					Upn = msalTokenResponse.Upn,
					AuthorityUrl = msalTokenResponse.AuthorityUrl,
					TenantId = msalTokenResponse.TenantId,
					AccountUserId = msalTokenResponse.AccountUserId
				};
			}
			if (MsalServiceExceptionFactory.IsInvalidGrant(error, text) || MsalServiceExceptionFactory.IsInteractionRequired(error))
			{
				ex = new MsalUiRequiredException(error, errorMessage);
			}
			if (string.Equals(error, "invalid_client", StringComparison.OrdinalIgnoreCase))
			{
				ex = new MsalServiceException("invalid_client", "A configuration issue is preventing authentication - check the error message from the server for details. You can modify the configuration in the application registration portal. See https://aka.ms/msal-net-invalid-client for details.  Original exception: " + errorMessage);
			}
			if (ex == null)
			{
				ex = new MsalServiceException(error, errorMessage);
			}
			MsalServiceExceptionFactory.SetHttpExceptionData(ex, httpResponse);
			ex.CorrelationId = correlationId;
			ex.SubError = text;
			return ex;
		}

		// Token: 0x06001242 RID: 4674 RVA: 0x0003E680 File Offset: 0x0003C880
		internal static MsalServiceException FromImdsResponse(string errorCode, string errorMessage, HttpResponse httpResponse, Exception innerException = null)
		{
			MsalServiceException ex = new MsalServiceException(errorCode, errorMessage, innerException);
			MsalServiceExceptionFactory.SetHttpExceptionData(ex, httpResponse);
			return ex;
		}

		// Token: 0x06001243 RID: 4675 RVA: 0x0003E694 File Offset: 0x0003C894
		internal static MsalException CreateManagedIdentityException(string errorCode, string errorMessage, Exception innerException, ManagedIdentitySource managedIdentitySource, int? statusCode)
		{
			MsalException ex;
			if (statusCode != null)
			{
				ex = new MsalServiceException(errorCode, errorMessage, statusCode.Value, innerException);
				bool flag;
				if (statusCode != null)
				{
					int valueOrDefault = statusCode.GetValueOrDefault();
					if (valueOrDefault <= 408)
					{
						if (valueOrDefault != 404 && valueOrDefault != 408)
						{
							goto IL_0062;
						}
					}
					else if (valueOrDefault != 429 && valueOrDefault != 500 && valueOrDefault - 503 > 1)
					{
						goto IL_0062;
					}
					flag = true;
					goto IL_0064;
				}
				IL_0062:
				flag = false;
				IL_0064:
				bool flag2 = flag;
				ex.IsRetryable = flag2;
			}
			else if (innerException != null)
			{
				ex = new MsalServiceException(errorCode, errorMessage, innerException);
			}
			else
			{
				ex = new MsalServiceException(errorCode, errorMessage);
			}
			return MsalServiceExceptionFactory.DecorateExceptionWithManagedIdentitySource(ex, managedIdentitySource);
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x0003E730 File Offset: 0x0003C930
		private static MsalException DecorateExceptionWithManagedIdentitySource(MsalException exception, ManagedIdentitySource managedIdentitySource)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string> { 
			{
				"ManagedIdentitySource",
				managedIdentitySource.ToString()
			} };
			exception.AdditionalExceptionData = dictionary;
			return exception;
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x0003E763 File Offset: 0x0003C963
		internal static MsalThrottledServiceException FromThrottledAuthenticationResponse(HttpResponse httpResponse)
		{
			MsalServiceException ex = new MsalServiceException("request_throttled", "Your app has been throttled by AAD due to too many requests. To avoid this, cache your tokens see https://aka.ms/msal-net-throttling.");
			MsalServiceExceptionFactory.SetHttpExceptionData(ex, httpResponse);
			return new MsalThrottledServiceException(ex);
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x0003E780 File Offset: 0x0003C980
		private static void SetHttpExceptionData(MsalServiceException ex, HttpResponse httpResponse)
		{
			ex.ResponseBody = ((httpResponse != null) ? httpResponse.Body : null);
			ex.StatusCode = (int)((httpResponse != null) ? httpResponse.StatusCode : ((HttpStatusCode)0));
			ex.Headers = ((httpResponse != null) ? httpResponse.Headers : null);
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x0003E7B8 File Offset: 0x0003C9B8
		private static bool IsInteractionRequired(string errorCode)
		{
			return string.Equals(errorCode, "interaction_required", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x0003E7C6 File Offset: 0x0003C9C6
		private static bool IsInvalidGrant(string errorCode, string subErrorCode)
		{
			return string.Equals(errorCode, "invalid_grant", StringComparison.OrdinalIgnoreCase) && MsalServiceExceptionFactory.IsInvalidGrantSubError(subErrorCode);
		}

		// Token: 0x06001249 RID: 4681 RVA: 0x0003E7DE File Offset: 0x0003C9DE
		private static bool IsAppProtectionPolicyRequired(string errorCode, string subErrorCode)
		{
			return false;
		}

		// Token: 0x0600124A RID: 4682 RVA: 0x0003E7E1 File Offset: 0x0003C9E1
		private static bool IsInvalidGrantSubError(string subError)
		{
			return string.IsNullOrEmpty(subError) || !MsalServiceExceptionFactory.s_nonUiSubErrors.Contains(subError);
		}

		// Token: 0x040006C2 RID: 1730
		private static readonly ISet<string> s_nonUiSubErrors = new HashSet<string>(new string[] { "client_mismatch", "protection_policy_required" }, StringComparer.OrdinalIgnoreCase);
	}
}
