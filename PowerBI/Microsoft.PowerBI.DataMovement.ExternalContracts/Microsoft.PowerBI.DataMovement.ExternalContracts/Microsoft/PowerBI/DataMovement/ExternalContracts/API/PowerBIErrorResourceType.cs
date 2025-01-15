using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000071 RID: 113
	[DataContract]
	public enum PowerBIErrorResourceType
	{
		// Token: 0x0400026D RID: 621
		[EnumMember]
		ResourceCodeReference,
		// Token: 0x0400026E RID: 622
		[EnumMember]
		EmbeddedString
	}
}
