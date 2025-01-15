using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x0200050A RID: 1290
	[Serializable]
	internal class HierarchyNodeList : ArrayList
	{
		// Token: 0x060043F0 RID: 17392 RVA: 0x0011CC8D File Offset: 0x0011AE8D
		internal HierarchyNodeList()
		{
		}

		// Token: 0x060043F1 RID: 17393 RVA: 0x0011CC9C File Offset: 0x0011AE9C
		internal HierarchyNodeList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x17001C84 RID: 7300
		internal ReportHierarchyNode this[int index]
		{
			get
			{
				return base[index] as ReportHierarchyNode;
			}
		}

		// Token: 0x17001C85 RID: 7301
		// (get) Token: 0x060043F3 RID: 17395 RVA: 0x0011CCBA File Offset: 0x0011AEBA
		internal List<int> LeafCellIndexes
		{
			get
			{
				if (this.m_leafCellIndexes == null)
				{
					HierarchyNodeList.CalculateLeafCellIndexes(this, ref this.m_leafCellIndexes, -1);
				}
				if (this.m_leafCellIndexes.Count != 0)
				{
					return this.m_leafCellIndexes;
				}
				return null;
			}
		}

		// Token: 0x060043F4 RID: 17396 RVA: 0x0011CCE6 File Offset: 0x0011AEE6
		public override int Add(object value)
		{
			if (((ReportHierarchyNode)value).IsDomainScope)
			{
				this.InitializeDomainScopeCount();
			}
			return base.Add(value);
		}

		// Token: 0x17001C86 RID: 7302
		// (get) Token: 0x060043F5 RID: 17397 RVA: 0x0011CD02 File Offset: 0x0011AF02
		internal int OriginalNodeCount
		{
			get
			{
				return this.Count - this.DomainScopeCount;
			}
		}

		// Token: 0x17001C87 RID: 7303
		// (get) Token: 0x060043F6 RID: 17398 RVA: 0x0011CD14 File Offset: 0x0011AF14
		private int DomainScopeCount
		{
			get
			{
				if (this.m_domainScopeCount == null)
				{
					this.m_domainScopeCount = new int?(0);
					using (IEnumerator enumerator = this.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (((ReportHierarchyNode)enumerator.Current).IsDomainScope)
							{
								this.m_domainScopeCount++;
							}
						}
					}
				}
				return this.m_domainScopeCount.Value;
			}
		}

		// Token: 0x060043F7 RID: 17399 RVA: 0x0011CDBC File Offset: 0x0011AFBC
		private void InitializeDomainScopeCount()
		{
			this.m_domainScopeCount = null;
		}

		// Token: 0x060043F8 RID: 17400 RVA: 0x0011CDCC File Offset: 0x0011AFCC
		internal List<int> GetLeafCellIndexes(int excludedCellIndex)
		{
			if (excludedCellIndex < 0)
			{
				return this.LeafCellIndexes;
			}
			if (this.m_leafCellIndexesWithoutExcluded == null)
			{
				this.m_excludedIndex = excludedCellIndex;
				HierarchyNodeList.CalculateLeafCellIndexes(this, ref this.m_leafCellIndexesWithoutExcluded, excludedCellIndex);
			}
			else if (this.m_excludedIndex != excludedCellIndex)
			{
				this.m_excludedIndex = excludedCellIndex;
				this.m_leafCellIndexesWithoutExcluded = null;
				HierarchyNodeList.CalculateLeafCellIndexes(this, ref this.m_leafCellIndexesWithoutExcluded, excludedCellIndex);
			}
			if (this.m_leafCellIndexesWithoutExcluded.Count != 0)
			{
				return this.m_leafCellIndexesWithoutExcluded;
			}
			return null;
		}

		// Token: 0x060043F9 RID: 17401 RVA: 0x0011CE3C File Offset: 0x0011B03C
		private static void CalculateLeafCellIndexes(HierarchyNodeList nodes, ref List<int> leafCellIndexes, int excludedCellIndex)
		{
			if (leafCellIndexes == null)
			{
				int count = nodes.Count;
				leafCellIndexes = new List<int>(count);
				for (int i = 0; i < count; i++)
				{
					ReportHierarchyNode reportHierarchyNode = nodes[i];
					if (reportHierarchyNode.InnerHierarchy == null && reportHierarchyNode.MemberCellIndex != excludedCellIndex)
					{
						leafCellIndexes.Add(reportHierarchyNode.MemberCellIndex);
					}
				}
			}
		}

		// Token: 0x060043FA RID: 17402 RVA: 0x0011CE90 File Offset: 0x0011B090
		internal List<ReportHierarchyNode> GetLeafNodes()
		{
			List<ReportHierarchyNode> list = new List<ReportHierarchyNode>();
			this.FindLeafNodes(list);
			return list;
		}

		// Token: 0x060043FB RID: 17403 RVA: 0x0011CEAC File Offset: 0x0011B0AC
		private void FindLeafNodes(List<ReportHierarchyNode> leafNodes)
		{
			for (int i = 0; i < base.Count; i++)
			{
				ReportHierarchyNode reportHierarchyNode = this[i];
				HierarchyNodeList innerHierarchy = reportHierarchyNode.InnerHierarchy;
				if (innerHierarchy == null)
				{
					leafNodes.Add(reportHierarchyNode);
				}
				else
				{
					innerHierarchy.FindLeafNodes(leafNodes);
				}
			}
		}

		// Token: 0x060043FC RID: 17404 RVA: 0x0011CEEC File Offset: 0x0011B0EC
		internal int GetMemberIndex(ReportHierarchyNode node)
		{
			Global.Tracer.Assert(node.InnerHierarchy == null, "GetMemberIndex should not be called for non leaf node");
			int num = -1;
			this.GetMemberIndex(ref num, node);
			return num;
		}

		// Token: 0x060043FD RID: 17405 RVA: 0x0011CF20 File Offset: 0x0011B120
		private bool GetMemberIndex(ref int index, ReportHierarchyNode node)
		{
			foreach (object obj in this)
			{
				ReportHierarchyNode reportHierarchyNode = (ReportHierarchyNode)obj;
				if (reportHierarchyNode.InnerHierarchy == null)
				{
					index++;
					if (node == reportHierarchyNode)
					{
						return true;
					}
				}
				else if (reportHierarchyNode.InnerHierarchy.GetMemberIndex(ref index, node))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x17001C88 RID: 7304
		// (get) Token: 0x060043FE RID: 17406 RVA: 0x0011CF9C File Offset: 0x0011B19C
		public HierarchyNodeList StaticMembersInSameScope
		{
			get
			{
				if (this.m_staticMembersInSameScope == null)
				{
					this.CalculateDependencies();
				}
				return this.m_staticMembersInSameScope;
			}
		}

		// Token: 0x17001C89 RID: 7305
		// (get) Token: 0x060043FF RID: 17407 RVA: 0x0011CFB2 File Offset: 0x0011B1B2
		public bool HasStaticLeafMembers
		{
			get
			{
				if (this.m_staticMembersInSameScope == null)
				{
					this.CalculateDependencies();
				}
				return this.m_hasStaticLeafMembers;
			}
		}

		// Token: 0x17001C8A RID: 7306
		// (get) Token: 0x06004400 RID: 17408 RVA: 0x0011CFC8 File Offset: 0x0011B1C8
		public HierarchyNodeList DynamicMembersAtScope
		{
			get
			{
				if (this.m_dynamicMembersAtScope == null)
				{
					this.CalculateDependencies();
				}
				return this.m_dynamicMembersAtScope;
			}
		}

		// Token: 0x06004401 RID: 17409 RVA: 0x0011CFDE File Offset: 0x0011B1DE
		private void CalculateDependencies()
		{
			this.m_staticMembersInSameScope = new HierarchyNodeList();
			this.m_dynamicMembersAtScope = new HierarchyNodeList();
			this.m_hasStaticLeafMembers = HierarchyNodeList.CalculateDependencies(this, this.m_staticMembersInSameScope, this.m_dynamicMembersAtScope);
		}

		// Token: 0x06004402 RID: 17410 RVA: 0x0011D010 File Offset: 0x0011B210
		private static bool CalculateDependencies(HierarchyNodeList members, HierarchyNodeList staticMembersInSameScope, HierarchyNodeList dynamicMembers)
		{
			if (members == null)
			{
				return false;
			}
			bool flag = false;
			int count = members.Count;
			foreach (object obj in members)
			{
				ReportHierarchyNode reportHierarchyNode = (ReportHierarchyNode)obj;
				if (!reportHierarchyNode.IsStatic)
				{
					dynamicMembers.Add(reportHierarchyNode);
				}
				else
				{
					staticMembersInSameScope.Add(reportHierarchyNode);
					flag = reportHierarchyNode.InnerHierarchy == null || (flag | HierarchyNodeList.CalculateDependencies(reportHierarchyNode.InnerHierarchy, staticMembersInSameScope, dynamicMembers));
				}
			}
			return flag;
		}

		// Token: 0x04001EE5 RID: 7909
		private List<int> m_leafCellIndexes;

		// Token: 0x04001EE6 RID: 7910
		private int m_excludedIndex = -1;

		// Token: 0x04001EE7 RID: 7911
		private List<int> m_leafCellIndexesWithoutExcluded;

		// Token: 0x04001EE8 RID: 7912
		private int? m_domainScopeCount;

		// Token: 0x04001EE9 RID: 7913
		[NonSerialized]
		private HierarchyNodeList m_staticMembersInSameScope;

		// Token: 0x04001EEA RID: 7914
		[NonSerialized]
		private HierarchyNodeList m_dynamicMembersAtScope;

		// Token: 0x04001EEB RID: 7915
		[NonSerialized]
		private bool m_hasStaticLeafMembers;
	}
}
