using System;
using System.Runtime.Serialization;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000144 RID: 324
	[DataContract]
	[Flags]
	public enum GenerateUtteranceRequestOptions
	{
		// Token: 0x04000644 RID: 1604
		NotSpecified = 0,
		// Token: 0x04000645 RID: 1605
		[EnumMember]
		GenerateSpans = 1,
		// Token: 0x04000646 RID: 1606
		[EnumMember]
		VerifyResult = 2,
		// Token: 0x04000647 RID: 1607
		[EnumMember]
		DefaultOptions = 3
	}
}
