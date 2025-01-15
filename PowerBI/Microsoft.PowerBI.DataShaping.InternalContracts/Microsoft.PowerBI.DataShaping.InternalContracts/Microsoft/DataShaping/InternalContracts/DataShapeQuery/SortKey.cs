using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x02000069 RID: 105
	internal sealed class SortKey : IIdentifiable
	{
		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000259 RID: 601 RVA: 0x00006055 File Offset: 0x00004255
		// (set) Token: 0x0600025A RID: 602 RVA: 0x0000605D File Offset: 0x0000425D
		public Identifier Id { get; set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600025B RID: 603 RVA: 0x00006066 File Offset: 0x00004266
		// (set) Token: 0x0600025C RID: 604 RVA: 0x0000606E File Offset: 0x0000426E
		public Expression Value { get; set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600025D RID: 605 RVA: 0x00006077 File Offset: 0x00004277
		// (set) Token: 0x0600025E RID: 606 RVA: 0x0000607F File Offset: 0x0000427F
		public Candidate<SortDirection> SortDirection { get; set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600025F RID: 607 RVA: 0x00006088 File Offset: 0x00004288
		public ObjectType ObjectType
		{
			get
			{
				return ObjectType.SortKey;
			}
		}
	}
}
