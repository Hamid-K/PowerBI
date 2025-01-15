using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000084 RID: 132
	[DataContract]
	public enum UserDatasourceLimitStatus
	{
		// Token: 0x040002D8 RID: 728
		[EnumMember]
		NotApplicable,
		// Token: 0x040002D9 RID: 729
		[EnumMember]
		WithinLimit,
		// Token: 0x040002DA RID: 730
		[EnumMember]
		NearLimit,
		// Token: 0x040002DB RID: 731
		[EnumMember]
		ExceededLimit
	}
}
