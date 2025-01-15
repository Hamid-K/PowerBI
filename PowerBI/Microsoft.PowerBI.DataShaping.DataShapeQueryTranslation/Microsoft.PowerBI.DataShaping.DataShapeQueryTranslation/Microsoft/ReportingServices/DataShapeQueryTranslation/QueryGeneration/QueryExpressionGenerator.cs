using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.InfoNav;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.CommonQueryGeneration;
using Microsoft.ReportingServices.DataShapeQueryTranslation.ExtensionEdm;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.QueryGeneration
{
	// Token: 0x02000080 RID: 128
	internal sealed class QueryExpressionGenerator : QueryExpressionGeneratorBase
	{
		// Token: 0x0600061D RID: 1565 RVA: 0x00015C86 File Offset: 0x00013E86
		internal QueryExpressionGenerator(ISubQueryGenerator subQueryGenerator, QueryGenerationContext context, WritableExpressionTable outputExpressionTable, List<IScope> queryMeasureScopes, GeneratedQueryParameterMap queryParameterMap)
			: base(context, outputExpressionTable, queryParameterMap)
		{
			this.m_subQueryGenerator = subQueryGenerator;
			this.m_queryMeasureScopes = queryMeasureScopes;
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x00015CA4 File Offset: 0x00013EA4
		public override List<KeyValuePair<ExpressionId, QueryExpressionContext>> TranslateCalculation(Calculation calculation)
		{
			ExpressionContext expressionContext = base.CreateExpressionContext(calculation);
			return base.TranslateExpressionInternal<List<KeyValuePair<ExpressionId, QueryExpressionContext>>>(expressionContext, () => this.TranslateCompoundExpressionImpl(calculation.Value.ExpressionId.Value, expressionContext, null));
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x00015CF0 File Offset: 0x00013EF0
		private List<KeyValuePair<ExpressionId, QueryExpressionContext>> TranslateCompoundExpressionImpl(ExpressionId expressionId, ExpressionContext expressionContext, Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal.FilterCondition filter)
		{
			List<KeyValuePair<ExpressionId, QueryExpressionContext>> list;
			if (this.m_cache.TryGetCompoundExpression(expressionId, out list))
			{
				return list;
			}
			list = new List<KeyValuePair<ExpressionId, QueryExpressionContext>>(1);
			ExpressionNode node = this.m_context.ExpressionTable.GetNode(expressionId);
			ExpressionNode expressionNode = this.TranslateNode(expressionContext, filter, node, list, new ExpressionId?(expressionId));
			this.m_outputExpressionTable.SetNode(expressionId, expressionNode);
			this.m_cache.PutCompoundExpression(expressionId, list);
			return list;
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x00015D54 File Offset: 0x00013F54
		private ExpressionNode TranslateNode(ExpressionContext expressionContext, Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal.FilterCondition filter, ExpressionNode expressionNode, List<KeyValuePair<ExpressionId, QueryExpressionContext>> generatedExpressions, ExpressionId? expressionId)
		{
			FunctionCallExpressionNode functionCallExpressionNode = expressionNode as FunctionCallExpressionNode;
			if (functionCallExpressionNode != null && functionCallExpressionNode.UsageKind == FunctionUsageKind.Processing)
			{
				ExpressionNode[] array = new ExpressionNode[functionCallExpressionNode.Arguments.Count];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this.TranslateNode(expressionContext, filter, functionCallExpressionNode.Arguments[i], generatedExpressions, null);
				}
				return new FunctionCallExpressionNode(functionCallExpressionNode.Descriptor, functionCallExpressionNode.UsageKind, array);
			}
			LiteralExpressionNode literalExpressionNode = expressionNode as LiteralExpressionNode;
			if (literalExpressionNode != null)
			{
				return literalExpressionNode;
			}
			if (expressionNode.Kind == ExpressionNodeKind.ResolvedRollup || expressionNode.Kind == ExpressionNodeKind.ResolvedLimitReference)
			{
				return expressionNode;
			}
			QueryExpressionContext queryExpressionContext = this.TranslateNode(expressionContext, expressionNode);
			QueryExpressionContext queryExpressionContext2 = queryExpressionContext;
			FederatedEntityDataModel model = this.m_context.Model;
			queryExpressionContext = QueryExpressionGeneratorBase.ApplyFilter(filter, queryExpressionContext2, (model != null) ? model.BaseModel : null, this.m_context.Schema.GetDefaultSchema(), this.m_context.DaxCapabilities, this.m_context.FeatureSwitchProvider, this.m_comparer, this.m_context.CancellationToken);
			ExpressionNode expressionNode2 = new PlaceholderExpressionNode();
			if (expressionId == null)
			{
				SubExpressionNode subExpressionNode = this.m_outputExpressionTable.CreateSubExpression(expressionNode2);
				expressionId = new ExpressionId?(subExpressionNode.ExpressionId);
				expressionNode2 = subExpressionNode;
			}
			generatedExpressions.Add(Microsoft.DataShaping.Util.ToKeyValuePair<ExpressionId, QueryExpressionContext>(expressionId.Value, queryExpressionContext));
			return expressionNode2;
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x00015E98 File Offset: 0x00014098
		public override QueryExpressionContext TranslateFilterExpression(ExpressionId expressionId, ExpressionContext expressionContext)
		{
			this.m_context.ExpressionTable.GetNode(expressionId);
			if (this.IsEvaluateWithRollup(expressionId))
			{
				return base.TranslateExpressionInternal<QueryExpressionContext>(expressionContext, () => this.TranslateTopLevelEvaluate(expressionId, expressionContext, false));
			}
			return base.TranslateExpressionInternal<QueryExpressionContext>(expressionContext, () => this.TranslateSingleExpressionImpl(expressionId, expressionContext, null));
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x00015F18 File Offset: 0x00014118
		public QueryExpressionContext TranslateSortExpression(ExpressionId expressionId, ExpressionContext expressionContext)
		{
			if (this.IsEvaluateWithRollup(expressionId))
			{
				return base.TranslateExpressionInternal<QueryExpressionContext>(expressionContext, () => this.TranslateTopLevelEvaluate(expressionId, expressionContext, true));
			}
			return base.TranslateExpressionInternal<QueryExpressionContext>(expressionContext, () => this.TranslateSingleExpressionImpl(expressionId, expressionContext, null));
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x00015F80 File Offset: 0x00014180
		private bool IsEvaluateWithRollup(ExpressionId expressionId)
		{
			FunctionCallExpressionNode functionCallExpressionNode = this.m_context.ExpressionTable.GetNode(expressionId) as FunctionCallExpressionNode;
			return functionCallExpressionNode != null && !(functionCallExpressionNode.Descriptor.Name != "Evaluate") && (functionCallExpressionNode.Arguments[1] as FunctionCallExpressionNode).Descriptor.Name == "Rollup";
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x00015FE8 File Offset: 0x000141E8
		private QueryExpressionContext TranslateTopLevelEvaluate(ExpressionId expressionId, ExpressionContext expressionContext, bool calculateInMeasureContext = false)
		{
			FunctionCallExpressionNode functionCallExpressionNode = (FunctionCallExpressionNode)this.m_context.ExpressionTable.GetNode(expressionId);
			ResolvedScopeReferenceExpressionNode resolvedScopeReferenceExpressionNode = (functionCallExpressionNode.Arguments[1] as FunctionCallExpressionNode).Arguments[0] as ResolvedScopeReferenceExpressionNode;
			IScope rollupStartScope = resolvedScopeReferenceExpressionNode.Scope;
			if (!this.m_queryMeasureScopes.Exists((IScope queryMeasureScope) => this.m_context.ScopeTree.AreSameScope(rollupStartScope, queryMeasureScope)))
			{
				throw new ExpressionTranslationException(TranslationMessagePhrases.InvalidRollupStartScope(rollupStartScope.Id.Value).FormattedString);
			}
			ExpressionNode expressionNode = functionCallExpressionNode.Arguments[0];
			QueryExpressionContext queryExpressionContext = this.TranslateNode(expressionContext, expressionNode);
			return new QueryExpressionContext(queryExpressionContext.QueryExpression, queryExpressionContext.QueryExpressionFeatures, calculateInMeasureContext);
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x000160A8 File Offset: 0x000142A8
		protected override QueryExpressionContext TranslateNode(ExpressionContext expressionContext, ExpressionNode expressionNode)
		{
			return QueryExpressionTranslator.Translate(expressionContext, this.m_subQueryGenerator, this, this.m_queryParameterMap, expressionNode, this.m_context.Annotations, this.m_context.Model, this.m_context.Schema.GetDefaultSchema(), this.m_context.DaxCapabilities, this.m_context.FeatureSwitchProvider, this.m_comparer, this.m_context.SuppressModelGrouping, this.m_context.CancellationToken);
		}

		// Token: 0x0400030F RID: 783
		private readonly ISubQueryGenerator m_subQueryGenerator;

		// Token: 0x04000310 RID: 784
		private readonly List<IScope> m_queryMeasureScopes;
	}
}
