using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Http;
using Microsoft.Identity.Client.Internal;
using Microsoft.Identity.Client.TelemetryCore.Internal.Events;
using Microsoft.Identity.Client.Utils;

namespace Microsoft.Identity.Client.Region
{
	// Token: 0x02000268 RID: 616
	internal sealed class RegionManager : IRegionManager
	{
		// Token: 0x06001852 RID: 6226 RVA: 0x00050C94 File Offset: 0x0004EE94
		public RegionManager(IHttpManager httpManager, int imdsCallTimeout = 2000, bool shouldClearStaticCache = false)
		{
			this._httpManager = httpManager;
			this._imdsCallTimeoutMs = imdsCallTimeout;
			if (shouldClearStaticCache)
			{
				RegionManager.s_failedAutoDiscovery = false;
				RegionManager.s_autoDiscoveredRegion = null;
				RegionManager.s_regionDiscoveryDetails = null;
			}
		}

		// Token: 0x06001853 RID: 6227 RVA: 0x00050CC0 File Offset: 0x0004EEC0
		public async Task<string> GetAzureRegionAsync(RequestContext requestContext)
		{
			string azureRegionConfig = requestContext.ServiceBundle.Config.AzureRegion;
			ILoggerAdapter logger = requestContext.Logger;
			string text;
			if (string.IsNullOrEmpty(azureRegionConfig))
			{
				logger.Verbose(() => "[Region discovery] WithAzureRegion not configured. ");
				text = null;
			}
			else
			{
				RegionManager.RegionInfo regionInfo = await this.DiscoverAndCacheAsync(logger, requestContext.UserCancellationToken).ConfigureAwait(false);
				RegionManager.RegionInfo discoveredRegion = regionInfo;
				RegionManager.RecordTelemetry(requestContext.ApiEvent, azureRegionConfig, discoveredRegion);
				if (RegionManager.IsAutoDiscoveryRequested(azureRegionConfig))
				{
					if (discoveredRegion.RegionSource != RegionAutodetectionSource.FailedAutoDiscovery)
					{
						logger.Verbose(() => "[Region discovery] Discovered Region " + discoveredRegion.Region);
						requestContext.ApiEvent.RegionUsed = discoveredRegion.Region;
						requestContext.ApiEvent.AutoDetectedRegion = discoveredRegion.Region;
						text = discoveredRegion.Region;
					}
					else
					{
						logger.Verbose(() => "[Region discovery] " + RegionManager.s_regionDiscoveryDetails);
						requestContext.ApiEvent.RegionDiscoveryFailureReason = RegionManager.s_regionDiscoveryDetails;
						text = null;
					}
				}
				else
				{
					logger.Info(() => "[Region discovery] Returning user provided region: " + azureRegionConfig + ".");
					text = azureRegionConfig;
				}
			}
			return text;
		}

		// Token: 0x06001854 RID: 6228 RVA: 0x00050D0B File Offset: 0x0004EF0B
		private static bool IsAutoDiscoveryRequested(string azureRegionConfig)
		{
			return string.Equals(azureRegionConfig, "TryAutoDetect");
		}

		// Token: 0x06001855 RID: 6229 RVA: 0x00050D18 File Offset: 0x0004EF18
		private static void RecordTelemetry(ApiEvent apiEvent, string azureRegionConfig, RegionManager.RegionInfo discoveredRegion)
		{
			if (RegionManager.IsTelemetryRecorded(apiEvent))
			{
				return;
			}
			bool flag = RegionManager.IsAutoDiscoveryRequested(azureRegionConfig);
			apiEvent.RegionAutodetectionSource = discoveredRegion.RegionSource;
			if (flag)
			{
				apiEvent.RegionUsed = discoveredRegion.Region;
				apiEvent.RegionOutcome = ((discoveredRegion.RegionSource == RegionAutodetectionSource.FailedAutoDiscovery) ? RegionOutcome.FallbackToGlobal : RegionOutcome.AutodetectSuccess);
				return;
			}
			apiEvent.RegionUsed = azureRegionConfig;
			apiEvent.RegionDiscoveryFailureReason = discoveredRegion.RegionDetails;
			if (discoveredRegion.RegionSource == RegionAutodetectionSource.FailedAutoDiscovery)
			{
				apiEvent.RegionOutcome = RegionOutcome.UserProvidedAutodetectionFailed;
			}
			if (!string.IsNullOrEmpty(discoveredRegion.Region))
			{
				apiEvent.RegionOutcome = (string.Equals(discoveredRegion.Region, azureRegionConfig, StringComparison.OrdinalIgnoreCase) ? RegionOutcome.UserProvidedValid : RegionOutcome.UserProvidedInvalid);
			}
		}

