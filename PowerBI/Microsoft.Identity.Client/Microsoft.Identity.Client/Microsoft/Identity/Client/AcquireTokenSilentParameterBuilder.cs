using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.ApiConfig.Executors;
using Microsoft.Identity.Client.ApiConfig.Parameters;
using Microsoft.Identity.Client.AppConfig;
using Microsoft.Identity.Client.AuthScheme;
using Microsoft.Identity.Client.AuthScheme.PoP;
using Microsoft.Identity.Client.Internal.Broker;
using Microsoft.Identity.Client.TelemetryCore.Internal.Events;

namespace Microsoft.Identity.Client
{
	// Token: 0x02000124 RID: 292
	public sealed class AcquireTokenSilentParameterBuilder : AbstractClientAppBaseAcquireTokenParameterBuilder<AcquireTokenSilentParameterBuilder>
	{
		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000E67 RID: 3687 RVA: 0x00037D99 File Offset: 0x00035F99
		private AcquireTokenSilentParameters Parameters { get; } = new AcquireTokenSilentParameters();

		// Token: 0x06000E68 RID: 3688 RVA: 0x00037DA1 File Offset: 0x00035FA1
		internal AcquireTokenSilentParameterBuilder(IClientApplicationBaseExecutor clientApplicationBaseExecutor)
			: base(clientApplicationBaseExecutor)
		{
		}

		// Token: 0x06000E69 RID: 3689 RVA: 0x00037DB5 File Offset: 0x00035FB5
		internal static AcquireTokenSilentParameterBuilder Create(IClientApplicationBaseExecutor clientApplicationBaseExecutor, IEnumerable<string> scopes, IAccount account)
		{
			return new AcquireTokenSilentParameterBuilder(clientApplicationBaseExecutor).WithScopes(scopes).WithAccount(account);
		}

		// Token: 0x06000E6A RID: 3690 RVA: 0x00037DC9 File Offset: 0x00035FC9
		internal static AcquireTokenSilentParameterBuilder Create(IClientApplicationBaseExecutor clientApplicationBaseExecutor, IEnumerable<string> scopes, string loginHint)
		{
			return new AcquireTokenSilentParameterBuilder(clientApplicationBaseExecutor).WithScopes(scopes).WithLoginHint(loginHint);
		}

		// Token: 0x06000E6B RID: 3691 RVA: 0x00037DDD File Offset: 0x00035FDD
		private AcquireTokenSilentParameterBuilder WithAccount(IAccount account)
		{
			this.Parameters.Account = account;
			return this;
		}

		// Token: 0x06000E6C RID: 3692 RVA: 0x00037DEC File Offset: 0x00035FEC
		private AcquireTokenSilentParameterBuilder WithLoginHint(string loginHint)
		{
			this.Parameters.LoginHint = loginHint;
			return this;
		}

