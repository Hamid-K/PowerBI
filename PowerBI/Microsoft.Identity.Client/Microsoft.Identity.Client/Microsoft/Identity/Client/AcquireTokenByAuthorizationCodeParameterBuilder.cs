using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.Advanced;
using Microsoft.Identity.Client.ApiConfig.Executors;
using Microsoft.Identity.Client.ApiConfig.Parameters;
using Microsoft.Identity.Client.TelemetryCore.Internal.Events;
using Microsoft.Identity.Client.Utils;

namespace Microsoft.Identity.Client
{
	// Token: 0x0200011C RID: 284
	public sealed class AcquireTokenByAuthorizationCodeParameterBuilder : AbstractConfidentialClientAcquireTokenParameterBuilder<AcquireTokenByAuthorizationCodeParameterBuilder>
	{
		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000E0F RID: 3599 RVA: 0x000373A1 File Offset: 0x000355A1
		private AcquireTokenByAuthorizationCodeParameters Parameters { get; } = new AcquireTokenByAuthorizationCodeParameters();

		// Token: 0x06000E10 RID: 3600 RVA: 0x000373A9 File Offset: 0x000355A9
		internal AcquireTokenByAuthorizationCodeParameterBuilder(IConfidentialClientApplicationExecutor confidentialClientApplicationExecutor)
			: base(confidentialClientApplicationExecutor)
		{
			ApplicationBase.GuardMobileFrameworks();
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x000373C2 File Offset: 0x000355C2
		internal static AcquireTokenByAuthorizationCodeParameterBuilder Create(IConfidentialClientApplicationExecutor confidentialClientApplicationExecutor, IEnumerable<string> scopes, string authorizationCode)
		{
			ApplicationBase.GuardMobileFrameworks();
			return new AcquireTokenByAuthorizationCodeParameterBuilder(confidentialClientApplicationExecutor).WithScopes(scopes).WithAuthorizationCode(authorizationCode);
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x000373DB File Offset: 0x000355DB
		private AcquireTokenByAuthorizationCodeParameterBuilder WithAuthorizationCode(string authorizationCode)
		{
			this.Parameters.AuthorizationCode = authorizationCode;
			return this;
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x000373EA File Offset: 0x000355EA
		internal override ApiEvent.ApiIds CalculateApiEventId()
		{
			return ApiEvent.ApiIds.AcquireTokenByAuthorizationCode;
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x000373F4 File Offset: 0x000355F4
		protected override void Validate()
		{
			base.Validate();
			if (string.IsNullOrWhiteSpace(this.Parameters.AuthorizationCode))
			{
				throw new ArgumentException("AuthorizationCode can not be null or whitespace", "AuthorizationCode");
			}
			if (this.Parameters.SendX5C == null)
			{
				this.Parameters.SendX5C = new bool?(base.ServiceBundle.Config.SendX5C);
			}
		}

		// Token: 0x06000E15 RID: 3605 RVA: 0x0003745E File Offset: 0x0003565E
		internal override Task<AuthenticationResult> ExecuteInternalAsync(CancellationToken cancellationToken)
		{
			return base.ConfidentialClientApplicationExecutor.ExecuteAsync(base.CommonParameters, this.Parameters, cancellationToken);
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x00037478 File Offset: 0x00035678
		public AcquireTokenByAuthorizationCodeParameterBuilder WithSendX5C(bool withSendX5C)
		{
			this.Parameters.SendX5C = new bool?(withSendX5C);
			return this;
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x0003748C File Offset: 0x0003568C
		public AcquireTokenByAuthorizationCodeParameterBuilder WithPkceCodeVerifier(string pkceCodeVerifier)
		{
			this.Parameters.PkceCodeVerifier = pkceCodeVerifier;
			return this;
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x0003749C File Offset: 0x0003569C
		public AcquireTokenByAuthorizationCodeParameterBuilder WithCcsRoutingHint(string userObjectIdentifier, string tenantIdentifier)
		{
			if (string.IsNullOrEmpty(userObjectIdentifier) || string.IsNullOrEmpty(tenantIdentifier))
			{
				return this;
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string> { 
			{
				"x-anchormailbox",
				CoreHelpers.GetCcsClientInfoHint(userObjectIdentifier, tenantIdentifier)
			} };
			this.WithExtraHttpHeaders(dictionary);
			return this;
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x000374DC File Offset: 0x000356DC
		public AcquireTokenByAuthorizationCodeParameterBuilder WithCcsRoutingHint(string userName)
		{
			if (string.IsNullOrEmpty(userName))
			{
				return this;
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string> { 
			{
				"x-anchormailbox",
				CoreHelpers.GetCcsUpnHint(userName)
			} };
			this.WithExtraHttpHeaders(dictionary);
			return this;
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x00037513 File Offset: 0x00035713
		public AcquireTokenByAuthorizationCodeParameterBuilder WithSpaAuthorizationCode(bool requestSpaAuthorizationCode = true)
		{
			this.Parameters.SpaCode = requestSpaAuthorizationCode;
			return this;
		}
	}
}
