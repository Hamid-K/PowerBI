using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000359 RID: 857
	internal class Predicate
	{
		// Token: 0x0600296D RID: 10605 RVA: 0x0008498D File Offset: 0x00082B8D
		internal Predicate(Command command)
		{
			this.m_command = command;
			this.m_parts = new List<Node>();
		}

		// Token: 0x0600296E RID: 10606 RVA: 0x000849A7 File Offset: 0x00082BA7
		internal Predicate(Command command, Node andTree)
			: this(command)
		{
			PlanCompiler.Assert(andTree != null, "null node passed to Predicate() constructor");
			this.InitFromAndTree(andTree);
		}

		// Token: 0x0600296F RID: 10607 RVA: 0x000849C5 File Offset: 0x00082BC5
		internal void AddPart(Node n)
		{
			this.m_parts.Add(n);
		}

		// Token: 0x06002970 RID: 10608 RVA: 0x000849D4 File Offset: 0x00082BD4
		internal Node BuildAndTree()
		{
			Node node = null;
			foreach (Node node2 in this.m_parts)
			{
				if (node == null)
				{
					node = node2;
				}
				else
				{
					node = this.m_command.CreateNode(this.m_command.CreateConditionalOp(OpType.And), node, node2);
				}
			}
			return node;
		}

		// Token: 0x06002971 RID: 10609 RVA: 0x00084A48 File Offset: 0x00082C48
		internal Predicate GetSingleTablePredicates(VarVec tableDefinitions, out Predicate otherPredicates)
		{
			List<Predicate> list;
			this.GetSingleTablePredicates(new List<VarVec> { tableDefinitions }, out list, out otherPredicates);
			return list[0];
		}

		// Token: 0x06002972 RID: 10610 RVA: 0x00084A74 File Offset: 0x00082C74
		internal void GetEquiJoinPredicates(VarVec leftTableDefinitions, VarVec rightTableDefinitions, out List<Var> leftTableEquiJoinColumns, out List<Var> rightTableEquiJoinColumns, out Predicate otherPredicates)
		{
			otherPredicates = new Predicate(this.m_command);
			leftTableEquiJoinColumns = new List<Var>();
			rightTableEquiJoinColumns = new List<Var>();
			foreach (Node node in this.m_parts)
			{
				Var var;
				Var var2;
				if (Predicate.IsEquiJoinPredicate(node, leftTableDefinitions, rightTableDefinitions, out var, out var2))
				{
					leftTableEquiJoinColumns.Add(var);
					rightTableEquiJoinColumns.Add(var2);
				}
				else
				{
					otherPredicates.AddPart(node);
				}
			}
		}

		// Token: 0x06002973 RID: 10611 RVA: 0x00084B08 File Offset: 0x00082D08
		internal Predicate GetJoinPredicates(VarVec leftTableDefinitions, VarVec rightTableDefinitions, out Predicate otherPredicates)
		{
			Predicate predicate = new Predicate(this.m_command);
			otherPredicates = new Predicate(this.m_command);
			foreach (Node node in this.m_parts)
			{
				Var var;
				Var var2;
				if (Predicate.IsEquiJoinPredicate(node, leftTableDefinitions, rightTableDefinitions, out var, out var2))
				{
					predicate.AddPart(node);
				}
				else
				{
					otherPredicates.AddPart(node);
				}
			}
			return predicate;
		}

		// Token: 0x06002974 RID: 10612 RVA: 0x00084B90 File Offset: 0x00082D90
		internal bool SatisfiesKey(VarVec keyVars, VarVec definitions)
		{
			if (keyVars.Count > 0)
			{
				VarVec varVec = keyVars.Clone();
				foreach (Node node in this.m_parts)
				{
					if (node.Op.OpType == OpType.EQ)
					{
						Var var;
						if (this.IsKeyPredicate(node.Child0, node.Child1, keyVars, definitions, out var))
						{
							varVec.Clear(var);
						}
						else if (this.IsKeyPredicate(node.Child1, node.Child0, keyVars, definitions, out var))
						{
							varVec.Clear(var);
						}
					}
				}
				return varVec.IsEmpty;
			}
			return false;
		}

		// Token: 0x06002975 RID: 10613 RVA: 0x00084C48 File Offset: 0x00082E48
		internal bool PreservesNulls(VarVec tableColumns, bool ansiNullSemantics)
		{
			if (!ansiNullSemantics)
			{
				return true;
			}
			using (List<Node>.Enumerator enumerator = this.m_parts.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!Predicate.PreservesNulls(enumerator.Current, tableColumns))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06002976 RID: 10614 RVA: 0x00084CA8 File Offset: 0x00082EA8
		private void InitFromAndTree(Node andTree)
		{
			if (andTree.Op.OpType == OpType.And)
			{
				this.InitFromAndTree(andTree.Child0);
				this.InitFromAndTree(andTree.Child1);
				return;
			}
			this.m_parts.Add(andTree);
		}

		// Token: 0x06002977 RID: 10615 RVA: 0x00084CE0 File Offset: 0x00082EE0
		private void GetSingleTablePredicates(List<VarVec> tableDefinitions, out List<Predicate> singleTablePredicates, out Predicate otherPredicates)
		{
			singleTablePredicates = new List<Predicate>();
			foreach (VarVec varVec in tableDefinitions)
			{
				singleTablePredicates.Add(new Predicate(this.m_command));
			}
			otherPredicates = new Predicate(this.m_command);
			VarVec varVec2 = this.m_command.CreateVarVec();
			foreach (Node node in this.m_parts)
			{
				NodeInfo nodeInfo = this.m_command.GetNodeInfo(node);
				bool flag = false;
				for (int i = 0; i < tableDefinitions.Count; i++)
				{
					VarVec varVec3 = tableDefinitions[i];
					if (varVec3 != null)
					{
						varVec2.InitFrom(nodeInfo.ExternalReferences);
						varVec2.Minus(varVec3);
						if (varVec2.IsEmpty)
						{
							flag = true;
							singleTablePredicates[i].AddPart(node);
							break;
						}
					}
				}
				if (!flag)
				{
					otherPredicates.AddPart(node);
				}
			}
		}

		// Token: 0x06002978 RID: 10616 RVA: 0x00084E08 File Offset: 0x00083008
		private static bool IsEquiJoinPredicate(Node simplePredicateNode, out Var leftVar, out Var rightVar)
		{
			leftVar = null;
			rightVar = null;
			if (simplePredicateNode.Op.OpType != OpType.EQ)
			{
				return false;
			}
			VarRefOp varRefOp = simplePredicateNode.Child0.Op as VarRefOp;
			if (varRefOp == null)
			{
				return false;
			}
			VarRefOp varRefOp2 = simplePredicateNode.Child1.Op as VarRefOp;
			if (varRefOp2 == null)
			{
				return false;
			}
			leftVar = varRefOp.Var;
			rightVar = varRefOp2.Var;
			return true;
		}

		// Token: 0x06002979 RID: 10617 RVA: 0x00084E6C File Offset: 0x0008306C
		private static bool IsEquiJoinPredicate(Node simplePredicateNode, VarVec leftTableDefinitions, VarVec rightTableDefinitions, out Var leftVar, out Var rightVar)
		{
			leftVar = null;
			rightVar = null;
			Var var;
			Var var2;
			if (!Predicate.IsEquiJoinPredicate(simplePredicateNode, out var, out var2))
			{
				return false;
			}
			if (leftTableDefinitions.IsSet(var) && rightTableDefinitions.IsSet(var2))
			{
				leftVar = var;
				rightVar = var2;
			}
			else
			{
				if (!leftTableDefinitions.IsSet(var2) || !rightTableDefinitions.IsSet(var))
				{
					return false;
				}
				leftVar = var2;
				rightVar = var;
			}
			return true;
		}

		// Token: 0x0600297A RID: 10618 RVA: 0x00084EC8 File Offset: 0x000830C8
		private static bool PreservesNulls(Node simplePredNode, VarVec tableColumns)
		{
			OpType opType = simplePredNode.Op.OpType;
			if (opType - OpType.GT > 5)
			{
				if (opType != OpType.Like)
				{
					if (opType != OpType.Not)
					{
						return true;
					}
					if (simplePredNode.Child0.Op.OpType != OpType.IsNull)
					{
						return true;
					}
					VarRefOp varRefOp = simplePredNode.Child0.Child0.Op as VarRefOp;
					return varRefOp == null || !tableColumns.IsSet(varRefOp.Var);
				}
				else
				{
					ConstantBaseOp constantBaseOp = simplePredNode.Child1.Op as ConstantBaseOp;
					if (constantBaseOp == null || constantBaseOp.OpType == OpType.Null)
					{
						return true;
					}
					VarRefOp varRefOp = simplePredNode.Child0.Op as VarRefOp;
					return varRefOp == null || !tableColumns.IsSet(varRefOp.Var);
				}
			}
			else
			{
				VarRefOp varRefOp = simplePredNode.Child0.Op as VarRefOp;
				if (varRefOp != null && tableColumns.IsSet(varRefOp.Var))
				{
					return false;
				}
				varRefOp = simplePredNode.Child1.Op as VarRefOp;
				return varRefOp == null || !tableColumns.IsSet(varRefOp.Var);
			}
		}

		// Token: 0x0600297B RID: 10619 RVA: 0x00084FCC File Offset: 0x000831CC
		private bool IsKeyPredicate(Node left, Node right, VarVec keyVars, VarVec definitions, out Var keyVar)
		{
			keyVar = null;
			if (left.Op.OpType != OpType.VarRef)
			{
				return false;
			}
			VarRefOp varRefOp = (VarRefOp)left.Op;
			keyVar = varRefOp.Var;
			if (!keyVars.IsSet(keyVar))
			{
				return false;
			}
			VarVec varVec = this.m_command.GetNodeInfo(right).ExternalReferences.Clone();
			varVec.And(definitions);
			return varVec.IsEmpty;
		}

		// Token: 0x04000E56 RID: 3670
		private readonly Command m_command;

		// Token: 0x04000E57 RID: 3671
		private readonly List<Node> m_parts;
	}
}
