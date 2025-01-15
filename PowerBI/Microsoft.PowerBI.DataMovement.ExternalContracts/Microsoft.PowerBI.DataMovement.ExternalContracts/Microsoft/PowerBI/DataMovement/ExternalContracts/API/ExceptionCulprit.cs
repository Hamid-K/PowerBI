using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x0200004A RID: 74
	[DataContract]
	public enum ExceptionCulprit
	{
		// Token: 0x040001A5 RID: 421
		[EnumMember]
		System,
		// Token: 0x040001A6 RID: 422
		[EnumMember]
		User,
		// Token: 0x040001A7 RID: 423
		[EnumMember]
		External
	}
}
