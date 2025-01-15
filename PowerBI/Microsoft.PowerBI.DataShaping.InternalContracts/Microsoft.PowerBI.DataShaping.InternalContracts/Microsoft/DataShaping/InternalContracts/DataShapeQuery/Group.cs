using System;
using System.Collections.Generic;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x02000067 RID: 103
	internal sealed class Group
	{
		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000244 RID: 580 RVA: 0x00005FA8 File Offset: 0x000041A8
		// (set) Token: 0x06000245 RID: 581 RVA: 0x00005FB0 File Offset: 0x000041B0
		public DetailGroupIdentity DetailGroupIdentity { get; set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000246 RID: 582 RVA: 0x00005FB9 File Offset: 0x000041B9
		// (set) Token: 0x06000247 RID: 583 RVA: 0x00005FC1 File Offset: 0x000041C1
		public List<GroupKey> GroupKeys { get; set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000248 RID: 584 RVA: 0x00005FCA File Offset: 0x000041CA
		// (set) Token: 0x06000249 RID: 585 RVA: 0x00005FD2 File Offset: 0x000041D2
		public List<SortKey> SortKeys { get; set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600024A RID: 586 RVA: 0x00005FDB File Offset: 0x000041DB
		// (set) Token: 0x0600024B RID: 587 RVA: 0x00005FE3 File Offset: 0x000041E3
		public ScopeIdDefinition ScopeIdDefinition { get; set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600024C RID: 588 RVA: 0x00005FEC File Offset: 0x000041EC
		// (set) Token: 0x0600024D RID: 589 RVA: 0x00005FF4 File Offset: 0x000041F4
		public ScopeId StartPosition { get; set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600024E RID: 590 RVA: 0x00005FFD File Offset: 0x000041FD
		// (set) Token: 0x0600024F RID: 591 RVA: 0x00006005 File Offset: 0x00004205
		public bool SuppressSortByMeasureRollup { get; set; }
	}
}
