using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000052 RID: 82
	[DataContract]
	internal sealed class InFilterFlatteningTelemetry
	{
		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x0000CEB1 File Offset: 0x0000B0B1
		// (set) Token: 0x060003B6 RID: 950 RVA: 0x0000CEB9 File Offset: 0x0000B0B9
		[DataMember(Name = "MultiEntityNegated", IsRequired = false, EmitDefaultValue = false, Order = 10)]
		internal int MultiEntityNegated { get; set; }
	}
}
