using System;
using System.Runtime.Serialization;

namespace Microsoft.DataShaping.InternalContracts.DataShapeDefinition
{
	// Token: 0x02000138 RID: 312
	[DataContract]
	internal sealed class SortInformation
	{
		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000855 RID: 2133 RVA: 0x000102A6 File Offset: 0x0000E4A6
		// (set) Token: 0x06000856 RID: 2134 RVA: 0x000102AE File Offset: 0x0000E4AE
		[DataMember(EmitDefaultValue = true, Order = 1)]
		internal int SortIndex { get; set; }

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000857 RID: 2135 RVA: 0x000102B7 File Offset: 0x0000E4B7
		// (set) Token: 0x06000858 RID: 2136 RVA: 0x000102BF File Offset: 0x0000E4BF
		[DataMember(EmitDefaultValue = true, Order = 2)]
		internal SortDirection SortDirection { get; set; }
	}
}
