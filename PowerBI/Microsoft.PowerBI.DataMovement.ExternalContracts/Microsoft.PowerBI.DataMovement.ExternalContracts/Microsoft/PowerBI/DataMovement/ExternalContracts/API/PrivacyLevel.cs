using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000073 RID: 115
	public enum PrivacyLevel
	{
		// Token: 0x04000278 RID: 632
		[EnumMember]
		None,
		// Token: 0x04000279 RID: 633
		[EnumMember]
		Private,
		// Token: 0x0400027A RID: 634
		[EnumMember]
		Organizational,
		// Token: 0x0400027B RID: 635
		[EnumMember]
		Public
	}
}
