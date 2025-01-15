using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000180 RID: 384
	[DataContract(Name = "ApplicationContextSource", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class ApplicationContextSource
	{
		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000A15 RID: 2581 RVA: 0x000143AF File Offset: 0x000125AF
		// (set) Token: 0x06000A16 RID: 2582 RVA: 0x000143B7 File Offset: 0x000125B7
		[DataMember(Name = "ReportId", IsRequired = false, EmitDefaultValue = false, Order = 10)]
		public string ReportId { get; set; }

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000A17 RID: 2583 RVA: 0x000143C0 File Offset: 0x000125C0
		// (set) Token: 0x06000A18 RID: 2584 RVA: 0x000143C8 File Offset: 0x000125C8
		[DataMember(Name = "VisualId", IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public string VisualId { get; set; }

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000A19 RID: 2585 RVA: 0x000143D1 File Offset: 0x000125D1
		// (set) Token: 0x06000A1A RID: 2586 RVA: 0x000143D9 File Offset: 0x000125D9
		[DataMember(Name = "DashboardId", IsRequired = false, EmitDefaultValue = false, Order = 30)]
		public string DashboardId { get; set; }

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000A1B RID: 2587 RVA: 0x000143E2 File Offset: 0x000125E2
		// (set) Token: 0x06000A1C RID: 2588 RVA: 0x000143EA File Offset: 0x000125EA
		[DataMember(Name = "TileId", IsRequired = false, EmitDefaultValue = false, Order = 40)]
		public string TileId { get; set; }

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000A1D RID: 2589 RVA: 0x000143F3 File Offset: 0x000125F3
		// (set) Token: 0x06000A1E RID: 2590 RVA: 0x000143FB File Offset: 0x000125FB
		[DataMember(Name = "Operation", IsRequired = false, EmitDefaultValue = false, Order = 50)]
		public string Operation { get; set; }

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000A1F RID: 2591 RVA: 0x00014404 File Offset: 0x00012604
		// (set) Token: 0x06000A20 RID: 2592 RVA: 0x0001440C File Offset: 0x0001260C
		[DataMember(Name = "CustomProperties", IsRequired = false, EmitDefaultValue = false, Order = 60)]
		public Dictionary<string, object> CustomProperties { get; set; }
	}
}
