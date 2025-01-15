using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x0200008C RID: 140
	[ImmutableObject(true)]
	internal class ResolvedQueryExpressionValidator : DefaultResolvedQueryExpressionVisitor
	{
		// Token: 0x06000579 RID: 1401 RVA: 0x00014217 File Offset: 0x00012417
		private ResolvedQueryExpressionValidator(DataShapeGenerationErrorContext errorContext, AllowedExpressionContentFlags allowedContent, ExpressionContext expressionContext)
		{
			this._errorContext = errorContext;
			this._allowedContent = allowedContent;
			this._expressionContext = expressionContext;
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00014234 File Offset: 0x00012434
		internal static bool Validate(ResolvedQueryExpression expression, DataShapeGenerationErrorContext errorContext, AllowedExpressionContentFlags allowedContent, ExpressionContext expressionContext)
		{
			HashSet<string> hashSet;
			return ResolvedQueryExpressionValidator.Validate(expression, errorContext, allowedContent, expressionContext, out hashSet);
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x0001424C File Offset: 0x0001244C
		internal static bool Validate(ResolvedQueryExpression expression, DataShapeGenerationErrorContext errorContext, AllowedExpressionContentFlags allowedContent, ExpressionContext expressionContext, out HashSet<string> referencedSubqueries)
		{
			return new ResolvedQueryExpressionValidator(errorContext, allowedContent, expressionContext).Validate(expression, out referencedSubqueries);
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00014260 File Offset: 0x00012460
		private bool Validate(ResolvedQueryExpression expression, out HashSet<string> referencedSubqueries)
		{
			this._currentExpressionTreeDepth = 0;
			this._isValid = true;
			this._referencedSubqueries = new HashSet<string>(StringComparer.Ordinal);
			int count = this._errorContext.Messages.Count;
			this.VisitExpression(expression);
			referencedSubqueries = this._referencedSubqueries;
			return this._isValid;
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x000142B1 File Offset: 0x000124B1
		public override void Visit(ResolvedQueryFilteredEvalExpression expression)
		{
			this._isValid = this.ValidateFilteredEvalExpression(expression);
			if (this._isValid)
			{
				expression.Expression.Accept(this);
			}
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x000142D4 File Offset: 0x000124D4
		public override void Visit(ResolvedQuerySourceRefExpression expression)
		{
			if (this.IsNotAllowed(AllowedExpressionContentFlags.ModelReferences))
			{
				this._errorContext.Register(DataShapeGenerationMessages.InvalidExpression(EngineMessageSeverity.Error, this._expressionContext.OwningQueryName, this._expressionContext.ObjectType, this._expressionContext.ObjectId, DataShapeGenerationMessagePhrases.ModelReferenceNotAllowed()));
				this._isValid = false;
			}
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00014328 File Offset: 0x00012528
		public override void Visit(ResolvedQueryExpressionSourceRefExpression expression)
		{
			if (expression.Expression is ResolvedQuerySubqueryExpression || expression.Expression is ResolvedQueryLetRefExpression)
			{
				if (this.IsNotAllowed(AllowedExpressionContentFlags.SubqueryReferences))
				{
					this._errorContext.Register(DataShapeGenerationMessages.InvalidExpression(EngineMessageSeverity.Error, this._expressionContext.OwningQueryName, this._expressionContext.ObjectType, this._expressionContext.ObjectId, DataShapeGenerationMessagePhrases.SubqueryReferenceNotAllowed()));
					this._isValid = false;
				}
				this._referencedSubqueries.Add(expression.SourceName);
			}
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x000143A8 File Offset: 0x000125A8
		public override void Visit(ResolvedQueryScopedEvalExpression expression)
		{
			if (this.IsNotAllowed(AllowedExpressionContentFlags.ScopedEval))
			{
				this._errorContext.Register(DataShapeGenerationMessages.InvalidExpression(EngineMessageSeverity.Error, this._expressionContext.OwningQueryName, this._expressionContext.ObjectType, this._expressionContext.ObjectId, DataShapeGenerationMessagePhrases.ScopedEvalNotAllowed()));
				this._isValid = false;
			}
			this.VisitExpression(expression.Expression);
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x0001440C File Offset: 0x0001260C
		public override void Visit(ResolvedQueryAggregationExpression expression)
		{
			if (this.IsGroupByAggregateFunction(expression.Function) && this.IsNotAllowed(AllowedExpressionContentFlags.GroupByAggregates))
			{
				this._errorContext.Register(DataShapeGenerationMessages.InvalidExpression(EngineMessageSeverity.Error, this._expressionContext.OwningQueryName, this._expressionContext.ObjectType, this._expressionContext.ObjectId, DataShapeGenerationMessagePhrases.GroupByAggregateNotAllowed()));
				this._isValid = false;
			}
			if (expression.Function == QueryAggregateFunction.Median && this.IsNotAllowed(AllowedExpressionContentFlags.MedianAggregate))
			{
				this._errorContext.Register(DataShapeGenerationMessages.InvalidExpression(EngineMessageSeverity.Error, this._expressionContext.OwningQueryName, this._expressionContext.ObjectType, this._expressionContext.ObjectId, DataShapeGenerationMessagePhrases.MedianNotAllowed()));
				this._isValid = false;
			}
			if (expression.Function == QueryAggregateFunction.SingleValue && (this.IsNotAllowed(AllowedExpressionContentFlags.SingleValueAggregate) || !(expression.Expression is ResolvedQueryColumnReferenceExpression)))
			{
				this._errorContext.Register(DataShapeGenerationMessages.InvalidSingleValueAggregation(EngineMessageSeverity.Error));
				this._isValid = false;
			}
			this.VisitExpression(expression.Expression);
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00014504 File Offset: 0x00012704
		public override void Visit(ResolvedQueryPercentileExpression expression)
		{
			if (this.IsNotAllowed(AllowedExpressionContentFlags.Percentile))
			{
				this._errorContext.Register(DataShapeGenerationMessages.InvalidExpression(EngineMessageSeverity.Error, this._expressionContext.OwningQueryName, this._expressionContext.ObjectType, this._expressionContext.ObjectId, DataShapeGenerationMessagePhrases.PercentileNotAllowed()));
				this._isValid = false;
			}
			this.VisitExpression(expression.Expression);
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00014568 File Offset: 0x00012768
		public override void Visit(ResolvedQueryLiteralExpression expression)
		{
			if (expression.Value == PrimitiveValue.Null)
			{
				return;
			}
			object valueAsObject = expression.Value.GetValueAsObject();
			if (valueAsObject is double)
			{
				double num = (double)valueAsObject;
				if (double.IsNaN(num))
				{
					this._errorContext.Register(DataShapeGenerationMessages.NaNLiteralNotSupported(EngineMessageSeverity.Error));
					this._isValid = false;
				}
			}
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x000145BE File Offset: 0x000127BE
		public override void Visit(ResolvedQuerySparklineDataExpression expression)
		{
			this._errorContext.Register(DataShapeGenerationMessages.InvalidExpression(EngineMessageSeverity.Error, this._expressionContext.OwningQueryName, this._expressionContext.ObjectType, this._expressionContext.ObjectId, DataShapeGenerationMessagePhrases.SparklineDataNotAllowed()));
			this._isValid = false;
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x00014600 File Offset: 0x00012800
		public override void Visit(ResolvedQueryNativeVisualCalculationExpression expression)
		{
			if (this.IsNotAllowed(AllowedExpressionContentFlags.VisualCalculations))
			{
				this._errorContext.Register(DataShapeGenerationMessages.InvalidExpression(EngineMessageSeverity.Error, this._expressionContext.OwningQueryName, this._expressionContext.ObjectType, this._expressionContext.ObjectId, DataShapeGenerationMessagePhrases.NativeVisualCalculationNotAllowed()));
				this._isValid = false;
				return;
			}
			if (this._currentExpressionTreeDepth > 1)
			{
				this._errorContext.Register(DataShapeGenerationMessages.InvalidExpression(EngineMessageSeverity.Error, this._expressionContext.OwningQueryName, this._expressionContext.ObjectType, this._expressionContext.ObjectId, DataShapeGenerationMessagePhrases.NativeVisualCalculationNotAllowed()));
				this._isValid = false;
				return;
			}
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x000146A1 File Offset: 0x000128A1
		protected override void VisitExpression(ResolvedQueryExpression expression)
		{
			this._currentExpressionTreeDepth++;
			base.VisitExpression(expression);
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x000146B8 File Offset: 0x000128B8
		private bool ValidateFilteredEvalExpression(ResolvedQueryFilteredEvalExpression expression)
		{
			if (this.IsNotAllowed(AllowedExpressionContentFlags.FilteredEval))
			{
				this._errorContext.Register(DataShapeGenerationMessages.InvalidFilteredEval(EngineMessageSeverity.Error, DataShapeGenerationMessagePhrases.FilteredEvalExpressionInUnsupportedSQClause()));
				return false;
			}
			if (this._currentExpressionTreeDepth > 1)
			{
				this._errorContext.Register(DataShapeGenerationMessages.InvalidFilteredEval(EngineMessageSeverity.Error, DataShapeGenerationMessagePhrases.FilteredEvalUnsupportedAsInnerExpression()));
				return false;
			}
			if (!ResolvedQueryExpressionValidator.IsValidFilteredEvalMeasure(expression.Expression))
			{
				this._errorContext.Register(DataShapeGenerationMessages.InvalidFilteredEval(EngineMessageSeverity.Error, DataShapeGenerationMessagePhrases.InvalidFilteredEvalMeasure()));
				return false;
			}
			if (!ResolvedQueryExpressionValidator.IsValidFilteredEvalFilter(expression.Filters))
			{
				this._errorContext.Register(DataShapeGenerationMessages.InvalidFilteredEval(EngineMessageSeverity.Error, DataShapeGenerationMessagePhrases.InvalidFilteredEvalFilter()));
				return false;
			}
			return true;
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x00014756 File Offset: 0x00012956
		private static bool IsValidFilteredEvalMeasure(ResolvedQueryExpression expression)
		{
			return expression is ResolvedQueryScopedEvalExpression || expression is ResolvedQueryArithmeticExpression || expression is ResolvedQueryPercentileExpression || expression is ResolvedQueryAggregationExpression || expression is ResolvedQueryMeasureExpression;
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x00014784 File Offset: 0x00012984
		private static bool IsValidFilteredEvalFilter(IReadOnlyList<ResolvedQueryFilter> filters)
		{
			foreach (ResolvedQueryFilter resolvedQueryFilter in filters)
			{
				ResolvedQueryInExpression resolvedQueryInExpression = resolvedQueryFilter.Condition as ResolvedQueryInExpression;
				if (resolvedQueryInExpression != null && resolvedQueryInExpression.HasTable)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x000147E8 File Offset: 0x000129E8
		private bool IsNotAllowed(AllowedExpressionContentFlags flags)
		{
			return !this._allowedContent.HasFlag(flags);
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x00014803 File Offset: 0x00012A03
		private bool IsGroupByAggregateFunction(QueryAggregateFunction aggregateFunction)
		{
			return aggregateFunction == QueryAggregateFunction.Min || aggregateFunction == QueryAggregateFunction.Max || aggregateFunction == QueryAggregateFunction.Sum || aggregateFunction == QueryAggregateFunction.Count || aggregateFunction == QueryAggregateFunction.Avg || aggregateFunction == QueryAggregateFunction.StandardDeviation;
		}

		// Token: 0x040002FA RID: 762
		private readonly DataShapeGenerationErrorContext _errorContext;

		// Token: 0x040002FB RID: 763
		private readonly AllowedExpressionContentFlags _allowedContent;

		// Token: 0x040002FC RID: 764
		private readonly ExpressionContext _expressionContext;

		// Token: 0x040002FD RID: 765
		private bool _isValid;

		// Token: 0x040002FE RID: 766
		private int _currentExpressionTreeDepth;

		// Token: 0x040002FF RID: 767
		private HashSet<string> _referencedSubqueries;
	}
}
