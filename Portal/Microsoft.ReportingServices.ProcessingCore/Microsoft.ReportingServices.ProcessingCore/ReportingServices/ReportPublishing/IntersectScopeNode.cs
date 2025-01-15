using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.ReportPublishing
{
	// Token: 0x020003A2 RID: 930
	internal class IntersectScopeNode : ScopeTreeNode
	{
		// Token: 0x060025F3 RID: 9715 RVA: 0x000B5078 File Offset: 0x000B3278
		internal IntersectScopeNode(IRIFDataScope scope, ScopeTreeNode parentRowScope, ScopeTreeNode parentColScope)
			: base(scope)
		{
			this.m_parentRowScope = parentRowScope;
			if (this.m_parentRowScope != null)
			{
				this.m_parentRowScope.AddChildScope(scope);
			}
			this.m_parentColumnScope = parentColScope;
			if (this.m_parentColumnScope != null)
			{
				this.m_parentColumnScope.AddChildScope(scope);
			}
			this.m_peerDataCells = new List<IRIFDataScope>();
		}

		// Token: 0x170013D2 RID: 5074
		// (get) Token: 0x060025F4 RID: 9716 RVA: 0x000B50CD File Offset: 0x000B32CD
		internal ScopeTreeNode ParentRowScope
		{
			get
			{
				return this.m_parentRowScope;
			}
		}

		// Token: 0x170013D3 RID: 5075
		// (get) Token: 0x060025F5 RID: 9717 RVA: 0x000B50D5 File Offset: 0x000B32D5
		internal ScopeTreeNode ParentColumnScope
		{
			get
			{
				return this.m_parentColumnScope;
			}
		}

		// Token: 0x170013D4 RID: 5076
		// (get) Token: 0x060025F6 RID: 9718 RVA: 0x000B50E0 File Offset: 0x000B32E0
		internal override string ScopeName
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder("(");
				stringBuilder.Append(this.m_parentRowScope.ScopeName);
				stringBuilder.Append(".");
				stringBuilder.Append(this.m_parentColumnScope.ScopeName);
				stringBuilder.Append(")");
				return stringBuilder.ToString();
			}
		}

		// Token: 0x060025F7 RID: 9719 RVA: 0x000B5138 File Offset: 0x000B3338
		internal override bool IsSameOrParentScope(IRIFDataScope parentScope, bool isProperParent)
		{
			if (this.HasCell(parentScope))
			{
				return true;
			}
			if (this.m_parentRowScope == null || this.m_parentColumnScope == null)
			{
				return false;
			}
			bool flag = this.m_parentRowScope.IsSameOrParentScope(parentScope, isProperParent);
			bool flag2 = this.m_parentColumnScope.IsSameOrParentScope(parentScope, isProperParent);
			if (!isProperParent)
			{
				return flag || flag2;
			}
			return flag && flag2;
		}

		// Token: 0x060025F8 RID: 9720 RVA: 0x000B5188 File Offset: 0x000B3388
		internal override void Traverse(ScopeTree.ScopeTreeVisitor visitor, IRIFDataScope outerScope, bool visitOuterScope)
		{
			bool flag = this.HasCell(outerScope);
			if (visitOuterScope || !flag)
			{
				this.TraverseDefinitionCells(visitor);
			}
			if (!flag)
			{
				if (this.m_parentRowScope != null)
				{
					this.m_parentRowScope.Traverse(visitor, outerScope, visitOuterScope);
				}
				if (this.m_parentColumnScope != null)
				{
					this.m_parentColumnScope.Traverse(visitor, outerScope, visitOuterScope);
				}
			}
		}

		// Token: 0x060025F9 RID: 9721 RVA: 0x000B51D9 File Offset: 0x000B33D9
		internal override bool Traverse(ScopeTree.DirectedScopeTreeVisitor visitor)
		{
			return this.TraverseDefinitionCells(visitor) && ScopeTreeNode.TraverseNode(visitor, this.m_parentRowScope) && ScopeTreeNode.TraverseNode(visitor, this.m_parentColumnScope);
		}

		// Token: 0x060025FA RID: 9722 RVA: 0x000B5200 File Offset: 0x000B3400
		private void TraverseDefinitionCells(ScopeTree.ScopeTreeVisitor visitor)
		{
			visitor(base.Scope);
			foreach (IRIFDataScope irifdataScope in this.m_peerDataCells)
			{
				visitor(irifdataScope);
			}
		}

		// Token: 0x060025FB RID: 9723 RVA: 0x000B5260 File Offset: 0x000B3460
		private bool TraverseDefinitionCells(ScopeTree.DirectedScopeTreeVisitor visitor)
		{
			if (!visitor(base.Scope))
			{
				return false;
			}
			foreach (IRIFDataScope irifdataScope in this.m_peerDataCells)
			{
				if (!visitor(irifdataScope))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060025FC RID: 9724 RVA: 0x000B52CC File Offset: 0x000B34CC
		internal void AddCell(IRIFDataScope cell)
		{
			if (!this.HasCell(cell))
			{
				this.m_peerDataCells.Add(cell);
			}
		}

		// Token: 0x060025FD RID: 9725 RVA: 0x000B52E4 File Offset: 0x000B34E4
		internal bool HasCell(IRIFDataScope cell)
		{
			if (cell == null)
			{
				return false;
			}
			if (cell == base.Scope)
			{
				return true;
			}
			foreach (IRIFDataScope irifdataScope in this.m_peerDataCells)
			{
				if (ScopeTree.SameScope(cell, irifdataScope))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400160D RID: 5645
		private readonly ScopeTreeNode m_parentRowScope;

		// Token: 0x0400160E RID: 5646
		private readonly ScopeTreeNode m_parentColumnScope;

		// Token: 0x0400160F RID: 5647
		private readonly List<IRIFDataScope> m_peerDataCells;
	}
}
