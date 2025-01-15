using System;
using System.Runtime.Serialization;

namespace Microsoft.DataShaping.InternalContracts.DataShapeDefinition
{
	// Token: 0x0200010A RID: 266
	[DataContract]
	internal enum CorrelationMode
	{
		// Token: 0x040002CD RID: 717
		[EnumMember]
		IndexBased,
		// Token: 0x040002CE RID: 718
		[EnumMember]
		ValueBased
	}
}
