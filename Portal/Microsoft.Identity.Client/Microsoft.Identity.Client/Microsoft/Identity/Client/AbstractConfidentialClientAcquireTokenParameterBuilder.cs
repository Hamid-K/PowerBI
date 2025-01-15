using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.ApiConfig.Executors;
using Microsoft.Identity.Client.ApiConfig.Parameters;
using Microsoft.Identity.Client.AppConfig;
using Microsoft.Identity.Client.AuthScheme.PoP;
using Microsoft.Identity.Client.Internal;

namespace Microsoft.Identity.Client
{
	// Token: 0x02000119 RID: 281
	public abstract class AbstractConfidentialClientAcquireTokenParameterBuilder<T> : AbstractAcquireTokenParameterBuilder<T> where T : AbstractAcquireTokenParameterBuilder<T>
	{
		// Token: 0x06000E00 RID: 3584 RVA: 0x0003723D File Offset: 0x0003543D
		internal AbstractConfidentialClientAcquireTokenParameterBuilder(IConfidentialClientApplicationExecutor confidentialClientApplicationExecutor)
			: base(confidentialClientApplicationExecutor.ServiceBundle)
		{
			ApplicationBase.GuardMobileFrameworks();
			this.ConfidentialClientApplicationExecutor = confidentialClientApplicationExecutor;
		}

		// Token: 0x06000E01 RID: 3585
		internal abstract Task<AuthenticationResult> ExecuteInternalAsync(CancellationToken cancellationToken);

		// Token: 0x06000E02 RID: 3586 RVA: 0x00037257 File Offset: 0x00035457
		public override Task<AuthenticationResult> ExecuteAsync(CancellationToken cancellationToken)
		{
			ApplicationBase.GuardMobileFrameworks();
			base.ValidateAndCalculateApiId();
			return this.ExecuteInternalAsync(cancellationToken);
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x0003726C File Offset: 0x0003546C
		protected override void Validate()
		{
			IServiceBundle serviceBundle = base.ServiceBundle;
			if (((serviceBundle != null) ? serviceBundle.Config.ClientCredential : null) == null && base.CommonParameters.OnBeforeTokenRequestHandler == null)
			{
				IServiceBundle serviceBundle2 = base.ServiceBundle;
				if (((serviceBundle2 != null) ? serviceBundle2.Config.AppTokenProvider : null) == null)
				{
					throw new MsalClientException("Client_Credentials_Required_In_Confidential_Client_Application", "One client credential type required either: ClientSecret, Certificate, ClientAssertion or AppTokenProvider must be defined when creating a Confidential Client. Only specify one. See https://aka.ms/msal-net-client-credentials. ");
				}
			}
			base.Validate();
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000E04 RID: 3588 RVA: 0x000372CE File Offset: 0x000354CE
		internal IConfidentialClientApplicationExecutor ConfidentialClientApplicationExecutor { get; }

		// Token: 0x06000E05 RID: 3589 RVA: 0x000372D8 File Offset: 0x000354D8
		public T WithProofOfPossession(PoPAuthenticationConfiguration popAuthenticationConfiguration)
		{
			base.ValidateUseOfExperimentalFeature("WithProofOfPossession");
			AcquireTokenCommonParameters commonParameters = base.CommonParameters;
			if (popAuthenticationConfiguration == null)
			{
				throw new ArgumentNullException("popAuthenticationConfiguration");
			}
			commonParameters.PopAuthenticationConfiguration = popAuthenticationConfiguration;
			base.CommonParameters.AuthenticationScheme = new PopAuthenticationScheme(base.CommonParameters.PopAuthenticationConfiguration, base.ServiceBundle);
			return this as T;
		}
	}
}
