using System;
using System.Runtime.Serialization;

namespace Microsoft.Lucia.Core
{
	// Token: 0x0200010E RID: 270
	[DataContract]
	public enum SpanMatchType
	{
		// Token: 0x040005B9 RID: 1465
		[EnumMember(Value = "Default")]
		Default,
		// Token: 0x040005BA RID: 1466
		[EnumMember(Value = "ExactMatch")]
		ExactMatch,
		// Token: 0x040005BB RID: 1467
		[EnumMember(Value = "PartialMatch")]
		PartialMatch,
		// Token: 0x040005BC RID: 1468
		[EnumMember(Value = "PrefixMatch")]
		PrefixMatch,
		// Token: 0x040005BD RID: 1469
		[EnumMember(Value = "SynonymMatch")]
		SynonymMatch
	}
}
