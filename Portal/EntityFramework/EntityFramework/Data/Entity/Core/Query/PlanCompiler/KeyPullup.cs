using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x0200034E RID: 846
	internal class KeyPullup : BasicOpVisitor
	{
		// Token: 0x060028A1 RID: 10401 RVA: 0x0007D2AE File Offset: 0x0007B4AE
		internal KeyPullup(Command command)
		{
			this.m_command = command;
		}

		// Token: 0x060028A2 RID: 10402 RVA: 0x0007D2BD File Offset: 0x0007B4BD
		internal KeyVec GetKeys(Node node)
		{
			ExtendedNodeInfo extendedNodeInfo = node.GetExtendedNodeInfo(this.m_command);
			if (extendedNodeInfo.Keys.NoKeys)
			{
				this.VisitNode(node);
			}
			return extendedNodeInfo.Keys;
		}

		// Token: 0x060028A3 RID: 10403 RVA: 0x0007D2E4 File Offset: 0x0007B4E4
		protected override void VisitChildren(Node n)
		{
			foreach (Node node in n.Children)
			{
				if (node.Op.IsRelOp || node.Op.IsPhysicalOp)
				{
					this.GetKeys(node);
				}
			}
		}

		// Token: 0x060028A4 RID: 10404 RVA: 0x0007D354 File Offset: 0x0007B554
		protected override void VisitRelOpDefault(RelOp op, Node n)
		{
			this.VisitChildren(n);
			this.m_command.RecomputeNodeInfo(n);
		}

		// Token: 0x060028A5 RID: 10405 RVA: 0x0007D369 File Offset: 0x0007B569
		public override void Visit(ScanTableOp op, Node n)
		{
			op.Table.ReferencedColumns.Or(op.Table.Keys);
			this.m_command.RecomputeNodeInfo(n);
		}

		// Token: 0x060028A6 RID: 10406 RVA: 0x0007D394 File Offset: 0x0007B594
		public override void Visit(ProjectOp op, Node n)
		{
			this.VisitChildren(n);
			ExtendedNodeInfo extendedNodeInfo = n.Child0.GetExtendedNodeInfo(this.m_command);
			if (!extendedNodeInfo.Keys.NoKeys)
			{
				VarVec varVec = this.m_command.CreateVarVec(op.Outputs);
				Dictionary<Var, Var> dictionary = NodeInfoVisitor.ComputeVarRemappings(n.Child1);
				VarVec varVec2 = extendedNodeInfo.Keys.KeyVars.Remap(dictionary);
				varVec.Or(varVec2);
				op.Outputs.InitFrom(varVec);
			}
			this.m_command.RecomputeNodeInfo(n);
		}

		// Token: 0x060028A7 RID: 10407 RVA: 0x0007D418 File Offset: 0x0007B618
		public override void Visit(UnionAllOp op, Node n)
		{
			this.VisitChildren(n);
			Var var = this.m_command.CreateSetOpVar(this.m_command.IntegerType);
			VarList varList = Command.CreateVarList();
			VarVec[] array = new VarVec[n.Children.Count];
			for (int i = 0; i < n.Children.Count; i++)
			{
				Node node = n.Children[i];
				VarVec varVec = this.m_command.GetExtendedNodeInfo(node).Keys.KeyVars.Remap(op.VarMap[i]);
				array[i] = this.m_command.CreateVarVec(varVec);
				array[i].Minus(op.Outputs);
				if (OpType.UnionAll == node.Op.OpType)
				{
					UnionAllOp unionAllOp = (UnionAllOp)node.Op;
					array[i].Clear(unionAllOp.BranchDiscriminator);
				}
				varList.AddRange(array[i]);
			}
			VarList varList2 = Command.CreateVarList();
			foreach (Var var2 in varList)
			{
				Var var3 = this.m_command.CreateSetOpVar(var2.Type);
				varList2.Add(var3);
			}
			for (int j = 0; j < n.Children.Count; j++)
			{
				Node node2 = n.Children[j];
				ExtendedNodeInfo extendedNodeInfo = this.m_command.GetExtendedNodeInfo(node2);
				VarVec varVec2 = this.m_command.CreateVarVec();
				List<Node> list = new List<Node>();
				Var branchDiscriminator;
				if (OpType.UnionAll == node2.Op.OpType && ((UnionAllOp)node2.Op).BranchDiscriminator != null)
				{
					branchDiscriminator = ((UnionAllOp)node2.Op).BranchDiscriminator;
					if (!op.VarMap[j].ContainsValue(branchDiscriminator))
					{
						op.VarMap[j].Add(var, branchDiscriminator);
					}
					else
					{
						PlanCompiler.Assert(j == 0, "right branch has a discriminator var that the left branch doesn't have?");
						var = op.VarMap[j].GetReverseMap()[branchDiscriminator];
					}
				}
				else
				{
					list.Add(this.m_command.CreateVarDefNode(this.m_command.CreateNode(this.m_command.CreateConstantOp(this.m_command.IntegerType, this.m_command.NextBranchDiscriminatorValue)), out branchDiscriminator));
					varVec2.Set(branchDiscriminator);
					op.VarMap[j].Add(var, branchDiscriminator);
				}
				for (int k = 0; k < varList.Count; k++)
				{
					Var var4 = varList[k];
					if (!array[j].IsSet(var4))
					{
						list.Add(this.m_command.CreateVarDefNode(this.m_command.CreateNode(this.m_command.CreateNullOp(var4.Type)), out var4));
						varVec2.Set(var4);
					}
					op.VarMap[j].Add(varList2[k], var4);
				}
				if (varVec2.IsEmpty)
				{
					extendedNodeInfo.Keys.KeyVars.Set(branchDiscriminator);
				}
				else
				{
					PlanCompiler.Assert(list.Count != 0, "no new nodes?");
					foreach (Var var5 in op.VarMap[j].Values)
					{
						varVec2.Set(var5);
					}
					n.Children[j] = this.m_command.CreateNode(this.m_command.CreateProjectOp(varVec2), node2, this.m_command.CreateNode(this.m_command.CreateVarDefListOp(), list));
					this.m_command.RecomputeNodeInfo(n.Children[j]);
					ExtendedNodeInfo extendedNodeInfo2 = this.m_command.GetExtendedNodeInfo(n.Children[j]);
					extendedNodeInfo2.Keys.KeyVars.InitFrom(extendedNodeInfo.Keys.KeyVars);
					extendedNodeInfo2.Keys.KeyVars.Set(branchDiscriminator);
				}
			}
			n.Op = this.m_command.CreateUnionAllOp(op.VarMap[0], op.VarMap[1], var);
			this.m_command.RecomputeNodeInfo(n);
		}

		// Token: 0x04000E28 RID: 3624
		private readonly Command m_command;
	}
}
