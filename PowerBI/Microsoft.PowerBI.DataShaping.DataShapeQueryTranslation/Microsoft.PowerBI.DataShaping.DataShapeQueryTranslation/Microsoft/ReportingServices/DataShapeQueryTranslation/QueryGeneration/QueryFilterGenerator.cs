using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Utils;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.ExpressionBuilder.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.QueryGeneration
{
	// Token: 0x02000088 RID: 136
	internal sealed class QueryFilterGenerator : FilterVisitor<Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal.FilterCondition>
	{
		// Token: 0x06000679 RID: 1657 RVA: 0x00017799 File Offset: 0x00015999
		private QueryFilterGenerator(IQueryExpressionGenerator expressionGenerator, TranslationErrorContext errorContext, Identifier parentId, CancellationToken cancellationToken)
			: base(null)
		{
			this.m_expressionGenerator = expressionGenerator;
			this.m_errorContext = errorContext;
			this.m_parentId = parentId;
			this.m_cancellationToken = cancellationToken;
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x000177BF File Offset: 0x000159BF
		internal static Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal.FilterCondition CreateFilter(Microsoft.DataShaping.InternalContracts.DataShapeQuery.FilterCondition filterCondition, IQueryExpressionGenerator expressionGenerator, TranslationErrorContext errorContext, Identifier id, CancellationToken cancellationToken)
		{
			if (filterCondition == null)
			{
				return null;
			}
			return new QueryFilterGenerator(expressionGenerator, errorContext, id, cancellationToken).Visit(filterCondition);
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x000177D8 File Offset: 0x000159D8
		internal override Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal.FilterCondition Visit(Microsoft.DataShaping.InternalContracts.DataShapeQuery.CompoundFilterCondition dsqCondition)
		{
			this.CheckForCancellation();
			List<Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal.FilterCondition> list = this.TranslateCompoundFilterConditions(dsqCondition.Conditions);
			return new Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal.CompoundFilterCondition(this.TranslateCompoundFilterOperator(dsqCondition.Operator), list);
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x0001780C File Offset: 0x00015A0C
		private List<Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal.FilterCondition> TranslateCompoundFilterConditions(List<Microsoft.DataShaping.InternalContracts.DataShapeQuery.FilterCondition> conditions)
		{
			List<Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal.FilterCondition> list = new List<Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal.FilterCondition>(conditions.Count);
			for (int i = 0; i < conditions.Count; i++)
			{
				list.Add(this.Visit(conditions[i]));
			}
			return list;
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x0001784C File Offset: 0x00015A4C
		private Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal.CompoundFilterOperator TranslateCompoundFilterOperator(Candidate<Microsoft.DataShaping.InternalContracts.DataShapeQuery.CompoundFilterOperator> dsqOperator)
		{
			switch (dsqOperator.Value)
			{
			case Microsoft.DataShaping.InternalContracts.DataShapeQuery.CompoundFilterOperator.All:
				return Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal.CompoundFilterOperator.All;
			case Microsoft.DataShaping.InternalContracts.DataShapeQuery.CompoundFilterOperator.Any:
				return Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal.CompoundFilterOperator.Any;
			case Microsoft.DataShaping.InternalContracts.DataShapeQuery.CompoundFilterOperator.NotAll:
				return Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal.CompoundFilterOperator.NotAll;
			case Microsoft.DataShaping.InternalContracts.DataShapeQuery.CompoundFilterOperator.NotAny:
				return Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal.CompoundFilterOperator.NotAny;
			default:
				Contract.RetailFail("Unknown CompoundFilterOperator");
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x00017890 File Offset: 0x00015A90
		internal override Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal.FilterCondition Visit(BinaryFilterCondition dsqCondition)
		{
			this.CheckForCancellation();
			QueryExpression queryExpression = null;
			QueryExpression queryExpression2 = null;
			if (!this.m_expressionGenerator.IsNullLiteralExpression(dsqCondition.LeftExpression))
			{
				queryExpression = this.TranslateExpression(dsqCondition.LeftExpression, dsqCondition.ObjectType, "LeftExpression");
			}
			if (!this.m_expressionGenerator.IsNullLiteralExpression(dsqCondition.RightExpression))
			{
				queryExpression2 = this.TranslateExpression(dsqCondition.RightExpression, dsqCondition.ObjectType, "RightExpression");
			}
			if (queryExpression == null && queryExpression2 == null)
			{
				this.m_errorContext.Register(TranslationMessages.InvalidExpression(EngineMessageSeverity.Error, dsqCondition.ObjectType, this.m_parentId, "Condition", TranslationMessagePhrases.InvalidFilterCondition()));
				throw new QueryGenerationException("Could not generate query expression for filter condition");
			}
			if (queryExpression == null)
			{
				queryExpression = this.m_expressionGenerator.TranslateNullExpression(dsqCondition.LeftExpression, queryExpression2.ConceptualResultType).QueryExpression;
			}
			else if (queryExpression2 == null)
			{
				queryExpression2 = this.m_expressionGenerator.TranslateNullExpression(dsqCondition.RightExpression, queryExpression.ConceptualResultType).QueryExpression;
			}
			queryExpression2 = QueryExpressionBuilder.ResolveCrossTypeComparisons(queryExpression, queryExpression2);
			SimpleFilterCondition simpleFilterCondition = new SimpleFilterCondition(queryExpression, dsqCondition.Not.GetValueOrDefault<bool>(), this.TranslateBinaryFilterOperator(dsqCondition.Operator), queryExpression2);
			ResolvedPropertyExpressionNode resolvedPropertyExpressionNode = this.TryGetResolvedPropertyExpression(dsqCondition.LeftExpression);
			this.ValidateCondition(simpleFilterCondition, dsqCondition.ObjectType, resolvedPropertyExpressionNode);
			return simpleFilterCondition;
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x000179BC File Offset: 0x00015BBC
		private ResolvedPropertyExpressionNode TryGetResolvedPropertyExpression(Expression expression)
		{
			ResolvedPropertyExpressionNode resolvedPropertyExpressionNode = null;
			if (expression.ExpressionId != null)
			{
				resolvedPropertyExpressionNode = this.m_expressionGenerator.GetExpressionNode(expression.ExpressionId.Value) as ResolvedPropertyExpressionNode;
			}
			return resolvedPropertyExpressionNode;
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x000179FC File Offset: 0x00015BFC
		private Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal.FilterCondition ValidateCondition(SimpleFilterCondition condition, ObjectType conditionType, ResolvedPropertyExpressionNode filteredItemExpression)
		{
			try
			{
				condition.ToPredicate();
			}
			catch (ArgumentException)
			{
				IContainsTelemetryMarkup containsTelemetryMarkup = null;
				if (filteredItemExpression != null)
				{
					containsTelemetryMarkup = TranslationMessageUtils.GetPropertyNameForError(filteredItemExpression);
				}
				this.m_errorContext.Register(TranslationMessages.InvalidFilterConditionIncompatibleDataType(EngineMessageSeverity.Error, conditionType, this.m_parentId, "Condition", containsTelemetryMarkup));
				throw new QueryGenerationException("Could not generate query expression for filter condition");
			}
			return condition;
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x00017A5C File Offset: 0x00015C5C
		private FilterOperator TranslateBinaryFilterOperator(Candidate<BinaryFilterOperator> dsqOperator)
		{
			switch (dsqOperator.Value)
			{
			case BinaryFilterOperator.Equal:
				return FilterOperator.Equal;
			case BinaryFilterOperator.GreaterThan:
				return FilterOperator.GreaterThan;
			case BinaryFilterOperator.GreaterThanOrEqual:
				return FilterOperator.GreaterThanOrEqual;
			case BinaryFilterOperator.LessThanOrEqual:
				return FilterOperator.LessThanOrEqual;
			case BinaryFilterOperator.LessThan:
				return FilterOperator.LessThan;
			case BinaryFilterOperator.Contains:
				return FilterOperator.Contains;
			case BinaryFilterOperator.StartsWith:
				return FilterOperator.StartsWith;
			case BinaryFilterOperator.DateTimeEqualToSecond:
				return FilterOperator.DateTimeEqualToSecond;
			case BinaryFilterOperator.EndsWith:
				return FilterOperator.EndsWith;
			case BinaryFilterOperator.EqualIdentity:
				return FilterOperator.EqualIdentity;
			default:
				Contract.RetailFail("Unknown BinaryFilterOperator value.");
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x00017AC8 File Offset: 0x00015CC8
		internal override Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal.FilterCondition Visit(UnaryFilterCondition dsqCondition)
		{
			this.CheckForCancellation();
			SimpleFilterCondition simpleFilterCondition = new SimpleFilterCondition(this.TranslateExpression(dsqCondition.Expression, dsqCondition.ObjectType, "Value"), dsqCondition.Not.GetValueOrDefault<bool>());
			ResolvedPropertyExpressionNode resolvedPropertyExpressionNode = this.TryGetResolvedPropertyExpression(dsqCondition.Expression);
			this.ValidateCondition(simpleFilterCondition, dsqCondition.ObjectType, resolvedPropertyExpressionNode);
			return simpleFilterCondition;
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x00017B20 File Offset: 0x00015D20
		private QueryExpression TranslateExpression(Expression expression, ObjectType objectType, string propertyName)
		{
			return this.m_expressionGenerator.TranslateFilterExpression(expression.ExpressionId.Value, new ExpressionContext(this.m_errorContext, objectType, this.m_parentId, propertyName)).QueryExpression;
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x00017B60 File Offset: 0x00015D60
		internal override Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal.FilterCondition Visit(InFilterCondition dsqCondition)
		{
			this.CheckForCancellation();
			QueryExpression queryExpression;
			if (dsqCondition.HasValues)
			{
				queryExpression = this.TranslateInValues(dsqCondition);
			}
			else
			{
				queryExpression = this.TranslateInTable(dsqCondition);
			}
			SimpleFilterCondition simpleFilterCondition = new SimpleFilterCondition(queryExpression, false);
			this.ValidateCondition(simpleFilterCondition, dsqCondition.ObjectType, null);
			return simpleFilterCondition;
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x00017BA8 File Offset: 0x00015DA8
		private QueryInExpression TranslateInValues(InFilterCondition dsqCondition)
		{
			List<QueryExpression> list = this.TranslateExpressionList(dsqCondition.Expressions, dsqCondition.ObjectType, "Expressions");
			List<IReadOnlyList<QueryExpression>> list2 = this.TranslateInFilterValues(dsqCondition, list);
			return list.In(list2, dsqCondition.IdentityComparison);
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x00017BE4 File Offset: 0x00015DE4
		private List<QueryExpression> TranslateExpressionList(List<Expression> dsqExprs, ObjectType objectType, string propertyName)
		{
			List<QueryExpression> list = new List<QueryExpression>(dsqExprs.Count);
			for (int i = 0; i < dsqExprs.Count; i++)
			{
				list.Add(this.TranslateExpression(dsqExprs[i], objectType, propertyName));
			}
			return list;
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x00017C24 File Offset: 0x00015E24
		private List<IReadOnlyList<QueryExpression>> TranslateInFilterValues(InFilterCondition dsqCondition, IReadOnlyList<QueryExpression> queryExprs)
		{
			List<List<Expression>> values = dsqCondition.Values;
			List<IReadOnlyList<QueryExpression>> list = new List<IReadOnlyList<QueryExpression>>(values.Count);
			for (int i = 0; i < values.Count; i++)
			{
				List<Expression> list2 = values[i];
				List<QueryExpression> list3 = new List<QueryExpression>(list2.Count);
				for (int j = 0; j < list2.Count; j++)
				{
					QueryExpression queryExpression = queryExprs[j];
					Expression expression = list2[j];
					QueryExpression queryExpression2;
					if (this.m_expressionGenerator.IsNullLiteralExpression(expression))
					{
						queryExpression2 = this.m_expressionGenerator.TranslateNullExpression(expression, queryExpression.ConceptualResultType).QueryExpression;
					}
					else
					{
						queryExpression2 = this.TranslateExpression(list2[j], dsqCondition.ObjectType, "Values");
					}
					list3.Add(queryExpression2);
				}
				list.Add(list3);
			}
			return list;
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x00017CF4 File Offset: 0x00015EF4
		private QueryInTableExpression TranslateInTable(InFilterCondition dsqCondition)
		{
			QueryExpression queryExpression = this.TranslateExpressionListAsTuple(dsqCondition.Expressions, dsqCondition.ObjectType, "Expressions");
			QueryExpression queryExpression2 = this.TranslateExpression(dsqCondition.Table, dsqCondition.ObjectType, "Table");
			return queryExpression.InTable(queryExpression2);
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x00017D38 File Offset: 0x00015F38
		private QueryExpression TranslateExpressionListAsTuple(List<Expression> dsqExprs, ObjectType objectType, string propertyName)
		{
			if (dsqExprs.Count == 1)
			{
				return this.TranslateExpression(dsqExprs[0], objectType, propertyName);
			}
			List<QueryExpression> list = new List<QueryExpression>(dsqExprs.Count);
			List<string> list2 = new List<string>(dsqExprs.Count);
			for (int i = 0; i < dsqExprs.Count; i++)
			{
				list.Add(this.TranslateExpression(dsqExprs[i], objectType, propertyName));
				list2.Add(i.ToString(CultureInfo.InvariantCulture));
			}
			return QueryExpressionBuilder.Tuple(list, list2);
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x00017DB8 File Offset: 0x00015FB8
		private void CheckForCancellation()
		{
			this.m_cancellationToken.ThrowIfCancellationRequested();
		}

		// Token: 0x04000326 RID: 806
		private readonly IQueryExpressionGenerator m_expressionGenerator;

		// Token: 0x04000327 RID: 807
		private readonly TranslationErrorContext m_errorContext;

		// Token: 0x04000328 RID: 808
		private readonly Identifier m_parentId;

		// Token: 0x04000329 RID: 809
		private readonly CancellationToken m_cancellationToken;
	}
}
