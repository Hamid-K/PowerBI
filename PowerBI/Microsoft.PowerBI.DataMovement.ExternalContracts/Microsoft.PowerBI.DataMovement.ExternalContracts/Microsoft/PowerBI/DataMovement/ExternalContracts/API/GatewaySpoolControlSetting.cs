using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000087 RID: 135
	[DataContract]
	public enum GatewaySpoolControlSetting
	{
		// Token: 0x040002E7 RID: 743
		[EnumMember]
		Default,
		// Token: 0x040002E8 RID: 744
		[EnumMember]
		ForceSpoolEnabled,
		// Token: 0x040002E9 RID: 745
		[EnumMember]
		ForceSpoolDisabled
	}
}
