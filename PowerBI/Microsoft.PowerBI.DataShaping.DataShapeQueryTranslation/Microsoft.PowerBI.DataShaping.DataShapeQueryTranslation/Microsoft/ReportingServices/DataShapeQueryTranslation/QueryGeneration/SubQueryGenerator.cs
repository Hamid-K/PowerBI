using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.QueryGeneration
{
	// Token: 0x02000096 RID: 150
	internal sealed class SubQueryGenerator : ISubQueryGenerator
	{
		// Token: 0x06000705 RID: 1797 RVA: 0x0001A698 File Offset: 0x00018898
		internal SubQueryGenerator(QueryGenerationContext context, DefaultContextManager defaultContextManager)
		{
			this.m_context = context;
			this.m_existingSubQueries = new List<SubQuery>();
			this.m_defaultContextManager = defaultContextManager;
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x0001A6BC File Offset: 0x000188BC
		public SubQuery GenerateAggregatableSubQuery(ExpressionId targetExpressionId, ExpressionContext expressionContext, DataSetPlan dataSetPlan)
		{
			SubQuery subQuery;
			if (this.TryGetFromCache(dataSetPlan, null, new ExpressionId?(targetExpressionId), out subQuery))
			{
				return subQuery;
			}
			QueryGenerationResult queryGenerationResult = this.GenerateSubQuery(dataSetPlan);
			string targetFieldName = SubQueryGenerator.GetTargetFieldName(targetExpressionId, expressionContext, queryGenerationResult);
			QueryExpression queryExpression = this.GenerateSubQueryExpression(expressionContext, queryGenerationResult, targetFieldName);
			return this.CreateSubquery(targetFieldName, dataSetPlan, queryGenerationResult.QueryDefinition.Declarations, queryExpression, new ExpressionId?(targetExpressionId));
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x0001A714 File Offset: 0x00018914
		public SubQuery GenerateTableSubQuery(string subqueryName, DataSetPlan dataSetPlan)
		{
			SubQuery subQuery;
			if (this.TryGetFromCache(dataSetPlan, subqueryName, null, out subQuery))
			{
				return subQuery;
			}
			QueryGenerationResult queryGenerationResult = this.GenerateSubQuery(dataSetPlan);
			QueryExpression queryExpression = queryGenerationResult.ToSubQueryExpression(this.m_context.FeatureSwitchProvider, true, this.m_context.CancellationToken, false);
			return this.CreateSubquery(subqueryName, dataSetPlan, queryGenerationResult.QueryDefinition.Declarations, queryExpression, null);
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x0001A77C File Offset: 0x0001897C
		private SubQuery CreateSubquery(string subqueryName, DataSetPlan dataSetPlan, IReadOnlyList<QueryBaseDeclarationExpression> declarations, QueryExpression subQueryExpression, ExpressionId? targetExpressionId)
		{
			SubQuery subQuery = new SubQuery(dataSetPlan, subQueryExpression, subqueryName, declarations, targetExpressionId);
			this.m_existingSubQueries.Add(subQuery);
			return subQuery;
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x0001A7A4 File Offset: 0x000189A4
		private bool TryGetFromCache(DataSetPlan dataSetPlan, string subqueryName, ExpressionId? targetExpressionId, out SubQuery subquery)
		{
			foreach (SubQuery subQuery in this.m_existingSubQueries)
			{
				if (subQuery.Matches(subqueryName, targetExpressionId, dataSetPlan))
				{
					subquery = subQuery;
					return true;
				}
			}
			subquery = null;
			return false;
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x0001A80C File Offset: 0x00018A0C
		private static string GetTargetFieldName(ExpressionId targetExpressionId, ExpressionContext expressionContext, QueryGenerationResult queryGenResult)
		{
			DataSetFieldReferenceExpressionNode dataSetFieldReferenceExpressionNode = queryGenResult.ExpressionTable.GetNode(targetExpressionId) as DataSetFieldReferenceExpressionNode;
			if (dataSetFieldReferenceExpressionNode == null)
			{
				expressionContext.ErrorContext.Register(TranslationMessages.InvalidExpression(EngineMessageSeverity.Error, expressionContext.ObjectType, expressionContext.ObjectId, expressionContext.PropertyName, TranslationMessagePhrases.InvalidSubQueryTarget()));
				throw new QueryGenerationException("Subquery over composite expression");
			}
			return dataSetFieldReferenceExpressionNode.FieldName;
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x0001A865 File Offset: 0x00018A65
		private QueryGenerationResult GenerateSubQuery(DataSetPlan dataSetPlan)
		{
			QueryGenerationResult queryGenerationResult = QueryGenerator.GenerateSubQuery(this.m_context, dataSetPlan, this.m_defaultContextManager);
			if (queryGenerationResult == null)
			{
				throw new QueryGenerationException("Could not generate subquery definition");
			}
			return queryGenerationResult;
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x0001A888 File Offset: 0x00018A88
		private QueryExpression GenerateSubQueryExpression(ExpressionContext expressionContext, QueryGenerationResult queryGenResult, string targetFieldName)
		{
			try
			{
				QueryDefinition queryDefinition = queryGenResult.QueryDefinition;
				Measure measure;
				if (queryDefinition.Measures.TryGetItem(targetFieldName, out measure))
				{
					return RangeQueryFactory.MeasureRangeQueryItem.CreateAggregatableSubQueryExpression(queryDefinition, this.m_context.FeatureSwitchProvider, measure, this.m_context.CancellationToken);
				}
				Group group;
				if (queryDefinition.Groups.TryGetItem(targetFieldName, out group))
				{
					return RangeQueryFactory.GroupRangeQueryItem.CreateAggregatableSubQueryExpression(queryDefinition, this.m_context.FeatureSwitchProvider, group, this.m_context.CancellationToken);
				}
				if (this.TryGetGroupByGroupDetail(queryDefinition, targetFieldName, out group))
				{
					return RangeQueryFactory.GroupRangeQueryItem.CreateAggregatableSubQueryExpression(queryDefinition, this.m_context.FeatureSwitchProvider, group, targetFieldName, this.m_context.CancellationToken);
				}
				expressionContext.ErrorContext.Register(TranslationMessages.InvalidExpression(EngineMessageSeverity.Error, expressionContext.ObjectType, expressionContext.ObjectId, expressionContext.PropertyName, TranslationMessagePhrases.InvalidSubQueryTarget()));
			}
			catch (OperationCanceledException)
			{
				throw;
			}
			catch (Exception ex)
			{
				if (ErrorUtils.IsStoppingException(ex))
				{
					throw;
				}
				this.m_context.ErrorContext.Register(TranslationMessages.UnexpectedQueryGenerationError(EngineMessageSeverity.Error, this.m_context.DataShape.ObjectType, this.m_context.DataShape.Id, "Value", queryGenResult.DataSetPlan.Name, ex.Message));
			}
			throw new QueryGenerationException("Could not generate subquery expression");
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x0001A9E4 File Offset: 0x00018BE4
		private bool TryGetGroupByGroupDetail(QueryDefinition queryDefinition, string targetFieldName, out Group group)
		{
			GroupDetail groupDetail;
			IEnumerable<Group> enumerable = queryDefinition.Groups.Where((Group g) => g.Details.TryGetItem(targetFieldName, out groupDetail));
			group = enumerable.SingleOrDefault<Group>();
			return group != null;
		}

		// Token: 0x04000369 RID: 873
		private readonly QueryGenerationContext m_context;

		// Token: 0x0400036A RID: 874
		private readonly List<SubQuery> m_existingSubQueries;

		// Token: 0x0400036B RID: 875
		private readonly DefaultContextManager m_defaultContextManager;
	}
}
