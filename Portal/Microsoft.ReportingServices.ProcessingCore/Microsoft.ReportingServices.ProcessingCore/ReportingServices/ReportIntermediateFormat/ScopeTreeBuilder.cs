using System;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000529 RID: 1321
	internal class ScopeTreeBuilder : IRIFScopeVisitor
	{
		// Token: 0x0600474E RID: 18254 RVA: 0x0012AA55 File Offset: 0x00128C55
		protected ScopeTreeBuilder(Report report)
		{
			this.m_tree = new ScopeTree(report);
		}

		// Token: 0x0600474F RID: 18255 RVA: 0x0012AA6C File Offset: 0x00128C6C
		public static ScopeTree BuildScopeTree(Report report)
		{
			ScopeTreeBuilder scopeTreeBuilder = new ScopeTreeBuilder(report);
			report.TraverseScopes(scopeTreeBuilder);
			return scopeTreeBuilder.Tree;
		}

		// Token: 0x06004750 RID: 18256 RVA: 0x0012AA8D File Offset: 0x00128C8D
		public virtual void PreVisit(DataRegion dataRegion)
		{
			this.m_tree.RegisterDataRegion(dataRegion);
		}

		// Token: 0x06004751 RID: 18257 RVA: 0x0012AA9B File Offset: 0x00128C9B
		public virtual void PostVisit(DataRegion dataRegion)
		{
			this.m_tree.UnRegisterDataRegion(dataRegion);
		}

		// Token: 0x06004752 RID: 18258 RVA: 0x0012AAA9 File Offset: 0x00128CA9
		public virtual void PreVisit(ReportHierarchyNode member)
		{
			this.m_tree.RegisterGrouping(member);
		}

		// Token: 0x06004753 RID: 18259 RVA: 0x0012AAB7 File Offset: 0x00128CB7
		public virtual void PostVisit(ReportHierarchyNode member)
		{
			this.m_tree.UnRegisterGrouping(member);
		}

		// Token: 0x06004754 RID: 18260 RVA: 0x0012AAC5 File Offset: 0x00128CC5
		public virtual void PreVisit(Cell cell, int rowIndex, int colIndex)
		{
			this.m_tree.RegisterCell(cell);
		}

		// Token: 0x06004755 RID: 18261 RVA: 0x0012AAD4 File Offset: 0x00128CD4
		public virtual void PostVisit(Cell cell, int rowIndex, int colIndex)
		{
			this.m_tree.UnRegisterCell(cell);
		}

		// Token: 0x17001DA1 RID: 7585
		// (get) Token: 0x06004756 RID: 18262 RVA: 0x0012AAE2 File Offset: 0x00128CE2
		internal ScopeTree Tree
		{
			get
			{
				return this.m_tree;
			}
		}

		// Token: 0x04001FDA RID: 8154
		protected ScopeTree m_tree;
	}
}
