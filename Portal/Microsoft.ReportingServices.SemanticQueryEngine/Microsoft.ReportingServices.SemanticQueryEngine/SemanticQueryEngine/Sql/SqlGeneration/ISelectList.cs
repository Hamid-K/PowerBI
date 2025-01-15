using System;
using Microsoft.ReportingServices.Modeling;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration
{
	// Token: 0x02000034 RID: 52
	internal interface ISelectList : ISqlSnippet
	{
		// Token: 0x0600023D RID: 573
		SqlSelectExpression GetSelectExpression(Expression expressionKey);

		// Token: 0x0600023E RID: 574
		SqlSelectExpression GetSelectExpression(DsvColumn expressionKey);

		// Token: 0x0600023F RID: 575
		SqlSelectExpression GetAggregationFlagExpression(Expression expressionKey);

		// Token: 0x06000240 RID: 576
		SqlSelectExpression GetAggregationFieldCountExpression();

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000241 RID: 577
		bool UseParensWhenNested { get; }
	}
}
