using System;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.QueryTranslation;

namespace Microsoft.DataShaping.SemanticQueryTranslation.TranslateQuery
{
	// Token: 0x0200001A RID: 26
	internal sealed class TranslateQueryCommandProcessorResult
	{
		// Token: 0x060000DA RID: 218 RVA: 0x00005008 File Offset: 0x00003208
		private TranslateQueryCommandProcessorResult(TranslatedQuery translatedQuery, DataShapeEngineErrorInfo errorInfo)
		{
			this.TranslatedQuery = translatedQuery;
			this.ErrorInfo = errorInfo;
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000DB RID: 219 RVA: 0x0000501E File Offset: 0x0000321E
		internal TranslatedQuery TranslatedQuery { get; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00005026 File Offset: 0x00003226
		internal DataShapeEngineErrorInfo ErrorInfo { get; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000DD RID: 221 RVA: 0x0000502E File Offset: 0x0000322E
		internal bool Succeeded
		{
			get
			{
				return this.ErrorInfo == null;
			}
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00005039 File Offset: 0x00003239
		internal static TranslateQueryCommandProcessorResult ForSuccess(TranslatedQuery translatedQuery)
		{
			return new TranslateQueryCommandProcessorResult(translatedQuery, null);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00005042 File Offset: 0x00003242
		internal static TranslateQueryCommandProcessorResult ForError(TranslatedQuery translatedQuery, DataShapeEngineErrorInfo errorInfo)
		{
			return new TranslateQueryCommandProcessorResult(translatedQuery, errorInfo);
		}
	}
}
