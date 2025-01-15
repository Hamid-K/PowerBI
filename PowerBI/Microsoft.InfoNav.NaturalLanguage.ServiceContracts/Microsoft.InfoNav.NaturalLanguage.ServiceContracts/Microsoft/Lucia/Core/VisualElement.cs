using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.Lucia.Core
{
	// Token: 0x0200014B RID: 331
	[DataContract(Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class VisualElement
	{
		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000692 RID: 1682 RVA: 0x0000B63F File Offset: 0x0000983F
		// (set) Token: 0x06000693 RID: 1683 RVA: 0x0000B647 File Offset: 0x00009847
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 10)]
		public IList<DataRole> DataRoles { get; set; }

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000694 RID: 1684 RVA: 0x0000B650 File Offset: 0x00009850
		// (set) Token: 0x06000695 RID: 1685 RVA: 0x0000B658 File Offset: 0x00009858
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public VisualElementSettings Settings { get; set; }
	}
}
