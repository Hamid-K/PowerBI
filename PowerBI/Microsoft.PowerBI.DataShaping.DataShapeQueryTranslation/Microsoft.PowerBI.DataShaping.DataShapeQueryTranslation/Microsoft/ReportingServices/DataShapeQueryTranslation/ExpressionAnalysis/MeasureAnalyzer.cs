using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.InfoNav;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.ExpressionAnalysis
{
	// Token: 0x020000B2 RID: 178
	internal sealed class MeasureAnalyzer : ExpressionNodeTreeTransform
	{
		// Token: 0x060007D2 RID: 2002 RVA: 0x0001E3AE File Offset: 0x0001C5AE
		private MeasureAnalyzer()
			: base(false)
		{
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x0001E3B8 File Offset: 0x0001C5B8
		public static bool IsMeasure(ExpressionNode node)
		{
			MeasureAnalyzer measureAnalyzer = MeasureAnalyzer.Analyze(node);
			return measureAnalyzer.m_isModelMeasure || measureAnalyzer.m_isQueryMeasure;
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x0001E3DC File Offset: 0x0001C5DC
		public static bool IsModelMeasure(ExpressionNode node)
		{
			return MeasureAnalyzer.Analyze(node).m_isModelMeasure;
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x0001E3E9 File Offset: 0x0001C5E9
		private static MeasureAnalyzer Analyze(ExpressionNode node)
		{
			MeasureAnalyzer measureAnalyzer = new MeasureAnalyzer();
			measureAnalyzer.Visit(node);
			return measureAnalyzer;
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x0001E3F8 File Offset: 0x0001C5F8
		public override ExpressionNode Visit(ResolvedPropertyExpressionNode node)
		{
			this.m_isModelMeasure |= node.Property is IConceptualMeasure;
			return base.Visit(node);
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x0001E41C File Offset: 0x0001C61C
		public override ExpressionNode Visit(FunctionCallExpressionNode node)
		{
			this.m_isQueryMeasure |= node.Descriptor.FunctionCategory == FunctionCategory.Aggregate;
			return base.Visit(node);
		}

		// Token: 0x040003E0 RID: 992
		private bool m_isModelMeasure;

		// Token: 0x040003E1 RID: 993
		private bool m_isQueryMeasure;
	}
}
