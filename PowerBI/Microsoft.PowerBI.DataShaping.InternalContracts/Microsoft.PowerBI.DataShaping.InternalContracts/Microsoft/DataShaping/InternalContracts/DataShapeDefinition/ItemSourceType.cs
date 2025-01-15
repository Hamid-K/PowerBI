using System;
using System.Runtime.Serialization;

namespace Microsoft.DataShaping.InternalContracts.DataShapeDefinition
{
	// Token: 0x0200011A RID: 282
	[DataContract]
	internal enum ItemSourceType
	{
		// Token: 0x04000300 RID: 768
		[EnumMember(Value = "QueryExtensionMeasure")]
		QueryExtensionMeasure,
		// Token: 0x04000301 RID: 769
		[EnumMember(Value = "QueryExtensionColumn")]
		QueryExtensionColumn
	}
}
