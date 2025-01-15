using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000083 RID: 131
	[ImmutableObject(true)]
	internal sealed class DsqExpressionGenerator : DefaultResolvedQueryExpressionVisitor<GeneratedDsqExpression>
	{
		// Token: 0x0600052C RID: 1324 RVA: 0x00012C29 File Offset: 0x00010E29
		internal DsqExpressionGenerator(IIntermediateQueryTransformResolver transformResolver, QuerySourceExpressionReferenceContext sourceRefContext, QueryParameterReferenceContext parameterRefContext, DataShapeGenerationErrorContext errorContext, bool suppressModelGrouping)
		{
			this._transformResolver = transformResolver;
			this._sourceRefContext = sourceRefContext;
			this._parameterRefContext = parameterRefContext;
			this._errorContext = errorContext;
			this._suppressModelGrouping = suppressModelGrouping;
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x00012C56 File Offset: 0x00010E56
		internal bool TryGenerate(ResolvedQueryExpression expr, out GeneratedDsqExpression dsqExpr)
		{
			if (!this.TryGenerateTransformColumnRef(expr, out dsqExpr))
			{
				dsqExpr = this.VisitExpression(expr);
			}
			return dsqExpr.Expression != null;
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x00012C78 File Offset: 0x00010E78
		internal bool TryGetAsTransformColumn(ResolvedQueryExpression expression, out IntermediateQueryTransformTableColumn column)
		{
			ResolvedQueryTransformTableColumnExpression resolvedQueryTransformTableColumnExpression = expression as ResolvedQueryTransformTableColumnExpression;
			if (resolvedQueryTransformTableColumnExpression == null)
			{
				column = null;
				return false;
			}
			if (!this._transformResolver.TryResolveColumn(resolvedQueryTransformTableColumnExpression.Column, out column))
			{
				this._errorContext.Register(DataShapeGenerationMessages.InvalidTransformColumnReference(EngineMessageSeverity.Error, resolvedQueryTransformTableColumnExpression.Column.Name));
				column = null;
				return false;
			}
			return true;
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x00012CD0 File Offset: 0x00010ED0
		private bool TryGenerateTransformColumnRef(ResolvedQueryExpression expression, out GeneratedDsqExpression result)
		{
			ResolvedQueryTransformTableColumnExpression resolvedQueryTransformTableColumnExpression = expression as ResolvedQueryTransformTableColumnExpression;
			IntermediateQueryTransformTableColumn intermediateQueryTransformTableColumn;
			if (resolvedQueryTransformTableColumnExpression == null || !this._transformResolver.TryResolveColumn(resolvedQueryTransformTableColumnExpression.Column, out intermediateQueryTransformTableColumn))
			{
				result = default(GeneratedDsqExpression);
				return false;
			}
			bool? flag = DsqExpressionGenerator.IsScalar(intermediateQueryTransformTableColumn.UnderlyingConceptualColumn);
			ExpressionContent expressionContent = ((intermediateQueryTransformTableColumn.UnderlyingConceptualColumn == null) ? ExpressionContent.None : ExpressionContent.ModelReference);
			result = new GeneratedDsqExpression(intermediateQueryTransformTableColumn.DsqExpression(), intermediateQueryTransformTableColumn.ActAs == TransformTableColumnActAs.Measure, null, flag, expressionContent);
			return true;
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x00012D44 File Offset: 0x00010F44
		public override GeneratedDsqExpression Visit(ResolvedQueryArithmeticExpression expression)
		{
			GeneratedDsqExpression generatedDsqExpression = this.VisitExpression(expression.Left);
			if (generatedDsqExpression.Expression == null)
			{
				return default(GeneratedDsqExpression);
			}
			GeneratedDsqExpression generatedDsqExpression2 = this.VisitExpression(expression.Right);
			if (generatedDsqExpression2.Expression == null)
			{
				return default(GeneratedDsqExpression);
			}
			ExpressionNode expressionNode;
			switch (expression.Operator)
			{
			case QueryArithmeticOperatorKind.Add:
				expressionNode = generatedDsqExpression.Expression.Add(generatedDsqExpression2.Expression);
				break;
			case QueryArithmeticOperatorKind.Subtract:
				expressionNode = generatedDsqExpression.Expression.Subtract(generatedDsqExpression2.Expression);
				break;
			case QueryArithmeticOperatorKind.Multiply:
				expressionNode = generatedDsqExpression.Expression.Multiply(generatedDsqExpression2.Expression);
				break;
			case QueryArithmeticOperatorKind.Divide:
				expressionNode = generatedDsqExpression.Expression.Divide(generatedDsqExpression2.Expression);
				break;
			default:
				throw Contract.Except("Unhandled arithmetic operator: " + expression.Operator.ToString());
			}
			bool? flag = DsqExpressionGenerator.IsScalarArithmetic(generatedDsqExpression.IsScalar, generatedDsqExpression2.IsScalar);
			return new GeneratedDsqExpression(expressionNode, generatedDsqExpression.HasAggregate || generatedDsqExpression2.HasAggregate, null, flag, generatedDsqExpression.ExpressionContent | generatedDsqExpression2.ExpressionContent);
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x00012E60 File Offset: 0x00011060
		public override GeneratedDsqExpression Visit(ResolvedQueryScopedEvalExpression expression)
		{
			GeneratedDsqExpression generatedDsqExpression = this.VisitExpression(expression.Expression);
			if (generatedDsqExpression.Expression == null)
			{
				return default(GeneratedDsqExpression);
			}
			HashSet<IConceptualColumn> hashSet = new HashSet<IConceptualColumn>();
			List<ExpressionNode> list = new List<ExpressionNode>(hashSet.Count);
			foreach (ResolvedQueryColumnExpression resolvedQueryColumnExpression in expression.Scope.Cast<ResolvedQueryColumnExpression>())
			{
				foreach (IConceptualColumn conceptualColumn in resolvedQueryColumnExpression.Column.Grouping.QueryGroupColumns)
				{
					if (hashSet.Add(conceptualColumn))
					{
						list.Add(conceptualColumn.DsqExpression());
					}
				}
			}
			return new GeneratedDsqExpression(ExpressionNodeBuilder.Evaluate(FunctionUsageKind.Unassigned, new ExpressionNode[]
			{
				generatedDsqExpression.Expression,
				ExpressionNodeBuilder.Scope(FunctionUsageKind.Unassigned, list.ToArray())
			}), generatedDsqExpression.HasAggregate, null, generatedDsqExpression.IsScalar, generatedDsqExpression.ExpressionContent | ExpressionContent.ScopedEval);
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x00012F7C File Offset: 0x0001117C
		public override GeneratedDsqExpression Visit(ResolvedQueryFilteredEvalExpression expression)
		{
			GeneratedDsqExpression generatedDsqExpression = this.VisitExpression(expression.Expression);
			if (generatedDsqExpression.Expression == null)
			{
				return default(GeneratedDsqExpression);
			}
			return new GeneratedDsqExpression(generatedDsqExpression.Expression, generatedDsqExpression.HasAggregate, expression.Filters, generatedDsqExpression.IsScalar, generatedDsqExpression.ExpressionContent);
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x00012FCC File Offset: 0x000111CC
		public override GeneratedDsqExpression Visit(ResolvedQueryAggregationExpression expression)
		{
			GeneratedDsqExpression generatedDsqExpression = this.VisitExpression(expression.Expression);
			ExpressionNode expression2 = generatedDsqExpression.Expression;
			if (expression2 == null)
			{
				return default(GeneratedDsqExpression);
			}
			bool flag = expression.Expression is ResolvedQuerySourceRefExpression;
			string dsqFunctionName = DsqExpressionUtils.GetDsqFunctionName(expression, this._suppressModelGrouping);
			if (flag && dsqFunctionName == "CountRows")
			{
				return new GeneratedDsqExpression(ExpressionNodeBuilder.CountRows(FunctionUsageKind.Unassigned, new ExpressionNode[]
				{
					expression2,
					ExpressionNodeBuilder.Literal(true)
				}), true, null, new bool?(true), generatedDsqExpression.ExpressionContent);
			}
			bool? flag2 = DsqExpressionGenerator.IsScalarAggregate(expression.Function, generatedDsqExpression.IsScalar);
			return new GeneratedDsqExpression(ExpressionNodeBuilder.Function(dsqFunctionName, FunctionUsageKind.Unassigned, new ExpressionNode[] { expression2 }), true, null, flag2, generatedDsqExpression.ExpressionContent);
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x0001308C File Offset: 0x0001128C
		public override GeneratedDsqExpression Visit(ResolvedQueryPercentileExpression expression)
		{
			GeneratedDsqExpression generatedDsqExpression = this.VisitExpression(expression.Expression);
			if (generatedDsqExpression.Expression == null)
			{
				return default(GeneratedDsqExpression);
			}
			return new GeneratedDsqExpression(expression.Exclusive ? generatedDsqExpression.Expression.PercentileExc(expression.K, FunctionUsageKind.Unassigned) : generatedDsqExpression.Expression.PercentileInc(expression.K, FunctionUsageKind.Unassigned), true, null, new bool?(true), generatedDsqExpression.ExpressionContent);
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x000130F9 File Offset: 0x000112F9
		public override GeneratedDsqExpression Visit(ResolvedQuerySourceRefExpression expression)
		{
			return new GeneratedDsqExpression(expression.SourceEntity.DsqExpression(), false, null, new bool?(false), ExpressionContent.ModelReference);
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00013114 File Offset: 0x00011314
		public override GeneratedDsqExpression Visit(ResolvedQueryColumnExpression expression)
		{
			GeneratedDsqExpression generatedDsqExpression = this.VisitExpression(expression.Expression);
			if (generatedDsqExpression.Expression == null)
			{
				return default(GeneratedDsqExpression);
			}
			bool? flag = DsqExpressionGenerator.IsScalar(expression.Column);
			return new GeneratedDsqExpression(expression.Column.DsqExpression(), false, null, flag, generatedDsqExpression.ExpressionContent);
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x00013168 File Offset: 0x00011368
		public override GeneratedDsqExpression Visit(ResolvedQueryColumnReferenceExpression expression)
		{
			IntermediateTableSchemaColumn intermediateTableSchemaColumn;
			if (!this._sourceRefContext.TryGetColumnInSource(expression, this._errorContext, out intermediateTableSchemaColumn))
			{
				return default(GeneratedDsqExpression);
			}
			return new GeneratedDsqExpression(intermediateTableSchemaColumn.ValueCalculationId.StructureReference(), false, null, null, ExpressionContent.SubqueryReference);
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x000131B4 File Offset: 0x000113B4
		public override GeneratedDsqExpression Visit(ResolvedQueryMeasureExpression expression)
		{
			bool? flag = DsqExpressionGenerator.IsScalar(expression.Measure);
			return new GeneratedDsqExpression(expression.Measure.DsqExpression(), true, null, flag, ExpressionContent.ModelReference);
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x000131E4 File Offset: 0x000113E4
		public override GeneratedDsqExpression Visit(ResolvedQueryLiteralExpression expression)
		{
			bool flag = expression.Value.Type == ConceptualPrimitiveType.Null || expression.Value.Type.IsScalar();
			return new GeneratedDsqExpression(ExpressionNodeBuilder.Literal(expression.Value.GetValueAsObject()), false, null, new bool?(flag), ExpressionContent.None);
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00013230 File Offset: 0x00011430
		private string Resolve(ResolvedQueryExpressionSourceRefExpression expression)
		{
			return expression.SourceName;
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00013238 File Offset: 0x00011438
		public override GeneratedDsqExpression Visit(ResolvedQueryNativeFormatExpression expression)
		{
			GeneratedDsqExpression generatedDsqExpression = this.VisitExpression(expression.Expression);
			if (generatedDsqExpression.Expression == null)
			{
				return default(GeneratedDsqExpression);
			}
			return new GeneratedDsqExpression(generatedDsqExpression.Expression.Format(expression.FormatString, FunctionUsageKind.Unassigned), generatedDsqExpression.HasAggregate, null, new bool?(false), generatedDsqExpression.ExpressionContent);
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00013290 File Offset: 0x00011490
		public override GeneratedDsqExpression Visit(ResolvedQueryParameterRefExpression expression)
		{
			IntermediateQueryParameter intermediateQueryParameter;
			if (!this._parameterRefContext.TryGetParameter(expression, out intermediateQueryParameter))
			{
				this._errorContext.Register(DataShapeGenerationMessages.CouldNotResolveQueryParameterReference(EngineMessageSeverity.Error, expression.Declaration.Name));
				return default(GeneratedDsqExpression);
			}
			ConceptualPrimitiveResultType conceptualPrimitiveResultType = intermediateQueryParameter.Type as ConceptualPrimitiveResultType;
			if (conceptualPrimitiveResultType != null)
			{
				bool flag = conceptualPrimitiveResultType.ConceptualDataType.IsScalar();
				return new GeneratedDsqExpression(intermediateQueryParameter.Name.QueryParameter(), false, null, new bool?(flag), ExpressionContent.None);
			}
			this._errorContext.Register(DataShapeGenerationMessages.InvalidQueryParameterReferenceType(EngineMessageSeverity.Error, expression.Declaration.Name));
			return default(GeneratedDsqExpression);
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00013334 File Offset: 0x00011534
		public override GeneratedDsqExpression Visit(ResolvedQueryNativeVisualCalculationExpression expression)
		{
			return new GeneratedDsqExpression(expression.Expression.VisualCalculation(), true, null, null, ExpressionContent.None);
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00013360 File Offset: 0x00011560
		protected override GeneratedDsqExpression VisitUnhandledExpression(ResolvedQueryExpression expression)
		{
			this._errorContext.Register(DataShapeGenerationMessages.UnhandledExpression(EngineMessageSeverity.Warning, expression));
			return default(GeneratedDsqExpression);
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x00013388 File Offset: 0x00011588
		protected override GeneratedDsqExpression VisitExpression(ResolvedQueryExpression expression)
		{
			GeneratedDsqExpression generatedDsqExpression = base.VisitExpression(expression);
			if (generatedDsqExpression.Expression == null && !this._errorContext.HasError)
			{
				this._errorContext.Register(DataShapeGenerationMessages.InvalidOrMalformedExpression(EngineMessageSeverity.Warning, expression));
			}
			return generatedDsqExpression;
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x000133B8 File Offset: 0x000115B8
		private static bool? IsScalar(IConceptualProperty conceptualProperty)
		{
			if (conceptualProperty == null)
			{
				return null;
			}
			return new bool?(conceptualProperty.ConceptualDataType.IsScalar());
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x000133E4 File Offset: 0x000115E4
		private static bool? IsScalarArithmetic(bool? isScalar1, bool? isScalar2)
		{
			if (isScalar1 == null || isScalar2 == null)
			{
				return null;
			}
			return new bool?(isScalar1.Value && isScalar2.Value);
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00013428 File Offset: 0x00011628
		private static bool? IsScalarAggregate(QueryAggregateFunction queryAggregateFunction, bool? isScalarArgument)
		{
			switch (queryAggregateFunction)
			{
			case QueryAggregateFunction.Sum:
			case QueryAggregateFunction.Avg:
			case QueryAggregateFunction.Count:
			case QueryAggregateFunction.CountNonNull:
			case QueryAggregateFunction.Median:
			case QueryAggregateFunction.StandardDeviation:
			case QueryAggregateFunction.Variance:
				return new bool?(true);
			case QueryAggregateFunction.Min:
			case QueryAggregateFunction.Max:
			case QueryAggregateFunction.SingleValue:
				return isScalarArgument;
			default:
				Contract.RetailFail("Unhandled aggregate function: " + queryAggregateFunction.ToString());
				return new bool?(false);
			}
		}

		// Token: 0x040002D8 RID: 728
		private readonly IIntermediateQueryTransformResolver _transformResolver;

		// Token: 0x040002D9 RID: 729
		private readonly QuerySourceExpressionReferenceContext _sourceRefContext;

		// Token: 0x040002DA RID: 730
		private readonly QueryParameterReferenceContext _parameterRefContext;

		// Token: 0x040002DB RID: 731
		private readonly DataShapeGenerationErrorContext _errorContext;

		// Token: 0x040002DC RID: 732
		private readonly bool _suppressModelGrouping;
	}
}
