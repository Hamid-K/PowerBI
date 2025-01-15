using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.ReportPublishing
{
	// Token: 0x020003A1 RID: 929
	internal class SubScopeNode : ScopeTreeNode
	{
		// Token: 0x060025ED RID: 9709 RVA: 0x000B4FBB File Offset: 0x000B31BB
		internal SubScopeNode(IRIFDataScope scope, ScopeTreeNode parentScope)
			: base(scope)
		{
			this.m_parentScope = parentScope;
			if (this.m_parentScope != null)
			{
				this.m_parentScope.AddChildScope(scope);
			}
		}

		// Token: 0x170013D0 RID: 5072
		// (get) Token: 0x060025EE RID: 9710 RVA: 0x000B4FDF File Offset: 0x000B31DF
		internal ScopeTreeNode ParentScope
		{
			get
			{
				return this.m_parentScope;
			}
		}

		// Token: 0x170013D1 RID: 5073
		// (get) Token: 0x060025EF RID: 9711 RVA: 0x000B4FE7 File Offset: 0x000B31E7
		internal override string ScopeName
		{
			get
			{
				return this.m_scope.Name;
			}
		}

		// Token: 0x060025F0 RID: 9712 RVA: 0x000B4FF4 File Offset: 0x000B31F4
		internal override bool IsSameOrParentScope(IRIFDataScope parentScope, bool isProperParent)
		{
			return parentScope == base.Scope || (this.m_parentScope != null && this.m_parentScope.IsSameOrParentScope(parentScope, isProperParent));
		}

		// Token: 0x060025F1 RID: 9713 RVA: 0x000B5018 File Offset: 0x000B3218
		internal override void Traverse(ScopeTree.ScopeTreeVisitor visitor, IRIFDataScope outerScope, bool visitOuterScope)
		{
			bool flag = outerScope == base.Scope;
			if (visitOuterScope || !flag)
			{
				visitor(base.Scope);
			}
			if (!flag && this.m_parentScope != null)
			{
				this.m_parentScope.Traverse(visitor, outerScope, visitOuterScope);
			}
		}

		// Token: 0x060025F2 RID: 9714 RVA: 0x000B505A File Offset: 0x000B325A
		internal override bool Traverse(ScopeTree.DirectedScopeTreeVisitor visitor)
		{
			return visitor(base.Scope) && ScopeTreeNode.TraverseNode(visitor, this.m_parentScope);
		}

		// Token: 0x0400160C RID: 5644
		private readonly ScopeTreeNode m_parentScope;
	}
}
