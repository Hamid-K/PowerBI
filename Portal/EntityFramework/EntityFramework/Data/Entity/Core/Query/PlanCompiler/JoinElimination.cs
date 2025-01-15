using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x0200034A RID: 842
	internal class JoinElimination : BasicOpVisitorOfNode
	{
		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x06002855 RID: 10325 RVA: 0x00079CD7 File Offset: 0x00077ED7
		private Command Command
		{
			get
			{
				return this.m_compilerState.Command;
			}
		}

		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x06002856 RID: 10326 RVA: 0x00079CE4 File Offset: 0x00077EE4
		private ConstraintManager ConstraintManager
		{
			get
			{
				return this.m_compilerState.ConstraintManager;
			}
		}

		// Token: 0x06002857 RID: 10327 RVA: 0x00079CF4 File Offset: 0x00077EF4
		private JoinElimination(PlanCompiler compilerState)
		{
			this.m_compilerState = compilerState;
			this.m_varRemapper = new VarRemapper(this.m_compilerState.Command);
			this.m_varRefManager = new VarRefManager(this.m_compilerState.Command);
		}

		// Token: 0x06002858 RID: 10328 RVA: 0x00079D45 File Offset: 0x00077F45
		internal static bool Process(PlanCompiler compilerState)
		{
			JoinElimination joinElimination = new JoinElimination(compilerState);
			joinElimination.Process();
			return joinElimination.m_treeModified;
		}

		// Token: 0x06002859 RID: 10329 RVA: 0x00079D58 File Offset: 0x00077F58
		private void Process()
		{
			this.Command.Root = base.VisitNode(this.Command.Root);
		}

		// Token: 0x0600285A RID: 10330 RVA: 0x00079D76 File Offset: 0x00077F76
		private bool NeedsJoinGraph(Node joinNode)
		{
			return !this.m_joinGraphUnnecessaryMap.ContainsKey(joinNode);
		}

		// Token: 0x0600285B RID: 10331 RVA: 0x00079D88 File Offset: 0x00077F88
		private Node ProcessJoinGraph(Node joinNode)
		{
			VarMap varMap;
			Dictionary<Node, Node> dictionary;
			Node node = new JoinGraph(this.Command, this.ConstraintManager, this.m_varRefManager, joinNode).DoJoinElimination(out varMap, out dictionary);
			foreach (KeyValuePair<Var, Var> keyValuePair in varMap)
			{
				this.m_varRemapper.AddMapping(keyValuePair.Key, keyValuePair.Value);
			}
			foreach (Node node2 in dictionary.Keys)
			{
				this.m_joinGraphUnnecessaryMap[node2] = node2;
			}
			return node;
		}

		// Token: 0x0600285C RID: 10332 RVA: 0x00079E58 File Offset: 0x00078058
		private Node VisitDefaultForAllNodes(Node n)
		{
			this.VisitChildren(n);
			this.m_varRemapper.RemapNode(n);
			this.Command.RecomputeNodeInfo(n);
			return n;
		}

		// Token: 0x0600285D RID: 10333 RVA: 0x00079E7A File Offset: 0x0007807A
		protected override Node VisitDefault(Node n)
		{
			this.m_varRefManager.AddChildren(n);
			return this.VisitDefaultForAllNodes(n);
		}

		// Token: 0x0600285E RID: 10334 RVA: 0x00079E90 File Offset: 0x00078090
		protected override Node VisitJoinOp(JoinBaseOp op, Node joinNode)
		{
			Node node;
			if (this.NeedsJoinGraph(joinNode))
			{
				node = this.ProcessJoinGraph(joinNode);
				if (node != joinNode)
				{
					this.m_treeModified = true;
				}
			}
			else
			{
				node = joinNode;
			}
			return this.VisitDefaultForAllNodes(node);
		}

		// Token: 0x04000E07 RID: 3591
		private readonly PlanCompiler m_compilerState;

		// Token: 0x04000E08 RID: 3592
		private readonly Dictionary<Node, Node> m_joinGraphUnnecessaryMap = new Dictionary<Node, Node>();

		// Token: 0x04000E09 RID: 3593
		private readonly VarRemapper m_varRemapper;

		// Token: 0x04000E0A RID: 3594
		private bool m_treeModified;

		// Token: 0x04000E0B RID: 3595
		private readonly VarRefManager m_varRefManager;
	}
}
