using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000379 RID: 889
	internal class VarRefManager
	{
		// Token: 0x06002AE6 RID: 10982 RVA: 0x0008CDDF File Offset: 0x0008AFDF
		internal VarRefManager(Command command)
		{
			this.m_nodeToParentMap = new Dictionary<Node, Node>();
			this.m_nodeToSiblingNumber = new Dictionary<Node, int>();
			this.m_command = command;
		}

		// Token: 0x06002AE7 RID: 10983 RVA: 0x0008CE04 File Offset: 0x0008B004
		internal void AddChildren(Node parent)
		{
			for (int i = 0; i < parent.Children.Count; i++)
			{
				this.m_nodeToParentMap[parent.Children[i]] = parent;
				this.m_nodeToSiblingNumber[parent.Children[i]] = i;
			}
		}

		// Token: 0x06002AE8 RID: 10984 RVA: 0x0008CE58 File Offset: 0x0008B058
		internal bool HasKeyReferences(VarVec keys, Node definingNode, Node targetJoinNode)
		{
			Node node = definingNode;
			bool flag = true;
			Node node2;
			while (flag & this.m_nodeToParentMap.TryGetValue(node, out node2))
			{
				if (node2 != targetJoinNode)
				{
					if (VarRefManager.HasVarReferencesShallow(node2, keys, this.m_nodeToSiblingNumber[node], out flag))
					{
						return true;
					}
					for (int i = this.m_nodeToSiblingNumber[node] + 1; i < node2.Children.Count; i++)
					{
						if (node2.Children[i].GetNodeInfo(this.m_command).ExternalReferences.Overlaps(keys))
						{
							return true;
						}
					}
				}
				node = node2;
			}
			return false;
		}

		// Token: 0x06002AE9 RID: 10985 RVA: 0x0008CEE8 File Offset: 0x0008B0E8
		private static bool HasVarReferencesShallow(Node node, VarVec vars, int childIndex, out bool continueUp)
		{
			OpType opType = node.Op.OpType;
			if (opType != OpType.Project)
			{
				switch (opType)
				{
				case OpType.Sort:
				case OpType.ConstrainedSort:
					continueUp = true;
					return VarRefManager.HasVarReferences(((SortBaseOp)node.Op).Keys, vars);
				case OpType.GroupBy:
					continueUp = false;
					return VarRefManager.HasVarReferences(((GroupByOp)node.Op).Keys, vars);
				case OpType.UnionAll:
				case OpType.Intersect:
				case OpType.Except:
					continueUp = false;
					return VarRefManager.HasVarReferences((SetOp)node.Op, vars, childIndex);
				case OpType.Distinct:
					continueUp = false;
					return VarRefManager.HasVarReferences(((DistinctOp)node.Op).Keys, vars);
				case OpType.PhysicalProject:
					continueUp = false;
					return VarRefManager.HasVarReferences(((PhysicalProjectOp)node.Op).Outputs, vars);
				}
				continueUp = true;
				return false;
			}
			continueUp = false;
			return VarRefManager.HasVarReferences(((ProjectOp)node.Op).Outputs, vars);
		}

		// Token: 0x06002AEA RID: 10986 RVA: 0x0008CFEC File Offset: 0x0008B1EC
		private static bool HasVarReferences(VarList listToCheck, VarVec vars)
		{
			foreach (Var var in vars)
			{
				if (listToCheck.Contains(var))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002AEB RID: 10987 RVA: 0x0008D040 File Offset: 0x0008B240
		private static bool HasVarReferences(VarVec listToCheck, VarVec vars)
		{
			return listToCheck.Overlaps(vars);
		}

		// Token: 0x06002AEC RID: 10988 RVA: 0x0008D04C File Offset: 0x0008B24C
		private static bool HasVarReferences(List<SortKey> listToCheck, VarVec vars)
		{
			foreach (SortKey sortKey in listToCheck)
			{
				if (vars.IsSet(sortKey.Var))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002AED RID: 10989 RVA: 0x0008D0A8 File Offset: 0x0008B2A8
		private static bool HasVarReferences(SetOp op, VarVec vars, int index)
		{
			foreach (Var var in op.VarMap[index].Values)
			{
				if (vars.IsSet(var))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000ED3 RID: 3795
		private readonly Dictionary<Node, Node> m_nodeToParentMap;

		// Token: 0x04000ED4 RID: 3796
		private readonly Dictionary<Node, int> m_nodeToSiblingNumber;

		// Token: 0x04000ED5 RID: 3797
		private readonly Command m_command;
	}
}
