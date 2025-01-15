using System;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQuery.Expressions
{
	// Token: 0x02000034 RID: 52
	internal sealed class ResolvedScopeReferenceExpressionNode : ResolvedStructureReferenceExpressionNode
	{
		// Token: 0x06000260 RID: 608 RVA: 0x0000742E File Offset: 0x0000562E
		internal ResolvedScopeReferenceExpressionNode(IScope scope)
		{
			this.m_scope = scope;
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000261 RID: 609 RVA: 0x0000743D File Offset: 0x0000563D
		public override IIdentifiable Target
		{
			get
			{
				return this.m_scope;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000262 RID: 610 RVA: 0x00007445 File Offset: 0x00005645
		public override ExpressionNodeKind Kind
		{
			get
			{
				return ExpressionNodeKind.ResolvedScopeReference;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000263 RID: 611 RVA: 0x00007449 File Offset: 0x00005649
		public IScope Scope
		{
			get
			{
				return this.m_scope;
			}
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00007451 File Offset: 0x00005651
		public T CastScope<T>() where T : IScope
		{
			Contract.RetailAssert(this.Scope is T, "Expected scope from wrong type (Expected: {0}, Actual: {1})", typeof(T), this.Scope.GetType());
			return (T)((object)this.Scope);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000748C File Offset: 0x0000568C
		public override bool Equals(ExpressionNode other)
		{
			bool flag;
			ResolvedScopeReferenceExpressionNode resolvedScopeReferenceExpressionNode;
			if (ExpressionNode.CheckReferenceAndTypeEquality<ResolvedScopeReferenceExpressionNode>(this, other, out flag, out resolvedScopeReferenceExpressionNode))
			{
				return flag;
			}
			return this.Scope.Equals(resolvedScopeReferenceExpressionNode.Scope);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x000074B9 File Offset: 0x000056B9
		protected override int GetHashCodeImpl()
		{
			return this.Scope.GetHashCode();
		}

		// Token: 0x040000A6 RID: 166
		private readonly IScope m_scope;
	}
}
