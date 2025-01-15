using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.ApiConfig.Executors;
using Microsoft.Identity.Client.ApiConfig.Parameters;
using Microsoft.Identity.Client.TelemetryCore.Internal.Events;

namespace Microsoft.Identity.Client
{
	// Token: 0x0200011E RID: 286
	public sealed class AcquireTokenByRefreshTokenParameterBuilder : AbstractClientAppBaseAcquireTokenParameterBuilder<AcquireTokenByRefreshTokenParameterBuilder>
	{
		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000E22 RID: 3618 RVA: 0x0003758B File Offset: 0x0003578B
		private AcquireTokenByRefreshTokenParameters Parameters { get; } = new AcquireTokenByRefreshTokenParameters();

		// Token: 0x06000E23 RID: 3619 RVA: 0x00037593 File Offset: 0x00035793
		internal AcquireTokenByRefreshTokenParameterBuilder(IClientApplicationBaseExecutor clientApplicationBaseExecutor)
			: base(clientApplicationBaseExecutor)
		{
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x000375A7 File Offset: 0x000357A7
		internal static AcquireTokenByRefreshTokenParameterBuilder Create(IClientApplicationBaseExecutor clientApplicationBaseExecutor, IEnumerable<string> scopes, string refreshToken)
		{
			return new AcquireTokenByRefreshTokenParameterBuilder(clientApplicationBaseExecutor).WithScopes(scopes).WithRefreshToken(refreshToken);
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x000375BB File Offset: 0x000357BB
		internal AcquireTokenByRefreshTokenParameterBuilder WithRefreshToken(string refreshToken)
		{
			this.Parameters.RefreshToken = refreshToken;
			return this;
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x000375CA File Offset: 0x000357CA
		internal override Task<AuthenticationResult> ExecuteInternalAsync(CancellationToken cancellationToken)
		{
			return base.ClientApplicationBaseExecutor.ExecuteAsync(base.CommonParameters, this.Parameters, cancellationToken);
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x000375E4 File Offset: 0x000357E4
		protected override void Validate()
		{
			base.Validate();
			if (this.Parameters.SendX5C == null)
			{
				this.Parameters.SendX5C = new bool?(base.ServiceBundle.Config.SendX5C);
			}
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x0003762C File Offset: 0x0003582C
		internal override ApiEvent.ApiIds CalculateApiEventId()
		{
			return ApiEvent.ApiIds.AcquireTokenByRefreshToken;
		}

		// Token: 0x06000E29 RID: 3625 RVA: 0x00037633 File Offset: 0x00035833
		public AcquireTokenByRefreshTokenParameterBuilder WithSendX5C(bool withSendX5C)
		{
			this.Parameters.SendX5C = new bool?(withSendX5C);
			return this;
		}
	}
}
