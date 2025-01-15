using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Query.InternalTrees;
using System.Linq;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x0200034B RID: 843
	internal class JoinGraph
	{
		// Token: 0x0600285F RID: 10335 RVA: 0x00079EC4 File Offset: 0x000780C4
		internal JoinGraph(Command command, ConstraintManager constraintManager, VarRefManager varRefManager, Node joinNode)
		{
			this.m_command = command;
			this.m_constraintManager = constraintManager;
			this.m_varRefManager = varRefManager;
			this.m_vertexes = new List<AugmentedNode>();
			this.m_tableVertexMap = new Dictionary<Table, AugmentedTableNode>();
			this.m_varMap = new VarMap();
			this.m_reverseVarMap = new Dictionary<Var, VarVec>();
			this.m_varToDefiningNodeMap = new Dictionary<Var, AugmentedTableNode>();
			this.m_processedNodes = new Dictionary<Node, Node>();
			this.m_root = this.BuildAugmentedNodeTree(joinNode) as AugmentedJoinNode;
			PlanCompiler.Assert(this.m_root != null, "The root isn't a join?");
			this.BuildJoinEdges(this.m_root, this.m_root.Id);
		}

		// Token: 0x06002860 RID: 10336 RVA: 0x00079F6B File Offset: 0x0007816B
		internal Node DoJoinElimination(out VarMap varMap, out Dictionary<Node, Node> processedNodes)
		{
			this.TryTurnLeftOuterJoinsIntoInnerJoins();
			this.GenerateTransitiveEdges();
			this.EliminateSelfJoins();
			this.EliminateParentChildJoins();
			Node node = this.BuildNodeTree();
			varMap = this.m_varMap;
			processedNodes = this.m_processedNodes;
			return node;
		}

		// Token: 0x06002861 RID: 10337 RVA: 0x00079F9C File Offset: 0x0007819C
		private VarVec GetColumnVars(VarVec varVec)
		{
			VarVec varVec2 = this.m_command.CreateVarVec();
			foreach (Var var in varVec)
			{
				if (var.VarType == VarType.Column)
				{
					varVec2.Set(var);
				}
			}
			return varVec2;
		}

		// Token: 0x06002862 RID: 10338 RVA: 0x00079FFC File Offset: 0x000781FC
		private static void GetColumnVars(List<ColumnVar> columnVars, IEnumerable<Var> vec)
		{
			foreach (Var var in vec)
			{
				PlanCompiler.Assert(var.VarType == VarType.Column, "Expected a columnVar. Found " + var.VarType.ToString());
				columnVars.Add((ColumnVar)var);
			}
		}

		// Token: 0x06002863 RID: 10339 RVA: 0x0007A078 File Offset: 0x00078278
		private void SplitPredicate(Node joinNode, out List<ColumnVar> leftVars, out List<ColumnVar> rightVars, out Node otherPredicateNode)
		{
			leftVars = new List<ColumnVar>();
			rightVars = new List<ColumnVar>();
			otherPredicateNode = joinNode.Child2;
			if (joinNode.Op.OpType == OpType.FullOuterJoin)
			{
				return;
			}
			Predicate predicate = new Predicate(this.m_command, joinNode.Child2);
			ExtendedNodeInfo extendedNodeInfo = this.m_command.GetExtendedNodeInfo(joinNode.Child0);
			ExtendedNodeInfo extendedNodeInfo2 = this.m_command.GetExtendedNodeInfo(joinNode.Child1);
			VarVec columnVars = this.GetColumnVars(extendedNodeInfo.Definitions);
			VarVec columnVars2 = this.GetColumnVars(extendedNodeInfo2.Definitions);
			List<Var> list;
			List<Var> list2;
			Predicate predicate2;
			predicate.GetEquiJoinPredicates(columnVars, columnVars2, out list, out list2, out predicate2);
			otherPredicateNode = predicate2.BuildAndTree();
			JoinGraph.GetColumnVars(leftVars, list);
			JoinGraph.GetColumnVars(rightVars, list2);
		}

		// Token: 0x06002864 RID: 10340 RVA: 0x0007A124 File Offset: 0x00078324
		private AugmentedNode BuildAugmentedNodeTree(Node node)
		{
			AugmentedNode augmentedNode;
			switch (node.Op.OpType)
			{
			case OpType.ScanTable:
			{
				this.m_processedNodes[node] = node;
				ScanTableOp scanTableOp = (ScanTableOp)node.Op;
				augmentedNode = new AugmentedTableNode(this.m_vertexes.Count, node);
				this.m_tableVertexMap[scanTableOp.Table] = (AugmentedTableNode)augmentedNode;
				goto IL_016A;
			}
			case OpType.InnerJoin:
			case OpType.LeftOuterJoin:
			case OpType.FullOuterJoin:
			{
				this.m_processedNodes[node] = node;
				AugmentedNode augmentedNode2 = this.BuildAugmentedNodeTree(node.Child0);
				AugmentedNode augmentedNode3 = this.BuildAugmentedNodeTree(node.Child1);
				List<ColumnVar> list;
				List<ColumnVar> list2;
				Node node2;
				this.SplitPredicate(node, out list, out list2, out node2);
				this.m_varRefManager.AddChildren(node);
				augmentedNode = new AugmentedJoinNode(this.m_vertexes.Count, node, augmentedNode2, augmentedNode3, list, list2, node2);
				goto IL_016A;
			}
			case OpType.CrossJoin:
			{
				this.m_processedNodes[node] = node;
				List<AugmentedNode> list3 = new List<AugmentedNode>();
				foreach (Node node3 in node.Children)
				{
					list3.Add(this.BuildAugmentedNodeTree(node3));
				}
				augmentedNode = new AugmentedJoinNode(this.m_vertexes.Count, node, list3);
				this.m_varRefManager.AddChildren(node);
				goto IL_016A;
			}
			}
			augmentedNode = new AugmentedNode(this.m_vertexes.Count, node);
			IL_016A:
			this.m_vertexes.Add(augmentedNode);
			return augmentedNode;
		}

		// Token: 0x06002865 RID: 10341 RVA: 0x0007A2B8 File Offset: 0x000784B8
		private bool AddJoinEdge(AugmentedJoinNode joinNode, ColumnVar leftVar, ColumnVar rightVar)
		{
			AugmentedTableNode augmentedTableNode;
			if (!this.m_tableVertexMap.TryGetValue(leftVar.Table, out augmentedTableNode))
			{
				return false;
			}
			AugmentedTableNode augmentedTableNode2;
			if (!this.m_tableVertexMap.TryGetValue(rightVar.Table, out augmentedTableNode2))
			{
				return false;
			}
			foreach (JoinEdge joinEdge in augmentedTableNode.JoinEdges)
			{
				if (joinEdge.Right.Table.Equals(rightVar.Table))
				{
					return joinEdge.AddCondition(joinNode, leftVar, rightVar);
				}
			}
			JoinEdge joinEdge2 = JoinEdge.CreateJoinEdge(augmentedTableNode, augmentedTableNode2, joinNode, leftVar, rightVar);
			augmentedTableNode.JoinEdges.Add(joinEdge2);
			joinNode.JoinEdges.Add(joinEdge2);
			return true;
		}

		// Token: 0x06002866 RID: 10342 RVA: 0x0007A384 File Offset: 0x00078584
		private static bool SingleTableVars(IEnumerable<ColumnVar> varList)
		{
			Table table = null;
			foreach (ColumnVar columnVar in varList)
			{
				if (table == null)
				{
					table = columnVar.Table;
				}
				else if (columnVar.Table != table)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002867 RID: 10343 RVA: 0x0007A3E4 File Offset: 0x000785E4
		private void BuildJoinEdges(AugmentedJoinNode joinNode, int maxVisibility)
		{
			OpType opType = joinNode.Node.Op.OpType;
			if (opType == OpType.CrossJoin)
			{
				foreach (AugmentedNode augmentedNode in joinNode.Children)
				{
					this.BuildJoinEdges(augmentedNode, maxVisibility);
				}
				return;
			}
			int num;
			int num2;
			if (opType == OpType.FullOuterJoin)
			{
				num = joinNode.Id;
				num2 = joinNode.Id;
			}
			else if (opType == OpType.LeftOuterJoin)
			{
				num = maxVisibility;
				num2 = joinNode.Id;
			}
			else
			{
				num = maxVisibility;
				num2 = maxVisibility;
			}
			this.BuildJoinEdges(joinNode.Children[0], num);
			this.BuildJoinEdges(joinNode.Children[1], num2);
			if (joinNode.Node.Op.OpType == OpType.FullOuterJoin || joinNode.LeftVars.Count == 0)
			{
				return;
			}
			if (opType == OpType.LeftOuterJoin && (!JoinGraph.SingleTableVars(joinNode.RightVars) || !JoinGraph.SingleTableVars(joinNode.LeftVars)))
			{
				return;
			}
			JoinKind joinKind = ((opType == OpType.LeftOuterJoin) ? JoinKind.LeftOuter : JoinKind.Inner);
			for (int i = 0; i < joinNode.LeftVars.Count; i++)
			{
				if (this.AddJoinEdge(joinNode, joinNode.LeftVars[i], joinNode.RightVars[i]) && joinKind == JoinKind.Inner)
				{
					this.AddJoinEdge(joinNode, joinNode.RightVars[i], joinNode.LeftVars[i]);
				}
			}
		}

		// Token: 0x06002868 RID: 10344 RVA: 0x0007A550 File Offset: 0x00078750
		private void BuildJoinEdges(AugmentedNode node, int maxVisibility)
		{
			OpType opType = node.Node.Op.OpType;
			if (opType != OpType.ScanTable)
			{
				if (opType - OpType.InnerJoin <= 3)
				{
					this.BuildJoinEdges(node as AugmentedJoinNode, maxVisibility);
					return;
				}
			}
			else
			{
				((AugmentedTableNode)node).LastVisibleId = maxVisibility;
			}
		}

		// Token: 0x06002869 RID: 10345 RVA: 0x0007A594 File Offset: 0x00078794
		private static bool GenerateTransitiveEdge(JoinEdge edge1, JoinEdge edge2)
		{
			PlanCompiler.Assert(edge1.Right == edge2.Left, "need a common table for transitive predicate generation");
			if (edge1.RestrictedElimination || edge2.RestrictedElimination)
			{
				return false;
			}
			if (edge2.Right == edge1.Left)
			{
				return false;
			}
			if (edge1.JoinKind != edge2.JoinKind)
			{
				return false;
			}
			if (edge1.JoinKind == JoinKind.LeftOuter && (edge1.Left != edge1.Right || edge2.Left != edge2.Right))
			{
				return false;
			}
			if (edge1.JoinKind == JoinKind.LeftOuter && edge1.RightVars.Count != edge2.LeftVars.Count)
			{
				return false;
			}
			using (List<JoinEdge>.Enumerator enumerator = edge1.Left.JoinEdges.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Right == edge2.Right)
					{
						return false;
					}
				}
			}
			IEnumerable<KeyValuePair<ColumnVar, ColumnVar>> enumerable = JoinGraph.CreateOrderedKeyValueList(edge1.RightVars, edge1.LeftVars);
			IEnumerable<KeyValuePair<ColumnVar, ColumnVar>> enumerable2 = JoinGraph.CreateOrderedKeyValueList(edge2.LeftVars, edge2.RightVars);
			IEnumerator<KeyValuePair<ColumnVar, ColumnVar>> enumerator2 = enumerable.GetEnumerator();
			IEnumerator<KeyValuePair<ColumnVar, ColumnVar>> enumerator3 = enumerable2.GetEnumerator();
			List<ColumnVar> list = new List<ColumnVar>();
			List<ColumnVar> list2 = new List<ColumnVar>();
			bool flag = enumerator2.MoveNext() && enumerator3.MoveNext();
			while (flag)
			{
				KeyValuePair<ColumnVar, ColumnVar> keyValuePair = enumerator2.Current;
				ColumnVar key = keyValuePair.Key;
				keyValuePair = enumerator3.Current;
				if (key == keyValuePair.Key)
				{
					List<ColumnVar> list3 = list;
					keyValuePair = enumerator2.Current;
					list3.Add(keyValuePair.Value);
					List<ColumnVar> list4 = list2;
					keyValuePair = enumerator3.Current;
					list4.Add(keyValuePair.Value);
					flag = enumerator2.MoveNext() && enumerator3.MoveNext();
				}
				else
				{
					if (edge1.JoinKind == JoinKind.LeftOuter)
					{
						return false;
					}
					keyValuePair = enumerator2.Current;
					int id = keyValuePair.Key.Id;
					keyValuePair = enumerator3.Current;
					if (id > keyValuePair.Key.Id)
					{
						flag = enumerator3.MoveNext();
					}
					else
					{
						flag = enumerator2.MoveNext();
					}
				}
			}
			JoinEdge joinEdge = JoinEdge.CreateTransitiveJoinEdge(edge1.Left, edge2.Right, edge1.JoinKind, list, list2);
			edge1.Left.JoinEdges.Add(joinEdge);
			if (edge1.JoinKind == JoinKind.Inner)
			{
				JoinEdge joinEdge2 = JoinEdge.CreateTransitiveJoinEdge(edge2.Right, edge1.Left, edge1.JoinKind, list2, list);
				edge2.Right.JoinEdges.Add(joinEdge2);
			}
			return true;
		}

		// Token: 0x0600286A RID: 10346 RVA: 0x0007A7FC File Offset: 0x000789FC
		private static IEnumerable<KeyValuePair<ColumnVar, ColumnVar>> CreateOrderedKeyValueList(List<ColumnVar> keyVars, List<ColumnVar> valueVars)
		{
			List<KeyValuePair<ColumnVar, ColumnVar>> list = new List<KeyValuePair<ColumnVar, ColumnVar>>(keyVars.Count);
			for (int i = 0; i < keyVars.Count; i++)
			{
				list.Add(new KeyValuePair<ColumnVar, ColumnVar>(keyVars[i], valueVars[i]));
			}
			return list.OrderBy((KeyValuePair<ColumnVar, ColumnVar> kv) => kv.Key.Id);
		}

		// Token: 0x0600286B RID: 10347 RVA: 0x0007A864 File Offset: 0x00078A64
		private void TryTurnLeftOuterJoinsIntoInnerJoins()
		{
			foreach (AugmentedJoinNode augmentedJoinNode in from j in this.m_vertexes.OfType<AugmentedJoinNode>()
				where j.Node.Op.OpType == OpType.LeftOuterJoin && j.JoinEdges.Count > 0
				select j)
			{
				if (this.CanAllJoinEdgesBeTurnedIntoInnerJoins(augmentedJoinNode.Children[1], augmentedJoinNode.JoinEdges))
				{
					augmentedJoinNode.Node.Op = this.m_command.CreateInnerJoinOp();
					this.m_modifiedGraph = true;
					List<JoinEdge> list = new List<JoinEdge>(augmentedJoinNode.JoinEdges.Count);
					foreach (JoinEdge joinEdge in augmentedJoinNode.JoinEdges)
					{
						joinEdge.JoinKind = JoinKind.Inner;
						if (!JoinGraph.ContainsJoinEdgeForTable(joinEdge.Right.JoinEdges, joinEdge.Left.Table))
						{
							JoinEdge joinEdge2 = JoinEdge.CreateJoinEdge(joinEdge.Right, joinEdge.Left, augmentedJoinNode, joinEdge.RightVars[0], joinEdge.LeftVars[0]);
							joinEdge.Right.JoinEdges.Add(joinEdge2);
							list.Add(joinEdge2);
							for (int i = 1; i < joinEdge.LeftVars.Count; i++)
							{
								joinEdge2.AddCondition(augmentedJoinNode, joinEdge.RightVars[i], joinEdge.LeftVars[i]);
							}
						}
					}
					augmentedJoinNode.JoinEdges.AddRange(list);
				}
			}
		}

		// Token: 0x0600286C RID: 10348 RVA: 0x0007AA40 File Offset: 0x00078C40
		private static bool AreAllTableRowsPreserved(AugmentedNode root, AugmentedTableNode table)
		{
			if (root is AugmentedTableNode)
			{
				return true;
			}
			AugmentedNode augmentedNode = table;
			for (;;)
			{
				AugmentedJoinNode augmentedJoinNode = (AugmentedJoinNode)augmentedNode.Parent;
				if (augmentedJoinNode.Node.Op.OpType != OpType.LeftOuterJoin || augmentedJoinNode.Children[0] != augmentedNode)
				{
					break;
				}
				augmentedNode = augmentedJoinNode;
				if (augmentedNode == root)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600286D RID: 10349 RVA: 0x0007AA94 File Offset: 0x00078C94
		private static bool ContainsJoinEdgeForTable(IEnumerable<JoinEdge> joinEdges, Table table)
		{
			using (IEnumerator<JoinEdge> enumerator = joinEdges.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Right.Table.Equals(table))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600286E RID: 10350 RVA: 0x0007AAF0 File Offset: 0x00078CF0
		private bool CanAllJoinEdgesBeTurnedIntoInnerJoins(AugmentedNode rightNode, IEnumerable<JoinEdge> joinEdges)
		{
			foreach (JoinEdge joinEdge in joinEdges)
			{
				if (!this.CanJoinEdgeBeTurnedIntoInnerJoin(rightNode, joinEdge))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600286F RID: 10351 RVA: 0x0007AB44 File Offset: 0x00078D44
		private bool CanJoinEdgeBeTurnedIntoInnerJoin(AugmentedNode rightNode, JoinEdge joinEdge)
		{
			return !joinEdge.RestrictedElimination && JoinGraph.AreAllTableRowsPreserved(rightNode, joinEdge.Right) && this.IsConstraintPresentForTurningIntoInnerJoin(joinEdge);
		}

		// Token: 0x06002870 RID: 10352 RVA: 0x0007AB68 File Offset: 0x00078D68
		private bool IsConstraintPresentForTurningIntoInnerJoin(JoinEdge joinEdge)
		{
			List<ForeignKeyConstraint> list;
			if (this.m_constraintManager.IsParentChildRelationship(joinEdge.Right.Table.TableMetadata.Extent, joinEdge.Left.Table.TableMetadata.Extent, out list))
			{
				PlanCompiler.Assert(list != null && list.Count > 0, "Invalid foreign key constraints");
				foreach (ForeignKeyConstraint foreignKeyConstraint in list)
				{
					IList<ColumnVar> list2;
					if (JoinGraph.IsJoinOnFkConstraint(foreignKeyConstraint, joinEdge.RightVars, joinEdge.LeftVars, out list2) && foreignKeyConstraint.ParentKeys.Count == joinEdge.RightVars.Count)
					{
						if (list2.Where((ColumnVar v) => v.ColumnMetadata.IsNullable).Count<ColumnVar>() == 0)
						{
							return true;
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06002871 RID: 10353 RVA: 0x0007AC68 File Offset: 0x00078E68
		private void GenerateTransitiveEdges()
		{
			foreach (AugmentedNode augmentedNode in this.m_vertexes)
			{
				AugmentedTableNode augmentedTableNode = augmentedNode as AugmentedTableNode;
				if (augmentedTableNode != null)
				{
					for (int i = 0; i < augmentedTableNode.JoinEdges.Count; i++)
					{
						JoinEdge joinEdge = augmentedTableNode.JoinEdges[i];
						int j = 0;
						AugmentedTableNode right = joinEdge.Right;
						while (j < right.JoinEdges.Count)
						{
							JoinEdge joinEdge2 = right.JoinEdges[j];
							JoinGraph.GenerateTransitiveEdge(joinEdge, joinEdge2);
							j++;
						}
					}
				}
			}
		}

		// Token: 0x06002872 RID: 10354 RVA: 0x0007AD1C File Offset: 0x00078F1C
		private static bool CanBeEliminatedBasedOnLojParticipation(AugmentedTableNode table, AugmentedTableNode replacingTable)
		{
			if (replacingTable.Id < table.NewLocationId)
			{
				return JoinGraph.CanBeMovedBasedOnLojParticipation(table, replacingTable);
			}
			return JoinGraph.CanBeMovedBasedOnLojParticipation(replacingTable, table);
		}

		// Token: 0x06002873 RID: 10355 RVA: 0x0007AD3C File Offset: 0x00078F3C
		private static bool CanBeEliminatedViaStarJoinBasedOnOtherJoinParticipation(JoinEdge tableJoinEdge, JoinEdge replacingTableJoinEdge)
		{
			if (tableJoinEdge.JoinNode == null || replacingTableJoinEdge.JoinNode == null)
			{
				return false;
			}
			AugmentedNode leastCommonAncestor = JoinGraph.GetLeastCommonAncestor(tableJoinEdge.Right, replacingTableJoinEdge.Right);
			return !JoinGraph.CanGetFileredByJoins(tableJoinEdge, leastCommonAncestor, true) && !JoinGraph.CanGetFileredByJoins(replacingTableJoinEdge, leastCommonAncestor, false);
		}

		// Token: 0x06002874 RID: 10356 RVA: 0x0007AD84 File Offset: 0x00078F84
		private static bool CanGetFileredByJoins(JoinEdge joinEdge, AugmentedNode leastCommonAncestor, bool disallowAnyJoin)
		{
			AugmentedNode augmentedNode = joinEdge.Right;
			AugmentedNode augmentedNode2 = augmentedNode.Parent;
			while (augmentedNode2 != null && augmentedNode != leastCommonAncestor)
			{
				if (augmentedNode2.Node != joinEdge.JoinNode.Node && (disallowAnyJoin || augmentedNode2.Node.Op.OpType != OpType.LeftOuterJoin || augmentedNode2.Children[0] != augmentedNode))
				{
					return true;
				}
				augmentedNode = augmentedNode.Parent;
				augmentedNode2 = augmentedNode.Parent;
			}
			return false;
		}

		// Token: 0x06002875 RID: 10357 RVA: 0x0007ADF4 File Offset: 0x00078FF4
		private static bool CanBeMovedBasedOnLojParticipation(AugmentedTableNode table, AugmentedTableNode replacingTable)
		{
			AugmentedNode leastCommonAncestor = JoinGraph.GetLeastCommonAncestor(table, replacingTable);
			AugmentedNode augmentedNode = table;
			while (augmentedNode.Parent != null && augmentedNode != leastCommonAncestor)
			{
				if (augmentedNode.Parent.Node.Op.OpType == OpType.LeftOuterJoin && augmentedNode.Parent.Children[0] == augmentedNode)
				{
					return false;
				}
				augmentedNode = augmentedNode.Parent;
			}
			return true;
		}

		// Token: 0x06002876 RID: 10358 RVA: 0x0007AE50 File Offset: 0x00079050
		private static AugmentedNode GetLeastCommonAncestor(AugmentedNode node1, AugmentedNode node2)
		{
			if (node1.Id == node2.Id)
			{
				return node1;
			}
			AugmentedNode augmentedNode;
			AugmentedNode augmentedNode2;
			if (node1.Id < node2.Id)
			{
				augmentedNode = node1;
				augmentedNode2 = node2;
			}
			else
			{
				augmentedNode = node2;
				augmentedNode2 = node1;
			}
			while (augmentedNode.Id < augmentedNode2.Id)
			{
				augmentedNode = augmentedNode.Parent;
			}
			return augmentedNode;
		}

		// Token: 0x06002877 RID: 10359 RVA: 0x0007AEA0 File Offset: 0x000790A0
		private void MarkTableAsEliminated<T>(AugmentedTableNode tableNode, AugmentedTableNode replacementNode, List<T> tableVars, List<T> replacementVars) where T : Var
		{
			PlanCompiler.Assert(tableVars != null && replacementVars != null, "null vars");
			PlanCompiler.Assert(tableVars.Count == replacementVars.Count, "var count mismatch");
			PlanCompiler.Assert(tableVars.Count > 0, "no vars in the table ?");
			this.m_modifiedGraph = true;
			if (tableNode.Id < replacementNode.NewLocationId)
			{
				tableNode.ReplacementTable = replacementNode;
				replacementNode.NewLocationId = tableNode.Id;
			}
			else
			{
				tableNode.ReplacementTable = null;
			}
			for (int i = 0; i < tableVars.Count; i++)
			{
				if (tableNode.Table.ReferencedColumns.IsSet(tableVars[i]))
				{
					this.m_varMap[tableVars[i]] = replacementVars[i];
					this.AddReverseMapping(replacementVars[i], tableVars[i]);
					replacementNode.Table.ReferencedColumns.Set(replacementVars[i]);
				}
			}
			foreach (Var var in replacementNode.Table.ReferencedColumns)
			{
				this.m_varToDefiningNodeMap[var] = replacementNode;
			}
		}

		// Token: 0x06002878 RID: 10360 RVA: 0x0007B000 File Offset: 0x00079200
		private void AddReverseMapping(Var replacingVar, Var replacedVar)
		{
			VarVec varVec;
			if (this.m_reverseVarMap.TryGetValue(replacedVar, out varVec))
			{
				this.m_reverseVarMap.Remove(replacedVar);
			}
			VarVec varVec2;
			if (!this.m_reverseVarMap.TryGetValue(replacingVar, out varVec2))
			{
				if (varVec != null)
				{
					varVec2 = varVec;
				}
				else
				{
					varVec2 = this.m_command.CreateVarVec();
				}
				this.m_reverseVarMap[replacingVar] = varVec2;
			}
			else if (varVec != null)
			{
				varVec2.Or(varVec);
			}
			varVec2.Set(replacedVar);
		}

		// Token: 0x06002879 RID: 10361 RVA: 0x0007B06D File Offset: 0x0007926D
		private void EliminateSelfJoinedTable(AugmentedTableNode tableNode, AugmentedTableNode replacementNode)
		{
			this.MarkTableAsEliminated<Var>(tableNode, replacementNode, tableNode.Table.Columns, replacementNode.Table.Columns);
		}

		// Token: 0x0600287A RID: 10362 RVA: 0x0007B090 File Offset: 0x00079290
		private void EliminateStarSelfJoin(List<JoinEdge> joinEdges)
		{
			List<List<JoinEdge>> list = new List<List<JoinEdge>>();
			foreach (JoinEdge joinEdge in joinEdges)
			{
				bool flag = false;
				foreach (List<JoinEdge> list2 in list)
				{
					if (JoinGraph.AreMatchingForStarSelfJoinElimination(list2[0], joinEdge))
					{
						list2.Add(joinEdge);
						flag = true;
						break;
					}
				}
				if (!flag && this.QualifiesForStarSelfJoinGroup(joinEdge))
				{
					list.Add(new List<JoinEdge> { joinEdge });
				}
			}
			foreach (List<JoinEdge> list3 in list.Where((List<JoinEdge> l) => l.Count > 1))
			{
				JoinEdge joinEdge2 = list3[0];
				foreach (JoinEdge joinEdge3 in list3)
				{
					if (joinEdge2.Right.Id > joinEdge3.Right.Id)
					{
						joinEdge2 = joinEdge3;
					}
				}
				foreach (JoinEdge joinEdge4 in list3)
				{
					if (joinEdge4 != joinEdge2 && JoinGraph.CanBeEliminatedViaStarJoinBasedOnOtherJoinParticipation(joinEdge4, joinEdge2))
					{
						this.EliminateSelfJoinedTable(joinEdge4.Right, joinEdge2.Right);
					}
				}
			}
		}

		// Token: 0x0600287B RID: 10363 RVA: 0x0007B274 File Offset: 0x00079474
		private static bool AreMatchingForStarSelfJoinElimination(JoinEdge edge1, JoinEdge edge2)
		{
			if (edge2.LeftVars.Count != edge1.LeftVars.Count || edge2.JoinKind != edge1.JoinKind)
			{
				return false;
			}
			for (int i = 0; i < edge2.LeftVars.Count; i++)
			{
				if (!edge2.LeftVars[i].Equals(edge1.LeftVars[i]) || !edge2.RightVars[i].ColumnMetadata.Name.Equals(edge1.RightVars[i].ColumnMetadata.Name))
				{
					return false;
				}
			}
			return JoinGraph.MatchOtherPredicates(edge1, edge2);
		}

		// Token: 0x0600287C RID: 10364 RVA: 0x0007B31C File Offset: 0x0007951C
		private static bool MatchOtherPredicates(JoinEdge edge1, JoinEdge edge2)
		{
			if (edge1.JoinNode == null)
			{
				return edge2.JoinNode == null;
			}
			if (edge2.JoinNode == null)
			{
				return false;
			}
			if (edge1.JoinNode.OtherPredicate == null)
			{
				return edge2.JoinNode.OtherPredicate == null;
			}
			return edge2.JoinNode.OtherPredicate != null && JoinGraph.MatchOtherPredicates(edge1.JoinNode.OtherPredicate, edge2.JoinNode.OtherPredicate);
		}

		// Token: 0x0600287D RID: 10365 RVA: 0x0007B38C File Offset: 0x0007958C
		private static bool MatchOtherPredicates(Node x, Node y)
		{
			if (x.Children.Count != y.Children.Count)
			{
				return false;
			}
			if (x.Op.IsEquivalent(y.Op))
			{
				return !x.Children.Where((Node t, int i) => !JoinGraph.MatchOtherPredicates(t, y.Children[i])).Any<Node>();
			}
			VarRefOp varRefOp = x.Op as VarRefOp;
			if (varRefOp == null)
			{
				return false;
			}
			VarRefOp varRefOp2 = y.Op as VarRefOp;
			if (varRefOp2 == null)
			{
				return false;
			}
			ColumnVar columnVar = varRefOp.Var as ColumnVar;
			if (columnVar == null)
			{
				return false;
			}
			ColumnVar columnVar2 = varRefOp2.Var as ColumnVar;
			return columnVar2 != null && columnVar.ColumnMetadata.Name.Equals(columnVar2.ColumnMetadata.Name);
		}

		// Token: 0x0600287E RID: 10366 RVA: 0x0007B464 File Offset: 0x00079664
		private bool QualifiesForStarSelfJoinGroup(JoinEdge joinEdge)
		{
			VarVec varVec = this.m_command.CreateVarVec(joinEdge.Right.Table.Keys);
			foreach (Var var in joinEdge.RightVars)
			{
				if (joinEdge.JoinKind == JoinKind.LeftOuter && !varVec.IsSet(var))
				{
					return false;
				}
				varVec.Clear(var);
			}
			return varVec.IsEmpty && (joinEdge.JoinNode == null || joinEdge.JoinNode.OtherPredicate == null || JoinGraph.QualifiesForStarSelfJoinGroup(joinEdge.JoinNode.OtherPredicate, this.m_command.GetExtendedNodeInfo(joinEdge.Right.Node).Definitions));
		}

		// Token: 0x0600287F RID: 10367 RVA: 0x0007B538 File Offset: 0x00079738
		private static bool QualifiesForStarSelfJoinGroup(Node otherPredicateNode, VarVec rightTableColumnVars)
		{
			VarRefOp varRefOp = otherPredicateNode.Op as VarRefOp;
			if (varRefOp == null)
			{
				return true;
			}
			ColumnVar columnVar = varRefOp.Var as ColumnVar;
			return columnVar == null || (rightTableColumnVars.IsSet(columnVar) && otherPredicateNode.Children.All((Node node) => JoinGraph.QualifiesForStarSelfJoinGroup(node, rightTableColumnVars)));
		}

		// Token: 0x06002880 RID: 10368 RVA: 0x0007B59C File Offset: 0x0007979C
		private void EliminateStarSelfJoins(AugmentedTableNode tableNode)
		{
			Dictionary<EntitySetBase, List<JoinEdge>> dictionary = new Dictionary<EntitySetBase, List<JoinEdge>>();
			foreach (JoinEdge joinEdge in tableNode.JoinEdges)
			{
				if (!joinEdge.IsEliminated)
				{
					List<JoinEdge> list;
					if (!dictionary.TryGetValue(joinEdge.Right.Table.TableMetadata.Extent, out list))
					{
						list = new List<JoinEdge>();
						dictionary[joinEdge.Right.Table.TableMetadata.Extent] = list;
					}
					list.Add(joinEdge);
				}
			}
			foreach (KeyValuePair<EntitySetBase, List<JoinEdge>> keyValuePair in dictionary)
			{
				if (keyValuePair.Value.Count > 1)
				{
					this.EliminateStarSelfJoin(keyValuePair.Value);
				}
			}
		}

		// Token: 0x06002881 RID: 10369 RVA: 0x0007B694 File Offset: 0x00079894
		private bool EliminateSelfJoin(JoinEdge joinEdge)
		{
			if (joinEdge.RestrictedElimination)
			{
				return false;
			}
			if (joinEdge.IsEliminated)
			{
				return false;
			}
			if (!joinEdge.Left.Table.TableMetadata.Extent.Equals(joinEdge.Right.Table.TableMetadata.Extent))
			{
				return false;
			}
			for (int i = 0; i < joinEdge.LeftVars.Count; i++)
			{
				if (!joinEdge.LeftVars[i].ColumnMetadata.Name.Equals(joinEdge.RightVars[i].ColumnMetadata.Name))
				{
					return false;
				}
			}
			VarVec varVec = this.m_command.CreateVarVec(joinEdge.Left.Table.Keys);
			foreach (Var var in joinEdge.LeftVars)
			{
				if (joinEdge.JoinKind == JoinKind.LeftOuter && !varVec.IsSet(var))
				{
					return false;
				}
				varVec.Clear(var);
			}
			if (!varVec.IsEmpty)
			{
				return false;
			}
			if (!JoinGraph.CanBeEliminatedBasedOnLojParticipation(joinEdge.Right, joinEdge.Left))
			{
				return false;
			}
			this.EliminateSelfJoinedTable(joinEdge.Right, joinEdge.Left);
			return true;
		}

		// Token: 0x06002882 RID: 10370 RVA: 0x0007B7E4 File Offset: 0x000799E4
		private void EliminateSelfJoins(AugmentedTableNode tableNode)
		{
			if (tableNode.IsEliminated)
			{
				return;
			}
			foreach (JoinEdge joinEdge in tableNode.JoinEdges)
			{
				this.EliminateSelfJoin(joinEdge);
			}
		}

		// Token: 0x06002883 RID: 10371 RVA: 0x0007B844 File Offset: 0x00079A44
		private void EliminateSelfJoins()
		{
			foreach (AugmentedNode augmentedNode in this.m_vertexes)
			{
				AugmentedTableNode augmentedTableNode = augmentedNode as AugmentedTableNode;
				if (augmentedTableNode != null)
				{
					this.EliminateSelfJoins(augmentedTableNode);
					this.EliminateStarSelfJoins(augmentedTableNode);
				}
			}
		}

		// Token: 0x06002884 RID: 10372 RVA: 0x0007B8A8 File Offset: 0x00079AA8
		private void EliminateLeftTable(JoinEdge joinEdge)
		{
			PlanCompiler.Assert(joinEdge.JoinKind == JoinKind.Inner, "Expected inner join");
			this.MarkTableAsEliminated<ColumnVar>(joinEdge.Left, joinEdge.Right, joinEdge.LeftVars, joinEdge.RightVars);
			if (joinEdge.Right.NullableColumns == null)
			{
				joinEdge.Right.NullableColumns = this.m_command.CreateVarVec();
			}
			foreach (ColumnVar columnVar in joinEdge.RightVars)
			{
				if (columnVar.ColumnMetadata.IsNullable)
				{
					joinEdge.Right.NullableColumns.Set(columnVar);
				}
			}
		}

		// Token: 0x06002885 RID: 10373 RVA: 0x0007B968 File Offset: 0x00079B68
		private void EliminateRightTable(JoinEdge joinEdge)
		{
			PlanCompiler.Assert(joinEdge.JoinKind == JoinKind.LeftOuter, "Expected left-outer-join");
			PlanCompiler.Assert(joinEdge.Left.Id < joinEdge.Right.Id, string.Concat(new string[]
			{
				"(left-id, right-id) = (",
				joinEdge.Left.Id.ToString(),
				",",
				joinEdge.Right.Id.ToString(),
				")"
			}));
			this.MarkTableAsEliminated<ColumnVar>(joinEdge.Right, joinEdge.Left, joinEdge.RightVars, joinEdge.LeftVars);
		}

		// Token: 0x06002886 RID: 10374 RVA: 0x0007BA12 File Offset: 0x00079C12
		private static bool HasNonKeyReferences(Table table)
		{
			return !table.Keys.Subsumes(table.ReferencedColumns);
		}

		// Token: 0x06002887 RID: 10375 RVA: 0x0007BA28 File Offset: 0x00079C28
		private bool RightTableHasKeyReferences(JoinEdge joinEdge)
		{
			if (joinEdge.JoinNode == null)
			{
				return true;
			}
			VarVec varVec = null;
			foreach (Var var in joinEdge.Right.Table.Keys)
			{
				VarVec varVec2;
				if (this.m_reverseVarMap.TryGetValue(var, out varVec2))
				{
					if (varVec == null)
					{
						varVec = joinEdge.Right.Table.Keys.Clone();
					}
					varVec.Or(varVec2);
				}
			}
			if (varVec == null)
			{
				varVec = joinEdge.Right.Table.Keys;
			}
			return this.m_varRefManager.HasKeyReferences(varVec, joinEdge.Right.Node, joinEdge.JoinNode.Node);
		}

		// Token: 0x06002888 RID: 10376 RVA: 0x0007BAEC File Offset: 0x00079CEC
		private bool TryEliminateParentChildJoin(JoinEdge joinEdge, ForeignKeyConstraint fkConstraint)
		{
			if (joinEdge.JoinKind == JoinKind.LeftOuter && fkConstraint.ChildMultiplicity == RelationshipMultiplicity.Many)
			{
				return false;
			}
			IList<ColumnVar> list;
			if (!JoinGraph.IsJoinOnFkConstraint(fkConstraint, joinEdge.LeftVars, joinEdge.RightVars, out list))
			{
				return false;
			}
			if (joinEdge.JoinKind != JoinKind.Inner)
			{
				return this.TryEliminateRightTable(joinEdge, fkConstraint.ChildKeys.Count, fkConstraint.ChildMultiplicity == RelationshipMultiplicity.One);
			}
			if (JoinGraph.HasNonKeyReferences(joinEdge.Left.Table))
			{
				return false;
			}
			if (!JoinGraph.CanBeEliminatedBasedOnLojParticipation(joinEdge.Right, joinEdge.Left))
			{
				return false;
			}
			this.EliminateLeftTable(joinEdge);
			return true;
		}

		// Token: 0x06002889 RID: 10377 RVA: 0x0007BB7C File Offset: 0x00079D7C
		private static bool IsJoinOnFkConstraint(ForeignKeyConstraint fkConstraint, IList<ColumnVar> parentVars, IList<ColumnVar> childVars, out IList<ColumnVar> childForeignKeyVars)
		{
			childForeignKeyVars = new List<ColumnVar>(fkConstraint.ChildKeys.Count);
			foreach (string text in fkConstraint.ParentKeys)
			{
				bool flag = false;
				using (IEnumerator<ColumnVar> enumerator2 = parentVars.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						if (enumerator2.Current.ColumnMetadata.Name.Equals(text))
						{
							flag = true;
							break;
						}
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			foreach (string text2 in fkConstraint.ChildKeys)
			{
				bool flag2 = false;
				int i = 0;
				while (i < parentVars.Count)
				{
					ColumnVar columnVar = childVars[i];
					if (columnVar.ColumnMetadata.Name.Equals(text2))
					{
						childForeignKeyVars.Add(columnVar);
						flag2 = true;
						ColumnVar columnVar2 = parentVars[i];
						string text3;
						if (!fkConstraint.GetParentProperty(columnVar.ColumnMetadata.Name, out text3) || !text3.Equals(columnVar2.ColumnMetadata.Name))
						{
							return false;
						}
						break;
					}
					else
					{
						i++;
					}
				}
				if (!flag2)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600288A RID: 10378 RVA: 0x0007BCF8 File Offset: 0x00079EF8
		private bool TryEliminateChildParentJoin(JoinEdge joinEdge, ForeignKeyConstraint fkConstraint)
		{
			IList<ColumnVar> list;
			if (!JoinGraph.IsJoinOnFkConstraint(fkConstraint, joinEdge.RightVars, joinEdge.LeftVars, out list))
			{
				return false;
			}
			if (list.Count > 1)
			{
				if (list.Where((ColumnVar v) => v.ColumnMetadata.IsNullable).Count<ColumnVar>() > 0)
				{
					return false;
				}
			}
			return this.TryEliminateRightTable(joinEdge, fkConstraint.ParentKeys.Count, true);
		}

		// Token: 0x0600288B RID: 10379 RVA: 0x0007BD68 File Offset: 0x00079F68
		private bool TryEliminateRightTable(JoinEdge joinEdge, int fkConstraintKeyCount, bool allowRefsForJoinedOnFkOnly)
		{
			if (JoinGraph.HasNonKeyReferences(joinEdge.Right.Table))
			{
				return false;
			}
			if ((!allowRefsForJoinedOnFkOnly || joinEdge.RightVars.Count != fkConstraintKeyCount) && this.RightTableHasKeyReferences(joinEdge))
			{
				return false;
			}
			if (!JoinGraph.CanBeEliminatedBasedOnLojParticipation(joinEdge.Right, joinEdge.Left))
			{
				return false;
			}
			this.EliminateRightTable(joinEdge);
			return true;
		}

		// Token: 0x0600288C RID: 10380 RVA: 0x0007BDC4 File Offset: 0x00079FC4
		private void EliminateParentChildJoin(JoinEdge joinEdge)
		{
			if (joinEdge.RestrictedElimination)
			{
				return;
			}
			List<ForeignKeyConstraint> list;
			if (this.m_constraintManager.IsParentChildRelationship(joinEdge.Left.Table.TableMetadata.Extent, joinEdge.Right.Table.TableMetadata.Extent, out list))
			{
				PlanCompiler.Assert(list != null && list.Count > 0, "Invalid foreign key constraints");
				foreach (ForeignKeyConstraint foreignKeyConstraint in list)
				{
					if (this.TryEliminateParentChildJoin(joinEdge, foreignKeyConstraint))
					{
						return;
					}
				}
			}
			if (joinEdge.JoinKind == JoinKind.LeftOuter && this.m_constraintManager.IsParentChildRelationship(joinEdge.Right.Table.TableMetadata.Extent, joinEdge.Left.Table.TableMetadata.Extent, out list))
			{
				PlanCompiler.Assert(list != null && list.Count > 0, "Invalid foreign key constraints");
				foreach (ForeignKeyConstraint foreignKeyConstraint2 in list)
				{
					if (this.TryEliminateChildParentJoin(joinEdge, foreignKeyConstraint2))
					{
						break;
					}
				}
			}
		}

		// Token: 0x0600288D RID: 10381 RVA: 0x0007BF18 File Offset: 0x0007A118
		private void EliminateParentChildJoins(AugmentedTableNode tableNode)
		{
			foreach (JoinEdge joinEdge in tableNode.JoinEdges)
			{
				this.EliminateParentChildJoin(joinEdge);
				if (tableNode.IsEliminated)
				{
					break;
				}
			}
		}

		// Token: 0x0600288E RID: 10382 RVA: 0x0007BF78 File Offset: 0x0007A178
		private void EliminateParentChildJoins()
		{
			foreach (AugmentedNode augmentedNode in this.m_vertexes)
			{
				AugmentedTableNode augmentedTableNode = augmentedNode as AugmentedTableNode;
				if (augmentedTableNode != null && !augmentedTableNode.IsEliminated)
				{
					this.EliminateParentChildJoins(augmentedTableNode);
				}
			}
		}

		// Token: 0x0600288F RID: 10383 RVA: 0x0007BFDC File Offset: 0x0007A1DC
		private Node BuildNodeTree()
		{
			if (!this.m_modifiedGraph)
			{
				return this.m_root.Node;
			}
			VarMap varMap = new VarMap();
			foreach (KeyValuePair<Var, Var> keyValuePair in this.m_varMap)
			{
				Var var = keyValuePair.Value;
				Var var2;
				while (this.m_varMap.TryGetValue(var, out var2))
				{
					PlanCompiler.Assert(var2 != null, "null var mapping?");
					var = var2;
				}
				varMap[keyValuePair.Key] = var;
			}
			this.m_varMap = varMap;
			Dictionary<Node, int> dictionary;
			Node node = this.RebuildNodeTree(this.m_root, out dictionary);
			PlanCompiler.Assert(node != null, "Resulting node tree is null");
			PlanCompiler.Assert(dictionary == null || dictionary.Count == 0, "Leaking predicates?");
			return node;
		}

		// Token: 0x06002890 RID: 10384 RVA: 0x0007C0BC File Offset: 0x0007A2BC
		private Node BuildFilterForNullableColumns(Node inputNode, VarVec nonNullableColumns)
		{
			if (nonNullableColumns == null)
			{
				return inputNode;
			}
			VarVec varVec = nonNullableColumns.Remap(this.m_varMap);
			if (varVec.IsEmpty)
			{
				return inputNode;
			}
			Node node = null;
			foreach (Var var in varVec)
			{
				Node node2 = this.m_command.CreateNode(this.m_command.CreateVarRefOp(var));
				Node node3 = this.m_command.CreateNode(this.m_command.CreateConditionalOp(OpType.IsNull), node2);
				node3 = this.m_command.CreateNode(this.m_command.CreateConditionalOp(OpType.Not), node3);
				if (node == null)
				{
					node = node3;
				}
				else
				{
					node = this.m_command.CreateNode(this.m_command.CreateConditionalOp(OpType.And), node, node3);
				}
			}
			PlanCompiler.Assert(node != null, "Null predicate?");
			return this.m_command.CreateNode(this.m_command.CreateFilterOp(), inputNode, node);
		}

		// Token: 0x06002891 RID: 10385 RVA: 0x0007C1BC File Offset: 0x0007A3BC
		private Node BuildFilterNode(Node inputNode, Node predicateNode)
		{
			if (predicateNode == null)
			{
				return inputNode;
			}
			return this.m_command.CreateNode(this.m_command.CreateFilterOp(), inputNode, predicateNode);
		}

		// Token: 0x06002892 RID: 10386 RVA: 0x0007C1DC File Offset: 0x0007A3DC
		private Node RebuildPredicate(AugmentedJoinNode joinNode, out int minLocationId)
		{
			minLocationId = joinNode.Id;
			if (joinNode.OtherPredicate != null)
			{
				foreach (Var var in joinNode.OtherPredicate.GetNodeInfo(this.m_command).ExternalReferences)
				{
					Var var2;
					if (!this.m_varMap.TryGetValue(var, out var2))
					{
						var2 = var;
					}
					minLocationId = this.GetLeastCommonAncestor(minLocationId, this.GetLocationId(var2, minLocationId));
				}
			}
			Node node = joinNode.OtherPredicate;
			for (int i = 0; i < joinNode.LeftVars.Count; i++)
			{
				Var var3;
				if (!this.m_varMap.TryGetValue(joinNode.LeftVars[i], out var3))
				{
					var3 = joinNode.LeftVars[i];
				}
				Var var4;
				if (!this.m_varMap.TryGetValue(joinNode.RightVars[i], out var4))
				{
					var4 = joinNode.RightVars[i];
				}
				if (!var3.Equals(var4))
				{
					minLocationId = this.GetLeastCommonAncestor(minLocationId, this.GetLocationId(var3, minLocationId));
					minLocationId = this.GetLeastCommonAncestor(minLocationId, this.GetLocationId(var4, minLocationId));
					Node node2 = this.m_command.CreateNode(this.m_command.CreateVarRefOp(var3));
					Node node3 = this.m_command.CreateNode(this.m_command.CreateVarRefOp(var4));
					Node node4 = this.m_command.CreateNode(this.m_command.CreateComparisonOp(OpType.EQ, false), node2, node3);
					if (node != null)
					{
						node = PlanCompilerUtil.CombinePredicates(node4, node, this.m_command);
					}
					else
					{
						node = node4;
					}
				}
			}
			return node;
		}

		// Token: 0x06002893 RID: 10387 RVA: 0x0007C388 File Offset: 0x0007A588
		private Node RebuildNodeTreeForCrossJoins(AugmentedJoinNode joinNode)
		{
			List<Node> list = new List<Node>();
			foreach (AugmentedNode augmentedNode in joinNode.Children)
			{
				Dictionary<Node, int> dictionary;
				list.Add(this.RebuildNodeTree(augmentedNode, out dictionary));
				PlanCompiler.Assert(dictionary == null || dictionary.Count == 0, "Leaking predicates");
			}
			if (list.Count == 0)
			{
				return null;
			}
			if (list.Count == 1)
			{
				return list[0];
			}
			Node node = this.m_command.CreateNode(this.m_command.CreateCrossJoinOp(), list);
			this.m_processedNodes[node] = node;
			return node;
		}

		// Token: 0x06002894 RID: 10388 RVA: 0x0007C448 File Offset: 0x0007A648
		private Node RebuildNodeTree(AugmentedJoinNode joinNode, out Dictionary<Node, int> predicates)
		{
			if (joinNode.Node.Op.OpType == OpType.CrossJoin)
			{
				predicates = null;
				return this.RebuildNodeTreeForCrossJoins(joinNode);
			}
			Dictionary<Node, int> dictionary;
			Node node = this.RebuildNodeTree(joinNode.Children[0], out dictionary);
			Dictionary<Node, int> dictionary2;
			Node node2 = this.RebuildNodeTree(joinNode.Children[1], out dictionary2);
			int id;
			Node node3;
			if (node != null && node2 == null && joinNode.Node.Op.OpType == OpType.LeftOuterJoin)
			{
				id = joinNode.Id;
				node3 = null;
			}
			else
			{
				node3 = this.RebuildPredicate(joinNode, out id);
			}
			node3 = this.CombinePredicateNodes(joinNode.Id, node3, id, dictionary, dictionary2, out predicates);
			if (node == null && node2 == null)
			{
				if (node3 == null)
				{
					return null;
				}
				Node node4 = this.m_command.CreateNode(this.m_command.CreateSingleRowTableOp());
				return this.BuildFilterNode(node4, node3);
			}
			else
			{
				if (node == null)
				{
					return this.BuildFilterNode(node2, node3);
				}
				if (node2 == null)
				{
					return this.BuildFilterNode(node, node3);
				}
				if (node3 == null)
				{
					node3 = this.m_command.CreateNode(this.m_command.CreateTrueOp());
				}
				Node node5 = this.m_command.CreateNode(joinNode.Node.Op, node, node2, node3);
				this.m_processedNodes[node5] = node5;
				return node5;
			}
		}

		// Token: 0x06002895 RID: 10389 RVA: 0x0007C578 File Offset: 0x0007A778
		private Node RebuildNodeTree(AugmentedTableNode tableNode)
		{
			AugmentedTableNode augmentedTableNode = tableNode;
			if (tableNode.IsMoved)
			{
				return null;
			}
			while (augmentedTableNode.IsEliminated)
			{
				augmentedTableNode = augmentedTableNode.ReplacementTable;
				if (augmentedTableNode == null)
				{
					return null;
				}
			}
			if (augmentedTableNode.NewLocationId < tableNode.Id)
			{
				return null;
			}
			return this.BuildFilterForNullableColumns(augmentedTableNode.Node, augmentedTableNode.NullableColumns);
		}

		// Token: 0x06002896 RID: 10390 RVA: 0x0007C5C8 File Offset: 0x0007A7C8
		private Node RebuildNodeTree(AugmentedNode augmentedNode, out Dictionary<Node, int> predicates)
		{
			OpType opType = augmentedNode.Node.Op.OpType;
			if (opType == OpType.ScanTable)
			{
				predicates = null;
				return this.RebuildNodeTree((AugmentedTableNode)augmentedNode);
			}
			if (opType - OpType.InnerJoin > 3)
			{
				predicates = null;
				return augmentedNode.Node;
			}
			return this.RebuildNodeTree((AugmentedJoinNode)augmentedNode, out predicates);
		}

		// Token: 0x06002897 RID: 10391 RVA: 0x0007C61C File Offset: 0x0007A81C
		private Node CombinePredicateNodes(int targetNodeId, Node localPredicateNode, int localPredicateMinLocationId, Dictionary<Node, int> leftPredicates, Dictionary<Node, int> rightPredicates, out Dictionary<Node, int> outPredicates)
		{
			Node node = null;
			outPredicates = new Dictionary<Node, int>();
			if (localPredicateNode != null)
			{
				node = this.ClassifyPredicate(targetNodeId, localPredicateNode, localPredicateMinLocationId, node, outPredicates);
			}
			if (leftPredicates != null)
			{
				foreach (KeyValuePair<Node, int> keyValuePair in leftPredicates)
				{
					node = this.ClassifyPredicate(targetNodeId, keyValuePair.Key, keyValuePair.Value, node, outPredicates);
				}
			}
			if (rightPredicates != null)
			{
				foreach (KeyValuePair<Node, int> keyValuePair2 in rightPredicates)
				{
					node = this.ClassifyPredicate(targetNodeId, keyValuePair2.Key, keyValuePair2.Value, node, outPredicates);
				}
			}
			return node;
		}

		// Token: 0x06002898 RID: 10392 RVA: 0x0007C6F4 File Offset: 0x0007A8F4
		private Node ClassifyPredicate(int targetNodeId, Node predicateNode, int predicateMinLocationId, Node result, Dictionary<Node, int> outPredicates)
		{
			if (targetNodeId >= predicateMinLocationId)
			{
				result = this.CombinePredicates(result, predicateNode);
			}
			else
			{
				outPredicates.Add(predicateNode, predicateMinLocationId);
			}
			return result;
		}

		// Token: 0x06002899 RID: 10393 RVA: 0x0007C712 File Offset: 0x0007A912
		private Node CombinePredicates(Node node1, Node node2)
		{
			if (node1 == null)
			{
				return node2;
			}
			if (node2 == null)
			{
				return node1;
			}
			return PlanCompilerUtil.CombinePredicates(node1, node2, this.m_command);
		}

		// Token: 0x0600289A RID: 10394 RVA: 0x0007C72C File Offset: 0x0007A92C
		private int GetLocationId(Var var, int defaultLocationId)
		{
			AugmentedTableNode augmentedTableNode;
			if (!this.m_varToDefiningNodeMap.TryGetValue(var, out augmentedTableNode))
			{
				return defaultLocationId;
			}
			if (augmentedTableNode.IsMoved)
			{
				return augmentedTableNode.NewLocationId;
			}
			return augmentedTableNode.Id;
		}

		// Token: 0x0600289B RID: 10395 RVA: 0x0007C760 File Offset: 0x0007A960
		private int GetLeastCommonAncestor(int nodeId1, int nodeId2)
		{
			if (nodeId1 == nodeId2)
			{
				return nodeId1;
			}
			AugmentedNode augmentedNode = this.m_root;
			AugmentedNode augmentedNode2 = augmentedNode;
			AugmentedNode augmentedNode3 = augmentedNode;
			while (augmentedNode2 == augmentedNode3)
			{
				augmentedNode = augmentedNode2;
				if (augmentedNode.Id == nodeId1 || augmentedNode.Id == nodeId2)
				{
					return augmentedNode.Id;
				}
				augmentedNode2 = JoinGraph.PickSubtree(nodeId1, augmentedNode);
				augmentedNode3 = JoinGraph.PickSubtree(nodeId2, augmentedNode);
			}
			return augmentedNode.Id;
		}

		// Token: 0x0600289C RID: 10396 RVA: 0x0007C7B8 File Offset: 0x0007A9B8
		private static AugmentedNode PickSubtree(int nodeId, AugmentedNode root)
		{
			AugmentedNode augmentedNode = root.Children[0];
			int num = 1;
			while (augmentedNode.Id < nodeId && num < root.Children.Count)
			{
				augmentedNode = root.Children[num];
				num++;
			}
			return augmentedNode;
		}

		// Token: 0x04000E0C RID: 3596
		private readonly Command m_command;

		// Token: 0x04000E0D RID: 3597
		private readonly AugmentedJoinNode m_root;

		// Token: 0x04000E0E RID: 3598
		private readonly List<AugmentedNode> m_vertexes;

		// Token: 0x04000E0F RID: 3599
		private readonly Dictionary<Table, AugmentedTableNode> m_tableVertexMap;

		// Token: 0x04000E10 RID: 3600
		private VarMap m_varMap;

		// Token: 0x04000E11 RID: 3601
		private readonly Dictionary<Var, VarVec> m_reverseVarMap;

		// Token: 0x04000E12 RID: 3602
		private readonly Dictionary<Var, AugmentedTableNode> m_varToDefiningNodeMap;

		// Token: 0x04000E13 RID: 3603
		private readonly Dictionary<Node, Node> m_processedNodes;

		// Token: 0x04000E14 RID: 3604
		private bool m_modifiedGraph;

		// Token: 0x04000E15 RID: 3605
		private readonly ConstraintManager m_constraintManager;

		// Token: 0x04000E16 RID: 3606
		private readonly VarRefManager m_varRefManager;
	}
}
