using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.ExpressionAnalysis
{
	// Token: 0x020000AD RID: 173
	internal sealed class EvaluateScopeAnalyzer : ExpressionNodeTreeTransform
	{
		// Token: 0x060007A9 RID: 1961 RVA: 0x0001D981 File Offset: 0x0001BB81
		private EvaluateScopeAnalyzer()
			: base(false)
		{
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x0001D98A File Offset: 0x0001BB8A
		public static bool UsesEvaluateWithScope(ExpressionNode node)
		{
			EvaluateScopeAnalyzer evaluateScopeAnalyzer = new EvaluateScopeAnalyzer();
			evaluateScopeAnalyzer.Visit(node);
			return evaluateScopeAnalyzer.m_usesEvaluateWithScope;
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x0001D99E File Offset: 0x0001BB9E
		public override ExpressionNode Visit(FunctionCallExpressionNode node)
		{
			this.m_usesEvaluateWithScope |= node.Descriptor.Name == "Scope";
			return base.Visit(node);
		}

		// Token: 0x040003D0 RID: 976
		private bool m_usesEvaluateWithScope;
	}
}
