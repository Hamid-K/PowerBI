using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.DataShapeDefinition
{
	// Token: 0x02000594 RID: 1428
	[DataContract]
	internal enum ExpressionPartKind
	{
		// Token: 0x0400294D RID: 10573
		FieldReference,
		// Token: 0x0400294E RID: 10574
		FirstFieldValue,
		// Token: 0x0400294F RID: 10575
		FunctionCall,
		// Token: 0x04002950 RID: 10576
		Literal,
		// Token: 0x04002951 RID: 10577
		ScopedFieldReference,
		// Token: 0x04002952 RID: 10578
		ServerAggregate
	}
}
