using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQuery.Expressions
{
	// Token: 0x02000035 RID: 53
	internal sealed class ResolvedScopeValueDefinitionReferenceExpressionNode : ResolvedGroupElementReferenceExpressionNode
	{
		// Token: 0x06000267 RID: 615 RVA: 0x000074C6 File Offset: 0x000056C6
		internal ResolvedScopeValueDefinitionReferenceExpressionNode(ScopeValueDefinition scopeValueDefinition)
		{
			this.m_scopeValueDefinition = scopeValueDefinition;
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000268 RID: 616 RVA: 0x000074D5 File Offset: 0x000056D5
		public override IIdentifiable Target
		{
			get
			{
				return this.m_scopeValueDefinition;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000269 RID: 617 RVA: 0x000074DD File Offset: 0x000056DD
		public override ExpressionNodeKind Kind
		{
			get
			{
				return ExpressionNodeKind.ResolvedScopeValueDefinitionReference;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600026A RID: 618 RVA: 0x000074E1 File Offset: 0x000056E1
		public ScopeValueDefinition ScopeValueDefinition
		{
			get
			{
				return this.m_scopeValueDefinition;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600026B RID: 619 RVA: 0x000074E9 File Offset: 0x000056E9
		protected override Expression ReferencedExpression
		{
			get
			{
				return this.m_scopeValueDefinition.Value;
			}
		}

		// Token: 0x0600026C RID: 620 RVA: 0x000074F8 File Offset: 0x000056F8
		public override bool Equals(ExpressionNode other)
		{
			bool flag;
			ResolvedScopeValueDefinitionReferenceExpressionNode resolvedScopeValueDefinitionReferenceExpressionNode;
			if (ExpressionNode.CheckReferenceAndTypeEquality<ResolvedScopeValueDefinitionReferenceExpressionNode>(this, other, out flag, out resolvedScopeValueDefinitionReferenceExpressionNode))
			{
				if (resolvedScopeValueDefinitionReferenceExpressionNode == null)
				{
					flag = base.Equals(other);
				}
				return flag;
			}
			return this.ScopeValueDefinition == resolvedScopeValueDefinitionReferenceExpressionNode.ScopeValueDefinition;
		}

		// Token: 0x040000A7 RID: 167
		private readonly ScopeValueDefinition m_scopeValueDefinition;
	}
}
