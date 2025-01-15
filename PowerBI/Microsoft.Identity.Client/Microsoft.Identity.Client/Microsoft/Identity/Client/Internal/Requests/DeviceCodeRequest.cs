using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.ApiConfig.Parameters;
using Microsoft.Identity.Client.OAuth2;
using Microsoft.Identity.Client.Utils;

namespace Microsoft.Identity.Client.Internal.Requests
{
	// Token: 0x02000244 RID: 580
	internal class DeviceCodeRequest : RequestBase
	{
		// Token: 0x06001781 RID: 6017 RVA: 0x0004DDAE File Offset: 0x0004BFAE
		public DeviceCodeRequest(IServiceBundle serviceBundle, AuthenticationRequestParameters authenticationRequestParameters, AcquireTokenWithDeviceCodeParameters deviceCodeParameters)
			: base(serviceBundle, authenticationRequestParameters, deviceCodeParameters)
		{
			this._deviceCodeParameters = deviceCodeParameters;
		}

		// Token: 0x06001782 RID: 6018 RVA: 0x0004DDC0 File Offset: 0x0004BFC0
		protected override async Task<AuthenticationResult> ExecuteAsync(CancellationToken cancellationToken)
		{
			await base.ResolveAuthorityAsync().ConfigureAwait(false);
			OAuth2Client client = new OAuth2Client(base.ServiceBundle.ApplicationLogger, base.ServiceBundle.HttpManager);
			HashSet<string> deviceCodeScopes = new HashSet<string>();
			deviceCodeScopes.UnionWith(base.AuthenticationRequestParameters.Scope);
			deviceCodeScopes.Add("offline_access");
			deviceCodeScopes.Add("profile");
			deviceCodeScopes.Add("openid");
			client.AddBodyParameter("client_id", base.AuthenticationRequestParameters.AppConfig.ClientId);
			client.AddBodyParameter("scope", deviceCodeScopes.AsSingleString());
			client.AddBodyParameter("claims", base.AuthenticationRequestParameters.ClaimsAndClientCapabilities);
			ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter configuredTaskAwaiter = base.AuthenticationRequestParameters.Authority.GetDeviceCodeEndpointAsync(base.AuthenticationRequestParameters.RequestContext).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter);
			}
			UriBuilder uriBuilder = new UriBuilder(configuredTaskAwaiter.GetResult());
			uriBuilder.AppendQueryParameters(base.AuthenticationRequestParameters.ExtraQueryParameters);
			DeviceCodeResponse deviceCodeResponse = await client.ExecuteRequestAsync<DeviceCodeResponse>(uriBuilder.Uri, HttpMethod.Post, base.AuthenticationRequestParameters.RequestContext, true, true, null).ConfigureAwait(false);
			DeviceCodeResult deviceCodeResult = deviceCodeResponse.GetResult(base.AuthenticationRequestParameters.AppConfig.ClientId, deviceCodeScopes);
			await this._deviceCodeParameters.DeviceCodeResultCallback(deviceCodeResult).ConfigureAwait(false);
			MsalTokenResponse msalTokenResponse = await this.WaitForTokenResponseAsync(deviceCodeResult, cancellationToken).ConfigureAwait(false);
			return await base.CacheTokenResponseAndCreateAuthenticationResultAsync(msalTokenResponse).ConfigureAwait(false);
		}

		// Token: 0x06001783 RID: 6019 RVA: 0x0004DE0C File Offset: 0x0004C00C
		private async Task<MsalTokenResponse> WaitForTokenResponseAsync(DeviceCodeResult deviceCodeResult, CancellationToken cancellationToken)
		{
			TimeSpan timeRemaining = deviceCodeResult.ExpiresOn - DateTimeOffset.UtcNow;
			while (timeRemaining.TotalSeconds > 0.0)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					throw new OperationCanceledException();
				}
				int num = 0;
				try
				{
					string text = await base.AuthenticationRequestParameters.Authority.GetTokenEndpointAsync(base.AuthenticationRequestParameters.RequestContext).ConfigureAwait(false);
					MsalTokenResponse msalTokenResponse = await base.SendTokenRequestAsync(text, DeviceCodeRequest.GetBodyParameters(deviceCodeResult), cancellationToken).ConfigureAwait(false);
					Metrics.IncrementTotalAccessTokensFromIdP();
					return msalTokenResponse;
				}
				catch (MsalServiceException obj)
				{
					num = 1;
				}
				if (num != 1)
				{
					continue;
				}
				object obj;
				if (((MsalServiceException)obj).ErrorCode.Equals("authorization_pending", StringComparison.OrdinalIgnoreCase))
				{
					await Task.Delay(TimeSpan.FromSeconds((double)deviceCodeResult.Interval), cancellationToken).ConfigureAwait(false);
					timeRemaining = deviceCodeResult.ExpiresOn - DateTimeOffset.UtcNow;
					continue;
				}
				Exception ex = obj as Exception;
				if (ex == null)
				{
					throw obj;
				}
				ExceptionDispatchInfo.Capture(ex).Throw();
				continue;
			}
			throw new MsalClientException("code_expired", "Verification code expired before contacting the server");
		}

		// Token: 0x06001784 RID: 6020 RVA: 0x0004DE5F File Offset: 0x0004C05F
		private static Dictionary<string, string> GetBodyParameters(DeviceCodeResult deviceCodeResult)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary["client_info"] = "1";
			dictionary["grant_type"] = "device_code";
			dictionary["device_code"] = deviceCodeResult.DeviceCode;
			return dictionary;
		}

		// Token: 0x06001785 RID: 6021 RVA: 0x0004DE98 File Offset: 0x0004C098
		protected override KeyValuePair<string, string>? GetCcsHeader(IDictionary<string, string> additionalBodyParameters)
		{
			return null;
		}

		// Token: 0x04000A4D RID: 2637
		private readonly AcquireTokenWithDeviceCodeParameters _deviceCodeParameters;
	}
}
