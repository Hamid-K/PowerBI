using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x0200032C RID: 812
	internal class AggregatePushdown
	{
		// Token: 0x060026E0 RID: 9952 RVA: 0x0006FFA8 File Offset: 0x0006E1A8
		private AggregatePushdown(Command command)
		{
			this.m_command = command;
		}

		// Token: 0x060026E1 RID: 9953 RVA: 0x0006FFB7 File Offset: 0x0006E1B7
		internal static void Process(PlanCompiler planCompilerState)
		{
			new AggregatePushdown(planCompilerState.Command).Process();
		}

		// Token: 0x060026E2 RID: 9954 RVA: 0x0006FFCC File Offset: 0x0006E1CC
		private void Process()
		{
			foreach (GroupAggregateVarInfo groupAggregateVarInfo in GroupAggregateRefComputingVisitor.Process(this.m_command, out this.m_tryGetParent))
			{
				if (groupAggregateVarInfo.HasCandidateAggregateNodes)
				{
					foreach (KeyValuePair<Node, List<Node>> keyValuePair in groupAggregateVarInfo.CandidateAggregateNodes)
					{
						this.TryProcessCandidate(keyValuePair, groupAggregateVarInfo);
					}
				}
			}
		}

		// Token: 0x060026E3 RID: 9955 RVA: 0x00070068 File Offset: 0x0006E268
		private void TryProcessCandidate(KeyValuePair<Node, List<Node>> candidate, GroupAggregateVarInfo groupAggregateVarInfo)
		{
			Node definingGroupNode = groupAggregateVarInfo.DefiningGroupNode;
			IList<Node> list;
			IList<Node> list2;
			this.FindPathsToLeastCommonAncestor(candidate.Key, definingGroupNode, out list, out list2);
			if (!AggregatePushdown.AreAllNodesSupportedForPropagation(list2))
			{
				return;
			}
			GroupByIntoOp groupByIntoOp = (GroupByIntoOp)definingGroupNode.Op;
			PlanCompiler.Assert(groupByIntoOp.Inputs.Count == 1, "There should be one input var to GroupByInto at this stage");
			Var first = groupByIntoOp.Inputs.First;
			FunctionOp functionOp = (FunctionOp)candidate.Key.Op;
			Dictionary<Var, Var> dictionary = new Dictionary<Var, Var>(1);
			dictionary.Add(groupAggregateVarInfo.GroupAggregateVar, first);
			VarRemapper varRemapper = new VarRemapper(this.m_command, dictionary);
			List<Node> list3 = new List<Node>(candidate.Value.Count);
			foreach (Node node in candidate.Value)
			{
				Node node2 = OpCopier.Copy(this.m_command, node);
				varRemapper.RemapSubtree(node2);
				list3.Add(node2);
			}
			Node node3 = this.m_command.CreateNode(this.m_command.CreateAggregateOp(functionOp.Function, false), list3);
			Var var;
			Node node4 = this.m_command.CreateVarDefNode(node3, out var);
			definingGroupNode.Child2.Children.Add(node4);
			((GroupByIntoOp)definingGroupNode.Op).Outputs.Set(var);
			for (int i = 0; i < list2.Count; i++)
			{
				Node node5 = list2[i];
				if (node5.Op.OpType == OpType.Project)
				{
					((ProjectOp)node5.Op).Outputs.Set(var);
				}
			}
			candidate.Key.Op = this.m_command.CreateVarRefOp(var);
			candidate.Key.Children.Clear();
		}

		// Token: 0x060026E4 RID: 9956 RVA: 0x00070240 File Offset: 0x0006E440
		private static bool AreAllNodesSupportedForPropagation(IList<Node> nodes)
		{
			foreach (Node node in nodes)
			{
				if (node.Op.OpType != OpType.Project && node.Op.OpType != OpType.Filter && node.Op.OpType != OpType.ConstrainedSort)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060026E5 RID: 9957 RVA: 0x000702B8 File Offset: 0x0006E4B8
		private void FindPathsToLeastCommonAncestor(Node node1, Node node2, out IList<Node> ancestors1, out IList<Node> ancestors2)
		{
			ancestors1 = this.FindAncestors(node1);
			ancestors2 = this.FindAncestors(node2);
			int num = ancestors1.Count - 1;
			int num2 = ancestors2.Count - 1;
			while (ancestors1[num] == ancestors2[num2])
			{
				num--;
				num2--;
			}
			for (int i = ancestors1.Count - 1; i > num; i--)
			{
				ancestors1.RemoveAt(i);
			}
			for (int j = ancestors2.Count - 1; j > num2; j--)
			{
				ancestors2.RemoveAt(j);
			}
		}

		// Token: 0x060026E6 RID: 9958 RVA: 0x00070344 File Offset: 0x0006E544
		private IList<Node> FindAncestors(Node node)
		{
			List<Node> list = new List<Node>();
			Node node2 = node;
			Node node3;
			while (this.m_tryGetParent(node2, out node3))
			{
				list.Add(node3);
				node2 = node3;
			}
			return list;
		}

		// Token: 0x04000D85 RID: 3461
		private readonly Command m_command;

		// Token: 0x04000D86 RID: 3462
		private TryGetValue m_tryGetParent;
	}
}
