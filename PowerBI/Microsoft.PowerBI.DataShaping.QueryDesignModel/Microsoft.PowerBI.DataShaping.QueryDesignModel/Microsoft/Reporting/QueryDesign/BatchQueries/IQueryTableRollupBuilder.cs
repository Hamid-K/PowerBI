using System;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.BatchQueries
{
	// Token: 0x0200027B RID: 635
	internal interface IQueryTableRollupBuilder
	{
		// Token: 0x06001B59 RID: 7001
		IQueryTableGroupBuilder AddRollupGroup(string name);

		// Token: 0x06001B5A RID: 7002
		void AddContextTable(QueryExpression expression);
	}
}
