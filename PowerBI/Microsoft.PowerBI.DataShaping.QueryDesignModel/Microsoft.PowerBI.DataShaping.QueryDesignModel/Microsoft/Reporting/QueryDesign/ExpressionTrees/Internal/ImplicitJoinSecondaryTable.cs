using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping.Common;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x02000193 RID: 403
	internal sealed class ImplicitJoinSecondaryTable
	{
		// Token: 0x06001566 RID: 5478 RVA: 0x0003BFDC File Offset: 0x0003A1DC
		internal ImplicitJoinSecondaryTable(QueryExpressionBinding table, IReadOnlyList<KeyValuePair<string, QueryFieldExpression>> keyColumnNames)
		{
			this.Table = table;
			this.KeyColumns = keyColumnNames;
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06001567 RID: 5479 RVA: 0x0003BFF2 File Offset: 0x0003A1F2
		internal QueryExpressionBinding Table { get; }

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06001568 RID: 5480 RVA: 0x0003BFFA File Offset: 0x0003A1FA
		internal IReadOnlyList<KeyValuePair<string, QueryFieldExpression>> KeyColumns { get; }

		// Token: 0x06001569 RID: 5481 RVA: 0x0003C004 File Offset: 0x0003A204
		public override bool Equals(object obj)
		{
			ImplicitJoinSecondaryTable implicitJoinSecondaryTable = obj as ImplicitJoinSecondaryTable;
			return implicitJoinSecondaryTable != null && this.Table.Equals(implicitJoinSecondaryTable.Table) && this.KeyColumns.SequenceEqual(implicitJoinSecondaryTable.KeyColumns);
		}

		// Token: 0x0600156A RID: 5482 RVA: 0x0003C043 File Offset: 0x0003A243
		public override int GetHashCode()
		{
			return Hashing.CombineHash(this.Table.GetHashCode(), Hashing.CombineHashReadonly<KeyValuePair<string, QueryFieldExpression>>(this.KeyColumns, null));
		}
	}
}
