using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using Microsoft.DataShaping;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.ExpressionBuilder.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.CommonQueryGeneration;
using Microsoft.ReportingServices.DataShapeQueryTranslation.ExtensionEdm;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.QueryGeneration
{
	// Token: 0x02000083 RID: 131
	internal abstract class QueryExpressionTranslatorBase : BasicQueryExpressionTranslatorBase
	{
		// Token: 0x06000646 RID: 1606 RVA: 0x00016E58 File Offset: 0x00015058
		protected QueryExpressionTranslatorBase(ExpressionContext expressionContext, IQueryExpressionGenerator expressionGenerator, GeneratedQueryParameterMap queryParameterMap, FederatedEntityDataModel model, IConceptualSchema schema, DaxCapabilities daxCapabilities, IFeatureSwitchProvider featureSwitchProvider, IDataComparer dataComparer, bool suppressModelGrouping, CancellationToken cancellationToken)
			: base(expressionContext, model, schema, suppressModelGrouping, cancellationToken, featureSwitchProvider, false)
		{
			this.m_expressionGenerator = expressionGenerator;
			this.m_queryParameterMap = queryParameterMap;
			this.m_daxCapabilities = daxCapabilities;
			this.m_dataComparer = dataComparer;
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x00016E8C File Offset: 0x0001508C
		protected override QueryExpression Translate(ExpressionNode node, out QueryExpressionFeatures features)
		{
			ExpressionNodeKind kind = node.Kind;
			if (kind <= ExpressionNodeKind.ResolvedCalculationReference)
			{
				if (kind <= ExpressionNodeKind.FilterInlinedCalculation)
				{
					switch (kind)
					{
					case ExpressionNodeKind.ApplyContextFilter:
						return this.TranslateApplyContextFilter((ApplyContextFilterExpressionNode)node, out features);
					case ExpressionNodeKind.BatchColumnReference:
						return this.TranslateBatchColumnReference((BatchColumnReferenceExpressionNode)node, out features);
					case ExpressionNodeKind.BatchColumnReferenceByExpressionId:
						return this.TranslateBatchColumnReference((BatchColumnReferenceByExpressionIdExpressionNode)node, out features);
					case ExpressionNodeKind.BatchFilterInlinedDeclarationCalculation:
						return this.TranslateBatchFilterInlinedDeclarationCalculation((BatchFilterInlinedDeclarationCalculationExpressionNode)node, out features);
					case ExpressionNodeKind.BatchScalarDeclarationReference:
						return this.TranslateBatchScalarDeclarationReference((BatchScalarDeclarationReferenceExpressionNode)node, out features);
					default:
						if (kind == ExpressionNodeKind.FilterInlinedCalculation)
						{
							FederatedEntityDataModel model = this.m_model;
							return this.TranslateFilterInlinedCalculation((model != null) ? model.BaseModel : null, (FilterInlinedCalculationExpressionNode)node, out features);
						}
						break;
					}
				}
				else
				{
					if (kind == ExpressionNodeKind.RemoveGroupings)
					{
						return this.TranslateRemoveGroupings((RemoveGroupingsExpressionNode)node, out features);
					}
					if (kind == ExpressionNodeKind.ResolvedCalculationReference)
					{
						return this.TranslateCalculationReference((ResolvedCalculationReferenceExpressionNode)node, out features);
					}
				}
			}
			else if (kind <= ExpressionNodeKind.SingleValue)
			{
				if (kind == ExpressionNodeKind.ResolvedGroupKeyReference)
				{
					return this.TranslateGroupKeyReference((ResolvedGroupKeyReferenceExpressionNode)node, out features);
				}
				if (kind == ExpressionNodeKind.SingleValue)
				{
					return this.TranslateSingleValue((SingleValueExpressionNode)node, out features);
				}
			}
			else
			{
				if (kind == ExpressionNodeKind.TableSubQueryExpression)
				{
					return this.TranslateTableSubQuery((TableSubQueryExpressionNode)node, out features);
				}
				if (kind == ExpressionNodeKind.QueryParameterReference)
				{
					return this.TranslateQueryParameterReference((QueryParameterReferenceExpressionNode)node, out features);
				}
			}
			return base.Translate(node, out features);
		}

		// Token: 0x06000648 RID: 1608
		protected abstract QueryExpression TranslateApplyContextFilter(ApplyContextFilterExpressionNode node, out QueryExpressionFeatures features);

		// Token: 0x06000649 RID: 1609
		protected abstract QueryExpression TranslateAggregatableCurrentGroup(AggregatableCurrentGroupExpressionNode node, out QueryExpressionFeatures features);

		// Token: 0x0600064A RID: 1610
		protected abstract QueryExpression TranslateAggregatableSubQuery(AggregatableSubQueryExpressionNode node, out QueryExpressionFeatures features);

		// Token: 0x0600064B RID: 1611
		protected abstract QueryExpression TranslateBatchSubQuery(BatchSubQueryExpressionNode node);

		// Token: 0x0600064C RID: 1612
		protected abstract QueryExpression TranslateTableSubQuery(TableSubQueryExpressionNode node, out QueryExpressionFeatures features);

		// Token: 0x0600064D RID: 1613 RVA: 0x00016FD0 File Offset: 0x000151D0
		protected QueryExpression TranslateCalculationReference(ResolvedCalculationReferenceExpressionNode node, out QueryExpressionFeatures features)
		{
			QueryExpressionContext queryExpressionContext = this.m_expressionGenerator.TranslateCalculationReference(node.Calculation);
			features = queryExpressionContext.QueryExpressionFeatures;
			return queryExpressionContext.QueryExpression;
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x00017000 File Offset: 0x00015200
		protected QueryExpression TranslateGroupKeyReference(ResolvedGroupKeyReferenceExpressionNode node, out QueryExpressionFeatures features)
		{
			QueryExpressionContext queryExpressionContext = this.m_expressionGenerator.TranslateGroupKeyReference(node.GroupKey);
			features = queryExpressionContext.QueryExpressionFeatures;
			return queryExpressionContext.QueryExpression;
		}

		// Token: 0x0600064F RID: 1615
		protected abstract QueryExpression TranslateBatchColumnReference(BatchColumnReferenceExpressionNode node, out QueryExpressionFeatures features);

		// Token: 0x06000650 RID: 1616
		protected abstract QueryExpression TranslateBatchColumnReference(BatchColumnReferenceByExpressionIdExpressionNode node, out QueryExpressionFeatures features);

		// Token: 0x06000651 RID: 1617
		protected abstract QueryExpression TranslateBatchScalarDeclarationReference(BatchScalarDeclarationReferenceExpressionNode node, out QueryExpressionFeatures features);

		// Token: 0x06000652 RID: 1618
		protected abstract QueryExpression TranslateBatchFilterInlinedDeclarationCalculation(BatchFilterInlinedDeclarationCalculationExpressionNode node, out QueryExpressionFeatures features);

		// Token: 0x06000653 RID: 1619 RVA: 0x00017030 File Offset: 0x00015230
		private QueryExpression TranslateFilterInlinedCalculation(IConceptualModel model, FilterInlinedCalculationExpressionNode node, out QueryExpressionFeatures features)
		{
			QueryExpressionContext queryExpressionContext = base.Translate(node.ExpressionNode);
			features = queryExpressionContext.QueryExpressionFeatures;
			if (node.FilterCondition != null)
			{
				FilterCondition filterCondition = QueryFilterGenerator.CreateFilter(node.FilterCondition, this.m_expressionGenerator, this.m_expressionContext.ErrorContext, node.Calculation.Id, this.m_cancellationToken);
				QueryExpression[] array = new FilterCondition[] { filterCondition }.QdmFilters(model, this.m_schema, this.m_daxCapabilities, this.m_featureSwitchProvider, this.m_dataComparer, this.m_cancellationToken, ScanKind.IndependentFilterContextIncludeBlankRow, false);
				return queryExpressionContext.QueryExpression.Calculate(array);
			}
			QueryExpressionContext queryExpressionContext2 = base.Translate(node.FilterExpression);
			return queryExpressionContext.QueryExpression.Calculate(new QueryExpression[] { queryExpressionContext2.QueryExpression });
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x000170F0 File Offset: 0x000152F0
		protected override QueryExpression TranslateFunctionCall(FunctionCallExpressionNode functionCallNode, out QueryExpressionFeatures features)
		{
			if (!this.m_ignoreFunctionUsageCheck)
			{
				base.CheckQueryFunctionUsage(functionCallNode);
			}
			string name = functionCallNode.Descriptor.Name;
			if (name == "Any")
			{
				return this.TranslateAnyFunctionCall(functionCallNode, out features);
			}
			if (name == "IsEmptyTable")
			{
				return this.TranslateIsEmptyTableFunctionCall(functionCallNode, out features);
			}
			if (name == "NegativeValues")
			{
				return this.TranslatePositiveNegativeValuesFunctionCall(functionCallNode, FilterOperator.LessThan, out features);
			}
			if (!(name == "PositiveValues"))
			{
				return base.TranslateFunctionCall(functionCallNode, out features);
			}
			return this.TranslatePositiveNegativeValuesFunctionCall(functionCallNode, FilterOperator.GreaterThan, out features);
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x0001717C File Offset: 0x0001537C
		protected override QueryExpression TranslateAggregateArgument(ExpressionNode node, out QueryExpressionFeatures features)
		{
			ExpressionNodeKind kind = node.Kind;
			switch (kind)
			{
			case ExpressionNodeKind.AggregatableCurrentGroupExpression:
				return this.TranslateAggregatableCurrentGroup((AggregatableCurrentGroupExpressionNode)node, out features);
			case ExpressionNodeKind.AggregatableSubQueryExpression:
				return this.TranslateAggregatableSubQuery((AggregatableSubQueryExpressionNode)node, out features);
			case ExpressionNodeKind.ApplyContextFilter:
			case ExpressionNodeKind.BatchColumnReference:
				break;
			case ExpressionNodeKind.BatchColumnReferenceByExpressionId:
				return this.TranslateBatchColumnReference((BatchColumnReferenceByExpressionIdExpressionNode)node, out features);
			default:
				if (kind == ExpressionNodeKind.ResolvedCalculationReference)
				{
					return this.TranslateCalculationReference((ResolvedCalculationReferenceExpressionNode)node, out features);
				}
				if (kind == ExpressionNodeKind.SingleValue)
				{
					return this.TranslateSingleValue((SingleValueExpressionNode)node, out features);
				}
				break;
			}
			return base.TranslateAggregateArgument(node, out features);
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x00017204 File Offset: 0x00015404
		private QueryExpression TranslateAnyFunctionCall(FunctionCallExpressionNode functionCallNode, out QueryExpressionFeatures features)
		{
			ReadOnlyCollection<ExpressionNode> arguments = functionCallNode.Arguments;
			if (arguments == null || arguments.Count != 1)
			{
				throw BasicQueryExpressionTranslatorBase.CannotInvokeFunction(functionCallNode, null);
			}
			QueryExpression queryExpression;
			if (!this.TryTranslateFilterArgument(functionCallNode.Arguments[0], out queryExpression, out features))
			{
				throw BasicQueryExpressionTranslatorBase.SyntaxError(functionCallNode.Arguments[0]);
			}
			QueryProjectExpression queryProjectExpression = queryExpression as QueryProjectExpression;
			features = BasicQueryExpressionTranslatorBase.UpdateFeaturesForAggregateFunction(features);
			QueryExpressionBinding input = queryProjectExpression.Input;
			QueryExpression projection = queryProjectExpression.Projection;
			return input.Filter(projection).HasAnyRows(false);
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x00017280 File Offset: 0x00015480
		private QueryExpression TranslatePositiveNegativeValuesFunctionCall(FunctionCallExpressionNode functionCallNode, FilterOperator filterOperator, out QueryExpressionFeatures features)
		{
			ReadOnlyCollection<ExpressionNode> arguments = functionCallNode.Arguments;
			if (arguments == null || arguments.Count != 1)
			{
				throw BasicQueryExpressionTranslatorBase.CannotInvokeFunction(functionCallNode, null);
			}
			QueryExpression queryExpression;
			if (this.TryTranslateFilterArgument(functionCallNode.Arguments[0], out queryExpression, out features))
			{
				return this.TranslateTabularPositiveNegativeValues(filterOperator, queryExpression);
			}
			return this.TranslateScalarPositiveNegativeValues(functionCallNode, filterOperator, out features);
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x000172D4 File Offset: 0x000154D4
		private QueryExpression TranslateTabularPositiveNegativeValues(FilterOperator filterOperator, QueryExpression translatedArg)
		{
			QueryProjectExpression queryProjectExpression = translatedArg as QueryProjectExpression;
			QueryExpressionBinding input = queryProjectExpression.Input;
			QueryExpression projection = queryProjectExpression.Projection;
			QueryComparisonExpression queryComparisonExpression = QueryExpressionTranslatorBase.TranslatePositiveNegativeValuesComparison(filterOperator, projection);
			return input.Filter(queryComparisonExpression).BindAs(input.Variable.VariableName).Project(projection, queryProjectExpression.ProjectSubsetStrategy);
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x00017324 File Offset: 0x00015524
		private static QueryComparisonExpression TranslatePositiveNegativeValuesComparison(FilterOperator filterOperator, QueryExpression valueExpr)
		{
			if (filterOperator == FilterOperator.GreaterThan)
			{
				return valueExpr.GreaterThan(Literals.Zero);
			}
			if (filterOperator != FilterOperator.LessThan)
			{
				Microsoft.DataShaping.Contract.RetailFail("We should never get here; filter operator '" + filterOperator.ToString() + "' not supported.");
				throw new NotSupportedException();
			}
			return valueExpr.LessThan(Literals.Zero);
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x0001737C File Offset: 0x0001557C
		private QueryExpression TranslateScalarPositiveNegativeValues(FunctionCallExpressionNode functionCallNode, FilterOperator filterOperator, out QueryExpressionFeatures features)
		{
			QueryExpression queryExpression = this.Translate(functionCallNode.Arguments[0], out features);
			return QueryExpressionTranslatorBase.TranslatePositiveNegativeValuesComparison(filterOperator, queryExpression).If(queryExpression, queryExpression.ConceptualResultType.Null());
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x000173B5 File Offset: 0x000155B5
		private bool TryTranslateFilterArgument(ExpressionNode node, out QueryExpression result, out QueryExpressionFeatures features)
		{
			if (node.Kind == ExpressionNodeKind.AggregatableSubQueryExpression)
			{
				result = this.TranslateAggregatableSubQuery((AggregatableSubQueryExpressionNode)node, out features);
				return true;
			}
			result = null;
			features = QueryExpressionFeatures.None;
			return false;
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x000173D8 File Offset: 0x000155D8
		private QueryExpression TranslateSingleValue(SingleValueExpressionNode castNode, out QueryExpressionFeatures features)
		{
			BatchSubQueryExpressionNode batchSubQueryExpressionNode = castNode.Input as BatchSubQueryExpressionNode;
			if (batchSubQueryExpressionNode == null)
			{
				throw BasicQueryExpressionTranslatorBase.SyntaxError(castNode.Input);
			}
			QueryExpression queryExpression = this.TranslateBatchSubQuery(batchSubQueryExpressionNode).SingleValue();
			features = QueryExpressionFeatures.None;
			return queryExpression;
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x00017410 File Offset: 0x00015610
		private QueryExpression TranslateQueryParameterReference(QueryParameterReferenceExpressionNode node, out QueryExpressionFeatures features)
		{
			QueryParameterReferenceExpression queryParameterReferenceExpression;
			if (!this.m_queryParameterMap.TryGetParameterReference(node.Name, out queryParameterReferenceExpression))
			{
				throw BasicQueryExpressionTranslatorBase.TranslationError(TranslationMessagePhrases.InvalidQueryParameterName(node.Name));
			}
			features = QueryExpressionFeatures.None;
			return queryParameterReferenceExpression;
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x0001744C File Offset: 0x0001564C
		protected override QueryExpression TranslateCountRowsFunctionCall(FunctionCallExpressionNode functionCallNode, out QueryExpressionFeatures features)
		{
			QueryExpression queryExpression;
			if (this.TryTranslateCountRowsOfSubQuery(functionCallNode, out queryExpression))
			{
				features = QueryExpressionFeatures.QueryMeasure | QueryExpressionFeatures.RequiresCalculate;
				return queryExpression;
			}
			return base.TranslateCountRowsFunctionCall(functionCallNode, out features);
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x00017474 File Offset: 0x00015674
		private bool TryTranslateCountRowsOfSubQuery(FunctionCallExpressionNode functionCallNode, out QueryExpression queryExpr)
		{
			queryExpr = null;
			ReadOnlyCollection<ExpressionNode> arguments = functionCallNode.Arguments;
			if (arguments == null || arguments.Count < 1)
			{
				return false;
			}
			BatchSubQueryExpressionNode batchSubQueryExpressionNode = arguments[0] as BatchSubQueryExpressionNode;
			if (batchSubQueryExpressionNode == null)
			{
				return false;
			}
			QueryExpression queryExpression = this.TranslateBatchSubQuery(batchSubQueryExpressionNode);
			queryExpr = queryExpression.CountRows();
			return true;
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x000174BC File Offset: 0x000156BC
		protected QueryExpression TranslateIsEmptyTableFunctionCall(FunctionCallExpressionNode functionCallNode, out QueryExpressionFeatures features)
		{
			BatchSubQueryExpressionNode batchSubQueryExpressionNode = functionCallNode.Arguments[0] as BatchSubQueryExpressionNode;
			QueryExpression queryExpression = this.TranslateBatchSubQuery(batchSubQueryExpressionNode);
			features = QueryExpressionFeatures.QueryMeasure | QueryExpressionFeatures.RequiresCalculate;
			return queryExpression.IsEmpty();
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x000174EC File Offset: 0x000156EC
		private QueryExpression TranslateRemoveGroupings(RemoveGroupingsExpressionNode removeGroupingsNode, out QueryExpressionFeatures features)
		{
			QueryExpression queryExpression = this.Translate(removeGroupingsNode.Expression, out features);
			List<QueryExpression> list = new List<QueryExpression>(removeGroupingsNode.GroupKeysToRemove.Count);
			for (int i = 0; i < removeGroupingsNode.GroupKeysToRemove.Count; i++)
			{
				ResolvedPropertyExpressionNode resolvedPropertyExpressionNode = (ResolvedPropertyExpressionNode)removeGroupingsNode.GroupKeysToRemove[i];
				Microsoft.DataShaping.Contract.RetailAssert(resolvedPropertyExpressionNode.Property != null, "propNode.Property is expected to not be null");
				IConceptualProperty property = resolvedPropertyExpressionNode.Property;
				if (this.m_useConceptualSchema)
				{
					list.Add(property.Entity.AllSelected(property.AsColumn()));
				}
				else
				{
					EntitySet entitySet;
					EdmField correspondingEdmField = this.m_model.GetCorrespondingEdmField(property, out entitySet);
					list.Add(entitySet.AllSelected(correspondingEdmField, null, null));
				}
			}
			features &= ~QueryExpressionFeatures.RequiresCalculate;
			return queryExpression.Calculate(list);
		}

		// Token: 0x0400031B RID: 795
		protected readonly IQueryExpressionGenerator m_expressionGenerator;

		// Token: 0x0400031C RID: 796
		protected readonly GeneratedQueryParameterMap m_queryParameterMap;

		// Token: 0x0400031D RID: 797
		protected readonly DaxCapabilities m_daxCapabilities;

		// Token: 0x0400031E RID: 798
		protected readonly IDataComparer m_dataComparer;
	}
}
