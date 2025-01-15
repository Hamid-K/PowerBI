using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x0200036D RID: 877
	internal abstract class SubqueryTrackingVisitor : BasicOpVisitorOfNode
	{
		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x06002A89 RID: 10889 RVA: 0x0008B8E8 File Offset: 0x00089AE8
		protected Command m_command
		{
			get
			{
				return this.m_compilerState.Command;
			}
		}

		// Token: 0x06002A8A RID: 10890 RVA: 0x0008B8F5 File Offset: 0x00089AF5
		protected SubqueryTrackingVisitor(PlanCompiler planCompilerState)
		{
			this.m_compilerState = planCompilerState;
		}

		// Token: 0x06002A8B RID: 10891 RVA: 0x0008B91C File Offset: 0x00089B1C
		protected void AddSubqueryToRelOpNode(Node relOpNode, Node subquery)
		{
			List<Node> list;
			if (!this.m_nodeSubqueries.TryGetValue(relOpNode, out list))
			{
				list = new List<Node>();
				this.m_nodeSubqueries[relOpNode] = list;
			}
			list.Add(subquery);
		}

		// Token: 0x06002A8C RID: 10892 RVA: 0x0008B954 File Offset: 0x00089B54
		protected Node AddSubqueryToParentRelOp(Var outputVar, Node subquery)
		{
			Node node = this.FindRelOpAncestor();
			PlanCompiler.Assert(node != null, "no ancestors found?");
			this.AddSubqueryToRelOpNode(node, subquery);
			subquery = this.m_command.CreateNode(this.m_command.CreateVarRefOp(outputVar));
			return subquery;
		}

		// Token: 0x06002A8D RID: 10893 RVA: 0x0008B998 File Offset: 0x00089B98
		protected Node FindRelOpAncestor()
		{
			foreach (Node node in this.m_ancestors)
			{
				if (node.Op.IsRelOp)
				{
					return node;
				}
				if (node.Op.IsPhysicalOp)
				{
					return null;
				}
			}
			return null;
		}

		// Token: 0x06002A8E RID: 10894 RVA: 0x0008BA0C File Offset: 0x00089C0C
		protected override void VisitChildren(Node n)
		{
			this.m_ancestors.Push(n);
			for (int i = 0; i < n.Children.Count; i++)
			{
				n.Children[i] = base.VisitNode(n.Children[i]);
			}
			this.m_ancestors.Pop();
		}

		// Token: 0x06002A8F RID: 10895 RVA: 0x0008BA68 File Offset: 0x00089C68
		private Node AugmentWithSubqueries(Node input, List<Node> subqueries, bool inputFirst)
		{
			Node node;
			int num;
			if (inputFirst)
			{
				node = input;
				num = 0;
			}
			else
			{
				node = subqueries[0];
				num = 1;
			}
			for (int i = num; i < subqueries.Count; i++)
			{
				OuterApplyOp outerApplyOp = this.m_command.CreateOuterApplyOp();
				node = this.m_command.CreateNode(outerApplyOp, node, subqueries[i]);
			}
			if (!inputFirst)
			{
				node = this.m_command.CreateNode(this.m_command.CreateCrossApplyOp(), node, input);
			}
			this.m_compilerState.MarkPhaseAsNeeded(PlanCompilerPhase.JoinElimination);
			return node;
		}

		// Token: 0x06002A90 RID: 10896 RVA: 0x0008BAE4 File Offset: 0x00089CE4
		protected override Node VisitRelOpDefault(RelOp op, Node n)
		{
			this.VisitChildren(n);
			List<Node> list;
			if (this.m_nodeSubqueries.TryGetValue(n, out list) && list.Count > 0)
			{
				PlanCompiler.Assert(n.Op.OpType == OpType.Project || n.Op.OpType == OpType.Filter || n.Op.OpType == OpType.GroupBy || n.Op.OpType == OpType.GroupByInto, "VisitRelOpDefault: Unexpected op?" + n.Op.OpType.ToString());
				Node node = this.AugmentWithSubqueries(n.Child0, list, true);
				n.Child0 = node;
			}
			return n;
		}

		// Token: 0x06002A91 RID: 10897 RVA: 0x0008BB94 File Offset: 0x00089D94
		protected bool ProcessJoinOp(Node n)
		{
			this.VisitChildren(n);
			List<Node> list;
			if (!this.m_nodeSubqueries.TryGetValue(n, out list))
			{
				return false;
			}
			PlanCompiler.Assert(n.Op.OpType == OpType.InnerJoin || n.Op.OpType == OpType.LeftOuterJoin || n.Op.OpType == OpType.FullOuterJoin, "unexpected op?");
			PlanCompiler.Assert(n.HasChild2, "missing second child to JoinOp?");
			Node child = n.Child2;
			Node node = this.m_command.CreateNode(this.m_command.CreateSingleRowTableOp());
			node = this.AugmentWithSubqueries(node, list, true);
			Node node2 = this.m_command.CreateNode(this.m_command.CreateFilterOp(), node, child);
			Node node3 = this.m_command.CreateNode(this.m_command.CreateExistsOp(), node2);
			n.Child2 = node3;
			return true;
		}

		// Token: 0x06002A92 RID: 10898 RVA: 0x0008BC68 File Offset: 0x00089E68
		public override Node Visit(UnnestOp op, Node n)
		{
			this.VisitChildren(n);
			List<Node> list;
			if (this.m_nodeSubqueries.TryGetValue(n, out list))
			{
				return this.AugmentWithSubqueries(n, list, false);
			}
			return n;
		}

		// Token: 0x04000EAA RID: 3754
		protected readonly PlanCompiler m_compilerState;

		// Token: 0x04000EAB RID: 3755
		protected readonly Stack<Node> m_ancestors = new Stack<Node>();

		// Token: 0x04000EAC RID: 3756
		private readonly Dictionary<Node, List<Node>> m_nodeSubqueries = new Dictionary<Node, List<Node>>();
	}
}
