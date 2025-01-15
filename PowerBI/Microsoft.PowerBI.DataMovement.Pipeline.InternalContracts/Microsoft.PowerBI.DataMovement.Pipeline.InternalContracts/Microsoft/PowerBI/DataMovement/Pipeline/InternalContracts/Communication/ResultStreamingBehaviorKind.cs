using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x02000059 RID: 89
	[DataContract]
	public enum ResultStreamingBehaviorKind
	{
		// Token: 0x040000E5 RID: 229
		[EnumMember]
		Direct,
		// Token: 0x040000E6 RID: 230
		[EnumMember]
		Spooled,
		// Token: 0x040000E7 RID: 231
		[EnumMember]
		Default = 0
	}
}
