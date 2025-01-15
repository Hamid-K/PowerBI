using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.ApiConfig.Executors;
using Microsoft.Identity.Client.ApiConfig.Parameters;
using Microsoft.Identity.Client.TelemetryCore.Internal.Events;

namespace Microsoft.Identity.Client
{
	// Token: 0x0200011D RID: 285
	public sealed class AcquireTokenByIntegratedWindowsAuthParameterBuilder : AbstractPublicClientAcquireTokenParameterBuilder<AcquireTokenByIntegratedWindowsAuthParameterBuilder>
	{
		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000E1B RID: 3611 RVA: 0x00037522 File Offset: 0x00035722
		private AcquireTokenByIntegratedWindowsAuthParameters Parameters { get; } = new AcquireTokenByIntegratedWindowsAuthParameters();

		// Token: 0x06000E1C RID: 3612 RVA: 0x0003752A File Offset: 0x0003572A
		internal AcquireTokenByIntegratedWindowsAuthParameterBuilder(IPublicClientApplicationExecutor publicClientApplicationExecutor)
			: base(publicClientApplicationExecutor)
		{
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x0003753E File Offset: 0x0003573E
		internal static AcquireTokenByIntegratedWindowsAuthParameterBuilder Create(IPublicClientApplicationExecutor publicClientApplicationExecutor, IEnumerable<string> scopes)
		{
			return new AcquireTokenByIntegratedWindowsAuthParameterBuilder(publicClientApplicationExecutor).WithScopes(scopes);
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x0003754C File Offset: 0x0003574C
		public AcquireTokenByIntegratedWindowsAuthParameterBuilder WithUsername(string username)
		{
			this.Parameters.Username = username;
			return this;
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x0003755B File Offset: 0x0003575B
		public AcquireTokenByIntegratedWindowsAuthParameterBuilder WithFederationMetadata(string federationMetadata)
		{
			this.Parameters.FederationMetadata = federationMetadata;
			return this;
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x0003756A File Offset: 0x0003576A
		internal override Task<AuthenticationResult> ExecuteInternalAsync(CancellationToken cancellationToken)
		{
			return base.PublicClientApplicationExecutor.ExecuteAsync(base.CommonParameters, this.Parameters, cancellationToken);
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x00037584 File Offset: 0x00035784
		internal override ApiEvent.ApiIds CalculateApiEventId()
		{
			return ApiEvent.ApiIds.AcquireTokenByIntegratedWindowsAuth;
		}
	}
}