		// Token: 0x06001856 RID: 6230 RVA: 0x00050DAB File Offset: 0x0004EFAB
		private static bool IsTelemetryRecorded(ApiEvent apiEvent)
		{
			return !string.IsNullOrEmpty(apiEvent.RegionUsed) || apiEvent.RegionAutodetectionSource != RegionAutodetectionSource.None || apiEvent.RegionOutcome > RegionOutcome.None;
		}

		// Token: 0x06001857 RID: 6231 RVA: 0x00050DD0 File Offset: 0x0004EFD0
		private async Task<RegionManager.RegionInfo> DiscoverAndCacheAsync(ILoggerAdapter logger, CancellationToken requestCancellationToken)
		{
			RegionManager.RegionInfo cachedRegion = RegionManager.GetCachedRegion(logger);
			RegionManager.RegionInfo regionInfo;
			if (cachedRegion != null)
			{
				regionInfo = cachedRegion;
			}
			else
			{
				regionInfo = await this.DiscoverAsync(logger, requestCancellationToken).ConfigureAwait(false);
			}
			return regionInfo;
		}

		// Token: 0x06001858 RID: 6232 RVA: 0x00050E24 File Offset: 0x0004F024
		private async Task<RegionManager.RegionInfo> DiscoverAsync(ILoggerAdapter logger, CancellationToken requestCancellationToken)
		{
			RegionManager.RegionInfo result = null;
			await RegionManager._lockDiscover.WaitAsync(requestCancellationToken).ConfigureAwait(false);
			try
			{
				RegionManager.RegionInfo cachedRegion = RegionManager.GetCachedRegion(logger);
				if (cachedRegion != null)
				{
					result = cachedRegion;
				}
				else
				{
					try
					{
						RegionManager.<>c__DisplayClass15_0 CS$<>8__locals1 = new RegionManager.<>c__DisplayClass15_0();
						RegionManager.<>c__DisplayClass15_0 CS$<>8__locals2 = CS$<>8__locals1;
						string environmentVariable = Environment.GetEnvironmentVariable("REGION_NAME");
						CS$<>8__locals2.region = ((environmentVariable != null) ? environmentVariable.Replace(" ", string.Empty).ToLowerInvariant() : null);
						if (RegionManager.ValidateRegion(CS$<>8__locals1.region, "REGION_NAME env variable", logger))
						{
							logger.Info(() => "[Region discovery] Region found in environment variable: " + CS$<>8__locals1.region + ".");
							result = new RegionManager.RegionInfo(CS$<>8__locals1.region, RegionAutodetectionSource.EnvVariable, null);
						}
						else
						{
							Dictionary<string, string> headers = new Dictionary<string, string> { { "Metadata", "true" } };
							Uri imdsUri = RegionManager.BuildImdsUri("2020-06-01");
							HttpResponse httpResponse = await this._httpManager.SendGetAsync(imdsUri, headers, logger, false, this.GetCancellationToken(requestCancellationToken)).ConfigureAwait(false);
							if (httpResponse.StatusCode == HttpStatusCode.BadRequest)
							{
								string text = await this.GetImdsUriApiVersionAsync(logger, headers, requestCancellationToken).ConfigureAwait(false);
								imdsUri = RegionManager.BuildImdsUri(text);
								httpResponse = await this._httpManager.SendGetAsync(RegionManager.BuildImdsUri(text), headers, logger, false, this.GetCancellationToken(requestCancellationToken)).ConfigureAwait(false);
							}
							if (httpResponse.StatusCode == HttpStatusCode.OK && !httpResponse.Body.IsNullOrEmpty<char>())
							{
								CS$<>8__locals1.region = httpResponse.Body;
								if (RegionManager.ValidateRegion(CS$<>8__locals1.region, "IMDS call to " + imdsUri.AbsoluteUri, logger))
								{
									logger.Info(() => string.Format("[Region discovery] Call to local IMDS succeeded. Region: {0}. {1}", CS$<>8__locals1.region, DateTime.UtcNow));
									result = new RegionManager.RegionInfo(CS$<>8__locals1.region, RegionAutodetectionSource.Imds, null);
								}
							}
							else
							{
								RegionManager.s_regionDiscoveryDetails = string.Format("Call to local IMDS failed with status code {0} or an empty response. {1}", httpResponse.StatusCode, DateTime.UtcNow);
								logger.Error("[Region discovery] " + RegionManager.s_regionDiscoveryDetails);
							}
							headers = null;
							imdsUri = null;
						}
						CS$<>8__locals1 = null;
					}
					catch (Exception ex)
					{
						MsalServiceException ex2 = ex as MsalServiceException;
						if (ex2 != null && "request_timeout".Equals((ex2 != null) ? ex2.ErrorCode : null))
						{
							RegionManager.s_regionDiscoveryDetails = string.Format("Call to local IMDS timed out after {0}.", this._imdsCallTimeoutMs);
							logger.Error("[Region discovery] " + RegionManager.s_regionDiscoveryDetails + ".");
						}
						else
						{
							RegionManager.s_regionDiscoveryDetails = string.Format("IMDS call failed with exception {0}. {1}", ex, DateTime.UtcNow);
							logger.Error("[Region discovery] " + RegionManager.s_regionDiscoveryDetails);
						}
					}
				}
				if (result == null)
				{
					result = new RegionManager.RegionInfo(null, RegionAutodetectionSource.FailedAutoDiscovery, RegionManager.s_regionDiscoveryDetails);
				}
			}
			finally
			{
				RegionManager.s_failedAutoDiscovery = result.RegionSource == RegionAutodetectionSource.FailedAutoDiscovery;
				RegionManager.s_autoDiscoveredRegion = result.Region;
				RegionManager.s_regionDiscoveryDetails = result.RegionDetails;
				RegionManager._lockDiscover.Release();
			}
			return result;
		}

