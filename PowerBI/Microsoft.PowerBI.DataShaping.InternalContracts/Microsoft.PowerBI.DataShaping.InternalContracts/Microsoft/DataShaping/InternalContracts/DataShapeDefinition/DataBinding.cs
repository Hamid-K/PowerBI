using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.DataShaping.InternalContracts.DataShapeDefinition
{
	// Token: 0x0200010B RID: 267
	[DataContract]
	internal sealed class DataBinding
	{
		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000726 RID: 1830 RVA: 0x0000F4FC File Offset: 0x0000D6FC
		// (set) Token: 0x06000727 RID: 1831 RVA: 0x0000F504 File Offset: 0x0000D704
		[DataMember(EmitDefaultValue = false, Order = 1)]
		internal string TableId { get; set; }

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000728 RID: 1832 RVA: 0x0000F50D File Offset: 0x0000D70D
		// (set) Token: 0x06000729 RID: 1833 RVA: 0x0000F515 File Offset: 0x0000D715
		[DataMember(EmitDefaultValue = false, Order = 2)]
		internal bool RestoreContext { get; set; }

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x0600072A RID: 1834 RVA: 0x0000F51E File Offset: 0x0000D71E
		// (set) Token: 0x0600072B RID: 1835 RVA: 0x0000F526 File Offset: 0x0000D726
		[DataMember(EmitDefaultValue = false, Order = 3)]
		internal IList<Relationship> Relationships { get; set; }
	}
}
