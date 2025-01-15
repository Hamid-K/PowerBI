using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000176 RID: 374
	[DataContract(Name = "DaxQueryRequest")]
	public sealed class DaxQueryRequest
	{
		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x060009BD RID: 2493 RVA: 0x00013C7A File Offset: 0x00011E7A
		// (set) Token: 0x060009BE RID: 2494 RVA: 0x00013C82 File Offset: 0x00011E82
		[DataMember(IsRequired = true, Order = 0)]
		public string Query { get; set; }

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x060009BF RID: 2495 RVA: 0x00013C8B File Offset: 0x00011E8B
		// (set) Token: 0x060009C0 RID: 2496 RVA: 0x00013C93 File Offset: 0x00011E93
		[DataMember(IsRequired = true, Order = 1)]
		public int MaxRowCount { get; set; }

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x060009C1 RID: 2497 RVA: 0x00013C9C File Offset: 0x00011E9C
		// (set) Token: 0x060009C2 RID: 2498 RVA: 0x00013CA4 File Offset: 0x00011EA4
		[DataMember(IsRequired = true, Order = 2)]
		public int MaxNumberOfValues { get; set; }

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x060009C3 RID: 2499 RVA: 0x00013CAD File Offset: 0x00011EAD
		// (set) Token: 0x060009C4 RID: 2500 RVA: 0x00013CB5 File Offset: 0x00011EB5
		[DataMember(IsRequired = false, Order = 3)]
		public int? MaxNumberOfBytes { get; set; }

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x060009C5 RID: 2501 RVA: 0x00013CBE File Offset: 0x00011EBE
		// (set) Token: 0x060009C6 RID: 2502 RVA: 0x00013CC6 File Offset: 0x00011EC6
		[DataMember(IsRequired = false, Order = 4)]
		public ApplicationContext ApplicationContext { get; set; }

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x060009C7 RID: 2503 RVA: 0x00013CCF File Offset: 0x00011ECF
		// (set) Token: 0x060009C8 RID: 2504 RVA: 0x00013CD7 File Offset: 0x00011ED7
		[DataMember(IsRequired = false, Order = 5)]
		public bool IncludeNulls { get; set; }
	}
}
