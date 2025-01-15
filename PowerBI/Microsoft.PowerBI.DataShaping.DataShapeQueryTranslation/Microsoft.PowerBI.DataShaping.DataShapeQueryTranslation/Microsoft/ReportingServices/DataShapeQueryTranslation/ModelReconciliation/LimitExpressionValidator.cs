using System;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.ModelReconciliation
{
	// Token: 0x020000A0 RID: 160
	internal sealed class LimitExpressionValidator
	{
		// Token: 0x06000767 RID: 1895 RVA: 0x0001C97C File Offset: 0x0001AB7C
		internal LimitExpressionValidator(ExpressionTable expressionTable, ScopeTree scopeTree, TranslationErrorContext errorContext)
		{
			this.m_expressionTable = expressionTable;
			this.m_scopeTree = scopeTree;
			this.m_errorContext = errorContext;
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x0001C99C File Offset: 0x0001AB9C
		public void ValidateTarget(Expression target, string id)
		{
			ResolvedScopeReferenceExpressionNode resolvedScopeReferenceExpressionNode = this.m_expressionTable.GetNode(target) as ResolvedScopeReferenceExpressionNode;
			if (resolvedScopeReferenceExpressionNode == null)
			{
				this.ValidateUnresolvedScopeReferenceExpression(target, id, "Targets");
				return;
			}
			DataMember dataMember = resolvedScopeReferenceExpressionNode.Scope as DataMember;
			if (dataMember != null && dataMember.IsDynamic)
			{
				return;
			}
			if (resolvedScopeReferenceExpressionNode.Scope is DataIntersection)
			{
				return;
			}
			this.m_errorContext.Register(TranslationMessages.InvalidLimitScopes(EngineMessageSeverity.Error, ObjectType.Limit, id, "Targets"));
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x0001CA18 File Offset: 0x0001AC18
		internal void ValidateWithin(Limit limit)
		{
			ResolvedScopeReferenceExpressionNode resolvedScopeReferenceExpressionNode = this.m_expressionTable.GetNode(limit.Within) as ResolvedScopeReferenceExpressionNode;
			if (resolvedScopeReferenceExpressionNode == null)
			{
				this.ValidateUnresolvedScopeReferenceExpression(limit.Within, limit.Id.Value, "Within");
				return;
			}
			DataMember dataMember = resolvedScopeReferenceExpressionNode.Scope as DataMember;
			if (dataMember != null && dataMember.IsDynamic)
			{
				return;
			}
			if (resolvedScopeReferenceExpressionNode.Scope is DataShape)
			{
				return;
			}
			this.m_errorContext.Register(TranslationMessages.InvalidLimitScopes(EngineMessageSeverity.Error, ObjectType.Limit, limit.Id.Value, "Within"));
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x0001CAB0 File Offset: 0x0001ACB0
		private void ValidateUnresolvedScopeReferenceExpression(Expression expression, Identifier objectId, string propertyName)
		{
			Contract.RetailAssert(!(this.m_expressionTable.GetNode(expression) is StructureReferenceExpressionNode) || this.m_errorContext.HasError, "An error should have already been registered if the expression does not resolve to a scope or calculation");
			if (this.m_expressionTable.GetNode(expression) is ResolvedCalculationReferenceExpressionNode)
			{
				this.m_errorContext.Register(TranslationMessages.InvalidLimitScopes(EngineMessageSeverity.Error, ObjectType.Limit, objectId, propertyName));
			}
		}

		// Token: 0x04000391 RID: 913
		private readonly ExpressionTable m_expressionTable;

		// Token: 0x04000392 RID: 914
		private readonly ScopeTree m_scopeTree;

		// Token: 0x04000393 RID: 915
		private readonly TranslationErrorContext m_errorContext;
	}
}
