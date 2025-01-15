using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x020000A3 RID: 163
	[DebuggerDisplay("[Limit] Id={Id}")]
	internal sealed class Limit : IIdentifiable, IDataBoundItem
	{
		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060003CF RID: 975 RVA: 0x00007500 File Offset: 0x00005700
		// (set) Token: 0x060003D0 RID: 976 RVA: 0x00007508 File Offset: 0x00005708
		public Identifier Id { get; set; }

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x00007511 File Offset: 0x00005711
		// (set) Token: 0x060003D2 RID: 978 RVA: 0x00007519 File Offset: 0x00005719
		public LimitOperator Operator { get; set; }

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x00007522 File Offset: 0x00005722
		// (set) Token: 0x060003D4 RID: 980 RVA: 0x0000752A File Offset: 0x0000572A
		public List<Expression> Targets { get; set; }

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x00007533 File Offset: 0x00005733
		// (set) Token: 0x060003D6 RID: 982 RVA: 0x0000753B File Offset: 0x0000573B
		public Expression Within { get; set; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x00007544 File Offset: 0x00005744
		// (set) Token: 0x060003D8 RID: 984 RVA: 0x0000754C File Offset: 0x0000574C
		public int? TelemetryId { get; set; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x00007555 File Offset: 0x00005755
		public ObjectType ObjectType
		{
			get
			{
				return ObjectType.Limit;
			}
		}
	}
}
