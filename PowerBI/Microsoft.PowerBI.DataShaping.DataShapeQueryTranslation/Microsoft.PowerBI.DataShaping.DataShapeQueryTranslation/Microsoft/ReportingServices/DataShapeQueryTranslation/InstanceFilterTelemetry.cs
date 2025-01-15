using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation
{
	// Token: 0x02000044 RID: 68
	[DataContract]
	internal class InstanceFilterTelemetry
	{
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x00007E73 File Offset: 0x00006073
		// (set) Token: 0x060002C7 RID: 711 RVA: 0x00007E7B File Offset: 0x0000607B
		[DataMember(Name = "QueryStage", IsRequired = false, EmitDefaultValue = false, Order = 10)]
		internal string QueryStage { get; set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x00007E84 File Offset: 0x00006084
		// (set) Token: 0x060002C9 RID: 713 RVA: 0x00007E8C File Offset: 0x0000608C
		[DataMember(Name = "HasNegatedTuples", IsRequired = false, EmitDefaultValue = false, Order = 20)]
		internal bool HasNegatedTuples { get; set; }
	}
}
