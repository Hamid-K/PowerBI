using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003DC RID: 988
	internal class RuleProcessor
	{
		// Token: 0x06002EDC RID: 11996 RVA: 0x00095082 File Offset: 0x00093282
		internal RuleProcessor()
		{
			this.m_processedNodeMap = new Dictionary<SubTreeId, SubTreeId>();
		}

		// Token: 0x06002EDD RID: 11997 RVA: 0x00095098 File Offset: 0x00093298
		private static bool ApplyRulesToNode(RuleProcessingContext context, ReadOnlyCollection<ReadOnlyCollection<Rule>> rules, Node currentNode, out Node newNode)
		{
			newNode = currentNode;
			context.PreProcess(currentNode);
			foreach (Rule rule in rules[(int)currentNode.Op.OpType])
			{
				if (rule.Match(currentNode) && rule.Apply(context, currentNode, out newNode))
				{
					context.PostProcess(newNode, rule);
					return true;
				}
			}
			context.PostProcess(currentNode, null);
			return false;
		}

		// Token: 0x06002EDE RID: 11998 RVA: 0x00095120 File Offset: 0x00093320
		private Node ApplyRulesToSubtree(RuleProcessingContext context, ReadOnlyCollection<ReadOnlyCollection<Rule>> rules, Node subTreeRoot, Node parent, int childIndexInParent)
		{
			int num = 0;
			Dictionary<SubTreeId, SubTreeId> dictionary = new Dictionary<SubTreeId, SubTreeId>();
			SubTreeId subTreeId;
			for (;;)
			{
				num++;
				context.PreProcessSubTree(subTreeRoot);
				subTreeId = new SubTreeId(context, subTreeRoot, parent, childIndexInParent);
				if (this.m_processedNodeMap.ContainsKey(subTreeId))
				{
					goto IL_00C7;
				}
				if (dictionary.ContainsKey(subTreeId))
				{
					break;
				}
				dictionary[subTreeId] = subTreeId;
				for (int i = 0; i < subTreeRoot.Children.Count; i++)
				{
					Node node = subTreeRoot.Children[i];
					if (RuleProcessor.ShouldApplyRules(node, subTreeRoot))
					{
						subTreeRoot.Children[i] = this.ApplyRulesToSubtree(context, rules, node, subTreeRoot, i);
					}
				}
				Node node2;
				if (!RuleProcessor.ApplyRulesToNode(context, rules, subTreeRoot, out node2))
				{
					goto Block_5;
				}
				context.PostProcessSubTree(subTreeRoot);
				subTreeRoot = node2;
			}
			this.m_processedNodeMap[subTreeId] = subTreeId;
			goto IL_00C7;
			Block_5:
			this.m_processedNodeMap[subTreeId] = subTreeId;
			IL_00C7:
			context.PostProcessSubTree(subTreeRoot);
			return subTreeRoot;
		}

		// Token: 0x06002EDF RID: 11999 RVA: 0x000951FC File Offset: 0x000933FC
		private static bool ShouldApplyRules(Node node, Node parent)
		{
			return parent.Op.OpType != OpType.In || node.Op.OpType > OpType.Constant;
		}

		// Token: 0x06002EE0 RID: 12000 RVA: 0x0009521D File Offset: 0x0009341D
		internal Node ApplyRulesToSubtree(RuleProcessingContext context, ReadOnlyCollection<ReadOnlyCollection<Rule>> rules, Node subTreeRoot)
		{
			return this.ApplyRulesToSubtree(context, rules, subTreeRoot, null, 0);
		}

		// Token: 0x04000FCF RID: 4047
		private readonly Dictionary<SubTreeId, SubTreeId> m_processedNodeMap;
	}
}