		// Token: 0x06001859 RID: 6233 RVA: 0x00050E78 File Offset: 0x0004F078
		private static RegionManager.RegionInfo GetCachedRegion(ILoggerAdapter logger)
		{
			if (RegionManager.s_failedAutoDiscovery)
			{
				string autoDiscoveryError = string.Format("[Region discovery] Auto-discovery failed in the past. Not trying again. {0}. {1}", RegionManager.s_regionDiscoveryDetails, DateTime.UtcNow);
				logger.Verbose(() => autoDiscoveryError);
				return new RegionManager.RegionInfo(null, RegionAutodetectionSource.FailedAutoDiscovery, autoDiscoveryError);
			}
			if (!RegionManager.s_failedAutoDiscovery && !string.IsNullOrEmpty(RegionManager.s_autoDiscoveredRegion))
			{
				logger.Info(() => "[Region discovery] Auto-discovery already ran and found " + RegionManager.s_autoDiscoveredRegion + ".");
				return new RegionManager.RegionInfo(RegionManager.s_autoDiscoveredRegion, RegionAutodetectionSource.Cache, null);
			}
			logger.Verbose(() => "[Region discovery] Auto-discovery did not run yet.");
			return null;
		}

		// Token: 0x0600185A RID: 6234 RVA: 0x00050F3C File Offset: 0x0004F13C
		private static bool ValidateRegion(string region, string source, ILoggerAdapter logger)
		{
			if (string.IsNullOrEmpty(region))
			{
				logger.Verbose(() => string.Format("[Region discovery] Region from {0} not detected. {1}", source, DateTime.UtcNow));
				return false;
			}
			if (!Uri.IsWellFormedUriString("https://" + region + ".login.microsoft.com", UriKind.Absolute))
			{
				logger.Error(string.Format("[Region discovery] Region from {0} was found but it's invalid: {1}. {2}", source, region, DateTime.UtcNow));
				return false;
			}
			return true;
		}

