using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x02000068 RID: 104
	internal sealed class GroupKey : IIdentifiable
	{
		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000251 RID: 593 RVA: 0x00006016 File Offset: 0x00004216
		// (set) Token: 0x06000252 RID: 594 RVA: 0x0000601E File Offset: 0x0000421E
		public Identifier Id { get; set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000253 RID: 595 RVA: 0x00006027 File Offset: 0x00004227
		// (set) Token: 0x06000254 RID: 596 RVA: 0x0000602F File Offset: 0x0000422F
		public Expression Value { get; set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000255 RID: 597 RVA: 0x00006038 File Offset: 0x00004238
		// (set) Token: 0x06000256 RID: 598 RVA: 0x00006040 File Offset: 0x00004240
		public Candidate<bool> ShowItemsWithNoData { get; set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000257 RID: 599 RVA: 0x00006049 File Offset: 0x00004249
		public ObjectType ObjectType
		{
			get
			{
				return ObjectType.GroupKey;
			}
		}
	}
}
