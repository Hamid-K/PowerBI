using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.ApiConfig.Executors;
using Microsoft.Identity.Client.ApiConfig.Parameters;
using Microsoft.Identity.Client.AppConfig;
using Microsoft.Identity.Client.AuthScheme.PoP;
using Microsoft.Identity.Client.TelemetryCore.Internal.Events;

namespace Microsoft.Identity.Client
{
	// Token: 0x0200011F RID: 287
	public sealed class AcquireTokenByUsernamePasswordParameterBuilder : AbstractPublicClientAcquireTokenParameterBuilder<AcquireTokenByUsernamePasswordParameterBuilder>
	{
		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000E2A RID: 3626 RVA: 0x00037647 File Offset: 0x00035847
		private AcquireTokenByUsernamePasswordParameters Parameters { get; } = new AcquireTokenByUsernamePasswordParameters();

		// Token: 0x06000E2B RID: 3627 RVA: 0x0003764F File Offset: 0x0003584F
		internal AcquireTokenByUsernamePasswordParameterBuilder(IPublicClientApplicationExecutor publicClientApplicationExecutor)
			: base(publicClientApplicationExecutor)
		{
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x00037663 File Offset: 0x00035863
		internal static AcquireTokenByUsernamePasswordParameterBuilder Create(IPublicClientApplicationExecutor publicClientApplicationExecutor, IEnumerable<string> scopes, string username, string password)
		{
			return new AcquireTokenByUsernamePasswordParameterBuilder(publicClientApplicationExecutor).WithScopes(scopes).WithUsername(username).WithPassword(password);
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x0003767D File Offset: 0x0003587D
		public AcquireTokenByUsernamePasswordParameterBuilder WithFederationMetadata(string federationMetadata)
		{
			this.Parameters.FederationMetadata = federationMetadata;
			return this;
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x0003768C File Offset: 0x0003588C
		public AcquireTokenByUsernamePasswordParameterBuilder WithProofOfPossession(string nonce, HttpMethod httpMethod, Uri requestUri)
		{
			ApplicationBase.GuardMobileFrameworks();
			if (!base.ServiceBundle.Config.IsBrokerEnabled)
			{
				throw new MsalClientException("broker_required_for_pop", "The request has Proof-of-Possession configured but does not have broker enabled. Broker is required to use Proof-of-Possession on public clients. Use IPublicClientApplication.IsProofOfPossessionSupportedByClient to ensure Proof-of-Possession can be performed before using WithProofOfPossession.");
			}
			if (!base.ServiceBundle.PlatformProxy.CreateBroker(base.ServiceBundle.Config, null).IsPopSupported)
			{
				throw new MsalClientException("broker_does_not_support_pop", "The broker does not support Proof-of-Possession on the current platform.");
			}
			if (string.IsNullOrEmpty(nonce))
			{
				throw new ArgumentNullException("nonce");
			}
			PoPAuthenticationConfiguration poPAuthenticationConfiguration = new PoPAuthenticationConfiguration(requestUri);
			poPAuthenticationConfiguration.Nonce = nonce;
			poPAuthenticationConfiguration.HttpMethod = httpMethod;
			base.CommonParameters.PopAuthenticationConfiguration = poPAuthenticationConfiguration;
			base.CommonParameters.AuthenticationScheme = new PopBrokerAuthenticationScheme();
			return this;
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x00037738 File Offset: 0x00035938
		private AcquireTokenByUsernamePasswordParameterBuilder WithUsername(string username)
		{
			this.Parameters.Username = username;
			return this;
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x00037747 File Offset: 0x00035947
		private AcquireTokenByUsernamePasswordParameterBuilder WithPassword(string password)
		{
			this.Parameters.Password = password;
			return this;
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x00037756 File Offset: 0x00035956
		internal override Task<AuthenticationResult> ExecuteInternalAsync(CancellationToken cancellationToken)
		{
			return base.PublicClientApplicationExecutor.ExecuteAsync(base.CommonParameters, this.Parameters, cancellationToken);
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x00037770 File Offset: 0x00035970
		internal override ApiEvent.ApiIds CalculateApiEventId()
		{
			return ApiEvent.ApiIds.AcquireTokenByUsernamePassword;
		}
	}
}
