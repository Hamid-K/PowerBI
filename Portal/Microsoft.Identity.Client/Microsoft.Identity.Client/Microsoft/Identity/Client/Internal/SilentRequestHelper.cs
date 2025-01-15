using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.Cache.Items;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Internal.Requests;
using Microsoft.Identity.Client.OAuth2;
using Microsoft.Identity.Client.TelemetryCore.Internal.Events;

namespace Microsoft.Identity.Client.Internal
{
	// Token: 0x0200023D RID: 573
	internal static class SilentRequestHelper
	{
		// Token: 0x06001737 RID: 5943 RVA: 0x0004CF60 File Offset: 0x0004B160
		internal static async Task<MsalTokenResponse> RefreshAccessTokenAsync(MsalRefreshTokenCacheItem msalRefreshTokenItem, RequestBase request, AuthenticationRequestParameters authenticationRequestParameters, CancellationToken cancellationToken)
		{
			authenticationRequestParameters.RequestContext.Logger.Verbose(() => "Refreshing access token...");
			await authenticationRequestParameters.AuthorityManager.RunInstanceDiscoveryAndValidationAsync().ConfigureAwait(false);
			Dictionary<string, string> bodyParameters = SilentRequestHelper.GetBodyParameters(msalRefreshTokenItem.Secret);
			MsalTokenResponse msalTokenResponse = await request.SendTokenRequestAsync(bodyParameters, cancellationToken).ConfigureAwait(false);
			if (msalTokenResponse.RefreshToken == null)
			{
				msalTokenResponse.RefreshToken = msalRefreshTokenItem.Secret;
				authenticationRequestParameters.RequestContext.Logger.Warning("Refresh token was missing from the token refresh response, so the refresh token in the request is returned instead. ");
			}
			return msalTokenResponse;
		}

		// Token: 0x06001738 RID: 5944 RVA: 0x0004CFBB File Offset: 0x0004B1BB
		private static Dictionary<string, string> GetBodyParameters(string refreshTokenSecret)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary["client_info"] = "1";
			dictionary["grant_type"] = "refresh_token";
			dictionary["refresh_token"] = refreshTokenSecret;
			return dictionary;
		}

		// Token: 0x06001739 RID: 5945 RVA: 0x0004CFF0 File Offset: 0x0004B1F0
		internal static bool NeedsRefresh(MsalAccessTokenCacheItem oldAccessToken)
		{
			DateTimeOffset? dateTimeOffset;
			return SilentRequestHelper.NeedsRefresh(oldAccessToken, out dateTimeOffset);
		}

		// Token: 0x0600173A RID: 5946 RVA: 0x0004D005 File Offset: 0x0004B205
		internal static bool NeedsRefresh(MsalAccessTokenCacheItem oldAccessToken, out DateTimeOffset? refreshOnWithJitter)
		{
			refreshOnWithJitter = SilentRequestHelper.GetRefreshOnWithJitter(oldAccessToken);
			return refreshOnWithJitter != null && refreshOnWithJitter.Value < DateTimeOffset.UtcNow;
		}

		// Token: 0x0600173B RID: 5947 RVA: 0x0004D030 File Offset: 0x0004B230
		internal static void ProcessFetchInBackground(MsalAccessTokenCacheItem oldAccessToken, Func<Task<AuthenticationResult>> fetchAction, ILoggerAdapter logger, IServiceBundle serviceBundle, ApiEvent.ApiIds apiId)
		{
			SilentRequestHelper.<>c__DisplayClass8_0 CS$<>8__locals1 = new SilentRequestHelper.<>c__DisplayClass8_0();
			CS$<>8__locals1.fetchAction = fetchAction;
			CS$<>8__locals1.serviceBundle = serviceBundle;
			CS$<>8__locals1.apiId = apiId;
			CS$<>8__locals1.logger = logger;
			Task.Run(delegate
			{
				SilentRequestHelper.<>c__DisplayClass8_0.<<ProcessFetchInBackground>b__0>d <<ProcessFetchInBackground>b__0>d;
				<<ProcessFetchInBackground>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
				<<ProcessFetchInBackground>b__0>d.<>4__this = CS$<>8__locals1;
				<<ProcessFetchInBackground>b__0>d.<>1__state = -1;
				<<ProcessFetchInBackground>b__0>d.<>t__builder.Start<SilentRequestHelper.<>c__DisplayClass8_0.<<ProcessFetchInBackground>b__0>d>(ref <<ProcessFetchInBackground>b__0>d);
				return <<ProcessFetchInBackground>b__0>d.<>t__builder.Task;
			});
		}

		// Token: 0x0600173C RID: 5948 RVA: 0x0004D068 File Offset: 0x0004B268
		private static DateTimeOffset? GetRefreshOnWithJitter(MsalAccessTokenCacheItem msalAccessTokenCacheItem)
		{
			if (msalAccessTokenCacheItem.RefreshOn != null)
			{
				int num = SilentRequestHelper.s_random.Next(-300, 300);
				return new DateTimeOffset?(msalAccessTokenCacheItem.RefreshOn.Value + TimeSpan.FromSeconds((double)num));
			}
			return null;
		}

		// Token: 0x04000A1E RID: 2590
		internal const string MamEnrollmentIdKey = "microsoft_enrollment_id";

		// Token: 0x04000A1F RID: 2591
		internal const string ProactiveRefreshServiceError = "Proactive token refresh failed with MsalServiceException.";

		// Token: 0x04000A20 RID: 2592
		internal const string ProactiveRefreshGeneralError = "Proactive token refresh failed with exception.";

		// Token: 0x04000A21 RID: 2593
		internal const string ProactiveRefreshCancellationError = "Proactive token refresh was canceled.";

		// Token: 0x04000A22 RID: 2594
		private static Random s_random = new Random();
	}
}
