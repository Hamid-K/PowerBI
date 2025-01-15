using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.ReportPublishing
{
	// Token: 0x020003A0 RID: 928
	internal abstract class ScopeTreeNode
	{
		// Token: 0x060025E4 RID: 9700 RVA: 0x000B4F75 File Offset: 0x000B3175
		internal ScopeTreeNode(IRIFDataScope scope)
		{
			this.m_scope = scope;
		}

		// Token: 0x170013CD RID: 5069
		// (get) Token: 0x060025E5 RID: 9701 RVA: 0x000B4F8F File Offset: 0x000B318F
		internal IRIFDataScope Scope
		{
			get
			{
				return this.m_scope;
			}
		}

		// Token: 0x170013CE RID: 5070
		// (get) Token: 0x060025E6 RID: 9702 RVA: 0x000B4F97 File Offset: 0x000B3197
		internal List<IRIFDataScope> ChildScopes
		{
			get
			{
				return this.m_childScopes;
			}
		}

		// Token: 0x060025E7 RID: 9703 RVA: 0x000B4F9F File Offset: 0x000B319F
		internal void AddChildScope(IRIFDataScope child)
		{
			this.m_childScopes.Add(child);
		}

		// Token: 0x170013CF RID: 5071
		// (get) Token: 0x060025E8 RID: 9704
		internal abstract string ScopeName { get; }

		// Token: 0x060025E9 RID: 9705
		internal abstract bool IsSameOrParentScope(IRIFDataScope parentScope, bool isProperParent);

		// Token: 0x060025EA RID: 9706
		internal abstract void Traverse(ScopeTree.ScopeTreeVisitor visitor, IRIFDataScope outerScope, bool visitOuterScope);

		// Token: 0x060025EB RID: 9707
		internal abstract bool Traverse(ScopeTree.DirectedScopeTreeVisitor visitor);

		// Token: 0x060025EC RID: 9708 RVA: 0x000B4FAD File Offset: 0x000B31AD
		protected static bool TraverseNode(ScopeTree.DirectedScopeTreeVisitor visitor, ScopeTreeNode node)
		{
			return node == null || node.Traverse(visitor);
		}

		// Token: 0x0400160A RID: 5642
		protected readonly IRIFDataScope m_scope;

		// Token: 0x0400160B RID: 5643
		private readonly List<IRIFDataScope> m_childScopes = new List<IRIFDataScope>();
	}
}
