using System;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.DataShaping.ServiceContracts.QueryTranslation;

namespace Microsoft.DataShaping.SemanticQueryTranslation.Utils
{
	// Token: 0x02000018 RID: 24
	internal static class SemanticQueryToDaxTranslationResultUtils
	{
		// Token: 0x060000CE RID: 206 RVA: 0x00004C37 File Offset: 0x00002E37
		internal static SemanticQueryToDaxTranslationResult ForSingleExpression(string daxExpression, SemanticQueryTranslationErrorContext errorContext)
		{
			return new SemanticQueryToDaxTranslationResult(daxExpression, null, null, SemanticQueryToDaxTranslationResultUtils.GetErrorInfo(errorContext));
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00004C47 File Offset: 0x00002E47
		internal static SemanticQueryToDaxTranslationResult ForClustering(ClusteringTranslationResult clusteringResult, SemanticQueryTranslationErrorContext errorContext)
		{
			return new SemanticQueryToDaxTranslationResult(null, clusteringResult, null, SemanticQueryToDaxTranslationResultUtils.GetErrorInfo(errorContext));
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00004C57 File Offset: 0x00002E57
		internal static SemanticQueryToDaxTranslationResult ForClusteringColumn(string columnExpression, SemanticQueryTranslationErrorContext errorContext)
		{
			return new SemanticQueryToDaxTranslationResult(null, null, columnExpression, SemanticQueryToDaxTranslationResultUtils.GetErrorInfo(errorContext));
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004C67 File Offset: 0x00002E67
		internal static SemanticQueryToDaxTranslationResult ForError(SemanticQueryTranslationErrorContext errorContext)
		{
			return new SemanticQueryToDaxTranslationResult(null, null, null, SemanticQueryToDaxTranslationResultUtils.GetErrorInfo(errorContext));
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004C77 File Offset: 0x00002E77
		private static DataShapeEngineErrorInfo GetErrorInfo(SemanticQueryTranslationErrorContext errorContext)
		{
			if (errorContext.HasError)
			{
				return SemanticQueryTranslationException.Create(errorContext).ToErrorInfo();
			}
			return null;
		}
	}
}
