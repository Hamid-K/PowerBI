using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000050 RID: 80
	[DataContract]
	internal sealed class SlicerTargetsTelemetry
	{
		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060003AC RID: 940 RVA: 0x0000CE65 File Offset: 0x0000B065
		// (set) Token: 0x060003AD RID: 941 RVA: 0x0000CE6D File Offset: 0x0000B06D
		[DataMember(Name = "FilterType", IsRequired = false, EmitDefaultValue = false, Order = 10)]
		internal DsqFilterType? FilterType { get; set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060003AE RID: 942 RVA: 0x0000CE76 File Offset: 0x0000B076
		// (set) Token: 0x060003AF RID: 943 RVA: 0x0000CE7E File Offset: 0x0000B07E
		[DataMember(Name = "Implicit", IsRequired = false, EmitDefaultValue = false, Order = 20)]
		internal int ImplicitTargetCount { get; set; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x0000CE87 File Offset: 0x0000B087
		// (set) Token: 0x060003B1 RID: 945 RVA: 0x0000CE8F File Offset: 0x0000B08F
		[DataMember(Name = "Explicit", IsRequired = false, EmitDefaultValue = false, Order = 30)]
		internal int ExplicitTargetCount { get; set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x0000CE98 File Offset: 0x0000B098
		// (set) Token: 0x060003B3 RID: 947 RVA: 0x0000CEA0 File Offset: 0x0000B0A0
		[DataMember(Name = "Match", IsRequired = false, EmitDefaultValue = false, Order = 40)]
		internal TargetMatchStatus? ExplicitTargetMatchStatus { get; set; }
	}
}
