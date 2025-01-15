using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000034 RID: 52
	[DataContract]
	public enum CredentialType
	{
		// Token: 0x040000E8 RID: 232
		[EnumMember]
		NotSpecified,
		// Token: 0x040000E9 RID: 233
		[EnumMember]
		Windows,
		// Token: 0x040000EA RID: 234
		[EnumMember]
		Anonymous,
		// Token: 0x040000EB RID: 235
		[EnumMember]
		Basic,
		// Token: 0x040000EC RID: 236
		[EnumMember]
		Key,
		// Token: 0x040000ED RID: 237
		[EnumMember]
		OAuth2,
		// Token: 0x040000EE RID: 238
		[EnumMember]
		EffectiveUserName,
		// Token: 0x040000EF RID: 239
		[EnumMember]
		SingleSignOn,
		// Token: 0x040000F0 RID: 240
		[EnumMember]
		WindowsWithoutImpersonation,
		// Token: 0x040000F1 RID: 241
		[EnumMember]
		SAS,
		// Token: 0x040000F2 RID: 242
		[EnumMember]
		ServicePrincipal
	}
}
