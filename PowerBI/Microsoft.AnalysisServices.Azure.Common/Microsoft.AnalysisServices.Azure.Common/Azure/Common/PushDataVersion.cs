using System;
using System.Runtime.Serialization;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x02000072 RID: 114
	[DataContract]
	public enum PushDataVersion
	{
		// Token: 0x040001C8 RID: 456
		[EnumMember(Value = "None")]
		None,
		// Token: 0x040001C9 RID: 457
		[EnumMember(Value = "V1")]
		V1,
		// Token: 0x040001CA RID: 458
		[EnumMember(Value = "V2")]
		V2,
		// Token: 0x040001CB RID: 459
		[EnumMember(Value = "UpgradeV1ToV2")]
		UpgradeV1ToV2 = 101,
		// Token: 0x040001CC RID: 460
		[EnumMember(Value = "RollbackV2ToV1")]
		RollbackV2ToV1
	}
}
