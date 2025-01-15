using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000369 RID: 873
	internal class SortRemover : BasicOpVisitorOfNode
	{
		// Token: 0x06002A53 RID: 10835 RVA: 0x0008A613 File Offset: 0x00088813
		private SortRemover(Command command, Node topMostSort)
		{
			this.m_command = command;
			this.m_topMostSort = topMostSort;
		}

		// Token: 0x06002A54 RID: 10836 RVA: 0x0008A634 File Offset: 0x00088834
		internal static void Process(Command command)
		{
			Node node;
			if (command.Root.Child0 != null && command.Root.Child0.Op.OpType == OpType.Sort)
			{
				node = command.Root.Child0;
			}
			else
			{
				node = null;
			}
			SortRemover sortRemover = new SortRemover(command, node);
			command.Root = sortRemover.VisitNode(command.Root);
		}

		// Token: 0x06002A55 RID: 10837 RVA: 0x0008A694 File Offset: 0x00088894
		protected override void VisitChildren(Node n)
		{
			bool flag = false;
			for (int i = 0; i < n.Children.Count; i++)
			{
				Node node = n.Children[i];
				n.Children[i] = base.VisitNode(n.Children[i]);
				if (node != n.Children[i] || this.changedNodes.Contains(node))
				{
					flag = true;
				}
			}
			if (flag)
			{
				this.m_command.RecomputeNodeInfo(n);
				this.changedNodes.Add(n);
			}
		}

		// Token: 0x06002A56 RID: 10838 RVA: 0x0008A720 File Offset: 0x00088920
		public override Node Visit(ConstrainedSortOp op, Node n)
		{
			if (op.Keys.Count > 0 || n.Children.Count != 3 || n.Child0 == null || n.Child1 == null || n.Child0.Op.OpType != OpType.Sort || n.Child1.Op.OpType != OpType.Null || n.Child0.Children.Count != 1)
			{
				return n;
			}
			return this.m_command.CreateNode(this.m_command.CreateConstrainedSortOp(((SortOp)n.Child0.Op).Keys, op.WithTies), n.Child0.Child0, n.Child1, n.Child2);
		}

		// Token: 0x06002A57 RID: 10839 RVA: 0x0008A7E0 File Offset: 0x000889E0
		public override Node Visit(SortOp op, Node n)
		{
			this.VisitChildren(n);
			Node node;
			if (n == this.m_topMostSort)
			{
				node = n;
			}
			else
			{
				node = n.Child0;
			}
			return node;
		}

		// Token: 0x04000E96 RID: 3734
		private readonly Command m_command;

		// Token: 0x04000E97 RID: 3735
		private readonly Node m_topMostSort;

		// Token: 0x04000E98 RID: 3736
		private readonly HashSet<Node> changedNodes = new HashSet<Node>();
	}
}
