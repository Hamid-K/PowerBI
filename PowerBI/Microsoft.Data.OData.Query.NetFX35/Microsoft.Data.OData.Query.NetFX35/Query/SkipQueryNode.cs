using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.Experimental.OData.Query
{
	// Token: 0x0200003D RID: 61
	public sealed class SkipQueryNode : CollectionQueryNode
	{
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600016B RID: 363 RVA: 0x00008977 File Offset: 0x00006B77
		// (set) Token: 0x0600016C RID: 364 RVA: 0x0000897F File Offset: 0x00006B7F
		public QueryNode Amount { get; set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00008988 File Offset: 0x00006B88
		// (set) Token: 0x0600016E RID: 366 RVA: 0x00008990 File Offset: 0x00006B90
		public CollectionQueryNode Collection { get; set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600016F RID: 367 RVA: 0x00008999 File Offset: 0x00006B99
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

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000170 RID: 368 RVA: 0x000089B0 File Offset: 0x00006BB0
		public override QueryNodeKind Kind
		{
			get
			{
				return QueryNodeKind.Skip;
			}
		}
	}
}
