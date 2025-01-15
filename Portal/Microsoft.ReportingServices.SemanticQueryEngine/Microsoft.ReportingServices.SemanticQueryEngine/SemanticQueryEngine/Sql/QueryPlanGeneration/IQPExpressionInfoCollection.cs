using System;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.QueryPlanGeneration
{
	// Token: 0x02000051 RID: 81
	internal interface IQPExpressionInfoCollection
	{
		// Token: 0x17000087 RID: 135
		IQPExpressionInfo this[int index] { get; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060003A7 RID: 935
		int Count { get; }
	}
}
