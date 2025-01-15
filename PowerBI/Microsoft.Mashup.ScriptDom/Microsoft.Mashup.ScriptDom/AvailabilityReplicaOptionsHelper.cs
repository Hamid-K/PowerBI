using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000055 RID: 85
	internal class AvailabilityReplicaOptionsHelper : OptionsHelper<AvailabilityReplicaOptionKind>
	{
		// Token: 0x060001EA RID: 490 RVA: 0x000064BC File Offset: 0x000046BC
		private AvailabilityReplicaOptionsHelper()
		{
			base.AddOptionMapping(AvailabilityReplicaOptionKind.ApplyDelay, "APPLY_DELAY");
			base.AddOptionMapping(AvailabilityReplicaOptionKind.AvailabilityMode, "AVAILABILITY_MODE");
			base.AddOptionMapping(AvailabilityReplicaOptionKind.EndpointUrl, "ENDPOINT_URL");
			base.AddOptionMapping(AvailabilityReplicaOptionKind.SecondaryRole, "SECONDARY_ROLE");
			base.AddOptionMapping(AvailabilityReplicaOptionKind.SessionTimeout, "SESSION_TIMEOUT");
		}

		// Token: 0x04000177 RID: 375
		public static readonly AvailabilityReplicaOptionsHelper Instance = new AvailabilityReplicaOptionsHelper();
	}
}
