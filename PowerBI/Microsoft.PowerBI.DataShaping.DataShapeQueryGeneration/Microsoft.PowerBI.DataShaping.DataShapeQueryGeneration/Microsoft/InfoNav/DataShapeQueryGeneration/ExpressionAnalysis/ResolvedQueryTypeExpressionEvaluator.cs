using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration.ExpressionAnalysis
{
	// Token: 0x02000105 RID: 261
	internal sealed class ResolvedQueryTypeExpressionEvaluator : DefaultResolvedQueryExpressionVisitor<ConceptualResultType>
	{
		// Token: 0x06000898 RID: 2200 RVA: 0x00022367 File Offset: 0x00020567
		internal bool TryEvaluate(ResolvedQueryExpression expression, out ConceptualResultType type)
		{
			type = this.VisitExpression(expression);
			return type != null;
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x00022377 File Offset: 0x00020577
		public override ConceptualResultType Visit(ResolvedQueryPrimitiveTypeExpression expression)
		{
			return ConceptualPrimitiveResultType.FromPrimitive(expression.Type);
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x00022384 File Offset: 0x00020584
		public override ConceptualResultType Visit(ResolvedQueryTypeOfExpression expression)
		{
			return this.VisitExpression(expression.Expression);
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x00022392 File Offset: 0x00020592
		public override ConceptualResultType Visit(ResolvedQueryColumnExpression expression)
		{
			return ConceptualPrimitiveResultType.FromPrimitive(expression.Column.ConceptualDataType);
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x000223A4 File Offset: 0x000205A4
		public override ConceptualResultType Visit(ResolvedQueryMeasureExpression expression)
		{
			if (expression.Measure.IsVariant)
			{
				return ConceptualPrimitiveResultType.Variant;
			}
			return ConceptualPrimitiveResultType.FromPrimitive(expression.Measure.ConceptualDataType);
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x000223C9 File Offset: 0x000205C9
		public override ConceptualResultType Visit(ResolvedQueryHierarchyLevelExpression expression)
		{
			return ConceptualPrimitiveResultType.FromPrimitive(expression.Level.Source.ConceptualDataType);
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x000223E0 File Offset: 0x000205E0
		public override ConceptualResultType Visit(ResolvedQueryTableTypeExpression expression)
		{
			List<ConceptualTypeColumn> list = new List<ConceptualTypeColumn>(expression.Columns.Count);
			foreach (ResolvedQueryTableTypeColumn resolvedQueryTableTypeColumn in expression.Columns)
			{
				ConceptualTypeColumn conceptualTypeColumn = this.Visit(resolvedQueryTableTypeColumn);
				if (conceptualTypeColumn == null)
				{
					return null;
				}
				list.Add(conceptualTypeColumn);
			}
			return list.Table();
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x00022458 File Offset: 0x00020658
		private ConceptualTypeColumn Visit(ResolvedQueryTableTypeColumn expressionColumn)
		{
			ConceptualResultType conceptualResultType = this.VisitExpression(expressionColumn.Expression);
			if (conceptualResultType == null)
			{
				return null;
			}
			return conceptualResultType.Column(expressionColumn.Name, null);
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x00022484 File Offset: 0x00020684
		protected override ConceptualResultType VisitUnhandledExpression(ResolvedQueryExpression expression)
		{
			return null;
		}
	}
}
