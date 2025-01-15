using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000054 RID: 84
	[DataContract]
	internal sealed class CommonSubqueryEliminationTelemetry
	{
		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060003BB RID: 955 RVA: 0x0000CEE3 File Offset: 0x0000B0E3
		// (set) Token: 0x060003BC RID: 956 RVA: 0x0000CEEB File Offset: 0x0000B0EB
		[DataMember(Name = "Duration", IsRequired = true, EmitDefaultValue = true, Order = 10)]
		internal long Duration { get; set; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060003BD RID: 957 RVA: 0x0000CEF4 File Offset: 0x0000B0F4
		// (set) Token: 0x060003BE RID: 958 RVA: 0x0000CEFC File Offset: 0x0000B0FC
		[DataMember(Name = "Considered", IsRequired = true, EmitDefaultValue = true, Order = 20)]
		public int ConsideredSubqueryCount { get; set; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060003BF RID: 959 RVA: 0x0000CF05 File Offset: 0x0000B105
		// (set) Token: 0x060003C0 RID: 960 RVA: 0x0000CF0D File Offset: 0x0000B10D
		[DataMember(Name = "Eliminated", IsRequired = true, EmitDefaultValue = true, Order = 30)]
		public int EliminatedSubqueryCount { get; set; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x0000CF16 File Offset: 0x0000B116
		// (set) Token: 0x060003C2 RID: 962 RVA: 0x0000CF1E File Offset: 0x0000B11E
		[DataMember(Name = "NewLets", IsRequired = true, EmitDefaultValue = true, Order = 40)]
		public int NewLetBindingCount { get; set; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x0000CF27 File Offset: 0x0000B127
		// (set) Token: 0x060003C4 RID: 964 RVA: 0x0000CF2F File Offset: 0x0000B12F
		[DataMember(Name = "Compared", IsRequired = false, EmitDefaultValue = false, Order = 50)]
		public int ComparedSubqueryCount { get; set; }
	}
}
