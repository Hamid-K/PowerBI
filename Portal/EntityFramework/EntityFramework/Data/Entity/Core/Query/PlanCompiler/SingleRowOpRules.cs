using System;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000367 RID: 871
	internal static class SingleRowOpRules
	{
		// Token: 0x06002A4E RID: 10830 RVA: 0x0008A41C File Offset: 0x0008861C
		private static bool ProcessSingleRowOpOverAnything(RuleProcessingContext context, Node singleRowNode, out Node newNode)
		{
			newNode = singleRowNode;
			TransformationRulesContext transformationRulesContext = (TransformationRulesContext)context;
			ExtendedNodeInfo extendedNodeInfo = context.Command.GetExtendedNodeInfo(singleRowNode.Child0);
			if (extendedNodeInfo.MaxRows <= RowCount.One)
			{
				newNode = singleRowNode.Child0;
				return true;
			}
			if (singleRowNode.Child0.Op.OpType == OpType.Filter && new Predicate(context.Command, singleRowNode.Child0.Child1).SatisfiesKey(extendedNodeInfo.Keys.KeyVars, extendedNodeInfo.Definitions))
			{
				extendedNodeInfo.MaxRows = RowCount.One;
				newNode = singleRowNode.Child0;
				return true;
			}
			return false;
		}

		// Token: 0x06002A4F RID: 10831 RVA: 0x0008A4AC File Offset: 0x000886AC
		private static bool ProcessSingleRowOpOverProject(RuleProcessingContext context, Node singleRowNode, out Node newNode)
		{
			newNode = singleRowNode;
			Node child = singleRowNode.Child0;
			Node child2 = child.Child0;
			singleRowNode.Child0 = child2;
			context.Command.RecomputeNodeInfo(singleRowNode);
			child.Child0 = singleRowNode;
			newNode = child;
			return true;
		}

		// Token: 0x04000E91 RID: 3729
		internal static readonly PatternMatchRule Rule_SingleRowOpOverAnything = new PatternMatchRule(new Node(SingleRowOp.Pattern, new Node[]
		{
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(SingleRowOpRules.ProcessSingleRowOpOverAnything));

		// Token: 0x04000E92 RID: 3730
		internal static readonly PatternMatchRule Rule_SingleRowOpOverProject = new PatternMatchRule(new Node(SingleRowOp.Pattern, new Node[]
		{
			new Node(ProjectOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0])
			})
		}), new Rule.ProcessNodeDelegate(SingleRowOpRules.ProcessSingleRowOpOverProject));

		// Token: 0x04000E93 RID: 3731
		internal static readonly Rule[] Rules = new Rule[]
		{
			SingleRowOpRules.Rule_SingleRowOpOverAnything,
			SingleRowOpRules.Rule_SingleRowOpOverProject
		};
	}
}
