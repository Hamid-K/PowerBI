using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.Experimental.OData.Query
{
	// Token: 0x0200003C RID: 60
	public sealed class TopQueryNode : CollectionQueryNode
	{
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00008932 File Offset: 0x00006B32
		// (set) Token: 0x06000165 RID: 357 RVA: 0x0000893A File Offset: 0x00006B3A
		public QueryNode Amount { get; set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000166 RID: 358 RVA: 0x00008943 File Offset: 0x00006B43
		// (set) Token: 0x06000167 RID: 359 RVA: 0x0000894B File Offset: 0x00006B4B
		public CollectionQueryNode Collection { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000168 RID: 360 RVA: 0x00008954 File Offset: 0x00006B54
		public override IEdmTypeReference ItemType
		{
			get
			{
				if (this.Collection == null)
				{
					return null;
				}
				return this.Collection.ItemType;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000169 RID: 361 RVA: 0x0000896B File Offset: 0x00006B6B
		public override QueryNodeKind Kind
		{
			get
			{
				return QueryNodeKind.Top;
			}
		}
	}
}
