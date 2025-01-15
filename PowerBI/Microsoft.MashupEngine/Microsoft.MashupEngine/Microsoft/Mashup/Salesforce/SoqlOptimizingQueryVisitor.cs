using System;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Salesforce
{
	// Token: 0x020001EF RID: 495
	internal class SoqlOptimizingQueryVisitor : OptimizingQueryVisitor
	{
		// Token: 0x060009EA RID: 2538 RVA: 0x00015944 File Offset: 0x00013B44
		protected override Query VisitNestedJoin(NestedJoinQuery query)
		{
			return base.VisitQuery(query.LeftQuery, (Query q) => q.NestedJoin(query.LeftKeyColumns, query.DelayedRightTable, query.RightKey, query.JoinKind, query.NewColumnName, query.JoinKeys, query.KeyEqualityComparers));
		}
	}
}
