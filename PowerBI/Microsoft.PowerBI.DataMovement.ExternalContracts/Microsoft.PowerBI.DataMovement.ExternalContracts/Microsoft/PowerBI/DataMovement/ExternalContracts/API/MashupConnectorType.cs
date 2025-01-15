using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000060 RID: 96
	[DataContract]
	public enum MashupConnectorType
	{
		// Token: 0x04000235 RID: 565
		[EnumMember]
		Custom,
		// Token: 0x04000236 RID: 566
		[EnumMember]
		Certified,
		// Token: 0x04000237 RID: 567
		[EnumMember]
		BuiltIn
	}
}
