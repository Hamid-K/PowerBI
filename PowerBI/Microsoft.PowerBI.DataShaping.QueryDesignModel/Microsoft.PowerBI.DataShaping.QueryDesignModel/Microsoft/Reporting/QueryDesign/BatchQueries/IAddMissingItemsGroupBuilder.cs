using System;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.BatchQueries
{
	// Token: 0x02000254 RID: 596
	internal interface IAddMissingItemsGroupBuilder
	{
		// Token: 0x06001A08 RID: 6664
		void AddGroupKey(QdmTableColumnReferenceExpression columnRef);

		// Token: 0x06001A09 RID: 6665
		void AddContextTable(QueryExpression expression);
	}
}
