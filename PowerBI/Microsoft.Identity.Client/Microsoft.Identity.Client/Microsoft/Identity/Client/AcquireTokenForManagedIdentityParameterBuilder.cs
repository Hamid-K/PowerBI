using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.ApiConfig.Executors;
using Microsoft.Identity.Client.ApiConfig.Parameters;
using Microsoft.Identity.Client.AppConfig;
using Microsoft.Identity.Client.TelemetryCore.Internal.Events;
using Microsoft.Identity.Client.Utils;

namespace Microsoft.Identity.Client
{
	// Token: 0x02000121 RID: 289
	public sealed class AcquireTokenForManagedIdentityParameterBuilder : AbstractManagedIdentityAcquireTokenParameterBuilder<AcquireTokenForManagedIdentityParameterBuilder>
	{
		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000E3D RID: 3645 RVA: 0x0003783B File Offset: 0x00035A3B
		private AcquireTokenForManagedIdentityParameters Parameters { get; } = new AcquireTokenForManagedIdentityParameters();

		// Token: 0x06000E3E RID: 3646 RVA: 0x00037843 File Offset: 0x00035A43
		internal AcquireTokenForManagedIdentityParameterBuilder(IManagedIdentityApplicationExecutor managedIdentityApplicationExecutor)
			: base(managedIdentityApplicationExecutor)
		{
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x00037857 File Offset: 0x00035A57
		internal static AcquireTokenForManagedIdentityParameterBuilder Create(IManagedIdentityApplicationExecutor managedIdentityApplicationExecutor, string resource)
		{
			return new AcquireTokenForManagedIdentityParameterBuilder(managedIdentityApplicationExecutor).WithResource(resource);
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x00037865 File Offset: 0x00035A65
		private AcquireTokenForManagedIdentityParameterBuilder WithResource(string resource)
		{
			this.Parameters.Resource = ScopeHelper.RemoveDefaultSuffixIfPresent(resource);
			base.CommonParameters.Scopes = new string[] { this.Parameters.Resource };
			return this;
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x00037898 File Offset: 0x00035A98
		public AcquireTokenForManagedIdentityParameterBuilder WithForceRefresh(bool forceRefresh)
		{
			this.Parameters.ForceRefresh = forceRefresh;
			return this;
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x000378A7 File Offset: 0x00035AA7
		internal override Task<AuthenticationResult> ExecuteInternalAsync(CancellationToken cancellationToken)
		{
			return base.ManagedIdentityApplicationExecutor.ExecuteAsync(base.CommonParameters, this.Parameters, cancellationToken);
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x000378C1 File Offset: 0x00035AC1
		internal override ApiEvent.ApiIds CalculateApiEventId()
		{
			if (base.ServiceBundle.Config.ManagedIdentityId.IdType == ManagedIdentityIdType.SystemAssigned)
			{
				return ApiEvent.ApiIds.AcquireTokenForSystemAssignedManagedIdentity;
			}
			return ApiEvent.ApiIds.AcquireTokenForUserAssignedManagedIdentity;
		}
	}
}
