using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportPublishing
{
	// Token: 0x0200039F RID: 927
	internal class ScopeTree
	{
		// Token: 0x060025BB RID: 9659 RVA: 0x000B478C File Offset: 0x000B298C
		internal ScopeTree()
		{
			this.m_scopes = new Dictionary<IRIFDataScope, ScopeTreeNode>();
			this.m_scopesByName = new Dictionary<string, ScopeTreeNode>(StringComparer.Ordinal);
			this.m_dataRegionScopes = FunctionalList<ScopeTreeNode>.Empty;
			this.m_activeScopes = FunctionalList<ScopeTreeNode>.Empty;
			this.m_activeRowScopes = FunctionalList<ScopeTreeNode>.Empty;
			this.m_activeColumnScopes = FunctionalList<ScopeTreeNode>.Empty;
			this.m_canonicalCellScopes = new Dictionary<string, Dictionary<string, ScopeTreeNode>>();
		}

		// Token: 0x060025BC RID: 9660 RVA: 0x000B47F1 File Offset: 0x000B29F1
		internal ScopeTree(Microsoft.ReportingServices.ReportIntermediateFormat.Report report)
			: this()
		{
			this.m_report = report;
		}

		// Token: 0x170013CC RID: 5068
		// (get) Token: 0x060025BD RID: 9661 RVA: 0x000B4800 File Offset: 0x000B2A00
		internal Microsoft.ReportingServices.ReportIntermediateFormat.Report Report
		{
			get
			{
				return this.m_report;
			}
		}

		// Token: 0x060025BE RID: 9662 RVA: 0x000B4808 File Offset: 0x000B2A08
		internal static bool SameScope(IRIFDataScope scope1, IRIFDataScope scope2)
		{
			return scope1 == scope2;
		}

		// Token: 0x060025BF RID: 9663 RVA: 0x000B480E File Offset: 0x000B2A0E
		internal static bool SameScope(IRIFDataScope scope1, string scope2)
		{
			return (scope1 == null && scope2 == null) || (scope1 != null && scope2 != null && ScopeTree.SameScope(scope1.Name, scope2));
		}

		// Token: 0x060025C0 RID: 9664 RVA: 0x000B482C File Offset: 0x000B2A2C
		internal static bool SameScope(string scope1, string scope2)
		{
			return (scope1 == null && scope2 == null) || (scope1 != null && scope2 != null && string.CompareOrdinal(scope1, scope2) == 0);
		}

		// Token: 0x060025C1 RID: 9665 RVA: 0x000B4848 File Offset: 0x000B2A48
		internal string FindAncestorScopeName(string scopeName, int ancestorLevel)
		{
			ScopeTreeNode scopeTreeNode;
			if (this.m_scopesByName.TryGetValue(scopeName, out scopeTreeNode))
			{
				SubScopeNode subScopeNode = scopeTreeNode as SubScopeNode;
				if (subScopeNode != null)
				{
					SubScopeNode subScopeNode2 = subScopeNode;
					int num = 0;
					while (num < ancestorLevel && !(subScopeNode2.Scope is Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion))
					{
						subScopeNode = subScopeNode.ParentScope as SubScopeNode;
						if (subScopeNode == null)
						{
							break;
						}
						subScopeNode2 = subScopeNode;
						num++;
					}
					return subScopeNode2.ScopeName;
				}
			}
			return null;
		}

		// Token: 0x060025C2 RID: 9666 RVA: 0x000B48A4 File Offset: 0x000B2AA4
		internal int MeasureScopeDistance(string innerScopeName, string outerScopeName)
		{
			ScopeTreeNode scopeTreeNode;
			if (this.m_scopesByName.TryGetValue(innerScopeName, out scopeTreeNode))
			{
				SubScopeNode subScopeNode = scopeTreeNode as SubScopeNode;
				if (subScopeNode != null)
				{
					int num = 0;
					SubScopeNode subScopeNode2 = subScopeNode;
					while (!string.Equals(subScopeNode2.ScopeName, outerScopeName, StringComparison.Ordinal))
					{
						subScopeNode = subScopeNode.ParentScope as SubScopeNode;
						if (subScopeNode == null)
						{
							return -1;
						}
						subScopeNode2 = subScopeNode;
						num++;
					}
					return num;
				}
			}
			return -1;
		}

		// Token: 0x060025C3 RID: 9667 RVA: 0x000B48FC File Offset: 0x000B2AFC
		internal bool IsSameOrProperParentScope(IRIFDataScope outerScope, IRIFDataScope innerScope)
		{
			ScopeTreeNode scopeTreeNode;
			return this.m_scopes.TryGetValue(innerScope, out scopeTreeNode) && scopeTreeNode.IsSameOrParentScope(outerScope, true);
		}

		// Token: 0x060025C4 RID: 9668 RVA: 0x000B4924 File Offset: 0x000B2B24
		internal bool IsSameOrParentScope(IRIFDataScope outerScope, IRIFDataScope innerScope)
		{
			ScopeTreeNode scopeTreeNode;
			return this.m_scopes.TryGetValue(innerScope, out scopeTreeNode) && scopeTreeNode.IsSameOrParentScope(outerScope, false);
		}

		// Token: 0x060025C5 RID: 9669 RVA: 0x000B494C File Offset: 0x000B2B4C
		internal bool IsParentScope(IRIFDataScope outerScope, IRIFDataScope innerScope)
		{
			ScopeTreeNode scopeTreeNode;
			return outerScope != innerScope && this.m_scopes.TryGetValue(innerScope, out scopeTreeNode) && scopeTreeNode.IsSameOrParentScope(outerScope, false);
		}

		// Token: 0x060025C6 RID: 9670 RVA: 0x000B4979 File Offset: 0x000B2B79
		internal IEnumerable<IRIFDataScope> GetChildScopes(IRIFDataScope parentScope)
		{
			return this.GetScopeNodeOrAssert(parentScope).ChildScopes;
		}

		// Token: 0x060025C7 RID: 9671 RVA: 0x000B4987 File Offset: 0x000B2B87
		internal bool IsIntersectionScope(IRIFDataScope scope)
		{
			return this.GetScopeNodeOrAssert(scope) is IntersectScopeNode;
		}

		// Token: 0x060025C8 RID: 9672 RVA: 0x000B4998 File Offset: 0x000B2B98
		internal IRIFDataScope GetParentScope(IRIFDataScope scope)
		{
			ScopeTreeNode parentScope = this.GetSubScopeNodeOrAssert(scope).ParentScope;
			if (parentScope != null)
			{
				return parentScope.Scope;
			}
			return null;
		}

		// Token: 0x060025C9 RID: 9673 RVA: 0x000B49BD File Offset: 0x000B2BBD
		internal IRIFDataScope GetParentRowScopeForIntersection(IRIFDataScope intersectScope)
		{
			return this.GetIntersectScopeNodeOrAssert(intersectScope).ParentRowScope.Scope;
		}

		// Token: 0x060025CA RID: 9674 RVA: 0x000B49D0 File Offset: 0x000B2BD0
		internal IRIFDataScope GetParentColumnScopeForIntersection(IRIFDataScope intersectScope)
		{
			return this.GetIntersectScopeNodeOrAssert(intersectScope).ParentColumnScope.Scope;
		}

		// Token: 0x060025CB RID: 9675 RVA: 0x000B49E3 File Offset: 0x000B2BE3
		internal void Traverse(ScopeTree.ScopeTreeVisitor visitor, IRIFDataScope outerScope, IRIFDataScope innerScope, bool visitOuterScope)
		{
			this.GetScopeNodeOrAssert(innerScope).Traverse(visitor, outerScope, visitOuterScope);
		}

		// Token: 0x060025CC RID: 9676 RVA: 0x000B49F5 File Offset: 0x000B2BF5
		internal bool Traverse(ScopeTree.DirectedScopeTreeVisitor visitor, IRIFDataScope startScope)
		{
			return this.GetScopeNodeOrAssert(startScope).Traverse(visitor);
		}

		// Token: 0x060025CD RID: 9677 RVA: 0x000B4A04 File Offset: 0x000B2C04
		internal Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion GetParentDataRegion(IRIFDataScope scope)
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion parentDataRegion = null;
			ScopeTree.DirectedScopeTreeVisitor directedScopeTreeVisitor = delegate(IRIFDataScope candidate)
			{
				if (candidate != scope)
				{
					parentDataRegion = candidate as Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion;
				}
				return parentDataRegion == null;
			};
			this.Traverse(directedScopeTreeVisitor, scope);
			return parentDataRegion;
		}

		// Token: 0x060025CE RID: 9678 RVA: 0x000B4A48 File Offset: 0x000B2C48
		private SubScopeNode GetSubScopeNodeOrAssert(IRIFDataScope scope)
		{
			SubScopeNode subScopeNode = this.GetScopeNodeOrAssert(scope) as SubScopeNode;
			Global.Tracer.Assert(subScopeNode != null, "Specified scope was not a SubScope");
			return subScopeNode;
		}

		// Token: 0x060025CF RID: 9679 RVA: 0x000B4A78 File Offset: 0x000B2C78
		private IntersectScopeNode GetIntersectScopeNodeOrAssert(IRIFDataScope scope)
		{
			IntersectScopeNode intersectScopeNode = this.GetScopeNodeOrAssert(scope) as IntersectScopeNode;
			Global.Tracer.Assert(intersectScopeNode != null, "Specified scope was not an IntersectScopeNode ");
			return intersectScopeNode;
		}

		// Token: 0x060025D0 RID: 9680 RVA: 0x000B4AA8 File Offset: 0x000B2CA8
		private ScopeTreeNode GetScopeNodeOrAssert(IRIFDataScope scope)
		{
			ScopeTreeNode scopeTreeNode;
			if (this.m_scopes.TryGetValue(scope, out scopeTreeNode))
			{
				return scopeTreeNode;
			}
			Global.Tracer.Assert(false, "Could not find scope in tree: {0}", new object[] { scope });
			throw new InvalidOperationException();
		}

		// Token: 0x060025D1 RID: 9681 RVA: 0x000B4AE6 File Offset: 0x000B2CE6
		internal void RegisterGrouping(Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode member)
		{
			if (member.IsColumn)
			{
				this.AddGroupScope(member, ref this.m_activeColumnScopes);
				return;
			}
			this.AddGroupScope(member, ref this.m_activeRowScopes);
		}

		// Token: 0x060025D2 RID: 9682 RVA: 0x000B4B0C File Offset: 0x000B2D0C
		private void AddGroupScope(Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode member, ref FunctionalList<ScopeTreeNode> axisScopes)
		{
			ScopeTreeNode scopeTreeNode;
			if (!this.m_scopes.TryGetValue(member, out scopeTreeNode))
			{
				if (!this.HasScope(axisScopes))
				{
					scopeTreeNode = new SubScopeNode(member, this.m_dataRegionScopes.First);
				}
				else
				{
					scopeTreeNode = new SubScopeNode(member, this.m_activeScopes.First);
				}
			}
			this.AddScope(scopeTreeNode);
			axisScopes = axisScopes.Add(scopeTreeNode);
		}

		// Token: 0x060025D3 RID: 9683 RVA: 0x000B4B6A File Offset: 0x000B2D6A
		internal void UnRegisterGrouping(Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode member)
		{
			if (member.IsColumn)
			{
				this.RemoveGroupScope(ref this.m_activeColumnScopes);
				return;
			}
			this.RemoveGroupScope(ref this.m_activeRowScopes);
		}

		// Token: 0x060025D4 RID: 9684 RVA: 0x000B4B8D File Offset: 0x000B2D8D
		private void RemoveGroupScope(ref FunctionalList<ScopeTreeNode> axisScopes)
		{
			axisScopes = axisScopes.Rest;
			this.m_activeScopes = this.m_activeScopes.Rest;
		}

		// Token: 0x060025D5 RID: 9685 RVA: 0x000B4BAC File Offset: 0x000B2DAC
		internal void RegisterDataRegion(Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion dataRegion)
		{
			ScopeTreeNode scopeTreeNode;
			if (!this.m_scopes.TryGetValue(dataRegion, out scopeTreeNode))
			{
				scopeTreeNode = new SubScopeNode(dataRegion, this.m_activeScopes.First);
			}
			this.AddScope(scopeTreeNode);
			this.m_dataRegionScopes = this.m_dataRegionScopes.Add(scopeTreeNode);
			this.m_activeRowScopes = this.m_activeRowScopes.Add(null);
			this.m_activeColumnScopes = this.m_activeColumnScopes.Add(null);
		}

		// Token: 0x060025D6 RID: 9686 RVA: 0x000B4C18 File Offset: 0x000B2E18
		internal Microsoft.ReportingServices.ReportIntermediateFormat.DataSet GetDataSet(IRIFDataScope dataScope, string dataSetName)
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSet = null;
			if (dataScope != null && dataScope.DataScopeInfo != null)
			{
				dataSet = dataScope.DataScopeInfo.DataSet;
			}
			if (dataSet != null)
			{
				return dataSet;
			}
			if (dataSetName == null)
			{
				return null;
			}
			return this.GetDataSet(dataSetName);
		}

		// Token: 0x060025D7 RID: 9687 RVA: 0x000B4C50 File Offset: 0x000B2E50
		internal Microsoft.ReportingServices.ReportIntermediateFormat.DataSet GetDataSet(string dataSetName)
		{
			if (string.IsNullOrEmpty(dataSetName))
			{
				return null;
			}
			Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSet;
			this.m_report.MappingNameToDataSet.TryGetValue(dataSetName, out dataSet);
			return dataSet;
		}

		// Token: 0x060025D8 RID: 9688 RVA: 0x000B4C7C File Offset: 0x000B2E7C
		internal Microsoft.ReportingServices.ReportIntermediateFormat.DataSet GetDefaultTopLevelDataSet()
		{
			if (this.m_report.OneDataSetName != null)
			{
				return this.GetDataSet(this.m_report.OneDataSetName);
			}
			return null;
		}

		// Token: 0x060025D9 RID: 9689 RVA: 0x000B4CA0 File Offset: 0x000B2EA0
		internal void UnRegisterDataRegion(Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion dataRegion)
		{
			this.m_activeScopes = this.m_activeScopes.Rest;
			this.m_activeRowScopes = this.m_activeRowScopes.Rest;
			this.m_activeColumnScopes = this.m_activeColumnScopes.Rest;
			this.m_dataRegionScopes = this.m_dataRegionScopes.Rest;
		}

		// Token: 0x060025DA RID: 9690 RVA: 0x000B4CF4 File Offset: 0x000B2EF4
		internal IRIFDataScope RegisterCell(IRIFDataScope cell)
		{
			ScopeTreeNode scopeTreeNode;
			if (!this.m_scopes.TryGetValue(cell, out scopeTreeNode))
			{
				if (this.HasScope(this.m_activeRowScopes) && this.HasScope(this.m_activeColumnScopes))
				{
					ScopeTreeNode first = this.m_activeRowScopes.First;
					ScopeTreeNode first2 = this.m_activeColumnScopes.First;
					if (!this.TryGetCanonicalCellScope(first, first2, out scopeTreeNode))
					{
						scopeTreeNode = new IntersectScopeNode(cell, first, first2);
						this.AddCanonicalCellScope(first, first2, scopeTreeNode);
					}
					((IntersectScopeNode)scopeTreeNode).AddCell(cell);
				}
				else
				{
					scopeTreeNode = new SubScopeNode(cell, this.m_activeScopes.First);
				}
			}
			this.AddScope(scopeTreeNode, cell);
			this.m_activeRowScopes = this.m_activeRowScopes.Add(null);
			this.m_activeColumnScopes = this.m_activeColumnScopes.Add(null);
			return scopeTreeNode.Scope;
		}

		// Token: 0x060025DB RID: 9691 RVA: 0x000B4DB5 File Offset: 0x000B2FB5
		internal void UnRegisterCell(IRIFDataScope cell)
		{
			this.m_activeScopes = this.m_activeScopes.Rest;
			this.m_activeRowScopes = this.m_activeRowScopes.Rest;
			this.m_activeColumnScopes = this.m_activeColumnScopes.Rest;
		}

		// Token: 0x060025DC RID: 9692 RVA: 0x000B4DEC File Offset: 0x000B2FEC
		internal IRIFDataScope GetCanonicalCellScope(IRIFDataScope cell)
		{
			ScopeTreeNode scopeTreeNode;
			if (!this.m_scopes.TryGetValue(cell, out scopeTreeNode))
			{
				Global.Tracer.Assert(false, "GetCanonicalCellScope must not be called for a cell outside the ScopeTree.");
			}
			return scopeTreeNode.Scope;
		}

		// Token: 0x060025DD RID: 9693 RVA: 0x000B4E20 File Offset: 0x000B3020
		private bool TryGetCanonicalCellScope(ScopeTreeNode rowScope, ScopeTreeNode colScope, out ScopeTreeNode canonicalCellScope)
		{
			Dictionary<string, ScopeTreeNode> dictionary;
			if (this.m_canonicalCellScopes.TryGetValue(rowScope.Scope.Name, out dictionary) && dictionary.TryGetValue(colScope.Scope.Name, out canonicalCellScope))
			{
				return true;
			}
			canonicalCellScope = null;
			return false;
		}

		// Token: 0x060025DE RID: 9694 RVA: 0x000B4E64 File Offset: 0x000B3064
		private void AddCanonicalCellScope(ScopeTreeNode rowScope, ScopeTreeNode colScope, ScopeTreeNode cellScope)
		{
			Dictionary<string, ScopeTreeNode> dictionary;
			if (!this.m_canonicalCellScopes.TryGetValue(rowScope.Scope.Name, out dictionary))
			{
				dictionary = new Dictionary<string, ScopeTreeNode>();
				this.m_canonicalCellScopes.Add(rowScope.Scope.Name, dictionary);
			}
			dictionary[colScope.Scope.Name] = cellScope;
		}

		// Token: 0x060025DF RID: 9695 RVA: 0x000B4EBA File Offset: 0x000B30BA
		private bool HasScope(FunctionalList<ScopeTreeNode> list)
		{
			return !list.IsEmpty() && list.First != null;
		}

		// Token: 0x060025E0 RID: 9696 RVA: 0x000B4ECF File Offset: 0x000B30CF
		private void AddScope(ScopeTreeNode scopeNode, IRIFDataScope scope)
		{
			this.m_activeScopes = this.m_activeScopes.Add(scopeNode);
			this.m_scopes[scope] = scopeNode;
			if (!string.IsNullOrEmpty(scopeNode.ScopeName))
			{
				this.m_scopesByName[scopeNode.ScopeName] = scopeNode;
			}
		}

		// Token: 0x060025E1 RID: 9697 RVA: 0x000B4F0F File Offset: 0x000B310F
		private void AddScope(ScopeTreeNode scopeNode)
		{
			this.AddScope(scopeNode, scopeNode.Scope);
		}

		// Token: 0x060025E2 RID: 9698 RVA: 0x000B4F20 File Offset: 0x000B3120
		internal string GetScopeName(IRIFDataScope scope)
		{
			ScopeTreeNode scopeTreeNode;
			if (this.m_scopes.TryGetValue(scope, out scopeTreeNode))
			{
				return scopeTreeNode.ScopeName;
			}
			return scope.Name;
		}

		// Token: 0x060025E3 RID: 9699 RVA: 0x000B4F50 File Offset: 0x000B3150
		internal IRIFDataScope GetScopeByName(string scopeName)
		{
			ScopeTreeNode scopeTreeNode;
			if (this.m_scopesByName.TryGetValue(scopeName, out scopeTreeNode))
			{
				return scopeTreeNode.Scope;
			}
			return null;
		}

		// Token: 0x04001602 RID: 5634
		private Dictionary<IRIFDataScope, ScopeTreeNode> m_scopes;

		// Token: 0x04001603 RID: 5635
		private Dictionary<string, ScopeTreeNode> m_scopesByName;

		// Token: 0x04001604 RID: 5636
		private Dictionary<string, Dictionary<string, ScopeTreeNode>> m_canonicalCellScopes;

		// Token: 0x04001605 RID: 5637
		private FunctionalList<ScopeTreeNode> m_dataRegionScopes;

		// Token: 0x04001606 RID: 5638
		private FunctionalList<ScopeTreeNode> m_activeScopes;

		// Token: 0x04001607 RID: 5639
		private FunctionalList<ScopeTreeNode> m_activeRowScopes;

		// Token: 0x04001608 RID: 5640
		private FunctionalList<ScopeTreeNode> m_activeColumnScopes;

		// Token: 0x04001609 RID: 5641
		private Microsoft.ReportingServices.ReportIntermediateFormat.Report m_report;

		// Token: 0x0200095E RID: 2398
		// (Invoke) Token: 0x0600800D RID: 32781
		internal delegate void ScopeTreeVisitor(IRIFDataScope scope);

		// Token: 0x0200095F RID: 2399
		// (Invoke) Token: 0x06008011 RID: 32785
		internal delegate bool DirectedScopeTreeVisitor(IRIFDataScope scope);
	}
}
