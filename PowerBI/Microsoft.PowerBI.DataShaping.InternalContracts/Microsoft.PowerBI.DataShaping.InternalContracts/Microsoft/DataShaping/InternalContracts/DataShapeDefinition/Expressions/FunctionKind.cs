using System;
using System.Runtime.Serialization;

namespace Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions
{
	// Token: 0x0200013E RID: 318
	[DataContract]
	internal enum FunctionKind
	{
		// Token: 0x04000369 RID: 873
		[EnumMember]
		Array,
		// Token: 0x0400036A RID: 874
		[EnumMember]
		Comparable,
		// Token: 0x0400036B RID: 875
		[EnumMember]
		MinValue,
		// Token: 0x0400036C RID: 876
		[EnumMember]
		MaxValue
	}
}
