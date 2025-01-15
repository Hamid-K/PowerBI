using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.QueryRewriting;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Data.Entity.Core.Mapping.ViewGeneration.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.Validation;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration
{
	// Token: 0x02000562 RID: 1378
	internal class BasicViewGenerator : InternalBase
	{
		// Token: 0x0600432D RID: 17197 RVA: 0x000E6FB4 File Offset: 0x000E51B4
		internal BasicViewGenerator(MemberProjectionIndex projectedSlotMap, List<LeftCellWrapper> usedCells, FragmentQuery activeDomain, ViewgenContext context, MemberDomainMap domainMap, ErrorLog errorLog, ConfigViewGenerator config)
		{
			this.m_projectedSlotMap = projectedSlotMap;
			this.m_usedCells = usedCells;
			this.m_viewgenContext = context;
			this.m_activeDomain = activeDomain;
			this.m_errorLog = errorLog;
			this.m_config = config;
			this.m_domainMap = domainMap;
		}

		// Token: 0x17000D55 RID: 3413
		// (get) Token: 0x0600432E RID: 17198 RVA: 0x000E6FF1 File Offset: 0x000E51F1
		private FragmentQueryProcessor LeftQP
		{
			get
			{
				return this.m_viewgenContext.LeftFragmentQP;
			}
		}

		// Token: 0x0600432F RID: 17199 RVA: 0x000E7000 File Offset: 0x000E5200
		internal CellTreeNode CreateViewExpression()
		{
			OpCellTreeNode opCellTreeNode = new OpCellTreeNode(this.m_viewgenContext, CellTreeOpType.FOJ);
			foreach (LeftCellWrapper leftCellWrapper in this.m_usedCells)
			{
				LeafCellTreeNode leafCellTreeNode = new LeafCellTreeNode(this.m_viewgenContext, leftCellWrapper);
				opCellTreeNode.Add(leafCellTreeNode);
			}
			CellTreeNode cellTreeNode = this.GroupByRightExtent(opCellTreeNode);
			cellTreeNode = this.IsolateUnions(cellTreeNode);
			cellTreeNode = this.IsolateByOperator(cellTreeNode, CellTreeOpType.Union);
			cellTreeNode = this.IsolateByOperator(cellTreeNode, CellTreeOpType.IJ);
			cellTreeNode = this.IsolateByOperator(cellTreeNode, CellTreeOpType.LOJ);
			if (this.m_viewgenContext.ViewTarget == ViewTarget.QueryView)
			{
				cellTreeNode = this.ConvertUnionsToNormalizedLOJs(cellTreeNode);
			}
			return cellTreeNode;
		}

		// Token: 0x06004330 RID: 17200 RVA: 0x000E70B0 File Offset: 0x000E52B0
		internal CellTreeNode GroupByRightExtent(CellTreeNode rootNode)
		{
			KeyToListMap<EntitySetBase, LeafCellTreeNode> keyToListMap = new KeyToListMap<EntitySetBase, LeafCellTreeNode>(EqualityComparer<EntitySetBase>.Default);
			foreach (CellTreeNode cellTreeNode in rootNode.Children)
			{
				LeafCellTreeNode leafCellTreeNode = (LeafCellTreeNode)cellTreeNode;
				EntitySetBase extent = leafCellTreeNode.LeftCellWrapper.RightCellQuery.Extent;
				keyToListMap.Add(extent, leafCellTreeNode);
			}
			OpCellTreeNode opCellTreeNode = new OpCellTreeNode(this.m_viewgenContext, CellTreeOpType.FOJ);
			foreach (EntitySetBase entitySetBase in keyToListMap.Keys)
			{
				OpCellTreeNode opCellTreeNode2 = new OpCellTreeNode(this.m_viewgenContext, CellTreeOpType.FOJ);
				foreach (LeafCellTreeNode leafCellTreeNode2 in keyToListMap.ListForKey(entitySetBase))
				{
					opCellTreeNode2.Add(leafCellTreeNode2);
				}
				opCellTreeNode.Add(opCellTreeNode2);
			}
			return opCellTreeNode.Flatten();
		}

		// Token: 0x06004331 RID: 17201 RVA: 0x000E71D4 File Offset: 0x000E53D4
		private CellTreeNode IsolateUnions(CellTreeNode rootNode)
		{
			if (rootNode.Children.Count <= 1)
			{
				return rootNode;
			}
			for (int i = 0; i < rootNode.Children.Count; i++)
			{
				rootNode.Children[i] = this.IsolateUnions(rootNode.Children[i]);
			}
			OpCellTreeNode opCellTreeNode = new OpCellTreeNode(this.m_viewgenContext, CellTreeOpType.Union);
			ModifiableIteratorCollection<CellTreeNode> modifiableIteratorCollection = new ModifiableIteratorCollection<CellTreeNode>(rootNode.Children);
			while (!modifiableIteratorCollection.IsEmpty)
			{
				OpCellTreeNode opCellTreeNode2 = new OpCellTreeNode(this.m_viewgenContext, CellTreeOpType.FOJ);
				CellTreeNode cellTreeNode = modifiableIteratorCollection.RemoveOneElement();
				opCellTreeNode2.Add(cellTreeNode);
				foreach (CellTreeNode cellTreeNode2 in modifiableIteratorCollection.Elements())
				{
					if (!this.IsDisjoint(opCellTreeNode2, cellTreeNode2))
					{
						opCellTreeNode2.Add(cellTreeNode2);
						modifiableIteratorCollection.RemoveCurrentOfIterator();
						modifiableIteratorCollection.ResetIterator();
					}
				}
				opCellTreeNode.Add(opCellTreeNode2);
			}
			return opCellTreeNode.Flatten();
		}

		// Token: 0x06004332 RID: 17202 RVA: 0x000E72D0 File Offset: 0x000E54D0
		private CellTreeNode ConvertUnionsToNormalizedLOJs(CellTreeNode rootNode)
		{
			for (int i = 0; i < rootNode.Children.Count; i++)
			{
				rootNode.Children[i] = this.ConvertUnionsToNormalizedLOJs(rootNode.Children[i]);
			}
			if (rootNode.OpType != CellTreeOpType.LOJ || rootNode.Children.Count < 2)
			{
				return rootNode;
			}
			OpCellTreeNode opCellTreeNode = new OpCellTreeNode(this.m_viewgenContext, rootNode.OpType);
			List<CellTreeNode> list = new List<CellTreeNode>();
			OpCellTreeNode opCellTreeNode2 = null;
			HashSet<CellTreeNode> hashSet = null;
			if (rootNode.Children[0].OpType == CellTreeOpType.IJ)
			{
				opCellTreeNode2 = new OpCellTreeNode(this.m_viewgenContext, rootNode.Children[0].OpType);
				opCellTreeNode.Add(opCellTreeNode2);
				list.AddRange(rootNode.Children[0].Children);
				hashSet = new HashSet<CellTreeNode>(rootNode.Children[0].Children);
			}
			else
			{
				opCellTreeNode.Add(rootNode.Children[0]);
			}
			foreach (CellTreeNode cellTreeNode in rootNode.Children.Skip(1))
			{
				OpCellTreeNode opCellTreeNode3 = cellTreeNode as OpCellTreeNode;
				if (opCellTreeNode3 != null && opCellTreeNode3.OpType == CellTreeOpType.Union)
				{
					list.AddRange(opCellTreeNode3.Children);
				}
				else
				{
					list.Add(cellTreeNode);
				}
			}
			KeyToListMap<EntitySet, LeafCellTreeNode> keyToListMap = new KeyToListMap<EntitySet, LeafCellTreeNode>(EqualityComparer<EntitySet>.Default);
			foreach (CellTreeNode cellTreeNode2 in list)
			{
				LeafCellTreeNode leafCellTreeNode = cellTreeNode2 as LeafCellTreeNode;
				if (leafCellTreeNode != null)
				{
					EntitySetBase leafNodeTable = BasicViewGenerator.GetLeafNodeTable(leafCellTreeNode);
					if (leafNodeTable != null)
					{
						keyToListMap.Add((EntitySet)leafNodeTable, leafCellTreeNode);
					}
				}
				else if (hashSet != null && hashSet.Contains(cellTreeNode2))
				{
					opCellTreeNode2.Add(cellTreeNode2);
				}
				else
				{
					opCellTreeNode.Add(cellTreeNode2);
				}
			}
			foreach (KeyValuePair<EntitySet, List<LeafCellTreeNode>> keyValuePair in keyToListMap.KeyValuePairs.Where((KeyValuePair<EntitySet, List<LeafCellTreeNode>> m) => m.Value.Count > 1).ToArray<KeyValuePair<EntitySet, List<LeafCellTreeNode>>>())
			{
				keyToListMap.RemoveKey(keyValuePair.Key);
				foreach (LeafCellTreeNode leafCellTreeNode2 in keyValuePair.Value)
				{
					if (hashSet != null && hashSet.Contains(leafCellTreeNode2))
					{
						opCellTreeNode2.Add(leafCellTreeNode2);
					}
					else
					{
						opCellTreeNode.Add(leafCellTreeNode2);
					}
				}
			}
			KeyToListMap<EntitySet, EntitySet> keyToListMap2 = new KeyToListMap<EntitySet, EntitySet>(EqualityComparer<EntitySet>.Default);
			Dictionary<EntitySet, OpCellTreeNode> dictionary = new Dictionary<EntitySet, OpCellTreeNode>(EqualityComparer<EntitySet>.Default);
			foreach (KeyValuePair<EntitySet, List<LeafCellTreeNode>> keyValuePair2 in keyToListMap.KeyValuePairs)
			{
				EntitySet key = keyValuePair2.Key;
				foreach (EntitySet entitySet in BasicViewGenerator.GetFKOverPKDependents(key))
				{
					ReadOnlyCollection<LeafCellTreeNode> readOnlyCollection;
					if (keyToListMap.TryGetListForKey(entitySet, out readOnlyCollection) && (hashSet == null || !hashSet.Contains(readOnlyCollection.Single<LeafCellTreeNode>())))
					{
						keyToListMap2.Add(key, entitySet);
					}
				}
				OpCellTreeNode opCellTreeNode4 = new OpCellTreeNode(this.m_viewgenContext, CellTreeOpType.LOJ);
				opCellTreeNode4.Add(keyValuePair2.Value.Single<LeafCellTreeNode>());
				dictionary.Add(key, opCellTreeNode4);
			}
			Dictionary<EntitySet, EntitySet> dictionary2 = new Dictionary<EntitySet, EntitySet>(EqualityComparer<EntitySet>.Default);
			foreach (KeyValuePair<EntitySet, List<EntitySet>> keyValuePair3 in keyToListMap2.KeyValuePairs)
			{
				EntitySet key2 = keyValuePair3.Key;
				foreach (EntitySet entitySet2 in keyValuePair3.Value)
				{
					OpCellTreeNode opCellTreeNode5;
					if (dictionary.TryGetValue(entitySet2, out opCellTreeNode5) && !dictionary2.ContainsKey(entitySet2) && !BasicViewGenerator.CheckLOJCycle(entitySet2, key2, dictionary2))
					{
						dictionary[keyValuePair3.Key].Add(opCellTreeNode5);
						dictionary2.Add(entitySet2, key2);
					}
				}
			}
			foreach (KeyValuePair<EntitySet, OpCellTreeNode> keyValuePair4 in dictionary)
			{
				if (!dictionary2.ContainsKey(keyValuePair4.Key))
				{
					OpCellTreeNode value = keyValuePair4.Value;
					if (hashSet != null && hashSet.Contains(value.Children[0]))
					{
						opCellTreeNode2.Add(value);
					}
					else
					{
						opCellTreeNode.Add(value);
					}
				}
			}
			return opCellTreeNode.Flatten();
		}

		// Token: 0x06004333 RID: 17203 RVA: 0x000E77D8 File Offset: 0x000E59D8
		private static IEnumerable<EntitySet> GetFKOverPKDependents(EntitySet principal)
		{
			using (IEnumerator<Tuple<AssociationSet, ReferentialConstraint>> enumerator = principal.ForeignKeyPrincipals.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Tuple<AssociationSet, ReferentialConstraint> pkFkInfo = enumerator.Current;
					ReadOnlyMetadataCollection<EdmMember> keyMembers = pkFkInfo.Item2.ToRole.GetEntityType().KeyMembers;
					ReadOnlyMetadataCollection<EdmProperty> toProperties = pkFkInfo.Item2.ToProperties;
					if (keyMembers.Count == toProperties.Count)
					{
						int num = 0;
						while (num < keyMembers.Count && keyMembers[num].EdmEquals(toProperties[num]))
						{
							num++;
						}
						if (num == keyMembers.Count)
						{
							yield return pkFkInfo.Item1.AssociationSetEnds.Where((AssociationSetEnd ase) => ase.Name == pkFkInfo.Item2.ToRole.Name).Single<AssociationSetEnd>().EntitySet;
						}
					}
				}
			}
			IEnumerator<Tuple<AssociationSet, ReferentialConstraint>> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06004334 RID: 17204 RVA: 0x000E77E8 File Offset: 0x000E59E8
		private static EntitySet GetLeafNodeTable(LeafCellTreeNode leaf)
		{
			return leaf.LeftCellWrapper.RightCellQuery.Extent as EntitySet;
		}

		// Token: 0x06004335 RID: 17205 RVA: 0x000E77FF File Offset: 0x000E59FF
		private static bool CheckLOJCycle(EntitySet child, EntitySet parent, Dictionary<EntitySet, EntitySet> nestedExtents)
		{
			while (!EqualityComparer<EntitySet>.Default.Equals(parent, child))
			{
				if (!nestedExtents.TryGetValue(parent, out parent))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06004336 RID: 17206 RVA: 0x000E7820 File Offset: 0x000E5A20
		internal CellTreeNode IsolateByOperator(CellTreeNode rootNode, CellTreeOpType opTypeToIsolate)
		{
			List<CellTreeNode> children = rootNode.Children;
			if (children.Count <= 1)
			{
				return rootNode;
			}
			for (int i = 0; i < children.Count; i++)
			{
				children[i] = this.IsolateByOperator(children[i], opTypeToIsolate);
			}
			if ((rootNode.OpType != CellTreeOpType.FOJ && rootNode.OpType != CellTreeOpType.LOJ) || rootNode.OpType == opTypeToIsolate)
			{
				return rootNode;
			}
			OpCellTreeNode opCellTreeNode = new OpCellTreeNode(this.m_viewgenContext, rootNode.OpType);
			ModifiableIteratorCollection<CellTreeNode> modifiableIteratorCollection = new ModifiableIteratorCollection<CellTreeNode>(children);
			while (!modifiableIteratorCollection.IsEmpty)
			{
				OpCellTreeNode opCellTreeNode2 = new OpCellTreeNode(this.m_viewgenContext, opTypeToIsolate);
				CellTreeNode cellTreeNode = modifiableIteratorCollection.RemoveOneElement();
				opCellTreeNode2.Add(cellTreeNode);
				foreach (CellTreeNode cellTreeNode2 in modifiableIteratorCollection.Elements())
				{
					if (this.TryAddChildToGroup(opTypeToIsolate, cellTreeNode2, opCellTreeNode2))
					{
						modifiableIteratorCollection.RemoveCurrentOfIterator();
						if (opTypeToIsolate == CellTreeOpType.LOJ)
						{
							modifiableIteratorCollection.ResetIterator();
						}
					}
				}
				opCellTreeNode.Add(opCellTreeNode2);
			}
			return opCellTreeNode.Flatten();
		}

		// Token: 0x06004337 RID: 17207 RVA: 0x000E7930 File Offset: 0x000E5B30
		private bool TryAddChildToGroup(CellTreeOpType opTypeToIsolate, CellTreeNode childNode, OpCellTreeNode groupNode)
		{
			switch (opTypeToIsolate)
			{
			case CellTreeOpType.Union:
				if (this.IsDisjoint(childNode, groupNode))
				{
					groupNode.Add(childNode);
					return true;
				}
				break;
			case CellTreeOpType.LOJ:
				if (this.IsContainedIn(childNode, groupNode))
				{
					groupNode.Add(childNode);
					return true;
				}
				if (this.IsContainedIn(groupNode, childNode))
				{
					groupNode.AddFirst(childNode);
					return true;
				}
				break;
			case CellTreeOpType.IJ:
				if (this.IsEquivalentTo(childNode, groupNode))
				{
					groupNode.Add(childNode);
					return true;
				}
				break;
			}
			return false;
		}

		// Token: 0x06004338 RID: 17208 RVA: 0x000E79A4 File Offset: 0x000E5BA4
		private bool IsDisjoint(CellTreeNode n1, CellTreeNode n2)
		{
			bool flag = this.LeftQP.IsDisjointFrom(n1.LeftFragmentQuery, n2.LeftFragmentQuery);
			if (flag && this.m_viewgenContext.ViewTarget == ViewTarget.QueryView)
			{
				return true;
			}
			bool isEmptyRightFragmentQuery = new OpCellTreeNode(this.m_viewgenContext, CellTreeOpType.IJ, new CellTreeNode[] { n1, n2 }).IsEmptyRightFragmentQuery;
			if (this.m_viewgenContext.ViewTarget != ViewTarget.UpdateView || !flag || isEmptyRightFragmentQuery)
			{
				return flag || isEmptyRightFragmentQuery;
			}
			if (ErrorPatternMatcher.FindMappingErrors(this.m_viewgenContext, this.m_domainMap, this.m_errorLog))
			{
				return false;
			}
			StringBuilder stringBuilder = new StringBuilder(Strings.Viewgen_RightSideNotDisjoint(this.m_viewgenContext.Extent.ToString()));
			stringBuilder.AppendLine();
			FragmentQuery fragmentQuery = this.LeftQP.Intersect(n1.RightFragmentQuery, n2.RightFragmentQuery);
			if (this.LeftQP.IsSatisfiable(fragmentQuery))
			{
				fragmentQuery.Condition.ExpensiveSimplify();
				RewritingValidator.EntityConfigurationToUserString(fragmentQuery.Condition, stringBuilder);
			}
			this.m_errorLog.AddEntry(new ErrorLog.Record(ViewGenErrorCode.DisjointConstraintViolation, stringBuilder.ToString(), this.m_viewgenContext.AllWrappersForExtent, string.Empty));
			ExceptionHelpers.ThrowMappingException(this.m_errorLog, this.m_config);
			return false;
		}

		// Token: 0x06004339 RID: 17209 RVA: 0x000E7AD4 File Offset: 0x000E5CD4
		private bool IsContainedIn(CellTreeNode n1, CellTreeNode n2)
		{
			FragmentQuery fragmentQuery = this.LeftQP.Intersect(n1.LeftFragmentQuery, this.m_activeDomain);
			FragmentQuery fragmentQuery2 = this.LeftQP.Intersect(n2.LeftFragmentQuery, this.m_activeDomain);
			return this.LeftQP.IsContainedIn(fragmentQuery, fragmentQuery2) || new OpCellTreeNode(this.m_viewgenContext, CellTreeOpType.LASJ, new CellTreeNode[] { n1, n2 }).IsEmptyRightFragmentQuery;
		}

		// Token: 0x0600433A RID: 17210 RVA: 0x000E7B41 File Offset: 0x000E5D41
		private bool IsEquivalentTo(CellTreeNode n1, CellTreeNode n2)
		{
			return this.IsContainedIn(n1, n2) && this.IsContainedIn(n2, n1);
		}

		// Token: 0x0600433B RID: 17211 RVA: 0x000E7B57 File Offset: 0x000E5D57
		internal override void ToCompactString(StringBuilder builder)
		{
			this.m_projectedSlotMap.ToCompactString(builder);
		}

		// Token: 0x040017FC RID: 6140
		private readonly MemberProjectionIndex m_projectedSlotMap;

		// Token: 0x040017FD RID: 6141
		private readonly List<LeftCellWrapper> m_usedCells;

		// Token: 0x040017FE RID: 6142
		private readonly FragmentQuery m_activeDomain;

		// Token: 0x040017FF RID: 6143
		private readonly ViewgenContext m_viewgenContext;

		// Token: 0x04001800 RID: 6144
		private readonly ErrorLog m_errorLog;

		// Token: 0x04001801 RID: 6145
		private readonly ConfigViewGenerator m_config;

		// Token: 0x04001802 RID: 6146
		private readonly MemberDomainMap m_domainMap;
	}
}
