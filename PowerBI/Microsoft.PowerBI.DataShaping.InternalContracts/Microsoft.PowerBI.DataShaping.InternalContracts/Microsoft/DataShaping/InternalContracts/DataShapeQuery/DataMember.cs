using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x02000066 RID: 102
	[DebuggerDisplay("[DataMember] Id={Id}")]
	internal sealed class DataMember : IScope, IContextItem, IIdentifiable, IDataBoundItem
	{
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600022F RID: 559 RVA: 0x00005EF8 File Offset: 0x000040F8
		// (set) Token: 0x06000230 RID: 560 RVA: 0x00005F00 File Offset: 0x00004100
		public Identifier Id { get; set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000231 RID: 561 RVA: 0x00005F09 File Offset: 0x00004109
		// (set) Token: 0x06000232 RID: 562 RVA: 0x00005F11 File Offset: 0x00004111
		public List<DataMember> DataMembers { get; set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000233 RID: 563 RVA: 0x00005F1A File Offset: 0x0000411A
		// (set) Token: 0x06000234 RID: 564 RVA: 0x00005F22 File Offset: 0x00004122
		public List<Calculation> Calculations { get; set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000235 RID: 565 RVA: 0x00005F2B File Offset: 0x0000412B
		// (set) Token: 0x06000236 RID: 566 RVA: 0x00005F33 File Offset: 0x00004133
		public Group Group { get; set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000237 RID: 567 RVA: 0x00005F3C File Offset: 0x0000413C
		// (set) Token: 0x06000238 RID: 568 RVA: 0x00005F44 File Offset: 0x00004144
		public List<DataShape> DataShapes { get; set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000239 RID: 569 RVA: 0x00005F4D File Offset: 0x0000414D
		public ObjectType ObjectType
		{
			get
			{
				return ObjectType.DataMember;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600023A RID: 570 RVA: 0x00005F51 File Offset: 0x00004151
		public bool IsDynamic
		{
			get
			{
				return this.Group != null;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600023B RID: 571 RVA: 0x00005F5C File Offset: 0x0000415C
		// (set) Token: 0x0600023C RID: 572 RVA: 0x00005F64 File Offset: 0x00004164
		public bool ContextOnly { get; set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600023D RID: 573 RVA: 0x00005F6D File Offset: 0x0000416D
		// (set) Token: 0x0600023E RID: 574 RVA: 0x00005F75 File Offset: 0x00004175
		public Candidate<bool> SubtotalStartPosition { get; set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600023F RID: 575 RVA: 0x00005F7E File Offset: 0x0000417E
		// (set) Token: 0x06000240 RID: 576 RVA: 0x00005F86 File Offset: 0x00004186
		internal List<FilterCondition> InstanceFilters { get; set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000241 RID: 577 RVA: 0x00005F8F File Offset: 0x0000418F
		// (set) Token: 0x06000242 RID: 578 RVA: 0x00005F97 File Offset: 0x00004197
		internal bool HasExplicitSubtotal { get; set; }
	}
}