		// Token: 0x06000E6D RID: 3693 RVA: 0x00037DFB File Offset: 0x00035FFB
		public AcquireTokenSilentParameterBuilder WithForceRefresh(bool forceRefresh)
		{
			this.Parameters.ForceRefresh = forceRefresh;
			return this;
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x00037E0A File Offset: 0x0003600A
		internal override Task<AuthenticationResult> ExecuteInternalAsync(CancellationToken cancellationToken)
		{
			return base.ClientApplicationBaseExecutor.ExecuteAsync(base.CommonParameters, this.Parameters, cancellationToken);
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x00037E24 File Offset: 0x00036024
		protected override void Validate()
		{
			base.Validate();
			if (this.Parameters.SendX5C == null)
			{
				this.Parameters.SendX5C = new bool?(base.ServiceBundle.Config.SendX5C);
			}
			if (base.ServiceBundle.Config.Authority.AuthorityInfo.AuthorityType == AuthorityType.B2C)
			{
				if (base.CommonParameters.Scopes != null)
				{
					IEnumerable<string> scopes = base.CommonParameters.Scopes;
					Func<string, bool> func;
					if ((func = AcquireTokenSilentParameterBuilder.<>O.<0>__IsNullOrWhiteSpace) == null)
					{
						func = (AcquireTokenSilentParameterBuilder.<>O.<0>__IsNullOrWhiteSpace = new Func<string, bool>(string.IsNullOrWhiteSpace));
					}
					if (!scopes.All(func))
					{
						return;
					}
				}
				throw new MsalUiRequiredException("scopes_required_client_credentials", "At least one scope needs to be requested for this authentication flow. ", null, UiRequiredExceptionClassification.AcquireTokenSilentFailed);
			}
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x00037ED5 File Offset: 0x000360D5
		internal override ApiEvent.ApiIds CalculateApiEventId()
		{
			return ApiEvent.ApiIds.AcquireTokenSilent;
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x00037EDC File Offset: 0x000360DC
		public AcquireTokenSilentParameterBuilder WithSendX5C(bool withSendX5C)
		{
			this.Parameters.SendX5C = new bool?(withSendX5C);
			return this;
		}

		// Token: 0x06000E72 RID: 3698 RVA: 0x00037EF0 File Offset: 0x000360F0
		public AcquireTokenSilentParameterBuilder WithProofOfPossession(PoPAuthenticationConfiguration popAuthenticationConfiguration)
		{
			ApplicationBase.GuardMobileFrameworks();
			base.ValidateUseOfExperimentalFeature("WithProofOfPossession");
			AcquireTokenCommonParameters commonParameters = base.CommonParameters;
			if (popAuthenticationConfiguration == null)
			{
				throw new ArgumentNullException("popAuthenticationConfiguration");
			}
			commonParameters.PopAuthenticationConfiguration = popAuthenticationConfiguration;
			base.CommonParameters.AuthenticationScheme = new PopAuthenticationScheme(base.CommonParameters.PopAuthenticationConfiguration, base.ServiceBundle);
			return this;
		}

		// Token: 0x06000E73 RID: 3699 RVA: 0x00037F4C File Offset: 0x0003614C
		public AcquireTokenSilentParameterBuilder WithProofOfPossession(string nonce, HttpMethod httpMethod, Uri requestUri)
		{
			if (base.ServiceBundle.Config.IsConfidentialClient)
			{
				base.ValidateUseOfExperimentalFeature("WithProofOfPossession");
			}
			if (!base.ServiceBundle.Config.IsConfidentialClient && !base.ServiceBundle.Config.IsBrokerEnabled)
			{
				throw new MsalClientException("broker_required_for_pop", "The request has Proof-of-Possession configured but does not have broker enabled. Broker is required to use Proof-of-Possession on public clients. Use IPublicClientApplication.IsProofOfPossessionSupportedByClient to ensure Proof-of-Possession can be performed before using WithProofOfPossession.");
			}
			ApplicationBase.GuardMobileFrameworks();
			IBroker broker = base.ServiceBundle.PlatformProxy.CreateBroker(base.ServiceBundle.Config, null);
			if (base.ServiceBundle.Config.IsBrokerEnabled)
			{
				if (string.IsNullOrEmpty(nonce))
				{
					throw new ArgumentNullException("nonce");
				}
				if (!broker.IsPopSupported)
				{
					throw new MsalClientException("broker_does_not_support_pop", "The broker does not support Proof-of-Possession on the current platform.");
				}
			}
			if (requestUri == null)
			{
				throw new ArgumentNullException("requestUri");
			}
			PoPAuthenticationConfiguration poPAuthenticationConfiguration = new PoPAuthenticationConfiguration(requestUri);
			PoPAuthenticationConfiguration poPAuthenticationConfiguration2 = poPAuthenticationConfiguration;
			if (httpMethod == null)
			{
				throw new ArgumentNullException("httpMethod");
			}
			poPAuthenticationConfiguration2.HttpMethod = httpMethod;
			poPAuthenticationConfiguration.Nonce = nonce;
			IAuthenticationScheme authenticationScheme;
			if (base.ServiceBundle.Config.IsBrokerEnabled)
			{
				poPAuthenticationConfiguration.SignHttpRequest = false;
				authenticationScheme = new PopBrokerAuthenticationScheme();
			}
			else
			{
				authenticationScheme = new PopAuthenticationScheme(poPAuthenticationConfiguration, base.ServiceBundle);
			}
			base.CommonParameters.PopAuthenticationConfiguration = poPAuthenticationConfiguration;
			base.CommonParameters.AuthenticationScheme = authenticationScheme;
			return this;
		}

		// Token: 0x020003BC RID: 956
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x0400109D RID: 4253
			public static Func<string, bool> <0>__IsNullOrWhiteSpace;
		}
	}
}
