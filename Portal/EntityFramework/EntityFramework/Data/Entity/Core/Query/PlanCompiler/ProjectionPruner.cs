using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Query.InternalTrees;
using System.Linq;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x0200035C RID: 860
	internal class ProjectionPruner : BasicOpVisitorOfNode
	{
		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x060029BE RID: 10686 RVA: 0x000877D3 File Offset: 0x000859D3
		private Command m_command
		{
			get
			{
				return this.m_compilerState.Command;
			}
		}

		// Token: 0x060029BF RID: 10687 RVA: 0x000877E0 File Offset: 0x000859E0
		private ProjectionPruner(PlanCompiler compilerState)
		{
			this.m_compilerState = compilerState;
			this.m_referencedVars = compilerState.Command.CreateVarVec();
		}

		// Token: 0x060029C0 RID: 10688 RVA: 0x00087800 File Offset: 0x00085A00
		internal static void Process(PlanCompiler compilerState)
		{
			compilerState.Command.Root = ProjectionPruner.Process(compilerState, compilerState.Command.Root);
		}

		// Token: 0x060029C1 RID: 10689 RVA: 0x0008781E File Offset: 0x00085A1E
		internal static Node Process(PlanCompiler compilerState, Node node)
		{
			return new ProjectionPruner(compilerState).Process(node);
		}

		// Token: 0x060029C2 RID: 10690 RVA: 0x0008782C File Offset: 0x00085A2C
		private Node Process(Node node)
		{
			return base.VisitNode(node);
		}

		// Token: 0x060029C3 RID: 10691 RVA: 0x00087835 File Offset: 0x00085A35
		private void AddReference(Var v)
		{
			this.m_referencedVars.Set(v);
		}

		// Token: 0x060029C4 RID: 10692 RVA: 0x00087844 File Offset: 0x00085A44
		private void AddReference(IEnumerable<Var> varSet)
		{
			foreach (Var var in varSet)
			{
				this.AddReference(var);
			}
		}

		// Token: 0x060029C5 RID: 10693 RVA: 0x0008788C File Offset: 0x00085A8C
		private bool IsReferenced(Var v)
		{
			return this.m_referencedVars.IsSet(v);
		}

		// Token: 0x060029C6 RID: 10694 RVA: 0x0008789A File Offset: 0x00085A9A
		private bool IsUnreferenced(Var v)
		{
			return !this.IsReferenced(v);
		}

		// Token: 0x060029C7 RID: 10695 RVA: 0x000878A8 File Offset: 0x00085AA8
		private void PruneVarMap(VarMap varMap)
		{
			List<Var> list = new List<Var>();
			foreach (Var var in varMap.Keys)
			{
				if (!this.IsReferenced(var))
				{
					list.Add(var);
				}
				else
				{
					this.AddReference(varMap[var]);
				}
			}
			foreach (Var var2 in list)
			{
				varMap.Remove(var2);
			}
		}

		// Token: 0x060029C8 RID: 10696 RVA: 0x0008795C File Offset: 0x00085B5C
		private void PruneVarSet(VarVec varSet)
		{
			varSet.And(this.m_referencedVars);
		}

		// Token: 0x060029C9 RID: 10697 RVA: 0x0008796A File Offset: 0x00085B6A
		protected override void VisitChildren(Node n)
		{
			base.VisitChildren(n);
			this.m_command.RecomputeNodeInfo(n);
		}

		// Token: 0x060029CA RID: 10698 RVA: 0x0008797F File Offset: 0x00085B7F
		protected override void VisitChildrenReverse(Node n)
		{
			base.VisitChildrenReverse(n);
			this.m_command.RecomputeNodeInfo(n);
		}

		// Token: 0x060029CB RID: 10699 RVA: 0x00087994 File Offset: 0x00085B94
		public override Node Visit(VarDefListOp op, Node n)
		{
			List<Node> list = new List<Node>();
			foreach (Node node in n.Children)
			{
				VarDefOp varDefOp = node.Op as VarDefOp;
				if (this.IsReferenced(varDefOp.Var))
				{
					list.Add(base.VisitNode(node));
				}
			}
			return this.m_command.CreateNode(op, list);
		}

		// Token: 0x060029CC RID: 10700 RVA: 0x00087A1C File Offset: 0x00085C1C
		public override Node Visit(PhysicalProjectOp op, Node n)
		{
			if (n == this.m_command.Root)
			{
				ProjectionPruner.ColumnMapVarTracker.FindVars(op.ColumnMap, this.m_referencedVars);
				op.Outputs.RemoveAll(new Predicate<Var>(this.IsUnreferenced));
			}
			else
			{
				this.AddReference(op.Outputs);
			}
			this.VisitChildren(n);
			return n;
		}

		// Token: 0x060029CD RID: 10701 RVA: 0x00087A76 File Offset: 0x00085C76
		protected override Node VisitNestOp(NestBaseOp op, Node n)
		{
			this.AddReference(op.Outputs);
			this.VisitChildren(n);
			return n;
		}

		// Token: 0x060029CE RID: 10702 RVA: 0x00087A8C File Offset: 0x00085C8C
		public override Node Visit(SingleStreamNestOp op, Node n)
		{
			this.AddReference(op.Discriminator);
			return this.VisitNestOp(op, n);
		}

		// Token: 0x060029CF RID: 10703 RVA: 0x00087AA2 File Offset: 0x00085CA2
		public override Node Visit(MultiStreamNestOp op, Node n)
		{
			return this.VisitNestOp(op, n);
		}

		// Token: 0x060029D0 RID: 10704 RVA: 0x00087AAC File Offset: 0x00085CAC
		protected override Node VisitApplyOp(ApplyBaseOp op, Node n)
		{
			this.VisitChildrenReverse(n);
			return n;
		}

		// Token: 0x060029D1 RID: 10705 RVA: 0x00087AB8 File Offset: 0x00085CB8
		public override Node Visit(DistinctOp op, Node n)
		{
			if (op.Keys.Count > 1 && n.Child0.Op.OpType == OpType.Project)
			{
				this.RemoveRedundantConstantKeys(op.Keys, ((ProjectOp)n.Child0.Op).Outputs, n.Child0.Child1);
			}
			this.AddReference(op.Keys);
			this.VisitChildren(n);
			return n;
		}

		// Token: 0x060029D2 RID: 10706 RVA: 0x00087B28 File Offset: 0x00085D28
		public override Node Visit(ElementOp op, Node n)
		{
			ExtendedNodeInfo extendedNodeInfo = this.m_command.GetExtendedNodeInfo(n.Child0);
			this.AddReference(extendedNodeInfo.Definitions);
			n.Child0 = base.VisitNode(n.Child0);
			this.m_command.RecomputeNodeInfo(n);
			return n;
		}

		// Token: 0x060029D3 RID: 10707 RVA: 0x00087B72 File Offset: 0x00085D72
		public override Node Visit(FilterOp op, Node n)
		{
			this.VisitChildrenReverse(n);
			return n;
		}

		// Token: 0x060029D4 RID: 10708 RVA: 0x00087B7C File Offset: 0x00085D7C
		protected override Node VisitGroupByOp(GroupByBaseOp op, Node n)
		{
			for (int i = n.Children.Count - 1; i >= 2; i--)
			{
				n.Children[i] = base.VisitNode(n.Children[i]);
			}
			if (op.Keys.Count > 1)
			{
				this.RemoveRedundantConstantKeys(op.Keys, op.Outputs, n.Child1);
			}
			this.AddReference(op.Keys);
			n.Children[1] = base.VisitNode(n.Children[1]);
			n.Children[0] = base.VisitNode(n.Children[0]);
			this.PruneVarSet(op.Outputs);
			if (op.Keys.Count == 0 && op.Outputs.Count == 0)
			{
				return this.m_command.CreateNode(this.m_command.CreateSingleRowTableOp());
			}
			this.m_command.RecomputeNodeInfo(n);
			return n;
		}

		// Token: 0x060029D5 RID: 10709 RVA: 0x00087C78 File Offset: 0x00085E78
		private void RemoveRedundantConstantKeys(VarVec keyVec, VarVec outputVec, Node varDefListNode)
		{
			List<Node> constantKeys = varDefListNode.Children.Where((Node d) => d.Op.OpType == OpType.VarDef && PlanCompilerUtil.IsConstantBaseOp(d.Child0.Op.OpType)).ToList<Node>();
			VarVec constantKeyVars = this.m_command.CreateVarVec(constantKeys.Select((Node d) => ((VarDefOp)d.Op).Var));
			constantKeyVars.Minus(this.m_referencedVars);
			keyVec.Minus(constantKeyVars);
			outputVec.Minus(constantKeyVars);
			varDefListNode.Children.RemoveAll((Node c) => constantKeys.Contains(c) && constantKeyVars.IsSet(((VarDefOp)c.Op).Var));
			if (keyVec.Count == 0)
			{
				Node node = constantKeys.First<Node>();
				Var var = ((VarDefOp)node.Op).Var;
				keyVec.Set(var);
				outputVec.Set(var);
				varDefListNode.Children.Add(node);
			}
		}

		// Token: 0x060029D6 RID: 10710 RVA: 0x00087D7C File Offset: 0x00085F7C
		public override Node Visit(GroupByIntoOp op, Node n)
		{
			Node node = this.VisitGroupByOp(op, n);
			if (node.Op.OpType == OpType.GroupByInto && n.Child3.Children.Count == 0)
			{
				GroupByIntoOp groupByIntoOp = (GroupByIntoOp)node.Op;
				node = this.m_command.CreateNode(this.m_command.CreateGroupByOp(groupByIntoOp.Keys, groupByIntoOp.Outputs), node.Child0, node.Child1, node.Child2);
			}
			return node;
		}

		// Token: 0x060029D7 RID: 10711 RVA: 0x00087DF8 File Offset: 0x00085FF8
		protected override Node VisitJoinOp(JoinBaseOp op, Node n)
		{
			if (n.Op.OpType == OpType.CrossJoin)
			{
				this.VisitChildren(n);
				return n;
			}
			n.Child2 = base.VisitNode(n.Child2);
			n.Child0 = base.VisitNode(n.Child0);
			n.Child1 = base.VisitNode(n.Child1);
			this.m_command.RecomputeNodeInfo(n);
			return n;
		}

		// Token: 0x060029D8 RID: 10712 RVA: 0x00087E60 File Offset: 0x00086060
		public override Node Visit(ProjectOp op, Node n)
		{
			this.PruneVarSet(op.Outputs);
			this.VisitChildrenReverse(n);
			if (!op.Outputs.IsEmpty)
			{
				return n;
			}
			return n.Child0;
		}

		// Token: 0x060029D9 RID: 10713 RVA: 0x00087E8A File Offset: 0x0008608A
		public override Node Visit(ScanTableOp op, Node n)
		{
			PlanCompiler.Assert(!n.HasChild0, "scanTable with an input?");
			op.Table.ReferencedColumns.And(this.m_referencedVars);
			this.m_command.RecomputeNodeInfo(n);
			return n;
		}

		// Token: 0x060029DA RID: 10714 RVA: 0x00087EC4 File Offset: 0x000860C4
		protected override Node VisitSetOp(SetOp op, Node n)
		{
			if (OpType.Intersect == op.OpType || OpType.Except == op.OpType)
			{
				this.AddReference(op.Outputs);
			}
			this.PruneVarSet(op.Outputs);
			foreach (VarMap varMap2 in op.VarMap)
			{
				this.PruneVarMap(varMap2);
			}
			this.VisitChildren(n);
			return n;
		}

		// Token: 0x060029DB RID: 10715 RVA: 0x00087F28 File Offset: 0x00086128
		protected override Node VisitSortOp(SortBaseOp op, Node n)
		{
			foreach (SortKey sortKey in op.Keys)
			{
				this.AddReference(sortKey.Var);
			}
			if (n.HasChild1)
			{
				n.Child1 = base.VisitNode(n.Child1);
			}
			n.Child0 = base.VisitNode(n.Child0);
			this.m_command.RecomputeNodeInfo(n);
			return n;
		}

		// Token: 0x060029DC RID: 10716 RVA: 0x00087FBC File Offset: 0x000861BC
		public override Node Visit(UnnestOp op, Node n)
		{
			this.AddReference(op.Var);
			this.VisitChildren(n);
			return n;
		}

		// Token: 0x060029DD RID: 10717 RVA: 0x00087FD2 File Offset: 0x000861D2
		public override Node Visit(VarRefOp op, Node n)
		{
			this.AddReference(op.Var);
			return n;
		}

		// Token: 0x060029DE RID: 10718 RVA: 0x00087FE4 File Offset: 0x000861E4
		public override Node Visit(ExistsOp op, Node n)
		{
			ProjectOp projectOp = (ProjectOp)n.Child0.Op;
			this.AddReference(projectOp.Outputs.First);
			this.VisitChildren(n);
			return n;
		}

		// Token: 0x04000E64 RID: 3684
		private readonly PlanCompiler m_compilerState;

		// Token: 0x04000E65 RID: 3685
		private readonly VarVec m_referencedVars;

		// Token: 0x020009F4 RID: 2548
		private class ColumnMapVarTracker : ColumnMapVisitor<VarVec>
		{
			// Token: 0x06006019 RID: 24601 RVA: 0x0014A750 File Offset: 0x00148950
			internal static void FindVars(ColumnMap columnMap, VarVec vec)
			{
				ProjectionPruner.ColumnMapVarTracker columnMapVarTracker = new ProjectionPruner.ColumnMapVarTracker();
				columnMap.Accept<VarVec>(columnMapVarTracker, vec);
			}

			// Token: 0x0600601A RID: 24602 RVA: 0x0014A76B File Offset: 0x0014896B
			private ColumnMapVarTracker()
			{
			}

			// Token: 0x0600601B RID: 24603 RVA: 0x0014A773 File Offset: 0x00148973
			internal override void Visit(VarRefColumnMap columnMap, VarVec arg)
			{
				arg.Set(columnMap.Var);
				base.Visit(columnMap, arg);
			}
		}
	}
}
