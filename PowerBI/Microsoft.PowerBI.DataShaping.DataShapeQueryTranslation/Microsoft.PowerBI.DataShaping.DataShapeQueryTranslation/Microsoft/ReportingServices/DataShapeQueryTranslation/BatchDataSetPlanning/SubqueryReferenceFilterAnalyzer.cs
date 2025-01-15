using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x020001AB RID: 427
	internal sealed class SubqueryReferenceFilterAnalyzer : FilterVisitor<FilterCondition>
	{
		// Token: 0x06000F03 RID: 3843 RVA: 0x0003D3FD File Offset: 0x0003B5FD
		private SubqueryReferenceFilterAnalyzer(ScopeTree scopeTree, WritableExpressionTable outputExpressionTable, DataShape ownerDataShape, VisitDataShapeDelegate visitDataShape = null)
			: base(visitDataShape)
		{
			this.m_scopeTree = scopeTree;
			this.m_outputExpressionTable = outputExpressionTable;
			this.m_ownerDataShape = ownerDataShape;
			this.m_visitedDataShapes = new HashSet<DataShape>();
		}

		// Token: 0x06000F04 RID: 3844 RVA: 0x0003D428 File Offset: 0x0003B628
		public static DataShape Analyze(FilterCondition filterCondition, ScopeTree scopeTree, WritableExpressionTable outputExpressionTable, DataShape ownerDataShape)
		{
			SubqueryReferenceFilterAnalyzer subqueryReferenceFilterAnalyzer = new SubqueryReferenceFilterAnalyzer(scopeTree, outputExpressionTable, ownerDataShape, null);
			subqueryReferenceFilterAnalyzer.Visit(filterCondition);
			if (!subqueryReferenceFilterAnalyzer.m_visitedDataShapes.IsNullOrEmpty<DataShape>())
			{
				return subqueryReferenceFilterAnalyzer.m_visitedDataShapes.Single("Filters should reference one subquery only!", Array.Empty<string>());
			}
			return null;
		}

		// Token: 0x06000F05 RID: 3845 RVA: 0x0003D46B File Offset: 0x0003B66B
		internal override FilterCondition Visit(UnaryFilterCondition condition)
		{
			this.VisitExpression(condition.Expression);
			return condition;
		}

		// Token: 0x06000F06 RID: 3846 RVA: 0x0003D47A File Offset: 0x0003B67A
		internal override FilterCondition Visit(BinaryFilterCondition condition)
		{
			this.VisitExpression(condition.LeftExpression);
			this.VisitExpression(condition.RightExpression);
			return condition;
		}

		// Token: 0x06000F07 RID: 3847 RVA: 0x0003D495 File Offset: 0x0003B695
		internal override FilterCondition Visit(CompoundFilterCondition condition)
		{
			this.Visit(condition.Conditions, condition.ObjectType, "Conditions");
			return condition;
		}

		// Token: 0x06000F08 RID: 3848 RVA: 0x0003D4AF File Offset: 0x0003B6AF
		internal override FilterCondition Visit(InFilterCondition condition)
		{
			this.VisitExpressions(condition.Expressions);
			return condition;
		}

		// Token: 0x06000F09 RID: 3849 RVA: 0x0003D4C0 File Offset: 0x0003B6C0
		private void VisitExpressions(IReadOnlyList<Expression> expressions)
		{
			for (int i = 0; i < expressions.Count; i++)
			{
				this.VisitExpression(expressions[i]);
			}
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x0003D4EC File Offset: 0x0003B6EC
		private void VisitExpression(Expression expression)
		{
			ResolvedCalculationReferenceExpressionNode resolvedCalculationReferenceExpressionNode = this.m_outputExpressionTable.GetNode(expression.ExpressionId.Value) as ResolvedCalculationReferenceExpressionNode;
			if (resolvedCalculationReferenceExpressionNode != null)
			{
				IScope containingScope = this.m_scopeTree.GetContainingScope(resolvedCalculationReferenceExpressionNode.Calculation);
				DataShape containingDataShapeOrSelf = this.m_scopeTree.GetContainingDataShapeOrSelf(containingScope);
				if (containingDataShapeOrSelf != this.m_ownerDataShape)
				{
					this.m_visitedDataShapes.Add(containingDataShapeOrSelf);
				}
			}
		}

		// Token: 0x04000718 RID: 1816
		private readonly ScopeTree m_scopeTree;

		// Token: 0x04000719 RID: 1817
		private readonly WritableExpressionTable m_outputExpressionTable;

		// Token: 0x0400071A RID: 1818
		private readonly DataShape m_ownerDataShape;

		// Token: 0x0400071B RID: 1819
		private HashSet<DataShape> m_visitedDataShapes;
	}
}
