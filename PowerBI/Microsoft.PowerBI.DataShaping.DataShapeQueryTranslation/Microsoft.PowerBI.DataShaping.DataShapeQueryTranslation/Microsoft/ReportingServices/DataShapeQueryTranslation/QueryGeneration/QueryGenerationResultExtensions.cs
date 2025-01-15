using System;
using System.Threading;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.QueryGeneration
{
	// Token: 0x0200008D RID: 141
	internal static class QueryGenerationResultExtensions
	{
		// Token: 0x060006A5 RID: 1701 RVA: 0x00017FAB File Offset: 0x000161AB
		public static QueryExpression ToSubQueryExpression(this QueryGenerationResult result, IFeatureSwitchProvider featureSwitchProvider, bool composable, CancellationToken cancellationToken, bool allowSummarizeColumns = false)
		{
			return result.QueryDefinition.ToQueryCommandTree(new QdmTranslationSettings(composable, false, allowSummarizeColumns), featureSwitchProvider, cancellationToken, null).Query;
		}
	}
}
