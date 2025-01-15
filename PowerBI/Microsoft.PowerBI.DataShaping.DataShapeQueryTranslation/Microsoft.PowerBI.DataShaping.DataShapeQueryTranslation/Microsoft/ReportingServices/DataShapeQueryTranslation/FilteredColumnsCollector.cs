using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.InfoNav;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation
{
	// Token: 0x02000048 RID: 72
	internal sealed class FilteredColumnsCollector : FilterExpressionVisitor
	{
		// Token: 0x060002FA RID: 762 RVA: 0x00008D62 File Offset: 0x00006F62
		private FilteredColumnsCollector(ExpressionTable expressionTable, DataShapeAnnotations annotations)
			: base(null)
		{
			this.m_expressionTable = expressionTable;
			this.m_annotations = annotations;
			this.m_filteredColumns = new HashSet<IConceptualColumn>();
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00008D84 File Offset: 0x00006F84
		internal static IReadOnlyList<IConceptualColumn> Collect(FilterCondition slicerFilter, DataShapeAnnotations annotations, ExpressionTable expressionTable)
		{
			FilteredColumnsCollector filteredColumnsCollector = new FilteredColumnsCollector(expressionTable, annotations);
			filteredColumnsCollector.Visit(slicerFilter);
			return filteredColumnsCollector.m_filteredColumns.EvaluateReadOnly<IConceptualColumn>();
		}

		// Token: 0x060002FC RID: 764 RVA: 0x00008D9F File Offset: 0x00006F9F
		internal static IReadOnlyList<IConceptualColumn> Collect(DataMember dataMember, DataShapeAnnotations annotations, ExpressionTable expressionTable)
		{
			FilteredColumnsCollector filteredColumnsCollector = new FilteredColumnsCollector(expressionTable, annotations);
			filteredColumnsCollector.CollectFilteredColumns(dataMember);
			return filteredColumnsCollector.m_filteredColumns.EvaluateReadOnly<IConceptualColumn>();
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00008DB9 File Offset: 0x00006FB9
		internal override void VisitExpression(Expression expression, FilterCondition owner, string propertyName)
		{
			this.VisitExpression(expression);
		}

		// Token: 0x060002FE RID: 766 RVA: 0x00008DC2 File Offset: 0x00006FC2
		protected override void VisitFilterConditionDataShape(DataShape dataShape, ObjectType filterConditionType)
		{
			if (dataShape != null)
			{
				this.CollectFilteredColumns(dataShape.PrimaryHierarchy);
				this.CollectFilteredColumns(dataShape.SecondaryHierarchy);
			}
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00008DDF File Offset: 0x00006FDF
		internal override FilterCondition Visit(InFilterCondition condition)
		{
			this.VisitExpressions(condition.Expressions, condition, "Expressions");
			return condition;
		}

		// Token: 0x06000300 RID: 768 RVA: 0x00008DF4 File Offset: 0x00006FF4
		internal override FilterCondition Visit(ApplyFilterCondition condition)
		{
			DataShape applyFilterDataShape = condition.GetApplyFilterDataShape(this.m_expressionTable);
			this.VisitFilterConditionDataShape(applyFilterDataShape, condition.ObjectType);
			return condition;
		}

		// Token: 0x06000301 RID: 769 RVA: 0x00008E1C File Offset: 0x0000701C
		private void CollectFilteredColumns(DataHierarchy hierarchy)
		{
			if (hierarchy != null)
			{
				List<DataMember> dataMembers = hierarchy.DataMembers;
				int? num = ((dataMembers != null) ? new int?(dataMembers.Count) : null);
				int num2 = 0;
				if ((num.GetValueOrDefault() > num2) & (num != null))
				{
					foreach (Expression expression in hierarchy.GetAllGroupAndSortKeyExpressions(this.m_annotations))
					{
						this.VisitExpression(expression);
					}
				}
			}
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00008EAC File Offset: 0x000070AC
		private void CollectFilteredColumns(DataMember dataMember)
		{
			foreach (Expression expression in dataMember.GetGroupKeyExpressions(this.m_annotations).ConcatNullable(dataMember.GetSortKeyExpressions(this.m_annotations)))
			{
				ExpressionNode node = this.m_expressionTable.GetNode(expression);
				this.CollectFilteredColumn(node);
			}
		}

		// Token: 0x06000303 RID: 771 RVA: 0x00008F20 File Offset: 0x00007120
		private void VisitExpression(Expression expression)
		{
			ExpressionNode node = this.m_expressionTable.GetNode(expression);
			this.CollectFilteredColumn(node);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00008F44 File Offset: 0x00007144
		private void CollectFilteredColumn(ExpressionNode exprNode)
		{
			ResolvedPropertyExpressionNode resolvedPropertyExpressionNode = exprNode as ResolvedPropertyExpressionNode;
			if (resolvedPropertyExpressionNode != null)
			{
				IConceptualColumn conceptualColumn = resolvedPropertyExpressionNode.Property.AsColumn();
				if (conceptualColumn != null)
				{
					this.m_filteredColumns.Add(conceptualColumn);
				}
			}
		}

		// Token: 0x040000CC RID: 204
		private readonly HashSet<IConceptualColumn> m_filteredColumns;

		// Token: 0x040000CD RID: 205
		private readonly ExpressionTable m_expressionTable;

		// Token: 0x040000CE RID: 206
		private readonly DataShapeAnnotations m_annotations;
	}
}
