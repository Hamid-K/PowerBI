using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.CommonDataSetPlanning
{
	// Token: 0x02000128 RID: 296
	internal class DataSetPlannerFilterExpressionTranslatorBase : FilterExpressionVisitor
	{
		// Token: 0x06000B29 RID: 2857 RVA: 0x0002BB77 File Offset: 0x00029D77
		protected DataSetPlannerFilterExpressionTranslatorBase(DataSetPlannerFilterExpressionTreeTranslatorBase filterExpressionTreeTranslator, Filter filter, TranslationErrorContext errorContext, ExpressionTable inputExpressionTable, WritableExpressionTable outputExpressionTable, ScopeTree scopeTree)
			: base(null)
		{
			this.m_filterExpressionTreeTranslator = filterExpressionTreeTranslator;
			this.m_filter = filter;
			this.m_errorContext = errorContext;
			this.m_inputExpressionTable = inputExpressionTable;
			this.m_outputExpressionTable = outputExpressionTable;
			this.m_scopeTree = scopeTree;
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x0002BBB0 File Offset: 0x00029DB0
		internal override void VisitExpression(Expression expression, FilterCondition owner, string propertyName)
		{
			ExpressionNode node = this.m_inputExpressionTable.GetNode(this.m_filter.Target);
			IScope scope = null;
			ExpressionNodeKind kind = node.Kind;
			if (kind != ExpressionNodeKind.ResolvedCalculationReference)
			{
				if (kind == ExpressionNodeKind.ResolvedScopeReference)
				{
					scope = ((ResolvedScopeReferenceExpressionNode)node).Scope;
				}
			}
			else
			{
				scope = this.m_scopeTree.GetContainingScope(((ResolvedCalculationReferenceExpressionNode)node).Calculation);
			}
			if (scope != null)
			{
				ExpressionNode expressionNode = this.m_filterExpressionTreeTranslator.Translate(this.m_outputExpressionTable.GetNode(expression), new ExpressionContext(this.m_errorContext, owner.ObjectType, owner.Id, propertyName), scope);
				this.m_outputExpressionTable.SetNode(expression, expressionNode);
			}
		}

		// Token: 0x040005A5 RID: 1445
		protected readonly DataSetPlannerFilterExpressionTreeTranslatorBase m_filterExpressionTreeTranslator;

		// Token: 0x040005A6 RID: 1446
		protected readonly Filter m_filter;

		// Token: 0x040005A7 RID: 1447
		protected readonly TranslationErrorContext m_errorContext;

		// Token: 0x040005A8 RID: 1448
		protected readonly ExpressionTable m_inputExpressionTable;

		// Token: 0x040005A9 RID: 1449
		protected readonly WritableExpressionTable m_outputExpressionTable;

		// Token: 0x040005AA RID: 1450
		protected readonly ScopeTree m_scopeTree;
	}
}
