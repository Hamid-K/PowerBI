using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.ExpressionAnalysis;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation
{
	// Token: 0x02000049 RID: 73
	internal sealed class FilterExpressionCollector : FilterExpressionVisitor
	{
		// Token: 0x06000305 RID: 773 RVA: 0x00008F78 File Offset: 0x00007178
		private FilterExpressionCollector(ExpressionTable expressionTable, Identifier filterTargetId, TranslationErrorContext errorContext, DataShape containingDataShape, ScopeTree scopeTree)
			: base(null)
		{
			this.m_expressionTable = expressionTable;
			this.m_filterTargetId = filterTargetId;
			this.m_errorContext = errorContext;
			this.m_containingDataShape = containingDataShape;
			this.m_scopeTree = scopeTree;
			this.m_measureExpressions = new List<FilterExpressionInfo>(1);
			this.m_nonMeasureModelExpressions = new List<FilterExpressionInfo>(1);
			this.m_subqueryReferenceExpressions = new List<FilterExpressionInfo>(1);
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00008FD8 File Offset: 0x000071D8
		public static List<FilterExpressionInfo> CollectMeasureExpressions(Filter filter, ExpressionTable expressionTable, TranslationErrorContext errorContext)
		{
			Identifier id = filter.Target.GetResolvedScope(expressionTable).Id;
			FilterExpressionCollector filterExpressionCollector = new FilterExpressionCollector(expressionTable, id, errorContext, null, null);
			filterExpressionCollector.Visit(filter);
			return filterExpressionCollector.m_measureExpressions;
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000900E File Offset: 0x0000720E
		public static List<FilterExpressionInfo> CollectNonMeasureModelExpressions(FilterCondition filter, Identifier ownerId, ExpressionTable expressionTable, TranslationErrorContext errorContext)
		{
			FilterExpressionCollector filterExpressionCollector = new FilterExpressionCollector(expressionTable, ownerId, errorContext, null, null);
			filterExpressionCollector.Visit(filter);
			return filterExpressionCollector.m_nonMeasureModelExpressions;
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00009027 File Offset: 0x00007227
		public static List<FilterExpressionInfo> CollectSubqueryReferenceExpressions(FilterCondition filter, Identifier ownerId, ExpressionTable expressionTable, TranslationErrorContext errorContext, DataShape containingDataShape, ScopeTree scopeTree)
		{
			FilterExpressionCollector filterExpressionCollector = new FilterExpressionCollector(expressionTable, ownerId, errorContext, containingDataShape, scopeTree);
			filterExpressionCollector.Visit(filter);
			return filterExpressionCollector.m_subqueryReferenceExpressions;
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00009044 File Offset: 0x00007244
		internal override void VisitExpression(Expression expression, FilterCondition owner, string propertyName)
		{
			ExpressionNode node = this.m_expressionTable.GetNode(expression);
			if (MeasureAnalyzer.IsMeasure(node))
			{
				this.AddToCollection(this.m_measureExpressions, expression, owner, propertyName, null);
				return;
			}
			if (ModelReferenceAnalyzer.ContainsModelReference(node))
			{
				this.AddToCollection(this.m_nonMeasureModelExpressions, expression, owner, propertyName, null);
				return;
			}
			DataShape dataShape;
			IReadOnlyList<Calculation> readOnlyList;
			if (this.m_containingDataShape != null && SubqueryCalculationReferenceAnalyzer.TryGetReferencedSubqueryDataShape(node, this.m_containingDataShape, this.m_scopeTree, out dataShape, out readOnlyList))
			{
				this.AddToCollection(this.m_subqueryReferenceExpressions, expression, owner, propertyName, readOnlyList);
			}
		}

		// Token: 0x0600030A RID: 778 RVA: 0x000090C0 File Offset: 0x000072C0
		private void AddToCollection(List<FilterExpressionInfo> collection, Expression expression, FilterCondition owner, string propertyName, IReadOnlyList<Calculation> referencedCalculations)
		{
			Identifier identifier = owner.Id ?? this.m_filterTargetId;
			ExpressionContext expressionContext = new ExpressionContext(this.m_errorContext, owner.ObjectType, identifier, propertyName);
			collection.Add(new FilterExpressionInfo(expression, expressionContext, referencedCalculations));
		}

		// Token: 0x040000CF RID: 207
		private readonly ExpressionTable m_expressionTable;

		// Token: 0x040000D0 RID: 208
		private readonly Identifier m_filterTargetId;

		// Token: 0x040000D1 RID: 209
		private readonly TranslationErrorContext m_errorContext;

		// Token: 0x040000D2 RID: 210
		private readonly List<FilterExpressionInfo> m_measureExpressions;

		// Token: 0x040000D3 RID: 211
		private readonly List<FilterExpressionInfo> m_nonMeasureModelExpressions;

		// Token: 0x040000D4 RID: 212
		private readonly List<FilterExpressionInfo> m_subqueryReferenceExpressions;

		// Token: 0x040000D5 RID: 213
		private readonly DataShape m_containingDataShape;

		// Token: 0x040000D6 RID: 214
		private readonly ScopeTree m_scopeTree;
	}
}
