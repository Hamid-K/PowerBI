using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002D3 RID: 723
	[DataContract]
	public enum EntitySourceType
	{
		// Token: 0x0400088E RID: 2190
		[EnumMember]
		Table,
		// Token: 0x0400088F RID: 2191
		[EnumMember]
		Pod,
		// Token: 0x04000890 RID: 2192
		[EnumMember]
		Expression
	}
}
