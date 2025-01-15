using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x0200034D RID: 845
	internal static class JoinOpRules
	{
		// Token: 0x0600289D RID: 10397 RVA: 0x0007C800 File Offset: 0x0007AA00
		private static bool ProcessJoinOverProject(RuleProcessingContext context, Node joinNode, out Node newNode)
		{
			newNode = joinNode;
			TransformationRulesContext transformationRulesContext = (TransformationRulesContext)context;
			Command command = transformationRulesContext.Command;
			Node node = (joinNode.HasChild2 ? joinNode.Child2 : null);
			Dictionary<Var, int> dictionary = new Dictionary<Var, int>();
			if (node != null && !transformationRulesContext.IsScalarOpTree(node, dictionary))
			{
				return false;
			}
			VarVec varVec = command.CreateVarVec();
			List<Node> list = new List<Node>();
			if (joinNode.Op.OpType != OpType.LeftOuterJoin && joinNode.Child0.Op.OpType == OpType.Project && joinNode.Child1.Op.OpType == OpType.Project)
			{
				ProjectOp projectOp = (ProjectOp)joinNode.Child0.Op;
				ProjectOp projectOp2 = (ProjectOp)joinNode.Child1.Op;
				Dictionary<Var, Node> varMap = transformationRulesContext.GetVarMap(joinNode.Child0.Child1, dictionary);
				Dictionary<Var, Node> varMap2 = transformationRulesContext.GetVarMap(joinNode.Child1.Child1, dictionary);
				if (varMap == null || varMap2 == null)
				{
					return false;
				}
				Node node2;
				if (node != null)
				{
					node = transformationRulesContext.ReMap(node, varMap);
					node = transformationRulesContext.ReMap(node, varMap2);
					node2 = context.Command.CreateNode(joinNode.Op, joinNode.Child0.Child0, joinNode.Child1.Child0, node);
				}
				else
				{
					node2 = context.Command.CreateNode(joinNode.Op, joinNode.Child0.Child0, joinNode.Child1.Child0);
				}
				varVec.InitFrom(projectOp.Outputs);
				foreach (Var var in projectOp2.Outputs)
				{
					varVec.Set(var);
				}
				ProjectOp projectOp3 = command.CreateProjectOp(varVec);
				list.AddRange(joinNode.Child0.Child1.Children);
				list.AddRange(joinNode.Child1.Child1.Children);
				Node node3 = command.CreateNode(command.CreateVarDefListOp(), list);
				Node node4 = command.CreateNode(projectOp3, node2, node3);
				newNode = node4;
				return true;
			}
			else
			{
				int num;
				int num2;
				if (joinNode.Child0.Op.OpType == OpType.Project)
				{
					num = 0;
					num2 = 1;
				}
				else
				{
					PlanCompiler.Assert(joinNode.Op.OpType != OpType.LeftOuterJoin, "unexpected non-LeftOuterJoin");
					num = 1;
					num2 = 0;
				}
				Node node5 = joinNode.Children[num];
				ProjectOp projectOp4 = node5.Op as ProjectOp;
				Dictionary<Var, Node> varMap3 = transformationRulesContext.GetVarMap(node5.Child1, dictionary);
				if (varMap3 == null)
				{
					return false;
				}
				ExtendedNodeInfo extendedNodeInfo = command.GetExtendedNodeInfo(joinNode.Children[num2]);
				VarVec varVec2 = command.CreateVarVec(projectOp4.Outputs);
				varVec2.Or(extendedNodeInfo.Definitions);
				projectOp4.Outputs.InitFrom(varVec2);
				if (node != null)
				{
					node = transformationRulesContext.ReMap(node, varMap3);
					joinNode.Child2 = node;
				}
				joinNode.Children[num] = node5.Child0;
				context.Command.RecomputeNodeInfo(joinNode);
				newNode = context.Command.CreateNode(projectOp4, joinNode, node5.Child1);
				return true;
			}
		}

		// Token: 0x0600289E RID: 10398 RVA: 0x0007CB0C File Offset: 0x0007AD0C
		private static bool ProcessJoinOverFilter(RuleProcessingContext context, Node joinNode, out Node newNode)
		{
			newNode = joinNode;
			TransformationRulesContext transformationRulesContext = (TransformationRulesContext)context;
			Command command = transformationRulesContext.Command;
			Node node = null;
			Node node2 = joinNode.Child0;
			if (joinNode.Child0.Op.OpType == OpType.Filter)
			{
				node = joinNode.Child0.Child1;
				node2 = joinNode.Child0.Child0;
			}
			Node node3 = joinNode.Child1;
			if (joinNode.Child1.Op.OpType == OpType.Filter && joinNode.Op.OpType != OpType.LeftOuterJoin)
			{
				if (node == null)
				{
					node = joinNode.Child1.Child1;
				}
				else
				{
					node = command.CreateNode(command.CreateConditionalOp(OpType.And), node, joinNode.Child1.Child1);
				}
				node3 = joinNode.Child1.Child0;
			}
			if (node == null)
			{
				return false;
			}
			Node node4;
			if (joinNode.Op.OpType == OpType.CrossJoin)
			{
				node4 = command.CreateNode(joinNode.Op, node2, node3);
			}
			else
			{
				node4 = command.CreateNode(joinNode.Op, node2, node3, joinNode.Child2);
			}
			FilterOp filterOp = command.CreateFilterOp();
			newNode = command.CreateNode(filterOp, node4, node);
			transformationRulesContext.SuppressFilterPushdown(newNode);
			return true;
		}

		// Token: 0x0600289F RID: 10399 RVA: 0x0007CC1F File Offset: 0x0007AE1F
		private static bool ProcessJoinOverSingleRowTable(RuleProcessingContext context, Node joinNode, out Node newNode)
		{
			newNode = joinNode;
			if (joinNode.Child0.Op.OpType == OpType.SingleRowTable)
			{
				newNode = joinNode.Child1;
			}
			else
			{
				newNode = joinNode.Child0;
			}
			return true;
		}

		// Token: 0x04000E1A RID: 3610
		internal static readonly PatternMatchRule Rule_CrossJoinOverProject1 = new PatternMatchRule(new Node(CrossJoinOp.Pattern, new Node[]
		{
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(ProjectOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0])
			})
		}), new Rule.ProcessNodeDelegate(JoinOpRules.ProcessJoinOverProject));

		// Token: 0x04000E1B RID: 3611
		internal static readonly PatternMatchRule Rule_CrossJoinOverProject2 = new PatternMatchRule(new Node(CrossJoinOp.Pattern, new Node[]
		{
			new Node(ProjectOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0])
			}),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(JoinOpRules.ProcessJoinOverProject));

		// Token: 0x04000E1C RID: 3612
		internal static readonly PatternMatchRule Rule_InnerJoinOverProject1 = new PatternMatchRule(new Node(InnerJoinOp.Pattern, new Node[]
		{
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(ProjectOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0])
			}),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(JoinOpRules.ProcessJoinOverProject));

		// Token: 0x04000E1D RID: 3613
		internal static readonly PatternMatchRule Rule_InnerJoinOverProject2 = new PatternMatchRule(new Node(InnerJoinOp.Pattern, new Node[]
		{
			new Node(ProjectOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0])
			}),
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(JoinOpRules.ProcessJoinOverProject));

		// Token: 0x04000E1E RID: 3614
		internal static readonly PatternMatchRule Rule_OuterJoinOverProject2 = new PatternMatchRule(new Node(LeftOuterJoinOp.Pattern, new Node[]
		{
			new Node(ProjectOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0])
			}),
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(JoinOpRules.ProcessJoinOverProject));

		// Token: 0x04000E1F RID: 3615
		internal static readonly PatternMatchRule Rule_CrossJoinOverFilter1 = new PatternMatchRule(new Node(CrossJoinOp.Pattern, new Node[]
		{
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(FilterOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0])
			})
		}), new Rule.ProcessNodeDelegate(JoinOpRules.ProcessJoinOverFilter));

		// Token: 0x04000E20 RID: 3616
		internal static readonly PatternMatchRule Rule_CrossJoinOverFilter2 = new PatternMatchRule(new Node(CrossJoinOp.Pattern, new Node[]
		{
			new Node(FilterOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0])
			}),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(JoinOpRules.ProcessJoinOverFilter));

		// Token: 0x04000E21 RID: 3617
		internal static readonly PatternMatchRule Rule_InnerJoinOverFilter1 = new PatternMatchRule(new Node(InnerJoinOp.Pattern, new Node[]
		{
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(FilterOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0])
			}),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(JoinOpRules.ProcessJoinOverFilter));

		// Token: 0x04000E22 RID: 3618
		internal static readonly PatternMatchRule Rule_InnerJoinOverFilter2 = new PatternMatchRule(new Node(InnerJoinOp.Pattern, new Node[]
		{
			new Node(FilterOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0])
			}),
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(JoinOpRules.ProcessJoinOverFilter));

		// Token: 0x04000E23 RID: 3619
		internal static readonly PatternMatchRule Rule_OuterJoinOverFilter2 = new PatternMatchRule(new Node(LeftOuterJoinOp.Pattern, new Node[]
		{
			new Node(FilterOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0])
			}),
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(JoinOpRules.ProcessJoinOverFilter));

		// Token: 0x04000E24 RID: 3620
		internal static readonly PatternMatchRule Rule_CrossJoinOverSingleRowTable1 = new PatternMatchRule(new Node(CrossJoinOp.Pattern, new Node[]
		{
			new Node(SingleRowTableOp.Pattern, new Node[0]),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(JoinOpRules.ProcessJoinOverSingleRowTable));

		// Token: 0x04000E25 RID: 3621
		internal static readonly PatternMatchRule Rule_CrossJoinOverSingleRowTable2 = new PatternMatchRule(new Node(CrossJoinOp.Pattern, new Node[]
		{
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(SingleRowTableOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(JoinOpRules.ProcessJoinOverSingleRowTable));

		// Token: 0x04000E26 RID: 3622
		internal static readonly PatternMatchRule Rule_LeftOuterJoinOverSingleRowTable = new PatternMatchRule(new Node(LeftOuterJoinOp.Pattern, new Node[]
		{
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(SingleRowTableOp.Pattern, new Node[0]),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(JoinOpRules.ProcessJoinOverSingleRowTable));

		// Token: 0x04000E27 RID: 3623
		internal static readonly Rule[] Rules = new Rule[]
		{
			JoinOpRules.Rule_CrossJoinOverProject1,
			JoinOpRules.Rule_CrossJoinOverProject2,
			JoinOpRules.Rule_InnerJoinOverProject1,
			JoinOpRules.Rule_InnerJoinOverProject2,
			JoinOpRules.Rule_OuterJoinOverProject2,
			JoinOpRules.Rule_CrossJoinOverFilter1,
			JoinOpRules.Rule_CrossJoinOverFilter2,
			JoinOpRules.Rule_InnerJoinOverFilter1,
			JoinOpRules.Rule_InnerJoinOverFilter2,
			JoinOpRules.Rule_OuterJoinOverFilter2,
			JoinOpRules.Rule_CrossJoinOverSingleRowTable1,
			JoinOpRules.Rule_CrossJoinOverSingleRowTable2,
			JoinOpRules.Rule_LeftOuterJoinOverSingleRowTable
		};
	}
}
