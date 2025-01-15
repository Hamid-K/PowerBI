using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.InfoNav.Utils;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.ExpressionBuilder.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.CommonQueryGeneration;
using Microsoft.ReportingServices.DataShapeQueryTranslation.ExtensionEdm;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.QueryGeneration
{
	// Token: 0x0200007F RID: 127
	internal abstract class QueryExpressionGeneratorBase : IQueryExpressionGenerator
	{
		// Token: 0x0600060A RID: 1546 RVA: 0x000157D0 File Offset: 0x000139D0
		protected QueryExpressionGeneratorBase(QueryGenerationContext context, WritableExpressionTable outputExpressionTable, GeneratedQueryParameterMap queryParameterMap)
		{
			this.m_context = context;
			this.m_outputExpressionTable = outputExpressionTable;
			this.m_queryParameterMap = queryParameterMap;
			this.m_cache = new QueryExpressionGeneratorCache();
			FederatedEntityDataModel model = this.m_context.Model;
			this.m_comparer = EntityDataModelExtensions.GetComparer((model != null) ? model.BaseModel : null, this.m_context.Schema.GetDefaultSchema(), this.m_context.FeatureSwitchProvider);
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x00015840 File Offset: 0x00013A40
		public QueryExpressionContext TranslateNullExpression(Expression expression, ConceptualResultType resultType)
		{
			return new QueryExpressionContext(resultType.ToExpression(ScalarValue.Null), QueryExpressionFeatures.None, false);
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x00015854 File Offset: 0x00013A54
		public bool IsNullLiteralExpression(Expression expression)
		{
			LiteralExpressionNode literalExpressionNode = this.m_context.ExpressionTable.GetNode(expression) as LiteralExpressionNode;
			return literalExpressionNode != null && literalExpressionNode.Value == ScalarValue.Null;
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x00015890 File Offset: 0x00013A90
		public virtual QueryExpressionContext TranslateExpression(ExpressionId expressionId, ExpressionContext expressionContext)
		{
			return this.TranslateExpressionInternal<QueryExpressionContext>(expressionContext, () => this.TranslateSingleExpressionImpl(expressionId, expressionContext, null));
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x000158D0 File Offset: 0x00013AD0
		public QueryExpressionContext TranslateExpression(ExpressionNode expressionNode, ExpressionContext expressionContext)
		{
			return this.TranslateExpressionInternal<QueryExpressionContext>(expressionContext, () => this.TranslateSingleExpressionImpl(expressionNode, expressionContext, null));
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x00015910 File Offset: 0x00013B10
		protected QueryExpressionContext TranslateSingleExpressionImpl(ExpressionId expressionId, ExpressionContext expressionContext, Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal.FilterCondition filter)
		{
			QueryExpressionContext queryExpressionContext;
			if (this.m_cache.TryGetExpression(expressionId, out queryExpressionContext))
			{
				return queryExpressionContext;
			}
			ExpressionNode node = this.m_context.ExpressionTable.GetNode(expressionId);
			queryExpressionContext = this.TranslateSingleExpressionImpl(node, expressionContext, filter);
			this.m_cache.PutExpression(expressionId, queryExpressionContext);
			return queryExpressionContext;
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x0001595C File Offset: 0x00013B5C
		private QueryExpressionContext TranslateSingleExpressionImpl(ExpressionNode expressionNode, ExpressionContext expressionContext, Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal.FilterCondition filter)
		{
			QueryExpressionContext queryExpressionContext = this.TranslateNode(expressionContext, expressionNode);
			QueryExpressionContext queryExpressionContext2 = queryExpressionContext;
			FederatedEntityDataModel model = this.m_context.Model;
			return QueryExpressionGeneratorBase.ApplyFilter(filter, queryExpressionContext2, (model != null) ? model.BaseModel : null, this.m_context.Schema.GetDefaultSchema(), this.m_context.DaxCapabilities, this.m_context.FeatureSwitchProvider, this.m_comparer, this.m_context.CancellationToken);
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x000159CC File Offset: 0x00013BCC
		public virtual QueryExpressionContext TranslateCalculationReference(Calculation calculation)
		{
			ExpressionContext expressionContext = this.CreateExpressionContext(calculation);
			return this.TranslateExpressionInternal<QueryExpressionContext>(expressionContext, () => this.TranslateSingleExpressionImpl(calculation.Value.ExpressionId.Value, expressionContext, null));
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x00015A18 File Offset: 0x00013C18
		public virtual QueryExpressionContext TranslateGroupKeyReference(Microsoft.DataShaping.InternalContracts.DataShapeQuery.GroupKey groupKey)
		{
			ExpressionContext expressionContext = new ExpressionContext(this.m_context.ErrorContext, ObjectType.GroupKey, groupKey.Id, "Value");
			return this.TranslateExpressionInternal<QueryExpressionContext>(expressionContext, () => this.TranslateSingleExpressionImpl(groupKey.Value.ExpressionId.Value, expressionContext, null));
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x00015A7C File Offset: 0x00013C7C
		public virtual QueryExpressionContext TranslateSortKeyReference(SortKey sortKey)
		{
			ExpressionContext expressionContext = new ExpressionContext(this.m_context.ErrorContext, ObjectType.GroupKey, sortKey.Id, "Value");
			return this.TranslateExpressionInternal<QueryExpressionContext>(expressionContext, () => this.TranslateSingleExpressionImpl(sortKey.Value.ExpressionId.Value, expressionContext, null));
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x00015ADD File Offset: 0x00013CDD
		protected ExpressionContext CreateExpressionContext(IContextItem contextItem)
		{
			return new ExpressionContext(this.m_context.ErrorContext, contextItem.ObjectType, contextItem.Id, "Value");
		}

		// Token: 0x06000615 RID: 1557
		public abstract List<KeyValuePair<ExpressionId, QueryExpressionContext>> TranslateCalculation(Calculation calculation);

		// Token: 0x06000616 RID: 1558
		public abstract QueryExpressionContext TranslateFilterExpression(ExpressionId expressionId, ExpressionContext expressionContext);

		// Token: 0x06000617 RID: 1559 RVA: 0x00015B00 File Offset: 0x00013D00
		protected TResult TranslateExpressionInternal<TResult>(ExpressionContext expressionContext, Func<TResult> workerFunc)
		{
			TResult tresult;
			try
			{
				tresult = workerFunc();
			}
			catch (QueryGenerationException)
			{
				throw;
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
				expressionContext.ErrorContext.Register(TranslationMessages.InvalidExpression(EngineMessageSeverity.Error, expressionContext.ObjectType, expressionContext.ObjectId, expressionContext.PropertyName, TranslationMessagePhrases.GeneralQueryError(ex.Message)));
				throw new QueryGenerationException("Expression translation failed");
			}
			return tresult;
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x00015B8C File Offset: 0x00013D8C
		protected static QueryExpressionContext ApplyFilter(Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal.FilterCondition filter, QueryExpressionContext queryExprContext, IConceptualModel model, IConceptualSchema schema, DaxCapabilities daxCapabilities, IFeatureSwitchProvider featureSwitchProvider, IDataComparer comparer, CancellationToken cancellationToken)
		{
			if (filter != null)
			{
				queryExprContext = new QueryExpressionContext(queryExprContext.QueryExpression.Calculate(model, schema, new Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal.FilterCondition[] { filter }, daxCapabilities, featureSwitchProvider, comparer, cancellationToken), queryExprContext.QueryExpressionFeatures, queryExprContext.CalculateInMeasureContext);
			}
			return queryExprContext;
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x00015BC4 File Offset: 0x00013DC4
		public bool ValidateLiteralType(QueryExpression queryExpr, ScalarValue value, ExpressionContext expressionContext, EngineMessageSeverity severity)
		{
			if (QueryExpressionGeneratorBase.HasMatchingLiteralType(queryExpr.ConceptualResultType, value))
			{
				return true;
			}
			string text = ((value.Value == null) ? "Null" : value.Value.GetType().Name);
			string text2 = queryExpr.ConceptualResultType.ToString();
			expressionContext.ErrorContext.Register(TranslationMessages.InvalidLiteralDataType(severity, expressionContext.ObjectType, expressionContext.ObjectId, expressionContext.PropertyName, text, text2.MarkAsModelInfo()));
			return false;
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00015C3C File Offset: 0x00013E3C
		private static bool HasMatchingLiteralType(ConceptualResultType conceptualResultType, ScalarValue value)
		{
			ConceptualPrimitiveResultType conceptualPrimitiveResultType = conceptualResultType as ConceptualPrimitiveResultType;
			return conceptualPrimitiveResultType != null && (value == ScalarValue.Null || value.Type.GetPrimitive() == conceptualPrimitiveResultType);
		}

		// Token: 0x0600061B RID: 1563
		protected abstract QueryExpressionContext TranslateNode(ExpressionContext expressionContext, ExpressionNode expressionNode);

		// Token: 0x0600061C RID: 1564 RVA: 0x00015C73 File Offset: 0x00013E73
		public ExpressionNode GetExpressionNode(ExpressionId expressionId)
		{
			return this.m_context.ExpressionTable.GetNode(expressionId);
		}

		// Token: 0x0400030A RID: 778
		protected readonly QueryGenerationContext m_context;

		// Token: 0x0400030B RID: 779
		protected readonly WritableExpressionTable m_outputExpressionTable;

		// Token: 0x0400030C RID: 780
		protected readonly GeneratedQueryParameterMap m_queryParameterMap;

		// Token: 0x0400030D RID: 781
		protected readonly QueryExpressionGeneratorCache m_cache;

		// Token: 0x0400030E RID: 782
		protected readonly IDataComparer m_comparer;
	}
}
