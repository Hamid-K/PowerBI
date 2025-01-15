using System;
using System.Threading.Tasks;
using Microsoft.DataShaping.Engine.DaxQueryExecution;
using Microsoft.DataShaping.Engine.QueryTranslation;
using Microsoft.DataShaping.ServiceContracts.QueryTranslation;

namespace Microsoft.DataShaping.Engine
{
	// Token: 0x02000019 RID: 25
	public interface IDataShapeEngine
	{
		// Token: 0x06000098 RID: 152
		Task ExecuteSemanticQueryAsync(ExecuteSemanticQueryContext context);

		// Token: 0x06000099 RID: 153
		Task<ExecuteSemanticQueryResult> ExecuteSemanticQueryWithResultAsync(ExecuteSemanticQueryContext context);

		// Token: 0x0600009A RID: 154
		void ExecuteSemanticQuery(ExecuteSemanticQueryContext context);

		// Token: 0x0600009B RID: 155
		ExecuteSemanticQueryResult ExecuteSemanticQueryWithResult(ExecuteSemanticQueryContext context);

		// Token: 0x0600009C RID: 156
		Task ExecuteSemanticQueryRawDataAsync(ExecuteSemanticQueryContext context);

		// Token: 0x0600009D RID: 157
		Task<ExecuteSemanticQueryResult> ExecuteSemanticQueryRawDataWithResultAsync(ExecuteSemanticQueryContext context);

		// Token: 0x0600009E RID: 158
		void ExecuteSemanticQueryRawData(ExecuteSemanticQueryContext context);

		// Token: 0x0600009F RID: 159
		ExecuteSemanticQueryResult ExecuteSemanticQueryRawDataWithResult(ExecuteSemanticQueryContext context);

		// Token: 0x060000A0 RID: 160
		ExecuteDaxQueryResult ExecuteDaxQuery(ExecuteDaxQueryContext context);

		// Token: 0x060000A1 RID: 161
		Task<ExecuteDaxQueryResult> ExecuteDaxQueryAsync(ExecuteDaxQueryContext context);

		// Token: 0x060000A2 RID: 162
		TranslateSemanticQueryResult TranslateSemanticQuery(TranslateSemanticQueryContext context);

		// Token: 0x060000A3 RID: 163
		SemanticQueryToDaxTranslationResult TranslateGroupingQuery(TranslateGroupingQueryContext context);

		// Token: 0x060000A4 RID: 164
		SemanticQueryToDaxTranslationResult TranslatePartitionColumn(TranslateGroupingQueryContext context);
	}
}