		// Token: 0x0600185B RID: 6235 RVA: 0x00050FB0 File Offset: 0x0004F1B0
		private async Task<string> GetImdsUriApiVersionAsync(ILoggerAdapter logger, Dictionary<string, string> headers, CancellationToken userCancellationToken)
		{
			Uri uri = new Uri("http://169.254.169.254/metadata/instance/compute/location");
			HttpResponse httpResponse = await this._httpManager.SendGetAsync(uri, headers, logger, false, this.GetCancellationToken(userCancellationToken)).ConfigureAwait(false);
			HttpResponse response = httpResponse;
			if (response.StatusCode == HttpStatusCode.BadRequest)
			{
				LocalImdsErrorResponse errorResponse = JsonHelper.DeserializeFromJson<LocalImdsErrorResponse>(response.Body);
				if (errorResponse != null && !errorResponse.NewestVersions.IsNullOrEmpty<string>())
				{
					logger.Info(() => "[Region discovery] Updated the version for IMDS endpoint to: " + errorResponse.NewestVersions[0] + ".");
					return errorResponse.NewestVersions[0];
				}
				logger.Info(() => string.Format("[Region discovery] The response is empty or does not contain the newest versions. {0}", DateTime.UtcNow));
			}
			logger.Info(() => string.Format("[Region discovery] Failed to get the updated version for IMDS endpoint. HttpStatusCode: {0}. {1}", response.StatusCode, DateTime.UtcNow));
			throw MsalServiceExceptionFactory.FromImdsResponse("region_discovery_failed", "Region discovery for the instance failed. Region discovery can only be made if the service resides in Azure function or Azure VM. See https://aka.ms/msal-net-region-discovery for more details. ", response, null);
		}

		// Token: 0x0600185C RID: 6236 RVA: 0x0005100B File Offset: 0x0004F20B
		private static Uri BuildImdsUri(string apiVersion)
		{
			UriBuilder uriBuilder = new UriBuilder("http://169.254.169.254/metadata/instance/compute/location");
			uriBuilder.AppendQueryParameters("api-version=" + apiVersion);
			uriBuilder.AppendQueryParameters("format=text");
			return uriBuilder.Uri;
		}

		// Token: 0x0600185D RID: 6237 RVA: 0x00051038 File Offset: 0x0004F238
		private CancellationToken GetCancellationToken(CancellationToken userCancellationToken)
		{
			CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(this._imdsCallTimeoutMs);
			return CancellationTokenSource.CreateLinkedTokenSource(userCancellationToken, cancellationTokenSource.Token).Token;
		}

		// Token: 0x04000AFA RID: 2810
		private const string ImdsEndpoint = "http://169.254.169.254/metadata/instance/compute/location";

		// Token: 0x04000AFB RID: 2811
		private const string DefaultApiVersion = "2020-06-01";

		// Token: 0x04000AFC RID: 2812
		private readonly IHttpManager _httpManager;

		// Token: 0x04000AFD RID: 2813
		private readonly int _imdsCallTimeoutMs;

		// Token: 0x04000AFE RID: 2814
		private static readonly SemaphoreSlim _lockDiscover = new SemaphoreSlim(1);

		// Token: 0x04000AFF RID: 2815
		private static string s_autoDiscoveredRegion;

		// Token: 0x04000B00 RID: 2816
		private static bool s_failedAutoDiscovery = false;

		// Token: 0x04000B01 RID: 2817
		private static string s_regionDiscoveryDetails;

		// Token: 0x020004DC RID: 1244
		private class RegionInfo
		{
			// Token: 0x0600210F RID: 8463 RVA: 0x0007AAAA File Offset: 0x00078CAA
			public RegionInfo(string region, RegionAutodetectionSource regionSource, string regionDetails)
			{
				this.Region = region;
				this.RegionSource = regionSource;
				this.RegionDetails = regionDetails;
			}

			// Token: 0x17000621 RID: 1569
			// (get) Token: 0x06002110 RID: 8464 RVA: 0x0007AAC7 File Offset: 0x00078CC7
			public string Region { get; }

			// Token: 0x17000622 RID: 1570
			// (get) Token: 0x06002111 RID: 8465 RVA: 0x0007AACF File Offset: 0x00078CCF
			public RegionAutodetectionSource RegionSource { get; }

			// Token: 0x040015D5 RID: 5589
			public readonly string RegionDetails;
		}
	}
}
