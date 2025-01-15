using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.ApiConfig.Executors;
using Microsoft.Identity.Client.ApiConfig.Parameters;
using Microsoft.Identity.Client.TelemetryCore.Internal.Events;

namespace Microsoft.Identity.Client
{
	// Token: 0x02000120 RID: 288
	public sealed class AcquireTokenForClientParameterBuilder : AbstractConfidentialClientAcquireTokenParameterBuilder<AcquireTokenForClientParameterBuilder>
	{
		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000E33 RID: 3635 RVA: 0x00037777 File Offset: 0x00035977
		private AcquireTokenForClientParameters Parameters { get; } = new AcquireTokenForClientParameters();

		// Token: 0x06000E34 RID: 3636 RVA: 0x0003777F File Offset: 0x0003597F
		internal AcquireTokenForClientParameterBuilder(IConfidentialClientApplicationExecutor confidentialClientApplicationExecutor)
			: base(confidentialClientApplicationExecutor)
		{
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x00037793 File Offset: 0x00035993
		internal static AcquireTokenForClientParameterBuilder Create(IConfidentialClientApplicationExecutor confidentialClientApplicationExecutor, IEnumerable<string> scopes)
		{
			return new AcquireTokenForClientParameterBuilder(confidentialClientApplicationExecutor).WithScopes(scopes);
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x000377A1 File Offset: 0x000359A1
		public AcquireTokenForClientParameterBuilder WithForceRefresh(bool forceRefresh)
		{
			this.Parameters.ForceRefresh = forceRefresh;
			return this;
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x000377B0 File Offset: 0x000359B0
		public AcquireTokenForClientParameterBuilder WithSendX5C(bool withSendX5C)
		{
			this.Parameters.SendX5C = new bool?(withSendX5C);
			return this;
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x000377C4 File Offset: 0x000359C4
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use WithAzureRegion on the ConfidentialClientApplicationBuilder object", true)]
		public AcquireTokenForClientParameterBuilder WithAzureRegion(bool useAzureRegion)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x000377CB File Offset: 0x000359CB
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use WithAzureRegion on the ConfidentialClientApplicationBuilder object", true)]
		public AcquireTokenForClientParameterBuilder WithPreferredAzureRegion(bool useAzureRegion = true, string regionUsedIfAutoDetectFails = "", bool fallbackToGlobal = true)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x000377D2 File Offset: 0x000359D2
		internal override Task<AuthenticationResult> ExecuteInternalAsync(CancellationToken cancellationToken)
		{
			return base.ConfidentialClientApplicationExecutor.ExecuteAsync(base.CommonParameters, this.Parameters, cancellationToken);
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x000377EC File Offset: 0x000359EC
		protected override void Validate()
		{
			base.Validate();
			if (this.Parameters.SendX5C == null)
			{
				this.Parameters.SendX5C = new bool?(base.ServiceBundle.Config.SendX5C);
			}
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x00037834 File Offset: 0x00035A34
		internal override ApiEvent.ApiIds CalculateApiEventId()
		{
			return ApiEvent.ApiIds.AcquireTokenForClient;
		}
	}
}
