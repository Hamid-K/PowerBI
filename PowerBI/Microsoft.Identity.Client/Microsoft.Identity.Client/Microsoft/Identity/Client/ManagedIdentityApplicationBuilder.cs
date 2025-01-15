using System;
using Microsoft.Identity.Client.AppConfig;
using Microsoft.Identity.Client.Internal;
using Microsoft.Identity.Client.TelemetryCore.TelemetryClient;
using Microsoft.IdentityModel.Abstractions;

namespace Microsoft.Identity.Client
{
	// Token: 0x0200013E RID: 318
	public sealed class ManagedIdentityApplicationBuilder : BaseAbstractApplicationBuilder<ManagedIdentityApplicationBuilder>
	{
		// Token: 0x06000FF1 RID: 4081 RVA: 0x0003A381 File Offset: 0x00038581
		internal ManagedIdentityApplicationBuilder(ApplicationConfiguration configuration)
			: base(configuration)
		{
			ApplicationBase.GuardMobileFrameworks();
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x0003A38F File Offset: 0x0003858F
		public static ManagedIdentityApplicationBuilder Create(ManagedIdentityId managedIdentityId)
		{
			ApplicationBase.GuardMobileFrameworks();
			return new ManagedIdentityApplicationBuilder(ManagedIdentityApplicationBuilder.BuildConfiguration(managedIdentityId));
		}

		// Token: 0x06000FF3 RID: 4083 RVA: 0x0003A3A1 File Offset: 0x000385A1
		private static ApplicationConfiguration BuildConfiguration(ManagedIdentityId managedIdentityId)
		{
			if (managedIdentityId == null)
			{
				throw new ArgumentNullException("managedIdentityId");
			}
			return new ApplicationConfiguration(MsalClientType.ManagedIdentityClient)
			{
				ManagedIdentityId = managedIdentityId,
				CacheSynchronizationEnabled = false,
				AccessorOptions = CacheOptions.EnableSharedCacheOptions
			};
		}

		// Token: 0x06000FF4 RID: 4084 RVA: 0x0003A3D0 File Offset: 0x000385D0
		public ManagedIdentityApplicationBuilder WithTelemetryClient(params ITelemetryClient[] telemetryClients)
		{
			base.ValidateUseOfExperimentalFeature("ITelemetryClient");
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

		// Token: 0x06000FF5 RID: 4085 RVA: 0x0003A434 File Offset: 0x00038634
		private void TelemetryClientLogMsalVersion()
		{
			if (base.Config.TelemetryClients.HasEnabledClients("config_update"))
			{
				MsalTelemetryEventDetails msalTelemetryEventDetails = new MsalTelemetryEventDetails("config_update");
				msalTelemetryEventDetails.SetProperty("MsalVersion", MsalIdHelper.GetMsalVersion());
				base.Config.TelemetryClients.TrackEvent(msalTelemetryEventDetails);
			}
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x0003A484 File Offset: 0x00038684
		internal ManagedIdentityApplicationBuilder WithAppTokenCacheInternalForTest(ITokenCacheInternal tokenCacheInternal)
		{
			base.Config.AppTokenCacheInternalForTest = tokenCacheInternal;
			return this;
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x0003A493 File Offset: 0x00038693
		public IManagedIdentityApplication Build()
		{
			return this.BuildConcrete();
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x0003A49B File Offset: 0x0003869B
		internal ManagedIdentityApplication BuildConcrete()
		{
			this.DefaultConfiguration();
			return new ManagedIdentityApplication(this.BuildConfiguration());
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x0003A4AE File Offset: 0x000386AE
		private void DefaultConfiguration()
		{
			this.ComputeClientIdForCaching();
			base.Config.TenantId = "managed_identity";
			base.Config.RedirectUri = "https://replyUrlNotSet";
			base.Config.IsInstanceDiscoveryEnabled = false;
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x0003A4E2 File Offset: 0x000386E2
		private void ComputeClientIdForCaching()
		{
			if (base.Config.ManagedIdentityId.IdType == ManagedIdentityIdType.SystemAssigned)
			{
				base.Config.ClientId = "system_assigned_managed_identity";
				return;
			}
			base.Config.ClientId = base.Config.ManagedIdentityId.UserAssignedId;
		}
	}
}
