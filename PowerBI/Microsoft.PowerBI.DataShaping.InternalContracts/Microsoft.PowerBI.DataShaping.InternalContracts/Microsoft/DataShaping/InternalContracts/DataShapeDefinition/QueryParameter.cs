using System;
using System.Runtime.Serialization;

namespace Microsoft.DataShaping.InternalContracts.DataShapeDefinition
{
	// Token: 0x0200011D RID: 285
	[DataContract]
	internal sealed class QueryParameter
	{
		// Token: 0x17000199 RID: 409
		// (get) Token: 0x0600079C RID: 1948 RVA: 0x0000F8DF File Offset: 0x0000DADF
		// (set) Token: 0x0600079D RID: 1949 RVA: 0x0000F8E7 File Offset: 0x0000DAE7
		[DataMember(EmitDefaultValue = false, Order = 1)]
		internal string Name { get; set; }

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x0600079E RID: 1950 RVA: 0x0000F8F0 File Offset: 0x0000DAF0
		// (set) Token: 0x0600079F RID: 1951 RVA: 0x0000F8F8 File Offset: 0x0000DAF8
		[DataMember(EmitDefaultValue = false, Order = 2)]
		internal string QueryName { get; set; }
	}
}
