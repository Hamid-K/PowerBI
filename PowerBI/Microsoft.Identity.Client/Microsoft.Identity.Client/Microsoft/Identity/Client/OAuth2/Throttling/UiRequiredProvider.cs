using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Internal.Requests;
using Microsoft.Identity.Client.PlatformsCommon.Interfaces;
using Microsoft.Identity.Client.TelemetryCore.Internal.Events;

namespace Microsoft.Identity.Client.OAuth2.Throttling
{
	// Token: 0x0200021A RID: 538
	internal class UiRequiredProvider : IThrottlingProvider
	{
		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x0600164C RID: 5708 RVA: 0x00049C03 File Offset: 0x00047E03
		internal ThrottlingCache ThrottlingCache { get; }

		// Token: 0x0600164D RID: 5709 RVA: 0x00049C0C File Offset: 0x00047E0C
		public UiRequiredProvider()
		{
			this.ThrottlingCache = new ThrottlingCache(null);
		}

		// Token: 0x0600164E RID: 5710 RVA: 0x00049C34 File Offset: 0x00047E34
		public void RecordException(AuthenticationRequestParameters requestParams, IReadOnlyDictionary<string, string> bodyParams, MsalServiceException ex)
		{
			if (ex is MsalUiRequiredException && UiRequiredProvider.IsRequestSupported(requestParams))
			{
				ILoggerAdapter logger = requestParams.RequestContext.Logger;
				logger.Info(() => "[Throttling] MsalUiRequiredException encountered - " + string.Format("throttling for {0} seconds. ", UiRequiredProvider.s_uiRequiredExpiration.TotalSeconds));
				string requestStrictThumbprint = UiRequiredProvider.GetRequestStrictThumbprint(bodyParams, requestParams.AuthorityInfo.CanonicalAuthority.ToString(), requestParams.RequestContext.ServiceBundle.PlatformProxy.CryptographyManager);
				ThrottlingCacheEntry throttlingCacheEntry = new ThrottlingCacheEntry(ex, UiRequiredProvider.s_uiRequiredExpiration);
				this.ThrottlingCache.AddAndCleanup(requestStrictThumbprint, throttlingCacheEntry, logger);
			}
		}

		// Token: 0x0600164F RID: 5711 RVA: 0x00049CC8 File Offset: 0x00047EC8
		public void ResetCache()
		{
			this.ThrottlingCache.Clear();
		}

		// Token: 0x06001650 RID: 5712 RVA: 0x00049CD8 File Offset: 0x00047ED8
		public void TryThrottle(AuthenticationRequestParameters requestParams, IReadOnlyDictionary<string, string> bodyParams)
		{
			if (!this.ThrottlingCache.IsEmpty() && UiRequiredProvider.IsRequestSupported(requestParams))
			{
				ILoggerAdapter logger = requestParams.RequestContext.Logger;
				string requestStrictThumbprint = UiRequiredProvider.GetRequestStrictThumbprint(bodyParams, requestParams.AuthorityInfo.CanonicalAuthority.ToString(), requestParams.RequestContext.ServiceBundle.PlatformProxy.CryptographyManager);
				this.TryThrowException(requestStrictThumbprint, logger);
			}
		}

		// Token: 0x06001651 RID: 5713 RVA: 0x00049D3C File Offset: 0x00047F3C
		private void TryThrowException(string thumbprint, ILoggerAdapter logger)
		{
			MsalServiceException ex;
			if (this.ThrottlingCache.TryGetOrRemoveExpired(thumbprint, logger, out ex))
			{
				MsalUiRequiredException ex2 = ex as MsalUiRequiredException;
				if (ex2 != null)
				{
					logger.WarningPii("[Throttling] Exception thrown because of throttling rule UiRequired - thumbprint: " + thumbprint, "[Throttling] Exception thrown because of throttling rule UiRequired ");
					throw new MsalThrottledUiRequiredException(ex2);
				}
			}
		}

		// Token: 0x06001652 RID: 5714 RVA: 0x00049D81 File Offset: 0x00047F81
		private static bool IsRequestSupported(AuthenticationRequestParameters requestParams)
		{
			return !requestParams.AppConfig.IsConfidentialClient && requestParams.ApiId == ApiEvent.ApiIds.AcquireTokenSilent;
		}

		// Token: 0x06001653 RID: 5715 RVA: 0x00049DA0 File Offset: 0x00047FA0
		private static string GetRequestStrictThumbprint(IReadOnlyDictionary<string, string> bodyParams, string authority, ICryptographyManager crypto)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text;
			if (bodyParams.TryGetValue("client_id", out text))
			{
				stringBuilder.Append(text);
				stringBuilder.Append('.');
			}
			stringBuilder.Append(authority);
			stringBuilder.Append('.');
			string text2;
			if (bodyParams.TryGetValue("scope", out text2))
			{
				stringBuilder.Append(text2);
				stringBuilder.Append('.');
			}
			string text3;
			if (bodyParams.TryGetValue("refresh_token", out text3) && !string.IsNullOrEmpty(text3))
			{
				stringBuilder.Append(crypto.CreateSha256Hash(text3));
				stringBuilder.Append('.');
			}
			string text4;
			if (bodyParams.TryGetValue("microsoft_enrollment_id", out text4))
			{
				stringBuilder.Append(crypto.CreateSha256Hash(text4));
				stringBuilder.Append('.');
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000978 RID: 2424
		internal static readonly TimeSpan s_uiRequiredExpiration = TimeSpan.FromSeconds(120.0);
	}
}
