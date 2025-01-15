using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace System.Data.Entity.Core.Common.Utils
{
	// Token: 0x020005F5 RID: 1525
	internal static class Helpers
	{
		// Token: 0x06004A89 RID: 19081 RVA: 0x0010846A File Offset: 0x0010666A
		internal static void FormatTraceLine(string format, params object[] args)
		{
			Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, format, args));
		}

		// Token: 0x06004A8A RID: 19082 RVA: 0x0010847D File Offset: 0x0010667D
		internal static void StringTrace(string arg)
		{
			Trace.Write(arg);
		}

		// Token: 0x06004A8B RID: 19083 RVA: 0x00108485 File Offset: 0x00106685
		internal static void StringTraceLine(string arg)
		{
			Trace.WriteLine(arg);
		}

		// Token: 0x06004A8C RID: 19084 RVA: 0x00108490 File Offset: 0x00106690
		internal static bool IsSetEqual<Type>(IEnumerable<Type> list1, IEnumerable<Type> list2, IEqualityComparer<Type> comparer)
		{
			Set<Type> set = new Set<Type>(list1, comparer);
			Set<Type> set2 = new Set<Type>(list2, comparer);
			return set.SetEquals(set2);
		}

		// Token: 0x06004A8D RID: 19085 RVA: 0x001084B2 File Offset: 0x001066B2
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

		// Token: 0x06004A8E RID: 19086 RVA: 0x001084C4 File Offset: 0x001066C4
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

		// Token: 0x06004A8F RID: 19087 RVA: 0x00108504 File Offset: 0x00106704
		internal static TNode BuildBalancedTreeInPlace<TNode>(IList<TNode> nodes, Func<TNode, TNode, TNode> combinator)
		{
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

		// Token: 0x06004A90 RID: 19088 RVA: 0x001085BA File Offset: 0x001067BA
		internal static IEnumerable<TNode> GetLeafNodes<TNode>(TNode root, Func<TNode, bool> isLeaf, Func<TNode, IEnumerable<TNode>> getImmediateSubNodes)
		{
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
