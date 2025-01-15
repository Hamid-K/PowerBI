using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.AppConfig;
using Microsoft.Identity.Client.Instance;
using Microsoft.Identity.Client.Internal;
using Microsoft.Identity.Client.Internal.ClientCredential;
using Microsoft.Identity.Client.TelemetryCore.TelemetryClient;
using Microsoft.IdentityModel.Abstractions;

namespace Microsoft.Identity.Client
{
	// Token: 0x02000137 RID: 311
	public class ConfidentialClientApplicationBuilder : AbstractApplicationBuilder<ConfidentialClientApplicationBuilder>
	{
		// Token: 0x06000FB7 RID: 4023 RVA: 0x00039F43 File Offset: 0x00038143
		internal ConfidentialClientApplicationBuilder(ApplicationConfiguration configuration)
			: base(configuration)
		{
			ApplicationBase.GuardMobileFrameworks();
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x00039F54 File Offset: 0x00038154
		public static ConfidentialClientApplicationBuilder CreateWithApplicationOptions(ConfidentialClientApplicationOptions options)
		{
			ApplicationBase.GuardMobileFrameworks();
			ConfidentialClientApplicationBuilder confidentialClientApplicationBuilder = new ConfidentialClientApplicationBuilder(new ApplicationConfiguration(MsalClientType.ConfidentialClient)).WithOptions(options);
			if (!string.IsNullOrWhiteSpace(options.ClientSecret))
			{
				confidentialClientApplicationBuilder = confidentialClientApplicationBuilder.WithClientSecret(options.ClientSecret);
			}
			if (!string.IsNullOrWhiteSpace(options.AzureRegion))
			{
				confidentialClientApplicationBuilder = confidentialClientApplicationBuilder.WithAzureRegion(options.AzureRegion);
			}
			return confidentialClientApplicationBuilder.WithCacheSynchronization(options.EnableCacheSynchronization);
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x00039FBA File Offset: 0x000381BA
		public static ConfidentialClientApplicationBuilder Create(string clientId)
		{
			ApplicationBase.GuardMobileFrameworks();
			return new ConfidentialClientApplicationBuilder(new ApplicationConfiguration(MsalClientType.ConfidentialClient)).WithClientId(clientId);
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x00039FD2 File Offset: 0x000381D2
		public ConfidentialClientApplicationBuilder WithCertificate(X509Certificate2 certificate)
		{
			return this.WithCertificate(certificate, false);
		}

		// Token: 0x06000FBB RID: 4027 RVA: 0x00039FDC File Offset: 0x000381DC
		public ConfidentialClientApplicationBuilder WithCertificate(X509Certificate2 certificate, bool sendX5C)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			if (!certificate.HasPrivateKey)
			{
				throw new MsalClientException("cert_without_private_key", MsalErrorMessage.CertMustHavePrivateKey("certificate"));
			}
			base.Config.ClientCredential = new CertificateClientCredential(certificate);
			base.Config.SendX5C = sendX5C;
			return this;
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x0003A032 File Offset: 0x00038232
		public ConfidentialClientApplicationBuilder WithClientClaims(X509Certificate2 certificate, IDictionary<string, string> claimsToSign, bool mergeWithDefaultClaims)
		{
			return this.WithClientClaims(certificate, claimsToSign, mergeWithDefaultClaims, false);
		}

		// Token: 0x06000FBD RID: 4029 RVA: 0x0003A040 File Offset: 0x00038240
		public ConfidentialClientApplicationBuilder WithClientClaims(X509Certificate2 certificate, IDictionary<string, string> claimsToSign, bool mergeWithDefaultClaims = true, bool sendX5C = false)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			if (claimsToSign == null || !claimsToSign.Any<KeyValuePair<string, string>>())
			{
				throw new ArgumentNullException("claimsToSign");
			}
			base.Config.ClientCredential = new CertificateAndClaimsClientCredential(certificate, claimsToSign, mergeWithDefaultClaims);
			base.Config.SendX5C = sendX5C;
			return this;
		}

		// Token: 0x06000FBE RID: 4030 RVA: 0x0003A092 File Offset: 0x00038292
		public ConfidentialClientApplicationBuilder WithClientSecret(string clientSecret)
		{
			if (string.IsNullOrWhiteSpace(clientSecret))
			{
				throw new ArgumentNullException("clientSecret");
			}
			base.Config.ClientCredential = new SecretStringClientCredential(clientSecret);
			return this;
		}

		// Token: 0x06000FBF RID: 4031 RVA: 0x0003A0B9 File Offset: 0x000382B9
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This method is not recommended. Use overload with Func<AssertionRequestOptions, Task<string>> instead, and return a non-expired assertion, which can be a Federated Credential. See https://aka.ms/msal-net-client-assertion", false)]
		public ConfidentialClientApplicationBuilder WithClientAssertion(string signedClientAssertion)
		{
			if (string.IsNullOrWhiteSpace(signedClientAssertion))
			{
				throw new ArgumentNullException("signedClientAssertion");
			}
			base.Config.ClientCredential = new SignedAssertionClientCredential(signedClientAssertion);
			return this;
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x0003A0E0 File Offset: 0x000382E0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public ConfidentialClientApplicationBuilder WithClientAssertion(Func<string> clientAssertionDelegate)
		{
			if (clientAssertionDelegate == null)
			{
				throw new ArgumentNullException("clientAssertionDelegate");
			}
			Func<CancellationToken, Task<string>> func = (CancellationToken _) => Task.FromResult<string>(clientAssertionDelegate());
			base.Config.ClientCredential = new SignedAssertionDelegateClientCredential(func);
			return this;
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x0003A12A File Offset: 0x0003832A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public ConfidentialClientApplicationBuilder WithClientAssertion(Func<CancellationToken, Task<string>> clientAssertionAsyncDelegate)
		{
			if (clientAssertionAsyncDelegate == null)
			{
				throw new ArgumentNullException("clientAssertionAsyncDelegate");
			}
			base.Config.ClientCredential = new SignedAssertionDelegateClientCredential(clientAssertionAsyncDelegate);
			return this;
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x0003A14C File Offset: 0x0003834C
		public ConfidentialClientApplicationBuilder WithClientAssertion(Func<AssertionRequestOptions, Task<string>> clientAssertionAsyncDelegate)
		{
			if (clientAssertionAsyncDelegate == null)
			{
				throw new ArgumentNullException("clientAssertionAsyncDelegate");
			}
			base.Config.ClientCredential = new SignedAssertionDelegateClientCredential(clientAssertionAsyncDelegate);
			return this;
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x0003A16E File Offset: 0x0003836E
		public ConfidentialClientApplicationBuilder WithAzureRegion(string azureRegion = "TryAutoDetect")
		{
			if (string.IsNullOrEmpty(azureRegion))
			{
				throw new ArgumentNullException("azureRegion");
			}
			base.Config.AzureRegion = azureRegion;
			return this;
		}

		// Token: 0x06000FC4 RID: 4036 RVA: 0x0003A190 File Offset: 0x00038390
		public ConfidentialClientApplicationBuilder WithCacheSynchronization(bool enableCacheSynchronization)
		{
			base.Config.CacheSynchronizationEnabled = enableCacheSynchronization;
			return this;
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x0003A19F File Offset: 0x0003839F
		[Obsolete("This method has been renamed to WithOidcAuthority.", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public ConfidentialClientApplicationBuilder WithGenericAuthority(string authorityUri)
		{
			return this.WithOidcAuthority(authorityUri);
		}

		// Token: 0x06000FC6 RID: 4038 RVA: 0x0003A1A8 File Offset: 0x000383A8
		public ConfidentialClientApplicationBuilder WithOidcAuthority(string authorityUri)
		{
			AuthorityInfo authorityInfo = AuthorityInfo.FromGenericAuthority(authorityUri);
			base.Config.Authority = Authority.CreateAuthority(authorityInfo);
			return this;
		}

		// Token: 0x06000FC7 RID: 4039 RVA: 0x0003A1D0 File Offset: 0x000383D0
		public ConfidentialClientApplicationBuilder WithTelemetryClient(params ITelemetryClient[] telemetryClients)
		{
			if (telemetryClients == null)
			{
				throw new ArgumentNullException("telemetryClients");
			}
			if (telemetryClients.Length != 0)
			{
				foreach (ITelemetryClient telemetryClient in telemetryClients)
				{
					if (telemetryClient == null)
					{
						throw new ArgumentNullException("telemetryClient");
					}
					telemetryClient.Initialize();
				}
				base.Config.TelemetryClients = telemetryClients;
			}
			this.TelemetryClientLogMsalVersion();
			return this;
		}

		// Token: 0x06000FC8 RID: 4040 RVA: 0x0003A228 File Offset: 0x00038428
		private void TelemetryClientLogMsalVersion()
		{
			if (base.Config.TelemetryClients.HasEnabledClients("config_update"))
			{
				MsalTelemetryEventDetails msalTelemetryEventDetails = new MsalTelemetryEventDetails("config_update");
				msalTelemetryEventDetails.SetProperty("MsalVersion", MsalIdHelper.GetMsalVersion());
				base.Config.TelemetryClients.TrackEvent(msalTelemetryEventDetails);
			}
		}

		// Token: 0x06000FC9 RID: 4041 RVA: 0x0003A278 File Offset: 0x00038478
		internal ConfidentialClientApplicationBuilder WithAppTokenCacheInternalForTest(ITokenCacheInternal tokenCacheInternal)
		{
			base.Config.AppTokenCacheInternalForTest = tokenCacheInternal;
			return this;
		}

		// Token: 0x06000FCA RID: 4042 RVA: 0x0003A288 File Offset: 0x00038488
		internal override void Validate()
		{
			base.Validate();
			if (string.IsNullOrWhiteSpace(base.Config.RedirectUri))
			{
				base.Config.RedirectUri = "https://replyUrlNotSet";
			}
			Uri uri;
			if (!Uri.TryCreate(base.Config.RedirectUri, UriKind.Absolute, out uri))
			{
				throw new InvalidOperationException(MsalErrorMessage.InvalidRedirectUriReceived(base.Config.RedirectUri));
			}
			if (!string.IsNullOrEmpty(base.Config.AzureRegion) && (base.Config.CustomInstanceDiscoveryMetadata != null || base.Config.CustomInstanceDiscoveryMetadataUri != null))
			{
				throw new MsalClientException("region_discovery_with_custom_instance_metadata", "Configure either region discovery or custom instance metadata. Custom instance discovery metadata overrides region discovery. ");
			}
		}

		// Token: 0x06000FCB RID: 4043 RVA: 0x0003A32A File Offset: 0x0003852A
		public IConfidentialClientApplication Build()
		{
			return this.BuildConcrete();
		}

		// Token: 0x06000FCC RID: 4044 RVA: 0x0003A332 File Offset: 0x00038532
		internal ConfidentialClientApplication BuildConcrete()
		{
			return new ConfidentialClientApplication(this.BuildConfiguration());
		}
	}
}
