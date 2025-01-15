using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000049 RID: 73
	public enum EncryptedConnection
	{
		// Token: 0x040001A1 RID: 417
		[EnumMember]
		Encrypted,
		// Token: 0x040001A2 RID: 418
		[EnumMember]
		Any,
		// Token: 0x040001A3 RID: 419
		[EnumMember]
		NotEncrypted
	}
}
