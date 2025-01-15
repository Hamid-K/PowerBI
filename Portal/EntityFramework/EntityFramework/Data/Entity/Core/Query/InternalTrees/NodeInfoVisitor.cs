using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003C3 RID: 963
	internal class NodeInfoVisitor : BasicOpVisitorOfT<NodeInfo>
	{
		// Token: 0x06002E05 RID: 11781 RVA: 0x000928E4 File Offset: 0x00090AE4
		internal void RecomputeNodeInfo(Node n)
		{
			if (n.IsNodeInfoInitialized)
			{
				base.VisitNode(n).ComputeHashValue(this.m_command, n);
			}
		}

		// Token: 0x06002E06 RID: 11782 RVA: 0x00092901 File Offset: 0x00090B01
		internal NodeInfoVisitor(Command command)
		{
			this.m_command = command;
		}

		// Token: 0x06002E07 RID: 11783 RVA: 0x00092910 File Offset: 0x00090B10
		private NodeInfo GetNodeInfo(Node n)
		{
			return n.GetNodeInfo(this.m_command);
		}

		// Token: 0x06002E08 RID: 11784 RVA: 0x0009291E File Offset: 0x00090B1E
		private ExtendedNodeInfo GetExtendedNodeInfo(Node n)
		{
			return n.GetExtendedNodeInfo(this.m_command);
		}

		// Token: 0x06002E09 RID: 11785 RVA: 0x0009292C File Offset: 0x00090B2C
		private NodeInfo InitNodeInfo(Node n)
		{
			NodeInfo nodeInfo = this.GetNodeInfo(n);
			nodeInfo.Clear();
			return nodeInfo;
		}

		// Token: 0x06002E0A RID: 11786 RVA: 0x0009293B File Offset: 0x00090B3B
		private ExtendedNodeInfo InitExtendedNodeInfo(Node n)
		{
			ExtendedNodeInfo extendedNodeInfo = this.GetExtendedNodeInfo(n);
			extendedNodeInfo.Clear();
			return extendedNodeInfo;
		}

		// Token: 0x06002E0B RID: 11787 RVA: 0x0009294C File Offset: 0x00090B4C
		protected override NodeInfo VisitDefault(Node n)
		{
			NodeInfo nodeInfo = this.InitNodeInfo(n);
			foreach (Node node in n.Children)
			{
				NodeInfo nodeInfo2 = this.GetNodeInfo(node);
				nodeInfo.ExternalReferences.Or(nodeInfo2.ExternalReferences);
			}
			return nodeInfo;
		}

		// Token: 0x06002E0C RID: 11788 RVA: 0x000929BC File Offset: 0x00090BBC
		private static bool IsDefinitionNonNullable(Node definition, VarVec nonNullableInputs)
		{
			return definition.Op.OpType == OpType.Constant || definition.Op.OpType == OpType.InternalConstant || definition.Op.OpType == OpType.NullSentinel || (definition.Op.OpType == OpType.VarRef && nonNullableInputs.IsSet(((VarRefOp)definition.Op).Var));
		}

		// Token: 0x06002E0D RID: 11789 RVA: 0x00092A1A File Offset: 0x00090C1A
		public override NodeInfo Visit(VarRefOp op, Node n)
		{
			NodeInfo nodeInfo = this.InitNodeInfo(n);
			nodeInfo.ExternalReferences.Set(op.Var);
			return nodeInfo;
		}

		// Token: 0x06002E0E RID: 11790 RVA: 0x00092A34 File Offset: 0x00090C34
		protected override NodeInfo VisitRelOpDefault(RelOp op, Node n)
		{
			return this.Unimplemented(n);
		}

		// Token: 0x06002E0F RID: 11791 RVA: 0x00092A40 File Offset: 0x00090C40
		protected override NodeInfo VisitTableOp(ScanTableBaseOp op, Node n)
		{
			ExtendedNodeInfo extendedNodeInfo = this.InitExtendedNodeInfo(n);
			extendedNodeInfo.LocalDefinitions.Or(op.Table.ReferencedColumns);
			extendedNodeInfo.Definitions.Or(op.Table.ReferencedColumns);
			if (op.Table.ReferencedColumns.Subsumes(op.Table.Keys))
			{
				extendedNodeInfo.Keys.InitFrom(op.Table.Keys);
			}
			extendedNodeInfo.NonNullableDefinitions.Or(op.Table.NonNullableColumns);
			extendedNodeInfo.NonNullableDefinitions.And(extendedNodeInfo.Definitions);
			return extendedNodeInfo;
		}

		// Token: 0x06002E10 RID: 11792 RVA: 0x00092ADC File Offset: 0x00090CDC
		public override NodeInfo Visit(UnnestOp op, Node n)
		{
			ExtendedNodeInfo extendedNodeInfo = this.InitExtendedNodeInfo(n);
			foreach (Var var in op.Table.Columns)
			{
				extendedNodeInfo.LocalDefinitions.Set(var);
				extendedNodeInfo.Definitions.Set(var);
			}
			if (n.Child0.Op.OpType == OpType.VarDef && n.Child0.Child0.Op.OpType == OpType.Function && op.Table.Keys.Count > 0 && op.Table.ReferencedColumns.Subsumes(op.Table.Keys))
			{
				extendedNodeInfo.Keys.InitFrom(op.Table.Keys);
			}
			if (n.HasChild0)
			{
				NodeInfo nodeInfo = this.GetNodeInfo(n.Child0);
				extendedNodeInfo.ExternalReferences.Or(nodeInfo.ExternalReferences);
			}
			else
			{
				extendedNodeInfo.ExternalReferences.Set(op.Var);
			}
			return extendedNodeInfo;
		}

		// Token: 0x06002E11 RID: 11793 RVA: 0x00092BFC File Offset: 0x00090DFC
		internal static Dictionary<Var, Var> ComputeVarRemappings(Node varDefListNode)
		{
			Dictionary<Var, Var> dictionary = new Dictionary<Var, Var>();
			foreach (Node node in varDefListNode.Children)
			{
				VarRefOp varRefOp = node.Child0.Op as VarRefOp;
				if (varRefOp != null)
				{
					VarDefOp varDefOp = node.Op as VarDefOp;
					dictionary[varRefOp.Var] = varDefOp.Var;
				}
			}
			return dictionary;
		}

		// Token: 0x06002E12 RID: 11794 RVA: 0x00092C84 File Offset: 0x00090E84
		public override NodeInfo Visit(ProjectOp op, Node n)
		{
			ExtendedNodeInfo extendedNodeInfo = this.InitExtendedNodeInfo(n);
			ExtendedNodeInfo extendedNodeInfo2 = this.GetExtendedNodeInfo(n.Child0);
			foreach (Var var in op.Outputs)
			{
				if (extendedNodeInfo2.Definitions.IsSet(var))
				{
					extendedNodeInfo.Definitions.Set(var);
				}
				else
				{
					extendedNodeInfo.ExternalReferences.Set(var);
				}
			}
			extendedNodeInfo.NonNullableDefinitions.InitFrom(extendedNodeInfo2.NonNullableDefinitions);
			extendedNodeInfo.NonNullableDefinitions.And(op.Outputs);
			extendedNodeInfo.NonNullableVisibleDefinitions.InitFrom(extendedNodeInfo2.NonNullableDefinitions);
			foreach (Node node in n.Child1.Children)
			{
				VarDefOp varDefOp = node.Op as VarDefOp;
				NodeInfo nodeInfo = this.GetNodeInfo(node.Child0);
				extendedNodeInfo.LocalDefinitions.Set(varDefOp.Var);
				extendedNodeInfo.ExternalReferences.Clear(varDefOp.Var);
				extendedNodeInfo.Definitions.Set(varDefOp.Var);
				extendedNodeInfo.ExternalReferences.Or(nodeInfo.ExternalReferences);
				if (NodeInfoVisitor.IsDefinitionNonNullable(node.Child0, extendedNodeInfo.NonNullableVisibleDefinitions))
				{
					extendedNodeInfo.NonNullableDefinitions.Set(varDefOp.Var);
				}
			}
			extendedNodeInfo.ExternalReferences.Minus(extendedNodeInfo2.Definitions);
			extendedNodeInfo.ExternalReferences.Or(extendedNodeInfo2.ExternalReferences);
			extendedNodeInfo.Keys.NoKeys = true;
			if (!extendedNodeInfo2.Keys.NoKeys)
			{
				VarVec varVec = this.m_command.CreateVarVec(extendedNodeInfo2.Keys.KeyVars);
				Dictionary<Var, Var> dictionary = NodeInfoVisitor.ComputeVarRemappings(n.Child1);
				VarVec varVec2 = varVec.Remap(dictionary);
				VarVec varVec3 = varVec2.Clone();
				VarVec varVec4 = this.m_command.CreateVarVec(op.Outputs);
				varVec2.Minus(varVec4);
				if (varVec2.IsEmpty)
				{
					extendedNodeInfo.Keys.InitFrom(varVec3);
				}
			}
			extendedNodeInfo.InitRowCountFrom(extendedNodeInfo2);
			return extendedNodeInfo;
		}

		// Token: 0x06002E13 RID: 11795 RVA: 0x00092EB4 File Offset: 0x000910B4
		public override NodeInfo Visit(FilterOp op, Node n)
		{
			ExtendedNodeInfo extendedNodeInfo = this.InitExtendedNodeInfo(n);
			ExtendedNodeInfo extendedNodeInfo2 = this.GetExtendedNodeInfo(n.Child0);
			NodeInfo nodeInfo = this.GetNodeInfo(n.Child1);
			extendedNodeInfo.Definitions.Or(extendedNodeInfo2.Definitions);
			extendedNodeInfo.ExternalReferences.Or(extendedNodeInfo2.ExternalReferences);
			extendedNodeInfo.ExternalReferences.Or(nodeInfo.ExternalReferences);
			extendedNodeInfo.ExternalReferences.Minus(extendedNodeInfo2.Definitions);
			extendedNodeInfo.Keys.InitFrom(extendedNodeInfo2.Keys);
			extendedNodeInfo.NonNullableDefinitions.InitFrom(extendedNodeInfo2.NonNullableDefinitions);
			extendedNodeInfo.NonNullableVisibleDefinitions.InitFrom(extendedNodeInfo2.NonNullableDefinitions);
			extendedNodeInfo.MinRows = RowCount.Zero;
			ConstantPredicateOp constantPredicateOp = n.Child1.Op as ConstantPredicateOp;
			if (constantPredicateOp != null && constantPredicateOp.IsFalse)
			{
				extendedNodeInfo.MaxRows = RowCount.Zero;
			}
			else
			{
				extendedNodeInfo.MaxRows = extendedNodeInfo2.MaxRows;
			}
			return extendedNodeInfo;
		}

		// Token: 0x06002E14 RID: 11796 RVA: 0x00092F94 File Offset: 0x00091194
		protected override NodeInfo VisitGroupByOp(GroupByBaseOp op, Node n)
		{
			ExtendedNodeInfo extendedNodeInfo = this.InitExtendedNodeInfo(n);
			ExtendedNodeInfo extendedNodeInfo2 = this.GetExtendedNodeInfo(n.Child0);
			extendedNodeInfo.Definitions.InitFrom(op.Outputs);
			extendedNodeInfo.LocalDefinitions.InitFrom(extendedNodeInfo.Definitions);
			extendedNodeInfo.ExternalReferences.Or(extendedNodeInfo2.ExternalReferences);
			foreach (Node node in n.Child1.Children)
			{
				NodeInfo nodeInfo = this.GetNodeInfo(node.Child0);
				extendedNodeInfo.ExternalReferences.Or(nodeInfo.ExternalReferences);
				if (NodeInfoVisitor.IsDefinitionNonNullable(node.Child0, extendedNodeInfo2.NonNullableDefinitions))
				{
					extendedNodeInfo.NonNullableDefinitions.Set(((VarDefOp)node.Op).Var);
				}
			}
			extendedNodeInfo.NonNullableDefinitions.Or(extendedNodeInfo2.NonNullableDefinitions);
			extendedNodeInfo.NonNullableDefinitions.And(op.Keys);
			for (int i = 2; i < n.Children.Count; i++)
			{
				foreach (Node node2 in n.Children[i].Children)
				{
					NodeInfo nodeInfo2 = this.GetNodeInfo(node2.Child0);
					extendedNodeInfo.ExternalReferences.Or(nodeInfo2.ExternalReferences);
				}
			}
			extendedNodeInfo.ExternalReferences.Minus(extendedNodeInfo2.Definitions);
			extendedNodeInfo.Keys.InitFrom(op.Keys);
			extendedNodeInfo.MinRows = (op.Keys.IsEmpty ? RowCount.One : ((extendedNodeInfo2.MinRows == RowCount.One) ? RowCount.One : RowCount.Zero));
			extendedNodeInfo.MaxRows = (op.Keys.IsEmpty ? RowCount.One : extendedNodeInfo2.MaxRows);
			return extendedNodeInfo;
		}

		// Token: 0x06002E15 RID: 11797 RVA: 0x00093184 File Offset: 0x00091384
		public override NodeInfo Visit(CrossJoinOp op, Node n)
		{
			ExtendedNodeInfo extendedNodeInfo = this.InitExtendedNodeInfo(n);
			List<KeyVec> list = new List<KeyVec>();
			RowCount rowCount = RowCount.Zero;
			RowCount rowCount2 = RowCount.One;
			foreach (Node node in n.Children)
			{
				ExtendedNodeInfo extendedNodeInfo2 = this.GetExtendedNodeInfo(node);
				extendedNodeInfo.Definitions.Or(extendedNodeInfo2.Definitions);
				extendedNodeInfo.ExternalReferences.Or(extendedNodeInfo2.ExternalReferences);
				list.Add(extendedNodeInfo2.Keys);
				extendedNodeInfo.NonNullableDefinitions.Or(extendedNodeInfo2.NonNullableDefinitions);
				if (extendedNodeInfo2.MaxRows > rowCount)
				{
					rowCount = extendedNodeInfo2.MaxRows;
				}
				if (extendedNodeInfo2.MinRows < rowCount2)
				{
					rowCount2 = extendedNodeInfo2.MinRows;
				}
			}
			extendedNodeInfo.Keys.InitFrom(list);
			extendedNodeInfo.SetRowCount(rowCount2, rowCount);
			return extendedNodeInfo;
		}

		// Token: 0x06002E16 RID: 11798 RVA: 0x00093270 File Offset: 0x00091470
		protected override NodeInfo VisitJoinOp(JoinBaseOp op, Node n)
		{
			if (op.OpType != OpType.InnerJoin && op.OpType != OpType.LeftOuterJoin && op.OpType != OpType.FullOuterJoin)
			{
				return this.Unimplemented(n);
			}
			ExtendedNodeInfo extendedNodeInfo = this.InitExtendedNodeInfo(n);
			ExtendedNodeInfo extendedNodeInfo2 = this.GetExtendedNodeInfo(n.Child0);
			ExtendedNodeInfo extendedNodeInfo3 = this.GetExtendedNodeInfo(n.Child1);
			NodeInfo nodeInfo = this.GetNodeInfo(n.Child2);
			extendedNodeInfo.Definitions.Or(extendedNodeInfo2.Definitions);
			extendedNodeInfo.Definitions.Or(extendedNodeInfo3.Definitions);
			extendedNodeInfo.ExternalReferences.Or(extendedNodeInfo2.ExternalReferences);
			extendedNodeInfo.ExternalReferences.Or(extendedNodeInfo3.ExternalReferences);
			extendedNodeInfo.ExternalReferences.Or(nodeInfo.ExternalReferences);
			extendedNodeInfo.ExternalReferences.Minus(extendedNodeInfo.Definitions);
			extendedNodeInfo.Keys.InitFrom(extendedNodeInfo2.Keys, extendedNodeInfo3.Keys);
			if (op.OpType == OpType.InnerJoin || op.OpType == OpType.LeftOuterJoin)
			{
				extendedNodeInfo.NonNullableDefinitions.InitFrom(extendedNodeInfo2.NonNullableDefinitions);
			}
			if (op.OpType == OpType.InnerJoin)
			{
				extendedNodeInfo.NonNullableDefinitions.Or(extendedNodeInfo3.NonNullableDefinitions);
			}
			extendedNodeInfo.NonNullableVisibleDefinitions.InitFrom(extendedNodeInfo2.NonNullableDefinitions);
			extendedNodeInfo.NonNullableVisibleDefinitions.Or(extendedNodeInfo3.NonNullableDefinitions);
			RowCount rowCount;
			RowCount rowCount2;
			if (op.OpType == OpType.FullOuterJoin)
			{
				rowCount = RowCount.Zero;
				rowCount2 = RowCount.Unbounded;
			}
			else
			{
				if (extendedNodeInfo2.MaxRows > RowCount.One || extendedNodeInfo3.MaxRows > RowCount.One)
				{
					rowCount2 = RowCount.Unbounded;
				}
				else
				{
					rowCount2 = RowCount.One;
				}
				if (op.OpType == OpType.LeftOuterJoin)
				{
					rowCount = extendedNodeInfo2.MinRows;
				}
				else
				{
					rowCount = RowCount.Zero;
				}
			}
			extendedNodeInfo.SetRowCount(rowCount, rowCount2);
			return extendedNodeInfo;
		}

		// Token: 0x06002E17 RID: 11799 RVA: 0x00093400 File Offset: 0x00091600
		protected override NodeInfo VisitApplyOp(ApplyBaseOp op, Node n)
		{
			ExtendedNodeInfo extendedNodeInfo = this.InitExtendedNodeInfo(n);
			ExtendedNodeInfo extendedNodeInfo2 = this.GetExtendedNodeInfo(n.Child0);
			ExtendedNodeInfo extendedNodeInfo3 = this.GetExtendedNodeInfo(n.Child1);
			extendedNodeInfo.Definitions.Or(extendedNodeInfo2.Definitions);
			extendedNodeInfo.Definitions.Or(extendedNodeInfo3.Definitions);
			extendedNodeInfo.ExternalReferences.Or(extendedNodeInfo2.ExternalReferences);
			extendedNodeInfo.ExternalReferences.Or(extendedNodeInfo3.ExternalReferences);
			extendedNodeInfo.ExternalReferences.Minus(extendedNodeInfo.Definitions);
			extendedNodeInfo.Keys.InitFrom(extendedNodeInfo2.Keys, extendedNodeInfo3.Keys);
			extendedNodeInfo.NonNullableDefinitions.InitFrom(extendedNodeInfo2.NonNullableDefinitions);
			if (op.OpType == OpType.CrossApply)
			{
				extendedNodeInfo.NonNullableDefinitions.Or(extendedNodeInfo3.NonNullableDefinitions);
			}
			extendedNodeInfo.NonNullableVisibleDefinitions.InitFrom(extendedNodeInfo2.NonNullableDefinitions);
			extendedNodeInfo.NonNullableVisibleDefinitions.Or(extendedNodeInfo3.NonNullableDefinitions);
			RowCount rowCount;
			if (extendedNodeInfo2.MaxRows <= RowCount.One && extendedNodeInfo3.MaxRows <= RowCount.One)
			{
				rowCount = RowCount.One;
			}
			else
			{
				rowCount = RowCount.Unbounded;
			}
			RowCount rowCount2 = ((op.OpType == OpType.CrossApply) ? RowCount.Zero : extendedNodeInfo2.MinRows);
			extendedNodeInfo.SetRowCount(rowCount2, rowCount);
			return extendedNodeInfo;
		}

		// Token: 0x06002E18 RID: 11800 RVA: 0x00093520 File Offset: 0x00091720
		protected override NodeInfo VisitSetOp(SetOp op, Node n)
		{
			ExtendedNodeInfo extendedNodeInfo = this.InitExtendedNodeInfo(n);
			extendedNodeInfo.Definitions.InitFrom(op.Outputs);
			extendedNodeInfo.LocalDefinitions.InitFrom(op.Outputs);
			ExtendedNodeInfo extendedNodeInfo2 = this.GetExtendedNodeInfo(n.Child0);
			ExtendedNodeInfo extendedNodeInfo3 = this.GetExtendedNodeInfo(n.Child1);
			RowCount rowCount = RowCount.Zero;
			extendedNodeInfo.ExternalReferences.Or(extendedNodeInfo2.ExternalReferences);
			extendedNodeInfo.ExternalReferences.Or(extendedNodeInfo3.ExternalReferences);
			if (op.OpType == OpType.UnionAll)
			{
				rowCount = ((extendedNodeInfo2.MinRows > extendedNodeInfo3.MinRows) ? extendedNodeInfo2.MinRows : extendedNodeInfo3.MinRows);
			}
			if (op.OpType == OpType.Intersect || op.OpType == OpType.Except)
			{
				extendedNodeInfo.Keys.InitFrom(op.Outputs);
			}
			else
			{
				UnionAllOp unionAllOp = (UnionAllOp)op;
				if (unionAllOp.BranchDiscriminator == null)
				{
					extendedNodeInfo.Keys.NoKeys = true;
				}
				else
				{
					VarVec varVec = this.m_command.CreateVarVec();
					for (int i = 0; i < n.Children.Count; i++)
					{
						ExtendedNodeInfo extendedNodeInfo4 = n.Children[i].GetExtendedNodeInfo(this.m_command);
						if (extendedNodeInfo4.Keys.NoKeys || extendedNodeInfo4.Keys.KeyVars.IsEmpty)
						{
							varVec.Clear();
							break;
						}
						VarVec varVec2 = extendedNodeInfo4.Keys.KeyVars.Remap(unionAllOp.VarMap[i].GetReverseMap());
						varVec.Or(varVec2);
					}
					if (varVec.IsEmpty)
					{
						extendedNodeInfo.Keys.NoKeys = true;
					}
					else
					{
						extendedNodeInfo.Keys.InitFrom(varVec);
					}
				}
			}
			VarVec varVec3 = extendedNodeInfo2.NonNullableDefinitions.Remap(op.VarMap[0].GetReverseMap());
			extendedNodeInfo.NonNullableDefinitions.InitFrom(varVec3);
			if (op.OpType != OpType.Except)
			{
				VarVec varVec4 = extendedNodeInfo3.NonNullableDefinitions.Remap(op.VarMap[1].GetReverseMap());
				if (op.OpType == OpType.Intersect)
				{
					extendedNodeInfo.NonNullableDefinitions.Or(varVec4);
				}
				else
				{
					extendedNodeInfo.NonNullableDefinitions.And(varVec4);
				}
			}
			extendedNodeInfo.NonNullableDefinitions.And(op.Outputs);
			extendedNodeInfo.MinRows = rowCount;
			return extendedNodeInfo;
		}

		// Token: 0x06002E19 RID: 11801 RVA: 0x00093754 File Offset: 0x00091954
		protected override NodeInfo VisitSortOp(SortBaseOp op, Node n)
		{
			ExtendedNodeInfo extendedNodeInfo = this.InitExtendedNodeInfo(n);
			ExtendedNodeInfo extendedNodeInfo2 = this.GetExtendedNodeInfo(n.Child0);
			extendedNodeInfo.Definitions.Or(extendedNodeInfo2.Definitions);
			extendedNodeInfo.ExternalReferences.Or(extendedNodeInfo2.ExternalReferences);
			extendedNodeInfo.ExternalReferences.Minus(extendedNodeInfo2.Definitions);
			extendedNodeInfo.Keys.InitFrom(extendedNodeInfo2.Keys);
			extendedNodeInfo.NonNullableDefinitions.InitFrom(extendedNodeInfo2.NonNullableDefinitions);
			extendedNodeInfo.NonNullableVisibleDefinitions.InitFrom(extendedNodeInfo2.NonNullableDefinitions);
			extendedNodeInfo.InitRowCountFrom(extendedNodeInfo2);
			if (OpType.ConstrainedSort == op.OpType && n.Child2.Op.OpType == OpType.Constant && !((ConstrainedSortOp)op).WithTies)
			{
				ConstantBaseOp constantBaseOp = (ConstantBaseOp)n.Child2.Op;
				if (TypeHelpers.IsIntegerConstant(constantBaseOp.Type, constantBaseOp.Value, 1L))
				{
					extendedNodeInfo.SetRowCount(RowCount.Zero, RowCount.One);
				}
			}
			return extendedNodeInfo;
		}

		// Token: 0x06002E1A RID: 11802 RVA: 0x0009383C File Offset: 0x00091A3C
		public override NodeInfo Visit(DistinctOp op, Node n)
		{
			ExtendedNodeInfo extendedNodeInfo = this.InitExtendedNodeInfo(n);
			extendedNodeInfo.Keys.InitFrom(op.Keys, true);
			ExtendedNodeInfo extendedNodeInfo2 = this.GetExtendedNodeInfo(n.Child0);
			extendedNodeInfo.ExternalReferences.InitFrom(extendedNodeInfo2.ExternalReferences);
			foreach (Var var in op.Keys)
			{
				if (extendedNodeInfo2.Definitions.IsSet(var))
				{
					extendedNodeInfo.Definitions.Set(var);
				}
				else
				{
					extendedNodeInfo.ExternalReferences.Set(var);
				}
			}
			extendedNodeInfo.NonNullableDefinitions.InitFrom(extendedNodeInfo2.NonNullableDefinitions);
			extendedNodeInfo.NonNullableDefinitions.And(op.Keys);
			extendedNodeInfo.InitRowCountFrom(extendedNodeInfo2);
			return extendedNodeInfo;
		}

		// Token: 0x06002E1B RID: 11803 RVA: 0x0009390C File Offset: 0x00091B0C
		public override NodeInfo Visit(SingleRowOp op, Node n)
		{
			ExtendedNodeInfo extendedNodeInfo = this.InitExtendedNodeInfo(n);
			ExtendedNodeInfo extendedNodeInfo2 = this.GetExtendedNodeInfo(n.Child0);
			extendedNodeInfo.Definitions.InitFrom(extendedNodeInfo2.Definitions);
			extendedNodeInfo.Keys.InitFrom(extendedNodeInfo2.Keys);
			extendedNodeInfo.ExternalReferences.InitFrom(extendedNodeInfo2.ExternalReferences);
			extendedNodeInfo.NonNullableDefinitions.InitFrom(extendedNodeInfo2.NonNullableDefinitions);
			extendedNodeInfo.SetRowCount(RowCount.Zero, RowCount.One);
			return extendedNodeInfo;
		}

		// Token: 0x06002E1C RID: 11804 RVA: 0x00093979 File Offset: 0x00091B79
		public override NodeInfo Visit(SingleRowTableOp op, Node n)
		{
			ExtendedNodeInfo extendedNodeInfo = this.InitExtendedNodeInfo(n);
			extendedNodeInfo.Keys.NoKeys = false;
			extendedNodeInfo.SetRowCount(RowCount.One, RowCount.One);
			return extendedNodeInfo;
		}

		// Token: 0x06002E1D RID: 11805 RVA: 0x00093998 File Offset: 0x00091B98
		public override NodeInfo Visit(PhysicalProjectOp op, Node n)
		{
			ExtendedNodeInfo extendedNodeInfo = this.InitExtendedNodeInfo(n);
			foreach (Node node in n.Children)
			{
				NodeInfo nodeInfo = this.GetNodeInfo(node);
				extendedNodeInfo.ExternalReferences.Or(nodeInfo.ExternalReferences);
			}
			extendedNodeInfo.Definitions.InitFrom(op.Outputs);
			extendedNodeInfo.LocalDefinitions.InitFrom(extendedNodeInfo.Definitions);
			ExtendedNodeInfo extendedNodeInfo2 = this.GetExtendedNodeInfo(n.Child0);
			if (!extendedNodeInfo2.Keys.NoKeys)
			{
				VarVec varVec = this.m_command.CreateVarVec(extendedNodeInfo2.Keys.KeyVars);
				varVec.Minus(extendedNodeInfo.Definitions);
				if (varVec.IsEmpty)
				{
					extendedNodeInfo.Keys.InitFrom(extendedNodeInfo2.Keys);
				}
			}
			extendedNodeInfo.NonNullableDefinitions.Or(extendedNodeInfo2.NonNullableDefinitions);
			extendedNodeInfo.NonNullableDefinitions.And(extendedNodeInfo.Definitions);
			extendedNodeInfo.NonNullableVisibleDefinitions.Or(extendedNodeInfo2.NonNullableVisibleDefinitions);
			return extendedNodeInfo;
		}

		// Token: 0x06002E1E RID: 11806 RVA: 0x00093AB4 File Offset: 0x00091CB4
		protected override NodeInfo VisitNestOp(NestBaseOp op, Node n)
		{
			SingleStreamNestOp singleStreamNestOp = op as SingleStreamNestOp;
			ExtendedNodeInfo extendedNodeInfo = this.InitExtendedNodeInfo(n);
			foreach (CollectionInfo collectionInfo in op.CollectionInfo)
			{
				extendedNodeInfo.LocalDefinitions.Set(collectionInfo.CollectionVar);
			}
			extendedNodeInfo.Definitions.InitFrom(op.Outputs);
			foreach (Node node in n.Children)
			{
				extendedNodeInfo.ExternalReferences.Or(this.GetExtendedNodeInfo(node).ExternalReferences);
			}
			extendedNodeInfo.ExternalReferences.Minus(extendedNodeInfo.Definitions);
			if (singleStreamNestOp == null)
			{
				extendedNodeInfo.Keys.InitFrom(this.GetExtendedNodeInfo(n.Child0).Keys);
			}
			else
			{
				extendedNodeInfo.Keys.InitFrom(singleStreamNestOp.Keys);
			}
			return extendedNodeInfo;
		}

		// Token: 0x04000F5E RID: 3934
		private readonly Command m_command;
	}
}
