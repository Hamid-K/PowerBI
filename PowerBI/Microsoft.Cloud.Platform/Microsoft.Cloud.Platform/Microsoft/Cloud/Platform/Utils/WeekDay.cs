using System;
using System.Runtime.Serialization;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002C7 RID: 711
	[DataContract]
	[Flags]
	public enum WeekDay
	{
		// Token: 0x0400071F RID: 1823
		[EnumMember]
		None = 0,
		// Token: 0x04000720 RID: 1824
		[EnumMember]
		Sunday = 1,
		// Token: 0x04000721 RID: 1825
		[EnumMember]
		Monday = 2,
		// Token: 0x04000722 RID: 1826
		[EnumMember]
		Tuesday = 4,
		// Token: 0x04000723 RID: 1827
		[EnumMember]
		Wednesday = 8,
		// Token: 0x04000724 RID: 1828
		[EnumMember]
		Thursday = 16,
		// Token: 0x04000725 RID: 1829
		[EnumMember]
		Friday = 32,
		// Token: 0x04000726 RID: 1830
		[EnumMember]
		Saturday = 64
	}
}
