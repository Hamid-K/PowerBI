using System;
using Microsoft.ReportingServices.Modeling;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.QueryPlanGeneration
{
	// Token: 0x02000053 RID: 83
	internal interface IQPExpressionInfo
	{
		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060003AE RID: 942
		Expression Expression { get; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060003AF RID: 943
		Expression ObjKey { get; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060003B0 RID: 944
		bool IsInnerMost { get; }

		// Token: 0x060003B1 RID: 945
		void CheckAggregateExpression();

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060003B2 RID: 946
		Expression AggregateArgument { get; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060003B3 RID: 947
		bool MustAggregate { get; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060003B4 RID: 948
		bool MustAggregateDegenerate { get; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060003B5 RID: 949
		bool IsAggregateInvocationPoint { get; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060003B6 RID: 950
		bool IsInnerMostAggregation { get; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060003B7 RID: 951
		bool Nullable { get; }
	}
}
