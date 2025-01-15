using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000057 RID: 87
	internal class AlterAvailabilityGroupActionTypeHelper : OptionsHelper<AlterAvailabilityGroupActionType>
	{
		// Token: 0x060001EE RID: 494 RVA: 0x0000655C File Offset: 0x0000475C
		private AlterAvailabilityGroupActionTypeHelper()
		{
			base.AddOptionMapping(AlterAvailabilityGroupActionType.Failover, "FAILOVER");
			base.AddOptionMapping(AlterAvailabilityGroupActionType.ForceFailoverAllowDataLoss, "FORCE_FAILOVER_ALLOW_DATA_LOSS");
			base.AddOptionMapping(AlterAvailabilityGroupActionType.Join, TSqlTokenType.Join);
			base.AddOptionMapping(AlterAvailabilityGroupActionType.Offline, "OFFLINE");
			base.AddOptionMapping(AlterAvailabilityGroupActionType.Online, "ONLINE");
		}

		// Token: 0x04000179 RID: 377
		public static readonly AlterAvailabilityGroupActionTypeHelper Instance = new AlterAvailabilityGroupActionTypeHelper();
	}
}
