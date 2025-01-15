using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataTransformBypass
{
	// Token: 0x020000C5 RID: 197
	internal interface IExpressionTableLookup
	{
		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000852 RID: 2130
		int Count { get; }

		// Token: 0x06000853 RID: 2131
		int GetExpressionTableIndex(IContextItem item);

		// Token: 0x06000854 RID: 2132
		ExpressionTable GetExpressionTable(IContextItem item);

		// Token: 0x06000855 RID: 2133
		ExpressionTable GetExpressionTable(int index);

		// Token: 0x06000856 RID: 2134
		ExpressionTable GetFallbackExpressionTable();
	}
}
