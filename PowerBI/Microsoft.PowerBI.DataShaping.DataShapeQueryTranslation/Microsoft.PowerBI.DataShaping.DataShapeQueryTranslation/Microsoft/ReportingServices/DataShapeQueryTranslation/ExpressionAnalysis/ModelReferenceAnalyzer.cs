using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.ExpressionAnalysis
{
	// Token: 0x020000B4 RID: 180
	internal sealed class ModelReferenceAnalyzer : ModelReferenceAnalyzerBase
	{
		// Token: 0x060007DB RID: 2011 RVA: 0x0001E469 File Offset: 0x0001C669
		private ModelReferenceAnalyzer()
		{
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x0001E471 File Offset: 0x0001C671
		public static bool ContainsModelReference(ExpressionNode node)
		{
			ModelReferenceAnalyzer modelReferenceAnalyzer = new ModelReferenceAnalyzer();
			modelReferenceAnalyzer.Visit(node);
			return modelReferenceAnalyzer.m_hasModelReference;
		}
	}
}
