using System;
using System.Collections.Generic;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Parser.Tree;

namespace dotless.Core.Utils
{
	// Token: 0x0200000D RID: 13
	internal class NodeHelper
	{
		// Token: 0x06000075 RID: 117 RVA: 0x00003164 File Offset: 0x00001364
		public static void ExpandNodes<TNode>(Env env, NodeList rules) where TNode : Node
		{
			for (int i = 0; i < rules.Count; i++)
			{
				Node node = rules[i];
				if (node is TNode)
				{
					Node node2 = node.Evaluate(env);
					IEnumerable<Node> enumerable = node2 as IEnumerable<Node>;
					if (enumerable != null)
					{
						rules.InsertRange(i + 1, enumerable);
						rules.RemoveAt(i);
						i--;
					}
					else
					{
						rules[i] = node2;
					}
				}
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000031C4 File Offset: 0x000013C4
		public static void RecursiveExpandNodes<TNode>(Env env, Ruleset parentRuleset) where TNode : Node
		{
			env.Frames.Push(parentRuleset);
			for (int i = 0; i < parentRuleset.Rules.Count; i++)
			{
				Node node = parentRuleset.Rules[i];
				if (node is TNode)
				{
					Node node2 = node.Evaluate(env);
					IEnumerable<Node> enumerable = node2 as IEnumerable<Node>;
					if (enumerable != null)
					{
						parentRuleset.Rules.InsertRange(i + 1, enumerable);
						parentRuleset.Rules.RemoveAt(i);
						i--;
					}
					else
					{
						parentRuleset.Rules[i] = node2;
					}
				}
				else
				{
					Ruleset ruleset = node as Ruleset;
					if (ruleset != null && ruleset.Rules != null)
					{
						NodeHelper.RecursiveExpandNodes<TNode>(env, ruleset);
					}
				}
			}
			env.Frames.Pop();
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003276 File Offset: 0x00001476
		public static IEnumerable<Node> NonDestructiveExpandNodes<TNode>(Env env, NodeList rules) where TNode : Node
		{
			foreach (Node node in rules)
			{
				if (node is TNode)
				{
					IEnumerable<Node> enumerable = (IEnumerable<Node>)node.Evaluate(env);
					foreach (Node node2 in enumerable)
					{
						yield return node2;
					}
					IEnumerator<Node> enumerator2 = null;
				}
				else
				{
					yield return node;
				}
			}
			IEnumerator<Node> enumerator = null;
			yield break;
			yield break;
		}
	}
}
