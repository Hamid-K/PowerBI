using System;
using System.Runtime.CompilerServices;
using Microsoft.Identity.Client.Http;
using Microsoft.Identity.Client.Instance;
using Microsoft.IdentityModel.Abstractions;

namespace Microsoft.Identity.Client
{
	// Token: 0x02000134 RID: 308
	public abstract class BaseAbstractApplicationBuilder<T> where T : BaseAbstractApplicationBuilder<T>
	{
		// Token: 0x06000F9C RID: 3996 RVA: 0x00039AE7 File Offset: 0x00037CE7
		internal BaseAbstractApplicationBuilder(ApplicationConfiguration configuration)
		{
			this.Config = configuration;
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000F9D RID: 3997 RVA: 0x00039AF6 File Offset: 0x00037CF6
		internal ApplicationConfiguration Config { get; }

		// Token: 0x06000F9E RID: 3998 RVA: 0x00039AFE File Offset: 0x00037CFE
		public T WithHttpClientFactory(IMsalHttpClientFactory httpClientFactory)
		{
			this.Config.HttpClientFactory = httpClientFactory;
			return (T)((object)this);
		}

		// Token: 0x06000F9F RID: 3999 RVA: 0x00039B12 File Offset: 0x00037D12
		public T WithHttpClientFactory(IMsalHttpClientFactory httpClientFactory, bool retryOnceOn5xx)
		{
			this.Config.HttpClientFactory = httpClientFactory;
			this.Config.RetryOnServerErrors = retryOnceOn5xx;
			return (T)((object)this);
		}

		// Token: 0x06000FA0 RID: 4000 RVA: 0x00039B32 File Offset: 0x00037D32
		internal T WithHttpManager(IHttpManager httpManager)
		{
			this.Config.HttpManager = httpManager;
			return (T)((object)this);
		}

		// Token: 0x06000FA1 RID: 4001 RVA: 0x00039B48 File Offset: 0x00037D48
		public T WithLogging(LogCallback loggingCallback, LogLevel? logLevel = null, bool? enablePiiLogging = null, bool? enableDefaultPlatformLogging = null)
		{
			if (this.Config.LoggingCallback != null)
			{
				throw new InvalidOperationException("LoggingCallback has already been set. ");
			}
			this.Config.LoggingCallback = loggingCallback;
			this.Config.LogLevel = logLevel ?? this.Config.LogLevel;
			this.Config.EnablePiiLogging = enablePiiLogging ?? this.Config.EnablePiiLogging;
			this.Config.IsDefaultPlatformLoggingEnabled = enableDefaultPlatformLogging ?? this.Config.IsDefaultPlatformLoggingEnabled;
			return (T)((object)this);
		}

		// Token: 0x06000FA2 RID: 4002 RVA: 0x00039BFE File Offset: 0x00037DFE
		public T WithLogging(IIdentityLogger identityLogger, bool enablePiiLogging = false)
		{
			this.Config.IdentityLogger = identityLogger;
			this.Config.EnablePiiLogging = enablePiiLogging;
			return (T)((object)this);
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x00039C1E File Offset: 0x00037E1E
		public T WithDebugLoggingCallback(LogLevel logLevel = LogLevel.Info, bool enablePiiLogging = false, bool withDefaultPlatformLoggingEnabled = false)
		{
			this.WithLogging(delegate(LogLevel level, string message, bool _)
			{
			}, new LogLevel?(logLevel), new bool?(enablePiiLogging), new bool?(withDefaultPlatformLoggingEnabled));
			return (T)((object)this);
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x00039C5E File Offset: 0x00037E5E
		protected T WithOptions(BaseApplicationOptions applicationOptions)
		{
			this.WithLogging(null, new LogLevel?(applicationOptions.LogLevel), new bool?(applicationOptions.EnablePiiLogging), new bool?(applicationOptions.IsDefaultPlatformLoggingEnabled));
			return (T)((object)this);
		}

		// Token: 0x06000FA5 RID: 4005 RVA: 0x00039C8F File Offset: 0x00037E8F
		public T WithExperimentalFeatures(bool enableExperimentalFeatures = true)
		{
			this.Config.ExperimentalFeaturesEnabled = enableExperimentalFeatures;
			return (T)((object)this);
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x00039CA3 File Offset: 0x00037EA3
		internal virtual ApplicationConfiguration BuildConfiguration()
		{
			this.ResolveAuthority();
			return this.Config;
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x00039CB4 File Offset: 0x00037EB4
		internal void ResolveAuthority()
		{
			Authority authority = this.Config.Authority;
			if (((authority != null) ? authority.AuthorityInfo : null) != null)
			{
				if (!string.IsNullOrEmpty(this.Config.TenantId))
				{
					string tenantedAuthority = this.Config.Authority.GetTenantedAuthority(this.Config.TenantId, true);
					this.Config.Authority = Authority.CreateAuthority(tenantedAuthority, this.Config.Authority.AuthorityInfo.ValidateAuthority);
					return;
				}
			}
			else
			{
				string authorityInstance = this.GetAuthorityInstance();
				string authorityAudience = this.GetAuthorityAudience();
				AuthorityInfo authorityInfo = new AuthorityInfo(AuthorityType.Aad, new Uri(authorityInstance + "/" + authorityAudience).ToString(), this.Config.ValidateAuthority);
				this.Config.Authority = new AadAuthority(authorityInfo);
			}
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x00039D7C File Offset: 0x00037F7C
		private string GetAuthorityAudience()
		{
			if (!string.IsNullOrWhiteSpace(this.Config.TenantId) && this.Config.AadAuthorityAudience != AadAuthorityAudience.None && this.Config.AadAuthorityAudience != AadAuthorityAudience.AzureAdMyOrg)
			{
				throw new InvalidOperationException("TenantId and AadAuthorityAudience are both set, but they're mutually exclusive. ");
			}
			if (this.Config.AadAuthorityAudience != AadAuthorityAudience.None)
			{
				return AuthorityInfo.GetAadAuthorityAudienceValue(this.Config.AadAuthorityAudience, this.Config.TenantId);
			}
			if (!string.IsNullOrWhiteSpace(this.Config.TenantId))
			{
				return this.Config.TenantId;
			}
			return AuthorityInfo.GetAadAuthorityAudienceValue(AadAuthorityAudience.AzureAdAndPersonalMicrosoftAccount, string.Empty);
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x00039E14 File Offset: 0x00038014
		private string GetAuthorityInstance()
		{
			if (!string.IsNullOrWhiteSpace(this.Config.Instance) && this.Config.AzureCloudInstance != AzureCloudInstance.None)
			{
				throw new InvalidOperationException("Instance and AzureCloudInstance are both set but they're mutually exclusive. ");
			}
			if (!string.IsNullOrWhiteSpace(this.Config.Instance))
			{
				this.Config.Instance = this.Config.Instance.TrimEnd(new char[] { ' ', '/' });
				return this.Config.Instance;
			}
			if (this.Config.AzureCloudInstance != AzureCloudInstance.None)
			{
				return AuthorityInfo.GetCloudUrl(this.Config.AzureCloudInstance);
			}
			return AuthorityInfo.GetCloudUrl(AzureCloudInstance.AzurePublic);
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x00039EB8 File Offset: 0x000380B8
		internal void ValidateUseOfExperimentalFeature([CallerMemberName] string memberName = "")
		{
			if (!this.Config.ExperimentalFeaturesEnabled)
			{
				throw new MsalClientException("experimental_feature", MsalErrorMessage.ExperimentalFeature(memberName));
			}
		}
	}
}
