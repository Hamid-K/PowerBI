using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using Microsoft.Identity.Client.Instance;
using Microsoft.Identity.Client.Instance.Discovery;
using Microsoft.Identity.Client.PlatformsCommon.Interfaces;
using Microsoft.Identity.Client.Utils;
using Microsoft.Identity.Json;

namespace Microsoft.Identity.Client
{
	// Token: 0x0200012D RID: 301
	public abstract class AbstractApplicationBuilder<T> : BaseAbstractApplicationBuilder<T> where T : BaseAbstractApplicationBuilder<T>
	{
		// Token: 0x06000EC9 RID: 3785 RVA: 0x00038708 File Offset: 0x00036908
		internal AbstractApplicationBuilder(ApplicationConfiguration configuration)
			: base(configuration)
		{
		}

		// Token: 0x06000ECA RID: 3786 RVA: 0x00038714 File Offset: 0x00036914
		[Obsolete("This method name has a typo, please use WithInstanceDiscoveryMetadata instead", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public T WithInstanceDicoveryMetadata(string instanceDiscoveryJson)
		{
			if (string.IsNullOrEmpty(instanceDiscoveryJson))
			{
				throw new ArgumentNullException(instanceDiscoveryJson);
			}
			T t;
			try
			{
				InstanceDiscoveryResponse instanceDiscoveryResponse = JsonHelper.DeserializeFromJson<InstanceDiscoveryResponse>(instanceDiscoveryJson);
				base.Config.CustomInstanceDiscoveryMetadata = instanceDiscoveryResponse;
				t = this as T;
			}
			catch (JsonException ex)
			{
				throw new MsalClientException("invalid-custom-instance-metadata", "The json containing instance metadata could not be parsed. See https://aka.ms/msal-net-custom-instance-metadata for details. ", ex);
			}
			return t;
		}

		// Token: 0x06000ECB RID: 3787 RVA: 0x00038778 File Offset: 0x00036978
		public T WithInstanceDiscoveryMetadata(string instanceDiscoveryJson)
		{
			if (string.IsNullOrEmpty(instanceDiscoveryJson))
			{
				throw new ArgumentNullException(instanceDiscoveryJson);
			}
			T t;
			try
			{
				InstanceDiscoveryResponse instanceDiscoveryResponse = JsonHelper.DeserializeFromJson<InstanceDiscoveryResponse>(instanceDiscoveryJson);
				base.Config.CustomInstanceDiscoveryMetadata = instanceDiscoveryResponse;
				t = this as T;
			}
			catch (JsonException ex)
			{
				throw new MsalClientException("invalid-custom-instance-metadata", "The json containing instance metadata could not be parsed. See https://aka.ms/msal-net-custom-instance-metadata for details. ", ex);
			}
			return t;
		}

		// Token: 0x06000ECC RID: 3788 RVA: 0x000387DC File Offset: 0x000369DC
		[Obsolete("This method name has a typo, please use WithInstanceDiscoveryMetadata instead", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public T WithInstanceDicoveryMetadata(Uri instanceDiscoveryUri)
		{
			ApplicationConfiguration config = base.Config;
			if (instanceDiscoveryUri == null)
			{
				throw new ArgumentNullException("instanceDiscoveryUri");
			}
			config.CustomInstanceDiscoveryMetadataUri = instanceDiscoveryUri;
			return this as T;
		}

		// Token: 0x06000ECD RID: 3789 RVA: 0x00038804 File Offset: 0x00036A04
		public T WithInstanceDiscoveryMetadata(Uri instanceDiscoveryUri)
		{
			ApplicationConfiguration config = base.Config;
			if (instanceDiscoveryUri == null)
			{
				throw new ArgumentNullException("instanceDiscoveryUri");
			}
			config.CustomInstanceDiscoveryMetadataUri = instanceDiscoveryUri;
			return this as T;
		}

		// Token: 0x06000ECE RID: 3790 RVA: 0x0003882C File Offset: 0x00036A2C
		internal T WithPlatformProxy(IPlatformProxy platformProxy)
		{
			base.Config.PlatformProxy = platformProxy;
			return this as T;
		}

		// Token: 0x06000ECF RID: 3791 RVA: 0x00038845 File Offset: 0x00036A45
		public T WithCacheOptions(CacheOptions options)
		{
			base.Config.AccessorOptions = options;
			return this as T;
		}

		// Token: 0x06000ED0 RID: 3792 RVA: 0x0003885E File Offset: 0x00036A5E
		internal T WithUserTokenCacheInternalForTest(ITokenCacheInternal tokenCacheInternal)
		{
			base.Config.UserTokenCacheInternalForTest = tokenCacheInternal;
			return this as T;
		}

		// Token: 0x06000ED1 RID: 3793 RVA: 0x00038877 File Offset: 0x00036A77
		public T WithLegacyCacheCompatibility(bool enableLegacyCacheCompatibility = true)
		{
			base.Config.LegacyCacheCompatibilityEnabled = enableLegacyCacheCompatibility;
			return this as T;
		}

		// Token: 0x06000ED2 RID: 3794 RVA: 0x00038890 File Offset: 0x00036A90
		[Obsolete("Telemetry is sent automatically by MSAL.NET. See https://aka.ms/msal-net-telemetry.", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal T WithTelemetry(TelemetryCallback telemetryCallback)
		{
			return this as T;
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x0003889D File Offset: 0x00036A9D
		public T WithClientId(string clientId)
		{
			base.Config.ClientId = clientId;
			return this as T;
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x000388B6 File Offset: 0x00036AB6
		public T WithRedirectUri(string redirectUri)
		{
			base.Config.RedirectUri = AbstractApplicationBuilder<T>.GetValueIfNotEmpty(base.Config.RedirectUri, redirectUri);
			return this as T;
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x000388DF File Offset: 0x00036ADF
		public T WithTenantId(string tenantId)
		{
			base.Config.TenantId = AbstractApplicationBuilder<T>.GetValueIfNotEmpty(base.Config.TenantId, tenantId);
			return this as T;
		}

		// Token: 0x06000ED6 RID: 3798 RVA: 0x00038908 File Offset: 0x00036B08
		public T WithClientName(string clientName)
		{
			base.Config.ClientName = AbstractApplicationBuilder<T>.GetValueIfNotEmpty(base.Config.ClientName, clientName);
			return this as T;
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x00038931 File Offset: 0x00036B31
		public T WithClientVersion(string clientVersion)
		{
			base.Config.ClientVersion = AbstractApplicationBuilder<T>.GetValueIfNotEmpty(base.Config.ClientVersion, clientVersion);
			return this as T;
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x0003895C File Offset: 0x00036B5C
		protected T WithOptions(ApplicationOptions applicationOptions)
		{
			this.WithClientId(applicationOptions.ClientId);
			this.WithRedirectUri(applicationOptions.RedirectUri);
			this.WithTenantId(applicationOptions.TenantId);
			this.WithClientName(applicationOptions.ClientName);
			this.WithClientVersion(applicationOptions.ClientVersion);
			this.WithClientCapabilities(applicationOptions.ClientCapabilities);
			this.WithLegacyCacheCompatibility(applicationOptions.LegacyCacheCompatibilityEnabled);
			base.WithLogging(null, new LogLevel?(applicationOptions.LogLevel), new bool?(applicationOptions.EnablePiiLogging), new bool?(applicationOptions.IsDefaultPlatformLoggingEnabled));
			base.Config.Instance = applicationOptions.Instance;
			base.Config.AadAuthorityAudience = applicationOptions.AadAuthorityAudience;
			base.Config.AzureCloudInstance = applicationOptions.AzureCloudInstance;
			return this as T;
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x00038A2B File Offset: 0x00036C2B
		public T WithExtraQueryParameters(IDictionary<string, string> extraQueryParameters)
		{
			base.Config.ExtraQueryParameters = extraQueryParameters ?? new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			return this as T;
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x00038A52 File Offset: 0x00036C52
		public T WithExtraQueryParameters(string extraQueryParameters)
		{
			if (!string.IsNullOrWhiteSpace(extraQueryParameters))
			{
				return this.WithExtraQueryParameters(CoreHelpers.ParseKeyValueList(extraQueryParameters, '&', true, null));
			}
			return this as T;
		}

		// Token: 0x06000EDB RID: 3803 RVA: 0x00038A78 File Offset: 0x00036C78
		public T WithClientCapabilities(IEnumerable<string> clientCapabilities)
		{
			if (clientCapabilities != null && clientCapabilities.Any<string>())
			{
				base.Config.ClientCapabilities = clientCapabilities;
			}
			return this as T;
		}

		// Token: 0x06000EDC RID: 3804 RVA: 0x00038A9C File Offset: 0x00036C9C
		public T WithInstanceDiscovery(bool enableInstanceDiscovery)
		{
			base.Config.IsInstanceDiscoveryEnabled = enableInstanceDiscovery;
			return this as T;
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x00038AB5 File Offset: 0x00036CB5
		[Obsolete("Telemetry is sent automatically by MSAL.NET. See https://aka.ms/msal-net-telemetry.", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public T WithTelemetry(ITelemetryConfig telemetryConfig)
		{
			return this as T;
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x00038AC4 File Offset: 0x00036CC4
		internal virtual void Validate()
		{
			if (string.IsNullOrWhiteSpace(base.Config.ClientId))
			{
				throw new MsalClientException("no_client_id", "No ClientId was specified. ");
			}
			if (base.Config.CustomInstanceDiscoveryMetadata != null && base.Config.CustomInstanceDiscoveryMetadataUri != null)
			{
				throw new MsalClientException("custom_metadata_instance_or_uri", "You have configured your own instance metadata using both an Uri and a string. Only one is supported. See https://aka.ms/msal-net-custom-instance-metadata for more details. ");
			}
			if (base.Config.Authority.AuthorityInfo.ValidateAuthority && (base.Config.CustomInstanceDiscoveryMetadata != null || base.Config.CustomInstanceDiscoveryMetadataUri != null))
			{
				throw new MsalClientException("validate_authority_or_custom_instance_metadata", "You have configured custom instance metadata, but the validateAuthority flag is set to true. These are mutually exclusive. Set the validateAuthority flag to false. See https://aka.ms/msal-net-custom-instance-metadata for more details. ");
			}
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x00038B6A File Offset: 0x00036D6A
		internal override ApplicationConfiguration BuildConfiguration()
		{
			base.ResolveAuthority();
			this.Validate();
			return base.Config;
		}

		// Token: 0x06000EE0 RID: 3808 RVA: 0x00038B7E File Offset: 0x00036D7E
		public T WithAuthority(Uri authorityUri, bool validateAuthority = true)
		{
			if (authorityUri == null)
			{
				throw new ArgumentNullException("authorityUri");
			}
			return this.WithAuthority(authorityUri.ToString(), validateAuthority);
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x00038BA1 File Offset: 0x00036DA1
		public T WithAuthority(string authorityUri, bool validateAuthority = true)
		{
			if (string.IsNullOrWhiteSpace(authorityUri))
			{
				throw new ArgumentNullException(authorityUri);
			}
			base.Config.Authority = Authority.CreateAuthority(authorityUri, validateAuthority);
			return this as T;
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x00038BCF File Offset: 0x00036DCF
		public T WithAuthority(string cloudInstanceUri, Guid tenantId, bool validateAuthority = true)
		{
			this.WithAuthority(cloudInstanceUri, tenantId.ToString("D", CultureInfo.InvariantCulture), validateAuthority);
			return this as T;
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x00038BF8 File Offset: 0x00036DF8
		public T WithAuthority(string cloudInstanceUri, string tenant, bool validateAuthority = true)
		{
			if (string.IsNullOrWhiteSpace(cloudInstanceUri))
			{
				throw new ArgumentNullException("cloudInstanceUri");
			}
			if (string.IsNullOrWhiteSpace(tenant))
			{
				throw new ArgumentNullException("tenant");
			}
			AuthorityInfo authorityInfo = AuthorityInfo.FromAadAuthority(cloudInstanceUri, tenant, validateAuthority);
			base.Config.Authority = new AadAuthority(authorityInfo);
			return this as T;
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x00038C50 File Offset: 0x00036E50
		public T WithAuthority(AzureCloudInstance azureCloudInstance, Guid tenantId, bool validateAuthority = true)
		{
			this.WithAuthority(azureCloudInstance, tenantId.ToString("D", CultureInfo.InvariantCulture), validateAuthority);
			return this as T;
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x00038C78 File Offset: 0x00036E78
		public T WithAuthority(AzureCloudInstance azureCloudInstance, string tenant, bool validateAuthority = true)
		{
			if (string.IsNullOrWhiteSpace(tenant))
			{
				throw new ArgumentNullException("tenant");
			}
			base.Config.AzureCloudInstance = azureCloudInstance;
			base.Config.TenantId = tenant;
			base.Config.ValidateAuthority = validateAuthority;
			return this as T;
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x00038CC7 File Offset: 0x00036EC7
		public T WithAuthority(AzureCloudInstance azureCloudInstance, AadAuthorityAudience authorityAudience, bool validateAuthority = true)
		{
			base.Config.AzureCloudInstance = azureCloudInstance;
			base.Config.AadAuthorityAudience = authorityAudience;
			base.Config.ValidateAuthority = validateAuthority;
			return this as T;
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x00038CF8 File Offset: 0x00036EF8
		public T WithAuthority(AadAuthorityAudience authorityAudience, bool validateAuthority = true)
		{
			base.Config.AadAuthorityAudience = authorityAudience;
			base.Config.ValidateAuthority = validateAuthority;
			return this as T;
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x00038D20 File Offset: 0x00036F20
		public T WithAdfsAuthority(string authorityUri, bool validateAuthority = true)
		{
			AuthorityInfo authorityInfo = AuthorityInfo.FromAdfsAuthority(authorityUri, validateAuthority);
			base.Config.Authority = Authority.CreateAuthority(authorityInfo);
			return this as T;
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x00038D54 File Offset: 0x00036F54
		public T WithB2CAuthority(string authorityUri)
		{
			AuthorityInfo authorityInfo = AuthorityInfo.FromB2CAuthority(authorityUri);
			base.Config.Authority = Authority.CreateAuthority(authorityInfo);
			return this as T;
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x00038D84 File Offset: 0x00036F84
		private static string GetValueIfNotEmpty(string original, string value)
		{
			if (!string.IsNullOrWhiteSpace(value))
			{
				return value;
			}
			return original;
		}
	}
}
