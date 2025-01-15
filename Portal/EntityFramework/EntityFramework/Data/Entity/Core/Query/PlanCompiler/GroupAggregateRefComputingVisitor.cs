using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000342 RID: 834
	internal class GroupAggregateRefComputingVisitor : BasicOpVisitor
	{
		// Token: 0x060027C0 RID: 10176 RVA: 0x0007556C File Offset: 0x0007376C
		internal static IEnumerable<GroupAggregateVarInfo> Process(Command itree, out TryGetValue tryGetParent)
		{
			GroupAggregateRefComputingVisitor groupAggregateRefComputingVisitor = new GroupAggregateRefComputingVisitor(itree);
			groupAggregateRefComputingVisitor.VisitNode(itree.Root);
			tryGetParent = new TryGetValue(groupAggregateRefComputingVisitor._childToParent.TryGetValue);
			return groupAggregateRefComputingVisitor._groupAggregateVarInfoManager.GroupAggregateVarInfos;
		}

		// Token: 0x060027C1 RID: 10177 RVA: 0x000755AB File Offset: 0x000737AB
		private GroupAggregateRefComputingVisitor(Command itree)
		{
			this._command = itree;
		}

		// Token: 0x060027C2 RID: 10178 RVA: 0x000755D0 File Offset: 0x000737D0
		public override void Visit(VarDefOp op, Node n)
		{
			this.VisitDefault(n);
			Node child = n.Child0;
			Op op2 = child.Op;
			GroupAggregateVarInfo groupAggregateVarInfo;
			Node node;
			bool flag;
			if (GroupAggregateVarComputationTranslator.TryTranslateOverGroupAggregateVar(child, true, this._command, this._groupAggregateVarInfoManager, out groupAggregateVarInfo, out node, out flag))
			{
				this._groupAggregateVarInfoManager.Add(op.Var, groupAggregateVarInfo, node, flag);
				return;
			}
			if (op2.OpType == OpType.NewRecord)
			{
				NewRecordOp newRecordOp = (NewRecordOp)op2;
				for (int i = 0; i < child.Children.Count; i++)
				{
					if (GroupAggregateVarComputationTranslator.TryTranslateOverGroupAggregateVar(child.Children[i], true, this._command, this._groupAggregateVarInfoManager, out groupAggregateVarInfo, out node, out flag))
					{
						this._groupAggregateVarInfoManager.Add(op.Var, groupAggregateVarInfo, node, flag, newRecordOp.Properties[i]);
					}
				}
			}
		}

		// Token: 0x060027C3 RID: 10179 RVA: 0x0007569C File Offset: 0x0007389C
		public override void Visit(GroupByIntoOp op, Node n)
		{
			this.VisitGroupByOp(op, n);
			foreach (Node node in n.Child3.Children)
			{
				Var var = ((VarDefOp)node.Op).Var;
				GroupAggregateVarRefInfo groupAggregateVarRefInfo;
				if (!this._groupAggregateVarInfoManager.TryGetReferencedGroupAggregateVarInfo(var, out groupAggregateVarRefInfo))
				{
					this._groupAggregateVarInfoManager.Add(var, new GroupAggregateVarInfo(n, var), this._command.CreateNode(this._command.CreateVarRefOp(var)), false);
				}
			}
		}

		// Token: 0x060027C4 RID: 10180 RVA: 0x00075740 File Offset: 0x00073940
		public override void Visit(UnnestOp op, Node n)
		{
			this.VisitDefault(n);
			GroupAggregateVarRefInfo groupAggregateVarRefInfo;
			if (this._groupAggregateVarInfoManager.TryGetReferencedGroupAggregateVarInfo(op.Var, out groupAggregateVarRefInfo))
			{
				PlanCompiler.Assert(op.Table.Columns.Count == 1, "Expected one column before NTE");
				this._groupAggregateVarInfoManager.Add(op.Table.Columns[0], groupAggregateVarRefInfo.GroupAggregateVarInfo, groupAggregateVarRefInfo.Computation, true);
			}
		}

		// Token: 0x060027C5 RID: 10181 RVA: 0x000757B0 File Offset: 0x000739B0
		public override void Visit(FunctionOp op, Node n)
		{
			this.VisitDefault(n);
			if (!PlanCompilerUtil.IsCollectionAggregateFunction(op, n))
			{
				return;
			}
			if (n.Children.Count > 1)
			{
				return;
			}
			Node child = n.Child0;
			GroupAggregateVarInfo groupAggregateVarInfo;
			Node node;
			bool flag;
			if (GroupAggregateVarComputationTranslator.TryTranslateOverGroupAggregateVar(n.Child0, false, this._command, this._groupAggregateVarInfoManager, out groupAggregateVarInfo, out node, out flag) && (flag || AggregatePushdownUtil.IsVarRefOverGivenVar(node, groupAggregateVarInfo.GroupAggregateVar)))
			{
				groupAggregateVarInfo.CandidateAggregateNodes.Add(new KeyValuePair<Node, List<Node>>(n, new List<Node> { node }));
			}
		}

		// Token: 0x060027C6 RID: 10182 RVA: 0x00075834 File Offset: 0x00073A34
		protected override void VisitDefault(Node n)
		{
			this.VisitChildren(n);
			foreach (Node node in n.Children)
			{
				if (node.Op.Arity != 0)
				{
					this._childToParent.Add(node, n);
				}
			}
		}

		// Token: 0x04000DE1 RID: 3553
		private readonly Command _command;

		// Token: 0x04000DE2 RID: 3554
		private readonly GroupAggregateVarInfoManager _groupAggregateVarInfoManager = new GroupAggregateVarInfoManager();

		// Token: 0x04000DE3 RID: 3555
		private readonly Dictionary<Node, Node> _childToParent = new Dictionary<Node, Node>();
	}
}
