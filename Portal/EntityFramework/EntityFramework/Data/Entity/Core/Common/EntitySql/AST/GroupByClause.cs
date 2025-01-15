using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x02000684 RID: 1668
	internal sealed class GroupByClause : Node
	{
		// Token: 0x06004F28 RID: 20264 RVA: 0x0011F9F6 File Offset: 0x0011DBF6
		internal GroupByClause(NodeList<AliasedExpr> groupItems)
		{
			this._groupItems = groupItems;
		}

		// Token: 0x17000F4D RID: 3917
		// (get) Token: 0x06004F29 RID: 20265 RVA: 0x0011FA05 File Offset: 0x0011DC05
		internal NodeList<AliasedExpr> GroupItems
		{
			get
			{
				return this._groupItems;
			}
		}

		// Token: 0x04001CDD RID: 7389
		private readonly NodeList<AliasedExpr> _groupItems;
	}
}
