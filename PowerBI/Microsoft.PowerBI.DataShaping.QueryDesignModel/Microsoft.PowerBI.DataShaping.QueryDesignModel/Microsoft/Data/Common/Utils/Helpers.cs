using System;
using System.Collections.Generic;
using System.Data;

namespace Microsoft.Data.Common.Utils
{
	// Token: 0x02000064 RID: 100
	internal static class Helpers
	{
		// Token: 0x06000904 RID: 2308 RVA: 0x000143DC File Offset: 0x000125DC
		internal static IEnumerable<SuperType> AsSuperTypeList<SubType, SuperType>(IEnumerable<SubType> values) where SubType : SuperType
		{
			foreach (SubType subType in values)
			{
				yield return (SuperType)((object)subType);
			}
			IEnumerator<SubType> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x000143EC File Offset: 0x000125EC
		internal static TElement[] Prepend<TElement>(TElement[] args, TElement arg)
		{
			TElement[] array = new TElement[args.Length + 1];
			array[0] = arg;
			for (int i = 0; i < args.Length; i++)
			{
				array[i + 1] = args[i];
			}
			return array;
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x0001442C File Offset: 0x0001262C
		internal static TNode BuildBalancedTreeInPlace<TNode>(IList<TNode> nodes, Func<TNode, TNode, TNode> combinator)
		{
			EntityUtil.CheckArgumentNull<IList<TNode>>(nodes, "nodes");
			EntityUtil.CheckArgumentNull<Func<TNode, TNode, TNode>>(combinator, "combinator");
			if (nodes.Count == 1)
			{
				return nodes[0];
			}
			if (nodes.Count == 2)
			{
				return combinator(nodes[0], nodes[1]);
			}
			for (int num = nodes.Count; num != 1; num /= 2)
			{
				bool flag = (num & 1) == 1;
				if (flag)
				{
					num--;
				}
				int num2 = 0;
				for (int i = 0; i < num; i += 2)
				{
					nodes[num2++] = combinator(nodes[i], nodes[i + 1]);
				}
				if (flag)
				{
					int num3 = num2 - 1;
					nodes[num3] = combinator(nodes[num3], nodes[num]);
				}
			}
			return nodes[0];
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x000144FA File Offset: 0x000126FA
		internal static IEnumerable<TNode> GetLeafNodes<TNode>(TNode root, Func<TNode, bool> isLeaf, Func<TNode, IEnumerable<TNode>> getImmediateSubNodes)
		{
			EntityUtil.CheckArgumentNull<Func<TNode, bool>>(isLeaf, "isLeaf");
			EntityUtil.CheckArgumentNull<Func<TNode, IEnumerable<TNode>>>(getImmediateSubNodes, "getImmediateSubNodes");
			Stack<TNode> nodes = new Stack<TNode>();
			nodes.Push(root);
			while (nodes.Count > 0)
			{
				TNode tnode = nodes.Pop();
				if (isLeaf(tnode))
				{
					yield return tnode;
				}
				else
				{
					List<TNode> list = new List<TNode>(getImmediateSubNodes(tnode));
					for (int i = list.Count - 1; i > -1; i--)
					{
						nodes.Push(list[i]);
					}
				}
			}
			yield break;
		}
	}
}
