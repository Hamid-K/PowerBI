using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000053 RID: 83
	[DataContract]
	internal sealed class QueryOptimizerTelemetry
	{
		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x0000CECA File Offset: 0x0000B0CA
		// (set) Token: 0x060003B9 RID: 953 RVA: 0x0000CED2 File Offset: 0x0000B0D2
		[DataMember(Name = "CSE", IsRequired = false, EmitDefaultValue = false, Order = 10)]
		internal CommonSubqueryEliminationTelemetry CommonSubqueryElimination { get; set; }
	}
}
