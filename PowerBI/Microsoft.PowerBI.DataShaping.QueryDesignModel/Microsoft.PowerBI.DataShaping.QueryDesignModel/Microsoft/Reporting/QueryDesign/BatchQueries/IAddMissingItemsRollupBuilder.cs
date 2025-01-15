using System;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.BatchQueries
{
	// Token: 0x02000255 RID: 597
	internal interface IAddMissingItemsRollupBuilder
	{
		// Token: 0x06001A0A RID: 6666
		IAddMissingItemsGroupBuilder AddRollupGroup(QdmTableColumnReferenceExpression subtotalIndicatorColumnRef);

		// Token: 0x06001A0B RID: 6667
		void AddContextTable(QueryExpression expression);
	}
}
