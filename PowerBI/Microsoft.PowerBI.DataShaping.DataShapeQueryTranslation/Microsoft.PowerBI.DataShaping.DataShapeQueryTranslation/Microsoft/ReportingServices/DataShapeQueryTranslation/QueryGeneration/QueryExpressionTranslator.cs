using System;
using System.Threading;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.CommonQueryGeneration;
using Microsoft.ReportingServices.DataShapeQueryTranslation.ExtensionEdm;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.QueryGeneration
{
	// Token: 0x02000086 RID: 134
	internal sealed class QueryExpressionTranslator : QueryExpressionTranslatorBase
	{
		// Token: 0x06000669 RID: 1641 RVA: 0x00017624 File Offset: 0x00015824
		private QueryExpressionTranslator(ExpressionContext expressionContext, ISubQueryGenerator subQueryGenerator, IQueryExpressionGenerator expressionGenerator, GeneratedQueryParameterMap queryParameterMap, DataShapeAnnotations annotations, FederatedEntityDataModel model, IConceptualSchema schema, DaxCapabilities daxCapabilities, IFeatureSwitchProvider featureSwitchProvider, IDataComparer comparer, bool suppressModelGrouping, CancellationToken cancellationToken)
			: base(expressionContext, expressionGenerator, queryParameterMap, model, schema, daxCapabilities, featureSwitchProvider, comparer, suppressModelGrouping, cancellationToken)
		{
			this.m_subQueryGenerator = subQueryGenerator;
			this.m_annotations = annotations;
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x00017658 File Offset: 0x00015858
		public static QueryExpressionContext Translate(ExpressionContext expressionContext, ISubQueryGenerator subQueryGenerator, IQueryExpressionGenerator expressionGenerator, GeneratedQueryParameterMap queryParameterMap, ExpressionNode expressionNode, DataShapeAnnotations annotations, FederatedEntityDataModel model, IConceptualSchema schema, DaxCapabilities daxCapabilities, IFeatureSwitchProvider featureSwitchProvider, IDataComparer comparer, bool suppressModelGrouping, CancellationToken cancellationToken)
		{
			return new QueryExpressionTranslator(expressionContext, subQueryGenerator, expressionGenerator, queryParameterMap, annotations, model, schema, daxCapabilities, featureSwitchProvider, comparer, suppressModelGrouping, cancellationToken).Translate(expressionNode);
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x00017685 File Offset: 0x00015885
		protected override QueryExpression TranslateAggregatableSubQuery(AggregatableSubQueryExpressionNode node, out QueryExpressionFeatures features)
		{
			features = QueryExpressionFeatures.QueryMeasure | QueryExpressionFeatures.RequiresCalculate;
			return this.m_subQueryGenerator.GenerateAggregatableSubQuery(node.TargetExpressionId, this.m_expressionContext, node.DataSetPlan).SubQueryExpression;
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x000176AD File Offset: 0x000158AD
		protected override QueryExpression TranslateTableSubQuery(TableSubQueryExpressionNode node, out QueryExpressionFeatures features)
		{
			features = QueryExpressionFeatures.RequiresCalculate;
			return this.m_subQueryGenerator.GenerateTableSubQuery(node.Name, node.DataSetPlan).SubQueryExpression;
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x000176CF File Offset: 0x000158CF
		protected override QueryExpression TranslateBatchSubQuery(BatchSubQueryExpressionNode node)
		{
			throw BasicQueryExpressionTranslatorBase.SyntaxError(node);
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x000176D7 File Offset: 0x000158D7
		protected override QueryExpression TranslateAggregatableCurrentGroup(AggregatableCurrentGroupExpressionNode node, out QueryExpressionFeatures features)
		{
			throw BasicQueryExpressionTranslatorBase.SyntaxError(node);
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x000176DF File Offset: 0x000158DF
		protected override QueryExpression TranslateBatchColumnReference(BatchColumnReferenceExpressionNode node, out QueryExpressionFeatures features)
		{
			throw BasicQueryExpressionTranslatorBase.SyntaxError(node);
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x000176E7 File Offset: 0x000158E7
		protected override QueryExpression TranslateBatchColumnReference(BatchColumnReferenceByExpressionIdExpressionNode node, out QueryExpressionFeatures features)
		{
			throw BasicQueryExpressionTranslatorBase.SyntaxError(node);
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x000176EF File Offset: 0x000158EF
		protected override QueryExpression TranslateBatchScalarDeclarationReference(BatchScalarDeclarationReferenceExpressionNode node, out QueryExpressionFeatures features)
		{
			throw BasicQueryExpressionTranslatorBase.SyntaxError(node);
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x000176F7 File Offset: 0x000158F7
		protected override QueryExpression TranslateApplyContextFilter(ApplyContextFilterExpressionNode node, out QueryExpressionFeatures features)
		{
			throw BasicQueryExpressionTranslatorBase.SyntaxError(node);
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x000176FF File Offset: 0x000158FF
		protected override QueryExpression TranslateBatchFilterInlinedDeclarationCalculation(BatchFilterInlinedDeclarationCalculationExpressionNode node, out QueryExpressionFeatures features)
		{
			throw BasicQueryExpressionTranslatorBase.SyntaxError(node);
		}

		// Token: 0x04000322 RID: 802
		private readonly ISubQueryGenerator m_subQueryGenerator;

		// Token: 0x04000323 RID: 803
		private readonly DataShapeAnnotations m_annotations;
	}
}
