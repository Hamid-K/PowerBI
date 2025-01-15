using System;
using Microsoft.Identity.Client.AuthScheme;
using Microsoft.Identity.Client.Cache.Items;
using Microsoft.Identity.Client.Internal.Requests;
using Microsoft.Identity.Client.TelemetryCore.Internal.Events;

namespace Microsoft.Identity.Client.Cache
{
	// Token: 0x020002A7 RID: 679
	internal static class CacheKeyFactory
	{
		// Token: 0x0600198C RID: 6540 RVA: 0x00053B74 File Offset: 0x00051D74
		public static string GetKeyFromRequest(AuthenticationRequestParameters requestParameters)
		{
			string text;
			if (CacheKeyFactory.GetOboOrAppKey(requestParameters, out text))
			{
				return text;
			}
			if (requestParameters.ApiId == ApiEvent.ApiIds.AcquireTokenSilent || requestParameters.ApiId == ApiEvent.ApiIds.RemoveAccount)
			{
				IAccount account = requestParameters.Account;
				if (account == null)
				{
					return null;
				}
				AccountId homeAccountId = account.HomeAccountId;
				if (homeAccountId == null)
				{
					return null;
				}
				return homeAccountId.Identifier;
			}
			else
			{
				if (requestParameters.ApiId == ApiEvent.ApiIds.GetAccountById)
				{
					return requestParameters.HomeAccountId;
				}
				return null;
			}
		}

		// Token: 0x0600198D RID: 6541 RVA: 0x00053BDC File Offset: 0x00051DDC
		public static string GetExternalCacheKeyFromResponse(AuthenticationRequestParameters requestParameters, string homeAccountIdFromResponse)
		{
			string text;
			if (CacheKeyFactory.GetOboOrAppKey(requestParameters, out text))
			{
				return text;
			}
			if (requestParameters.AppConfig.IsConfidentialClient || requestParameters.ApiId == ApiEvent.ApiIds.AcquireTokenSilent)
			{
				return homeAccountIdFromResponse;
			}
			return null;
		}

		// Token: 0x0600198E RID: 6542 RVA: 0x00053C12 File Offset: 0x00051E12
		public static string GetInternalPartitionKeyFromResponse(AuthenticationRequestParameters requestParameters, string homeAccountIdFromResponse)
		{
			return CacheKeyFactory.GetExternalCacheKeyFromResponse(requestParameters, homeAccountIdFromResponse) ?? homeAccountIdFromResponse;
		}

		// Token: 0x0600198F RID: 6543 RVA: 0x00053C20 File Offset: 0x00051E20
		private static bool GetOboOrAppKey(AuthenticationRequestParameters requestParameters, out string key)
		{
			if (ApiEvent.IsOnBehalfOfRequest(requestParameters.ApiId))
			{
				key = CacheKeyFactory.GetOboKey(requestParameters.LongRunningOboCacheKey, requestParameters.UserAssertion);
				return true;
			}
			if (requestParameters.ApiId == ApiEvent.ApiIds.AcquireTokenForClient || requestParameters.ApiId == ApiEvent.ApiIds.AcquireTokenForSystemAssignedManagedIdentity || requestParameters.ApiId == ApiEvent.ApiIds.AcquireTokenForUserAssignedManagedIdentity)
			{
				string text = requestParameters.Authority.TenantId ?? "";
				string clientId = requestParameters.AppConfig.ClientId;
				string text2 = text;
				IAuthenticationScheme authenticationScheme = requestParameters.AuthenticationScheme;
				key = CacheKeyFactory.GetClientCredentialKey(clientId, text2, (authenticationScheme != null) ? authenticationScheme.KeyId : null);
				return true;
			}
			key = null;
			return false;
		}

		// Token: 0x06001990 RID: 6544 RVA: 0x00053CB6 File Offset: 0x00051EB6
		public static string GetClientCredentialKey(string clientId, string tenantId, string popKid)
		{
			return string.Concat(new string[] { popKid, clientId, "_", tenantId, "_AppTokenCache" });
		}

		// Token: 0x06001991 RID: 6545 RVA: 0x00053CDF File Offset: 0x00051EDF
		public static string GetOboKey(string oboCacheKey, UserAssertion userAssertion)
		{
			if (!string.IsNullOrEmpty(oboCacheKey))
			{
				return oboCacheKey;
			}
			if (userAssertion == null)
			{
				return null;
			}
			return userAssertion.AssertionHash;
		}

		// Token: 0x06001992 RID: 6546 RVA: 0x00053CF6 File Offset: 0x00051EF6
		public static string GetOboKey(string oboCacheKey, string homeAccountId)
		{
			if (string.IsNullOrEmpty(oboCacheKey))
			{
				return homeAccountId;
			}
			return oboCacheKey;
		}

		// Token: 0x06001993 RID: 6547 RVA: 0x00053D03 File Offset: 0x00051F03
		public static string GetKeyFromCachedItem(MsalAccessTokenCacheItem accessTokenCacheItem)
		{
			return CacheKeyFactory.GetOboKey(accessTokenCacheItem.OboCacheKey, accessTokenCacheItem.HomeAccountId);
		}

		// Token: 0x06001994 RID: 6548 RVA: 0x00053D16 File Offset: 0x00051F16
		public static string GetKeyFromCachedItem(MsalRefreshTokenCacheItem refreshTokenCacheItem)
		{
			return CacheKeyFactory.GetOboKey(refreshTokenCacheItem.OboCacheKey, refreshTokenCacheItem.HomeAccountId);
		}

		// Token: 0x06001995 RID: 6549 RVA: 0x00053D29 File Offset: 0x00051F29
		public static string GetIdTokenKeyFromCachedItem(MsalAccessTokenCacheItem accessTokenCacheItem)
		{
			return accessTokenCacheItem.HomeAccountId;
		}

		// Token: 0x06001996 RID: 6550 RVA: 0x00053D31 File Offset: 0x00051F31
		public static string GetKeyFromAccount(MsalAccountCacheItem accountCacheItem)
		{
			return accountCacheItem.HomeAccountId;
		}

		// Token: 0x06001997 RID: 6551 RVA: 0x00053D39 File Offset: 0x00051F39
		public static string GetKeyFromCachedItem(MsalIdTokenCacheItem idTokenCacheItem)
		{
			return idTokenCacheItem.HomeAccountId;
		}

		// Token: 0x06001998 RID: 6552 RVA: 0x00053D41 File Offset: 0x00051F41
		public static string GetKeyFromCachedItem(MsalAccountCacheItem accountCacheItem)
		{
			return accountCacheItem.HomeAccountId;
		}
	}
}
