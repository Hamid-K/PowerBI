using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.ApiConfig.Parameters;
using Microsoft.Identity.Client.AuthScheme;
using Microsoft.Identity.Client.Cache;
using Microsoft.Identity.Client.Cache.Items;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Instance.Discovery;
using Microsoft.Identity.Client.OAuth2;
using Microsoft.Identity.Client.TelemetryCore;
using Microsoft.Identity.Client.TelemetryCore.Internal.Events;
using Microsoft.Identity.Client.TelemetryCore.TelemetryClient;
using Microsoft.Identity.Client.Utils;
using Microsoft.IdentityModel.Abstractions;

namespace Microsoft.Identity.Client.Internal.Requests
{
	// Token: 0x0200024A RID: 586
	internal abstract class RequestBase
	{
		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x060017A3 RID: 6051 RVA: 0x0004E6D5 File Offset: 0x0004C8D5
		internal AuthenticationRequestParameters AuthenticationRequestParameters { get; }

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x060017A4 RID: 6052 RVA: 0x0004E6DD File Offset: 0x0004C8DD
		internal ICacheSessionManager CacheManager
		{
			get
			{
				return this.AuthenticationRequestParameters.CacheSessionManager;
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x060017A5 RID: 6053 RVA: 0x0004E6EA File Offset: 0x0004C8EA
		internal IServiceBundle ServiceBundle { get; }

		// Token: 0x060017A6 RID: 6054 RVA: 0x0004E6F4 File Offset: 0x0004C8F4
		protected RequestBase(IServiceBundle serviceBundle, AuthenticationRequestParameters authenticationRequestParameters, IAcquireTokenParameters acquireTokenParameters)
		{
			if (serviceBundle == null)
			{
				throw new ArgumentNullException("serviceBundle");
			}
			this.ServiceBundle = serviceBundle;
			if (authenticationRequestParameters == null)
			{
				throw new ArgumentNullException("authenticationRequestParameters");
			}
			this.AuthenticationRequestParameters = authenticationRequestParameters;
			if (acquireTokenParameters == null)
			{
				throw new ArgumentNullException("acquireTokenParameters");
			}
			acquireTokenParameters.LogParameters(this.AuthenticationRequestParameters.RequestContext.Logger);
		}

		// Token: 0x060017A7 RID: 6055 RVA: 0x0004E757 File Offset: 0x0004C957
		protected virtual SortedSet<string> GetOverriddenScopes(ISet<string> inputScopes)
		{
			return null;
		}

		// Token: 0x060017A8 RID: 6056
		protected abstract Task<AuthenticationResult> ExecuteAsync(CancellationToken cancellationToken);

		// Token: 0x060017A9 RID: 6057 RVA: 0x0004E75C File Offset: 0x0004C95C
		public async Task<AuthenticationResult> RunAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			RequestBase.<>c__DisplayClass11_0 CS$<>8__locals1 = new RequestBase.<>c__DisplayClass11_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.cancellationToken = cancellationToken;
			CS$<>8__locals1.apiEvent = null;
			CS$<>8__locals1.telemetryEventDetails = null;
			CS$<>8__locals1.telemetryClients = null;
			MeasureDurationResult measureTelemetryDurationResult = StopwatchService.MeasureCodeBlock(delegate
			{
				RequestBase <>4__this = CS$<>8__locals1.<>4__this;
				IAccount account = CS$<>8__locals1.<>4__this.AuthenticationRequestParameters.Account;
				string text;
				if (account == null)
				{
					text = null;
				}
				else
				{
					AccountId homeAccountId = account.HomeAccountId;
					text = ((homeAccountId != null) ? homeAccountId.Identifier : null);
				}
				CS$<>8__locals1.apiEvent = <>4__this.InitializeApiEvent(text);
				CS$<>8__locals1.<>4__this.AuthenticationRequestParameters.RequestContext.ApiEvent = CS$<>8__locals1.apiEvent;
				CS$<>8__locals1.telemetryEventDetails = new MsalTelemetryEventDetails("acquire_token");
				CS$<>8__locals1.telemetryClients = CS$<>8__locals1.<>4__this.AuthenticationRequestParameters.RequestContext.ServiceBundle.Config.TelemetryClients;
			});
			AuthenticationResult authenticationResult;
			using (this.AuthenticationRequestParameters.RequestContext.CreateTelemetryHelper(CS$<>8__locals1.apiEvent))
			{
				try
				{
					RequestBase.<>c__DisplayClass11_1 CS$<>8__locals2 = new RequestBase.<>c__DisplayClass11_1();
					CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
					CS$<>8__locals2.authenticationResult = null;
					MeasureDurationResult measureDurationResult = await StopwatchService.MeasureCodeBlockAsync(delegate
					{
						RequestBase.<>c__DisplayClass11_1.<<RunAsync>b__1>d <<RunAsync>b__1>d;
						<<RunAsync>b__1>d.<>t__builder = AsyncTaskMethodBuilder.Create();
						<<RunAsync>b__1>d.<>4__this = CS$<>8__locals2;
						<<RunAsync>b__1>d.<>1__state = -1;
						<<RunAsync>b__1>d.<>t__builder.Start<RequestBase.<>c__DisplayClass11_1.<<RunAsync>b__1>d>(ref <<RunAsync>b__1>d);
						return <<RunAsync>b__1>d.<>t__builder.Task;
					}).ConfigureAwait(false);
					this.UpdateTelemetry(measureDurationResult.Milliseconds + measureTelemetryDurationResult.Milliseconds, CS$<>8__locals2.CS$<>8__locals1.apiEvent, CS$<>8__locals2.authenticationResult);
					RequestBase.LogMetricsFromAuthResult(CS$<>8__locals2.authenticationResult, this.AuthenticationRequestParameters.RequestContext.Logger);
					this.LogSuccessfulTelemetryToClient(CS$<>8__locals2.authenticationResult, CS$<>8__locals2.CS$<>8__locals1.telemetryEventDetails, CS$<>8__locals2.CS$<>8__locals1.telemetryClients);
					this.LogSuccessTelemetryToOtel(CS$<>8__locals2.authenticationResult, CS$<>8__locals2.CS$<>8__locals1.apiEvent.ApiId, measureDurationResult.Microseconds);
					authenticationResult = CS$<>8__locals2.authenticationResult;
				}
				catch (MsalException ex)
				{
					CS$<>8__locals1.apiEvent.ApiErrorCode = ex.ErrorCode;
					if (string.IsNullOrWhiteSpace(ex.CorrelationId))
					{
						ex.CorrelationId = this.AuthenticationRequestParameters.CorrelationId.ToString();
					}
					this.AuthenticationRequestParameters.RequestContext.Logger.ErrorPii(ex);
					RequestBase.LogMsalErrorTelemetryToClient(ex, CS$<>8__locals1.telemetryEventDetails, CS$<>8__locals1.telemetryClients);
					this.LogFailureTelemetryToOtel(ex.ErrorCode, CS$<>8__locals1.apiEvent.ApiId, CS$<>8__locals1.apiEvent.CacheInfo);
					throw;
				}
				catch (Exception ex2)
				{
					CS$<>8__locals1.apiEvent.ApiErrorCode = ex2.GetType().Name;
					this.AuthenticationRequestParameters.RequestContext.Logger.ErrorPii(ex2);
					RequestBase.LogMsalErrorTelemetryToClient(ex2, CS$<>8__locals1.telemetryEventDetails, CS$<>8__locals1.telemetryClients);
					this.LogFailureTelemetryToOtel(ex2.GetType().Name, CS$<>8__locals1.apiEvent.ApiId, CS$<>8__locals1.apiEvent.CacheInfo);
					throw;
				}
				finally
				{
					CS$<>8__locals1.telemetryClients.TrackEvent(CS$<>8__locals1.telemetryEventDetails);
				}
			}
			return authenticationResult;
		}

		// Token: 0x060017AA RID: 6058 RVA: 0x0004E7A8 File Offset: 0x0004C9A8
		private void LogSuccessTelemetryToOtel(AuthenticationResult authenticationResult, ApiEvent.ApiIds apiId, long durationInUs)
		{
			this.ServiceBundle.PlatformProxy.OtelInstrumentation.LogSuccessMetrics(this.ServiceBundle.PlatformProxy.GetProductName(), apiId, this.GetCacheLevel(authenticationResult), durationInUs, authenticationResult.AuthenticationResultMetadata, this.AuthenticationRequestParameters.RequestContext.Logger);
		}

		// Token: 0x060017AB RID: 6059 RVA: 0x0004E7F9 File Offset: 0x0004C9F9
		private void LogFailureTelemetryToOtel(string errorCodeToLog, ApiEvent.ApiIds apiId, CacheRefreshReason cacheRefreshReason)
		{
			this.ServiceBundle.PlatformProxy.OtelInstrumentation.LogFailureMetrics(this.ServiceBundle.PlatformProxy.GetProductName(), errorCodeToLog, apiId, cacheRefreshReason);
		}

		// Token: 0x060017AC RID: 6060 RVA: 0x0004E824 File Offset: 0x0004CA24
		private static void LogMsalErrorTelemetryToClient(Exception ex, MsalTelemetryEventDetails telemetryEventDetails, ITelemetryClient[] telemetryClients)
		{
			if (telemetryClients.HasEnabledClients("acquire_token"))
			{
				telemetryEventDetails.SetProperty("Succeeded", false);
				telemetryEventDetails.SetProperty("ErrorMessage", ex.Message);
				MsalClientException ex2 = ex as MsalClientException;
				if (ex2 != null)
				{
					telemetryEventDetails.SetProperty("ErrorCode", ex2.ErrorCode);
					return;
				}
				MsalServiceException ex3 = ex as MsalServiceException;
				if (ex3 != null)
				{
					telemetryEventDetails.SetProperty("ErrorCode", ex3.ErrorCode);
					string text = "StsErrorCode";
					string[] errorCodes = ex3.ErrorCodes;
					telemetryEventDetails.SetProperty(text, (errorCodes != null) ? errorCodes.FirstOrDefault<string>() : null);
					return;
				}
				telemetryEventDetails.SetProperty("ErrorCode", ex.GetType().ToString());
			}
		}

		// Token: 0x060017AD RID: 6061 RVA: 0x0004E8CC File Offset: 0x0004CACC
		private void LogSuccessfulTelemetryToClient(AuthenticationResult authenticationResult, MsalTelemetryEventDetails telemetryEventDetails, ITelemetryClient[] telemetryClients)
		{
			if (telemetryClients.HasEnabledClients("acquire_token"))
			{
				telemetryEventDetails.SetProperty("CacheInfoTelemetry", Convert.ToInt64(authenticationResult.AuthenticationResultMetadata.CacheRefreshReason));
				telemetryEventDetails.SetProperty("TokenSource", Convert.ToInt64(authenticationResult.AuthenticationResultMetadata.TokenSource));
				telemetryEventDetails.SetProperty("Duration", authenticationResult.AuthenticationResultMetadata.DurationTotalInMs);
				telemetryEventDetails.SetProperty("DurationInCache", authenticationResult.AuthenticationResultMetadata.DurationInCacheInMs);
				telemetryEventDetails.SetProperty("DurationInHttp", authenticationResult.AuthenticationResultMetadata.DurationInHttpInMs);
				telemetryEventDetails.SetProperty("Succeeded", true);
				telemetryEventDetails.SetProperty("TokenType", (long)this.AuthenticationRequestParameters.RequestContext.ApiEvent.TokenType.Value);
				telemetryEventDetails.SetProperty("RemainingLifetime", (authenticationResult.ExpiresOn - DateTime.Now).TotalMilliseconds);
				telemetryEventDetails.SetProperty("ActivityId", authenticationResult.CorrelationId);
				if (authenticationResult.AuthenticationResultMetadata.RefreshOn != null)
				{
					telemetryEventDetails.SetProperty("RefreshOn", authenticationResult.AuthenticationResultMetadata.RefreshOn.Value.ToUnixTimeMilliseconds());
				}
				telemetryEventDetails.SetProperty("AssertionType", (long)this.AuthenticationRequestParameters.RequestContext.ApiEvent.AssertionType);
				telemetryEventDetails.SetProperty("Endpoint", this.AuthenticationRequestParameters.Authority.AuthorityInfo.CanonicalAuthority.ToString());
				telemetryEventDetails.SetProperty("CacheLevel", (long)authenticationResult.AuthenticationResultMetadata.CacheLevel);
				Tuple<string, string> tuple = this.ParseScopesForTelemetry();
				if (tuple.Item1 != null)
				{
					telemetryEventDetails.SetProperty("Resource", tuple.Item1);
				}
				if (tuple.Item2 != null)
				{
					telemetryEventDetails.SetProperty("Scopes", tuple.Item2);
				}
			}
		}

		// Token: 0x060017AE RID: 6062 RVA: 0x0004EAAC File Offset: 0x0004CCAC
		private Tuple<string, string> ParseScopesForTelemetry()
		{
			string text = null;
			string text2 = null;
			if (this.AuthenticationRequestParameters.Scope.Count > 0)
			{
				string text3 = this.AuthenticationRequestParameters.Scope.First<string>();
				if (Uri.IsWellFormedUriString(text3, UriKind.Absolute))
				{
					Uri uri = new Uri(text3);
					text = uri.Scheme + "://" + uri.Host;
					StringBuilder stringBuilder = new StringBuilder();
					foreach (string text4 in this.AuthenticationRequestParameters.Scope)
					{
						string[] array = text4.Split(new string[] { uri.Host }, StringSplitOptions.None);
						string text5 = ((array.Length > 1) ? (array[1].TrimStart(new char[] { '/' }) + " ") : array.FirstOrDefault<string>());
						stringBuilder.Append(text5);
					}
					text2 = stringBuilder.ToString().TrimEnd(new char[] { ' ' });
				}
				else
				{
					text2 = this.AuthenticationRequestParameters.Scope.AsSingleString();
				}
			}
			return new Tuple<string, string>(text, text2);
		}

		// Token: 0x060017AF RID: 6063 RVA: 0x0004EBDC File Offset: 0x0004CDDC
		private CacheLevel GetCacheLevel(AuthenticationResult authenticationResult)
		{
			if (authenticationResult.AuthenticationResultMetadata.TokenSource != TokenSource.Cache)
			{
				return CacheLevel.None;
			}
			if (this.AuthenticationRequestParameters.RequestContext.ApiEvent.CacheLevel > CacheLevel.Unknown)
			{
				return this.AuthenticationRequestParameters.RequestContext.ApiEvent.CacheLevel;
			}
			return CacheLevel.Unknown;
		}

		// Token: 0x060017B0 RID: 6064 RVA: 0x0004EC28 File Offset: 0x0004CE28
		private static void LogMetricsFromAuthResult(AuthenticationResult authenticationResult, ILoggerAdapter logger)
		{
			if (logger.IsLoggingEnabled(LogLevel.Always))
			{
				AuthenticationResultMetadata authenticationResultMetadata = authenticationResult.AuthenticationResultMetadata;
				logger.Always(string.Format("\r\n[LogMetricsFromAuthResult] Cache Refresh Reason: {0}\r\n[LogMetricsFromAuthResult] DurationInCacheInMs: {1}\r\n[LogMetricsFromAuthResult] DurationTotalInMs: {2}\r\n[LogMetricsFromAuthResult] DurationInHttpInMs: {3}", new object[] { authenticationResultMetadata.CacheRefreshReason, authenticationResultMetadata.DurationInCacheInMs, authenticationResultMetadata.DurationTotalInMs, authenticationResultMetadata.DurationInHttpInMs }));
				logger.AlwaysPii("[LogMetricsFromAuthResult] TokenEndpoint: " + authenticationResultMetadata.TokenEndpoint, "TokenEndpoint: ****");
			}
		}

		// Token: 0x060017B1 RID: 6065 RVA: 0x0004ECB0 File Offset: 0x0004CEB0
		private void UpdateTelemetry(long elapsedMilliseconds, ApiEvent apiEvent, AuthenticationResult authenticationResult)
		{
			authenticationResult.AuthenticationResultMetadata.DurationTotalInMs = elapsedMilliseconds;
			authenticationResult.AuthenticationResultMetadata.DurationInHttpInMs = apiEvent.DurationInHttpInMs;
			authenticationResult.AuthenticationResultMetadata.DurationInCacheInMs = apiEvent.DurationInCacheInMs;
			authenticationResult.AuthenticationResultMetadata.TokenEndpoint = apiEvent.TokenEndpoint;
			authenticationResult.AuthenticationResultMetadata.CacheRefreshReason = apiEvent.CacheInfo;
			authenticationResult.AuthenticationResultMetadata.CacheLevel = this.GetCacheLevel(authenticationResult);
			authenticationResult.AuthenticationResultMetadata.Telemetry = apiEvent.MsalRuntimeTelemetry;
			authenticationResult.AuthenticationResultMetadata.RegionDetails = RequestBase.CreateRegionDetails(apiEvent);
			Metrics.IncrementTotalDurationInMs(authenticationResult.AuthenticationResultMetadata.DurationTotalInMs);
		}

		// Token: 0x060017B2 RID: 6066 RVA: 0x0004ED51 File Offset: 0x0004CF51
		protected virtual void EnrichTelemetryApiEvent(ApiEvent apiEvent)
		{
		}

		// Token: 0x060017B3 RID: 6067 RVA: 0x0004ED54 File Offset: 0x0004CF54
		private ApiEvent InitializeApiEvent(string accountId)
		{
			ApiEvent apiEvent = new ApiEvent(this.AuthenticationRequestParameters.RequestContext.CorrelationId)
			{
				ApiId = this.AuthenticationRequestParameters.ApiId
			};
			apiEvent.IsTokenCacheSerialized = this.AuthenticationRequestParameters.CacheSessionManager.TokenCacheInternal.IsAppSubscribedToSerializationEvents();
			apiEvent.IsLegacyCacheEnabled = this.AuthenticationRequestParameters.RequestContext.ServiceBundle.Config.LegacyCacheCompatibilityEnabled;
			apiEvent.CacheInfo = CacheRefreshReason.NotApplicable;
			apiEvent.TokenType = new TokenType?(this.AuthenticationRequestParameters.AuthenticationScheme.TelemetryTokenType);
			apiEvent.AssertionType = this.GetAssertionType();
			this.EnrichTelemetryApiEvent(apiEvent);
			return apiEvent;
		}

		// Token: 0x060017B4 RID: 6068 RVA: 0x0004EDFC File Offset: 0x0004CFFC
		private AssertionType GetAssertionType()
		{
			if (this.ServiceBundle.Config.IsManagedIdentity || this.ServiceBundle.Config.AppTokenProvider != null)
			{
				return AssertionType.ManagedIdentity;
			}
			if (this.ServiceBundle.Config.ClientCredential == null)
			{
				return AssertionType.None;
			}
			if (this.ServiceBundle.Config.ClientCredential.AssertionType != AssertionType.CertificateWithoutSni)
			{
				return this.ServiceBundle.Config.ClientCredential.AssertionType;
			}
			if (this.ServiceBundle.Config.SendX5C)
			{
				return AssertionType.CertificateWithSni;
			}
			return AssertionType.CertificateWithoutSni;
		}

		// Token: 0x060017B5 RID: 6069 RVA: 0x0004EE88 File Offset: 0x0004D088
		protected async Task<AuthenticationResult> CacheTokenResponseAndCreateAuthenticationResultAsync(MsalTokenResponse msalTokenResponse)
		{
			this.AuthenticationRequestParameters.RequestContext.Logger.Info("Checking client info returned from the server..");
			ClientInfo clientInfo = null;
			if (!this.AuthenticationRequestParameters.IsClientCredentialRequest && this.AuthenticationRequestParameters.ApiId != ApiEvent.ApiIds.AcquireTokenForSystemAssignedManagedIdentity && this.AuthenticationRequestParameters.ApiId != ApiEvent.ApiIds.AcquireTokenForUserAssignedManagedIdentity && this.AuthenticationRequestParameters.ApiId != ApiEvent.ApiIds.AcquireTokenByRefreshToken && this.AuthenticationRequestParameters.AuthorityInfo.AuthorityType != AuthorityType.Adfs && msalTokenResponse.ClientInfo != null)
			{
				clientInfo = ClientInfo.CreateFromJson(msalTokenResponse.ClientInfo);
			}
			this.ValidateAccountIdentifiers(clientInfo);
			this.AuthenticationRequestParameters.RequestContext.Logger.Info("Saving token response to cache..");
			object obj = await this.CacheManager.SaveTokenResponseAsync(msalTokenResponse).ConfigureAwait(false);
			MsalAccessTokenCacheItem item = obj.Item1;
			MsalIdTokenCacheItem item2 = obj.Item2;
			Account item3 = obj.Item3;
			return new AuthenticationResult(item, item2, this.AuthenticationRequestParameters.AuthenticationScheme, this.AuthenticationRequestParameters.RequestContext.CorrelationId, msalTokenResponse.TokenSource, this.AuthenticationRequestParameters.RequestContext.ApiEvent, item3, msalTokenResponse.SpaAuthCode, msalTokenResponse.CreateExtensionDataStringMap());
		}

		// Token: 0x060017B6 RID: 6070 RVA: 0x0004EED3 File Offset: 0x0004D0D3
		protected virtual void ValidateAccountIdentifiers(ClientInfo fromServer)
		{
		}

		// Token: 0x060017B7 RID: 6071 RVA: 0x0004EED5 File Offset: 0x0004D0D5
		protected Task ResolveAuthorityAsync()
		{
			return this.AuthenticationRequestParameters.AuthorityManager.RunInstanceDiscoveryAndValidationAsync();
		}

		// Token: 0x060017B8 RID: 6072 RVA: 0x0004EEE8 File Offset: 0x0004D0E8
		internal async Task<MsalTokenResponse> SendTokenRequestAsync(IDictionary<string, string> additionalBodyParameters, CancellationToken cancellationToken)
		{
			string text = await this.AuthenticationRequestParameters.Authority.GetTokenEndpointAsync(this.AuthenticationRequestParameters.RequestContext).ConfigureAwait(false);
			MsalTokenResponse msalTokenResponse = await this.SendTokenRequestAsync(text, additionalBodyParameters, cancellationToken).ConfigureAwait(false);
			Metrics.IncrementTotalAccessTokensFromIdP();
			return msalTokenResponse;
		}

		// Token: 0x060017B9 RID: 6073 RVA: 0x0004EF3C File Offset: 0x0004D13C
		protected Task<MsalTokenResponse> SendTokenRequestAsync(string tokenEndpoint, IDictionary<string, string> additionalBodyParameters, CancellationToken cancellationToken)
		{
			string text = this.GetOverriddenScopes(this.AuthenticationRequestParameters.Scope).AsSingleString();
			TokenClient tokenClient = new TokenClient(this.AuthenticationRequestParameters);
			KeyValuePair<string, string>? ccsHeader = this.GetCcsHeader(additionalBodyParameters);
			if (ccsHeader != null && !string.IsNullOrEmpty(ccsHeader.Value.Key))
			{
				tokenClient.AddHeaderToClient(ccsHeader.Value.Key, ccsHeader.Value.Value);
			}
			this.InjectPcaSsoPolicyHeader(tokenClient);
			return tokenClient.SendTokenRequestAsync(additionalBodyParameters, text, tokenEndpoint, cancellationToken);
		}

		// Token: 0x060017BA RID: 6074 RVA: 0x0004EFC8 File Offset: 0x0004D1C8
		private void InjectPcaSsoPolicyHeader(TokenClient tokenClient)
		{
			if (this.ServiceBundle.Config.IsPublicClient && this.ServiceBundle.Config.IsWebviewSsoPolicyEnabled)
			{
				foreach (KeyValuePair<string, string> keyValuePair in this.ServiceBundle.Config.BrokerCreatorFunc(null, this.ServiceBundle.Config, this.AuthenticationRequestParameters.RequestContext.Logger).GetSsoPolicyHeaders())
				{
					tokenClient.AddHeaderToClient(keyValuePair.Key, keyValuePair.Value);
				}
			}
		}

		// Token: 0x060017BB RID: 6075 RVA: 0x0004F078 File Offset: 0x0004D278
		protected virtual KeyValuePair<string, string>? GetCcsHeader(IDictionary<string, string> additionalBodyParameters)
		{
			AuthenticationRequestParameters authenticationRequestParameters = this.AuthenticationRequestParameters;
			bool flag;
			if (authenticationRequestParameters == null)
			{
				flag = null != null;
			}
			else
			{
				IAccount account = authenticationRequestParameters.Account;
				flag = ((account != null) ? account.HomeAccountId : null) != null;
			}
			if (flag)
			{
				if (!string.IsNullOrEmpty(this.AuthenticationRequestParameters.Account.HomeAccountId.Identifier))
				{
					string objectId = this.AuthenticationRequestParameters.Account.HomeAccountId.ObjectId;
					string tenantId = this.AuthenticationRequestParameters.Account.HomeAccountId.TenantId;
					string ccsClientInfoHint = CoreHelpers.GetCcsClientInfoHint(objectId, tenantId);
					return new KeyValuePair<string, string>?(new KeyValuePair<string, string>("x-anchormailbox", ccsClientInfoHint));
				}
				if (!string.IsNullOrEmpty(this.AuthenticationRequestParameters.Account.Username))
				{
					return this.GetCcsUpnHeader(this.AuthenticationRequestParameters.Account.Username);
				}
			}
			string text;
			if (additionalBodyParameters.TryGetValue("username", out text))
			{
				return this.GetCcsUpnHeader(text);
			}
			if (!string.IsNullOrEmpty(this.AuthenticationRequestParameters.LoginHint))
			{
				return this.GetCcsUpnHeader(this.AuthenticationRequestParameters.LoginHint);
			}
			return null;
		}

		// Token: 0x060017BC RID: 6076 RVA: 0x0004F17C File Offset: 0x0004D37C
		protected KeyValuePair<string, string>? GetCcsUpnHeader(string upnHeader)
		{
			if (this.AuthenticationRequestParameters.Authority.AuthorityInfo.AuthorityType == AuthorityType.B2C)
			{
				return null;
			}
			string ccsUpnHint = CoreHelpers.GetCcsUpnHint(upnHeader);
			return new KeyValuePair<string, string>?(new KeyValuePair<string, string>("x-anchormailbox", ccsUpnHint));
		}

		// Token: 0x060017BD RID: 6077 RVA: 0x0004F1C4 File Offset: 0x0004D3C4
		private void LogRequestStarted(AuthenticationRequestParameters authenticationRequestParameters)
		{
			if (authenticationRequestParameters.RequestContext.Logger.IsLoggingEnabled(LogLevel.Info))
			{
				string text = authenticationRequestParameters.Scope.AsSingleString();
				string name = base.GetType().Name;
				string text2 = "=== Token Acquisition ({0}) started:\n\tAuthority: {1}\n\tScope: {2}\n\tClientId: {3}\n\t";
				object[] array = new object[4];
				array[0] = name;
				int num = 1;
				AuthorityInfo authorityInfo = authenticationRequestParameters.AuthorityInfo;
				array[num] = ((authorityInfo != null) ? authorityInfo.CanonicalAuthority : null);
				array[2] = text;
				array[3] = authenticationRequestParameters.AppConfig.ClientId;
				string text3 = string.Format(text2, array);
				string text4 = "=== Token Acquisition (" + name + ") started:\n\t Scopes: " + text;
				if (authenticationRequestParameters.AuthorityInfo != null)
				{
					AuthorityInfo authorityInfo2 = authenticationRequestParameters.AuthorityInfo;
					if (KnownMetadataProvider.IsKnownEnvironment((authorityInfo2 != null) ? authorityInfo2.Host : null))
					{
						string text5 = text4;
						string text6 = "\n\tAuthority Host: ";
						AuthorityInfo authorityInfo3 = authenticationRequestParameters.AuthorityInfo;
						text4 = text5 + text6 + ((authorityInfo3 != null) ? authorityInfo3.Host : null);
					}
				}
				authenticationRequestParameters.RequestContext.Logger.InfoPii(text3, text4);
			}
			if (authenticationRequestParameters.AppConfig.IsConfidentialClient && !authenticationRequestParameters.IsClientCredentialRequest && !this.CacheManager.TokenCacheInternal.IsAppSubscribedToSerializationEvents())
			{
				authenticationRequestParameters.RequestContext.Logger.Warning("Only in-memory caching is used. The cache is not persisted and will be lost if the machine is restarted. It also does not scale for a web app or web API, where the number of users can grow large. In production, web apps and web APIs should use distributed caching like Redis. See https://aka.ms/msal-net-cca-token-cache-serialization");
			}
		}

		// Token: 0x060017BE RID: 6078 RVA: 0x0004F2DC File Offset: 0x0004D4DC
		private void LogReturnedToken(AuthenticationResult result)
		{
			if (result.AccessToken != null && this.AuthenticationRequestParameters.RequestContext.Logger.IsLoggingEnabled(LogLevel.Info))
			{
				string scopes = string.Join(" ", result.Scopes);
				this.AuthenticationRequestParameters.RequestContext.Logger.Info("\n\t=== Token Acquisition finished successfully:");
				this.AuthenticationRequestParameters.RequestContext.Logger.InfoPii(() => string.Format(" AT expiration time: {0}, scopes: {1}. ", result.ExpiresOn, scopes) + string.Format("source: {0}", result.AuthenticationResultMetadata.TokenSource), () => string.Format(" AT expiration time: {0}, scopes: {1}. ", result.ExpiresOn, scopes) + string.Format("source: {0}", result.AuthenticationResultMetadata.TokenSource));
				if (result.AuthenticationResultMetadata.TokenSource != TokenSource.Cache)
				{
					Uri canonicalAuthority = this.AuthenticationRequestParameters.AuthorityInfo.CanonicalAuthority;
					this.AuthenticationRequestParameters.RequestContext.Logger.InfoPii(() => string.Format("Fetched access token from host {0}. Endpoint: {1}. ", canonicalAuthority.Host, canonicalAuthority), () => "Fetched access token from host " + canonicalAuthority.Host + ". ");
				}
			}
		}

		// Token: 0x060017BF RID: 6079 RVA: 0x0004F3E0 File Offset: 0x0004D5E0
		internal async Task<AuthenticationResult> HandleTokenRefreshErrorAsync(MsalServiceException e, MsalAccessTokenCacheItem cachedAccessTokenItem)
		{
			ILoggerAdapter logger = this.AuthenticationRequestParameters.RequestContext.Logger;
			logger.Warning(string.Format("Fetching a new AT failed. Is exception retry-able? {0}. Is there an AT in the cache that is usable? {1}", e.IsRetryable, cachedAccessTokenItem != null));
			if (cachedAccessTokenItem != null && e.IsRetryable)
			{
				logger.Info("Returning existing access token. It is not expired, but should be refreshed. ");
				MsalIdTokenCacheItem msalIdTokenCacheItem = await this.CacheManager.GetIdTokenCacheItemAsync(cachedAccessTokenItem).ConfigureAwait(false);
				MsalIdTokenCacheItem idToken = msalIdTokenCacheItem;
				Account account = await this.CacheManager.GetAccountAssociatedWithAccessTokenAsync(cachedAccessTokenItem).ConfigureAwait(false);
				return new AuthenticationResult(cachedAccessTokenItem, idToken, this.AuthenticationRequestParameters.AuthenticationScheme, this.AuthenticationRequestParameters.RequestContext.CorrelationId, TokenSource.Cache, this.AuthenticationRequestParameters.RequestContext.ApiEvent, account, null, null);
			}
			logger.Warning("Either the exception does not indicate a problem with AAD or the token cache does not have an AT that is usable. ");
			throw e;
		}

		// Token: 0x060017C0 RID: 6080 RVA: 0x0004F433 File Offset: 0x0004D633
		private static RegionDetails CreateRegionDetails(ApiEvent apiEvent)
		{
			return new RegionDetails(apiEvent.RegionOutcome, apiEvent.RegionUsed, apiEvent.RegionDiscoveryFailureReason);
		}
	}
}
