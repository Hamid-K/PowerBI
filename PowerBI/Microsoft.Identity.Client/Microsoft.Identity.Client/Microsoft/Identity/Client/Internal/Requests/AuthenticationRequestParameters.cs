using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client.ApiConfig.Parameters;
using Microsoft.Identity.Client.AppConfig;
using Microsoft.Identity.Client.AuthScheme;
using Microsoft.Identity.Client.Cache;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Extensibility;
using Microsoft.Identity.Client.Instance;
using Microsoft.Identity.Client.TelemetryCore.Internal.Events;
using Microsoft.Identity.Client.Utils;

namespace Microsoft.Identity.Client.Internal.Requests
{
	// Token: 0x02000240 RID: 576
	internal class AuthenticationRequestParameters
	{
		// Token: 0x0600174C RID: 5964 RVA: 0x0004D2EC File Offset: 0x0004B4EC
		public AuthenticationRequestParameters(IServiceBundle serviceBundle, ITokenCacheInternal tokenCache, AcquireTokenCommonParameters commonParameters, RequestContext requestContext, Authority initialAuthority, string homeAccountId = null)
		{
			this._serviceBundle = serviceBundle;
			this._commonParameters = commonParameters;
			this.RequestContext = requestContext;
			this.CacheSessionManager = new CacheSessionManager(tokenCache, this);
			this.Scope = ScopeHelper.CreateScopeSet(commonParameters.Scopes);
			this.RedirectUri = new Uri(serviceBundle.Config.RedirectUri);
			this.AuthorityManager = new AuthorityManager(this.RequestContext, initialAuthority);
			this.ExtraQueryParameters = serviceBundle.Config.ExtraQueryParameters ?? new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			if (commonParameters.ExtraQueryParameters != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair in commonParameters.ExtraQueryParameters)
				{
					this.ExtraQueryParameters[keyValuePair.Key] = keyValuePair.Value;
				}
			}
			this.ClaimsAndClientCapabilities = ClaimsHelper.GetMergedClaimsAndClientCapabilities(this._commonParameters.Claims, this._serviceBundle.Config.ClientCapabilities);
			this.HomeAccountId = homeAccountId;
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x0600174D RID: 5965 RVA: 0x0004D404 File Offset: 0x0004B604
		public ApplicationConfiguration AppConfig
		{
			get
			{
				return this._serviceBundle.Config;
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x0600174E RID: 5966 RVA: 0x0004D411 File Offset: 0x0004B611
		public ApiEvent.ApiIds ApiId
		{
			get
			{
				return this._commonParameters.ApiId;
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x0600174F RID: 5967 RVA: 0x0004D41E File Offset: 0x0004B61E
		public RequestContext RequestContext { get; }

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06001750 RID: 5968 RVA: 0x0004D426 File Offset: 0x0004B626
		// (set) Token: 0x06001751 RID: 5969 RVA: 0x0004D42E File Offset: 0x0004B62E
		public AuthorityManager AuthorityManager { get; set; }

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06001752 RID: 5970 RVA: 0x0004D437 File Offset: 0x0004B637
		public Authority Authority
		{
			get
			{
				return this.AuthorityManager.Authority;
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06001753 RID: 5971 RVA: 0x0004D444 File Offset: 0x0004B644
		public AuthorityInfo AuthorityInfo
		{
			get
			{
				return this.AuthorityManager.Authority.AuthorityInfo;
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06001754 RID: 5972 RVA: 0x0004D456 File Offset: 0x0004B656
		public AuthorityInfo AuthorityOverride
		{
			get
			{
				return this._commonParameters.AuthorityOverride;
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06001755 RID: 5973 RVA: 0x0004D463 File Offset: 0x0004B663
		public ICacheSessionManager CacheSessionManager { get; }

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06001756 RID: 5974 RVA: 0x0004D46B File Offset: 0x0004B66B
		public HashSet<string> Scope { get; }

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06001757 RID: 5975 RVA: 0x0004D473 File Offset: 0x0004B673
		// (set) Token: 0x06001758 RID: 5976 RVA: 0x0004D47B File Offset: 0x0004B67B
		public Uri RedirectUri { get; set; }

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06001759 RID: 5977 RVA: 0x0004D484 File Offset: 0x0004B684
		public IDictionary<string, string> ExtraQueryParameters { get; }

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x0600175A RID: 5978 RVA: 0x0004D48C File Offset: 0x0004B68C
		// (set) Token: 0x0600175B RID: 5979 RVA: 0x0004D494 File Offset: 0x0004B694
		public string ClaimsAndClientCapabilities { get; private set; }

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x0600175C RID: 5980 RVA: 0x0004D49D File Offset: 0x0004B69D
		public Guid CorrelationId
		{
			get
			{
				return this._commonParameters.CorrelationId;
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x0600175D RID: 5981 RVA: 0x0004D4AA File Offset: 0x0004B6AA
		public string Claims
		{
			get
			{
				return this._commonParameters.Claims;
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x0600175E RID: 5982 RVA: 0x0004D4B7 File Offset: 0x0004B6B7
		public IAuthenticationScheme AuthenticationScheme
		{
			get
			{
				return this._commonParameters.AuthenticationScheme;
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x0600175F RID: 5983 RVA: 0x0004D4C4 File Offset: 0x0004B6C4
		// (set) Token: 0x06001760 RID: 5984 RVA: 0x0004D4CC File Offset: 0x0004B6CC
		public bool SendX5C { get; set; }

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06001761 RID: 5985 RVA: 0x0004D4D5 File Offset: 0x0004B6D5
		// (set) Token: 0x06001762 RID: 5986 RVA: 0x0004D4FE File Offset: 0x0004B6FE
		public string LoginHint
		{
			get
			{
				if (string.IsNullOrEmpty(this._loginHint) && this.Account != null)
				{
					return this.Account.Username;
				}
				return this._loginHint;
			}
			set
			{
				this._loginHint = value;
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06001763 RID: 5987 RVA: 0x0004D507 File Offset: 0x0004B707
		// (set) Token: 0x06001764 RID: 5988 RVA: 0x0004D50F File Offset: 0x0004B70F
		public IAccount Account { get; set; }

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06001765 RID: 5989 RVA: 0x0004D518 File Offset: 0x0004B718
		public string HomeAccountId { get; }

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06001766 RID: 5990 RVA: 0x0004D520 File Offset: 0x0004B720
		public Func<OnBeforeTokenRequestData, Task> OnBeforeTokenRequestHandler
		{
			get
			{
				return this._commonParameters.OnBeforeTokenRequestHandler;
			}
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06001767 RID: 5991 RVA: 0x0004D52D File Offset: 0x0004B72D
		public IDictionary<string, string> ExtraHttpHeaders
		{
			get
			{
				return this._commonParameters.ExtraHttpHeaders;
			}
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06001768 RID: 5992 RVA: 0x0004D53A File Offset: 0x0004B73A
		public bool IsClientCredentialRequest
		{
			get
			{
				return this.ApiId == ApiEvent.ApiIds.AcquireTokenForClient;
			}
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06001769 RID: 5993 RVA: 0x0004D549 File Offset: 0x0004B749
		public PoPAuthenticationConfiguration PopAuthenticationConfiguration
		{
			get
			{
				return this._commonParameters.PopAuthenticationConfiguration;
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x0600176A RID: 5994 RVA: 0x0004D556 File Offset: 0x0004B756
		// (set) Token: 0x0600176B RID: 5995 RVA: 0x0004D55E File Offset: 0x0004B75E
		public UserAssertion UserAssertion { get; set; }

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x0600176C RID: 5996 RVA: 0x0004D567 File Offset: 0x0004B767
		// (set) Token: 0x0600176D RID: 5997 RVA: 0x0004D56F File Offset: 0x0004B76F
		public string LongRunningOboCacheKey { get; set; }

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x0600176E RID: 5998 RVA: 0x0004D578 File Offset: 0x0004B778
		// (set) Token: 0x0600176F RID: 5999 RVA: 0x0004D580 File Offset: 0x0004B780
		public KeyValuePair<string, string>? CcsRoutingHint { get; set; }

		// Token: 0x06001770 RID: 6000 RVA: 0x0004D58C File Offset: 0x0004B78C
		public void LogParameters()
		{
			ILoggerAdapter logger = this.RequestContext.Logger;
			if (logger.IsLoggingEnabled(LogLevel.Info))
			{
				StringBuilder stringBuilder = new StringBuilder(string.Concat(new string[]
				{
					Environment.NewLine,
					"=== Request Data ===",
					Environment.NewLine,
					"Authority Provided? - ",
					(this.Authority != null).ToString(),
					Environment.NewLine
				}));
				stringBuilder.AppendLine("Client Id - " + this.AppConfig.ClientId);
				StringBuilder stringBuilder2 = stringBuilder;
				string text = "Scopes - ";
				HashSet<string> scope = this.Scope;
				stringBuilder2.AppendLine(text + ((scope != null) ? scope.AsSingleString() : null));
				StringBuilder stringBuilder3 = stringBuilder;
				string text2 = "Redirect Uri - ";
				Uri redirectUri = this.RedirectUri;
				stringBuilder3.AppendLine(text2 + ((redirectUri != null) ? redirectUri.OriginalString : null));
				stringBuilder.AppendLine("Extra Query Params Keys (space separated) - " + this.ExtraQueryParameters.Keys.AsSingleString());
				stringBuilder.AppendLine("ClaimsAndClientCapabilities - " + this.ClaimsAndClientCapabilities);
				StringBuilder stringBuilder4 = stringBuilder;
				string text3 = "Authority - ";
				AuthorityInfo authorityInfo = this.AuthorityInfo;
				Uri uri = ((authorityInfo != null) ? authorityInfo.CanonicalAuthority : null);
				stringBuilder4.AppendLine(text3 + ((uri != null) ? uri.ToString() : null));
				stringBuilder.AppendLine("ApiId - " + this.ApiId.ToString());
				stringBuilder.AppendLine("IsConfidentialClient - " + this.AppConfig.IsConfidentialClient.ToString());
				stringBuilder.AppendLine("SendX5C - " + this.SendX5C.ToString());
				stringBuilder.AppendLine("LoginHint - " + this.LoginHint);
				stringBuilder.AppendLine("IsBrokerConfigured - " + this.AppConfig.IsBrokerEnabled.ToString());
				stringBuilder.AppendLine("HomeAccountId - " + this.HomeAccountId);
				stringBuilder.AppendLine("CorrelationId - " + this.CorrelationId.ToString());
				stringBuilder.AppendLine("UserAssertion set: " + (this.UserAssertion != null).ToString());
				stringBuilder.AppendLine("LongRunningOboCacheKey set: " + (!string.IsNullOrWhiteSpace(this.LongRunningOboCacheKey)).ToString());
				stringBuilder.AppendLine("Region configured: " + this.AppConfig.AzureRegion);
				string text4 = stringBuilder.ToString();
				stringBuilder = new StringBuilder(string.Concat(new string[]
				{
					Environment.NewLine,
					"=== Request Data ===",
					Environment.NewLine,
					"Authority Provided? - ",
					(this.Authority != null).ToString(),
					Environment.NewLine
				}));
				StringBuilder stringBuilder5 = stringBuilder;
				string text5 = "Scopes - ";
				HashSet<string> scope2 = this.Scope;
				stringBuilder5.AppendLine(text5 + ((scope2 != null) ? scope2.AsSingleString() : null));
				stringBuilder.AppendLine("Extra Query Params Keys (space separated) - " + this.ExtraQueryParameters.Keys.AsSingleString());
				stringBuilder.AppendLine("ApiId - " + this.ApiId.ToString());
				stringBuilder.AppendLine("IsConfidentialClient - " + this.AppConfig.IsConfidentialClient.ToString());
				stringBuilder.AppendLine("SendX5C - " + this.SendX5C.ToString());
				stringBuilder.AppendLine("LoginHint ? " + (!string.IsNullOrEmpty(this.LoginHint)).ToString());
				stringBuilder.AppendLine("IsBrokerConfigured - " + this.AppConfig.IsBrokerEnabled.ToString());
				stringBuilder.AppendLine("HomeAccountId - " + (!string.IsNullOrEmpty(this.HomeAccountId)).ToString());
				stringBuilder.AppendLine("CorrelationId - " + this.CorrelationId.ToString());
				stringBuilder.AppendLine("UserAssertion set: " + (this.UserAssertion != null).ToString());
				stringBuilder.AppendLine("LongRunningOboCacheKey set: " + (!string.IsNullOrWhiteSpace(this.LongRunningOboCacheKey)).ToString());
				stringBuilder.AppendLine("Region configured: " + this.AppConfig.AzureRegion);
				logger.InfoPii(text4, stringBuilder.ToString());
			}
		}

		// Token: 0x04000A39 RID: 2617
		private readonly IServiceBundle _serviceBundle;

		// Token: 0x04000A3A RID: 2618
		private readonly AcquireTokenCommonParameters _commonParameters;

		// Token: 0x04000A3B RID: 2619
		private string _loginHint;
	}
}
