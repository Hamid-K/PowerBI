using System;
using System.Runtime.Serialization;

namespace Microsoft.DataShaping.InternalContracts.DataShapeDefinition
{
	// Token: 0x02000129 RID: 297
	[DataContract]
	internal enum DataType
	{
		// Token: 0x04000334 RID: 820
		[EnumMember]
		Boolean,
		// Token: 0x04000335 RID: 821
		[EnumMember]
		DateTime,
		// Token: 0x04000336 RID: 822
		[EnumMember]
		Decimal,
		// Token: 0x04000337 RID: 823
		[EnumMember]
		Double,
		// Token: 0x04000338 RID: 824
		[EnumMember]
		Int64,
		// Token: 0x04000339 RID: 825
		[EnumMember]
		String,
		// Token: 0x0400033A RID: 826
		[EnumMember]
		Variant
	}
}
