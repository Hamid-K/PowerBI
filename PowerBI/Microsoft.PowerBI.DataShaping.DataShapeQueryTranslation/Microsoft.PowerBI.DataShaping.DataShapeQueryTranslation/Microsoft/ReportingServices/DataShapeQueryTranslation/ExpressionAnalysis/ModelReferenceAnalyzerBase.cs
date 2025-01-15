using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.ExpressionAnalysis
{
	// Token: 0x020000B3 RID: 179
	internal abstract class ModelReferenceAnalyzerBase : ExpressionNodeTreeTransform
	{
		// Token: 0x060007D8 RID: 2008 RVA: 0x0001E440 File Offset: 0x0001C640
		protected ModelReferenceAnalyzerBase()
			: base(false)
		{
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x0001E449 File Offset: 0x0001C649
		public override ExpressionNode Visit(ResolvedPropertyExpressionNode node)
		{
			this.m_hasModelReference = true;
			return base.Visit(node);
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x0001E459 File Offset: 0x0001C659
		public override ExpressionNode Visit(ResolvedEntitySetExpressionNode node)
		{
			this.m_hasModelReference = true;
			return base.Visit(node);
		}

		// Token: 0x040003E2 RID: 994
		protected bool m_hasModelReference;
	}
}
