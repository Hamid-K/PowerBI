using System;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.BatchQueries
{
	// Token: 0x02000279 RID: 633
	internal interface IQueryTableGroupBuilder
	{
		// Token: 0x06001B42 RID: 6978
		QueryTableColumn AddGroupKey(QueryExpression key, string suggestedName);

		// Token: 0x06001B43 RID: 6979
		QueryTableColumn AddGroupDetail(QueryExpression detail, string suggestedName);

		// Token: 0x06001B44 RID: 6980
		void AddContextTable(QueryExpression expression);

		// Token: 0x06001B45 RID: 6981
		bool TryGetExistingColumn(QueryExpression expression, out QueryTableColumn column);

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x06001B46 RID: 6982
		QueryTableColumn SubtotalIndicatorColumn { get; }
	}
}
