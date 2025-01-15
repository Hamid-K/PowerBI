using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Explore.ServiceContracts.Internal
{
	// Token: 0x0200000C RID: 12
	[DataContract]
	public enum ErrorResourceType
	{
		// Token: 0x04000037 RID: 55
		[EnumMember]
		ResourceCodeReference,
		// Token: 0x04000038 RID: 56
		[EnumMember]
		EmbeddedString
	}
}
