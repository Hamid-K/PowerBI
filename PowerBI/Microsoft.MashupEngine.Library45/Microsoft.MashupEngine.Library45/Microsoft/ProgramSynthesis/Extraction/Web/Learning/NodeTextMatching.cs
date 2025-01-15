using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Extraction.Web.Semantics;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Learning
{
	// Token: 0x020010AF RID: 4271
	internal static class NodeTextMatching
	{
		// Token: 0x0600808C RID: 32908 RVA: 0x001AE308 File Offset: 0x001AC508
		internal static List<HashSet<IDomNode>> GetMatchingNodeSets(Grammar grammar, List<Tuple<State, string, int>> examplesList, StringComparer textComparer, string attribute = null, bool matchSubstrings = false)
		{
			List<HashSet<IDomNode>> list = new List<HashSet<IDomNode>>();
			Symbol inputSymbol = grammar.InputSymbol;
			List<IEnumerable<IDomNode>> list2 = new List<IEnumerable<IDomNode>>();
			List<string> outputs = new List<string>();
			List<bool> list3 = new List<bool>();
			foreach (Tuple<State, string, int> tuple in examplesList)
			{
				string item = tuple.Item2;
				outputs.Add(item);
				IEnumerable<IDomNode> enumerable = tuple.Item1[inputSymbol] as IEnumerable<IDomNode>;
				list2.Add(enumerable);
				int nodeIndex = tuple.Item3;
				if (nodeIndex == -1)
				{
					list3.Add(false);
				}
				else
				{
					enumerable = enumerable.Where((IDomNode n) => n.Start == nodeIndex).ConvertToHashSet<IDomNode>();
					list3.Add(true);
				}
				HashSet<IDomNode> matchingNodes = NodeTextMatching.GetMatchingNodes(enumerable, item, textComparer, matchSubstrings, attribute, 1);
				list.Add(matchingNodes);
			}
			if (matchSubstrings && attribute == null)
			{
				if (!list.Any((HashSet<IDomNode> s, int k) => s.All((IDomNode n) => !textComparer.Equals(n.TrimmedInnerText, outputs[k]))))
				{
					return null;
				}
				int i;
				int j;
				for (j = 0; j < list2.Count; j = i + 1)
				{
					if (!list3[j] && list[j].All((IDomNode n) => textComparer.Equals(n.TrimmedInnerText, outputs[j])))
					{
						list[j] = NodeTextMatching.GetMatchingNodes(list2[j], outputs[j], textComparer, true, null, 2);
					}
					i = j;
				}
			}
			return list;
		}

		// Token: 0x0600808D RID: 32909 RVA: 0x001AE504 File Offset: 0x001AC704
		internal static IEnumerable<IEnumerable<IDomNode>> GetMatchingNodeCombinations(List<HashSet<IDomNode>> matchingNodeSets)
		{
			HashSet<string> hashSet = null;
			foreach (HashSet<IDomNode> hashSet2 in matchingNodeSets)
			{
				if (hashSet == null)
				{
					hashSet = hashSet2.Select((IDomNode n) => n.NodeName).ConvertToHashSet<string>();
				}
				else
				{
					hashSet.IntersectWith(hashSet2.Select((IDomNode n) => n.NodeName));
				}
			}
			List<List<HashSet<IDomNode>>> tagBasedCombinationSets = new List<List<HashSet<IDomNode>>>();
			tagBasedCombinationSets.AddRange(hashSet.Select(delegate(string t)
			{
				Func<IDomNode, bool> <>9__5;
				return matchingNodeSets.Select(delegate(HashSet<IDomNode> s)
				{
					Func<IDomNode, bool> func;
					if ((func = <>9__5) == null)
					{
						func = (<>9__5 = (IDomNode n) => n.NodeName == t);
					}
					return NodeTextMatching.GetTextBasedMaximumNodes(s.Where(func).ConvertToHashSet<IDomNode>());
				}).ToList<HashSet<IDomNode>>();
			}));
			tagBasedCombinationSets.AddRange(hashSet.Select(delegate(string t)
			{
				Func<IDomNode, bool> <>9__7;
				return matchingNodeSets.Select(delegate(HashSet<IDomNode> s)
				{
					Func<IDomNode, bool> func2;
					if ((func2 = <>9__7) == null)
					{
						func2 = (<>9__7 = (IDomNode n) => n.NodeName == t);
					}
					return NodeTextMatching.GetTextBasedMinimumNodes(s.Where(func2).ConvertToHashSet<IDomNode>());
				}).ToList<HashSet<IDomNode>>();
			}));
			int i = 0;
			int num;
			for (int j = 0; j <= tagBasedCombinationSets.Count; j = num + 1)
			{
				IEnumerable<IEnumerable<IDomNode>> enumerable = ((j < tagBasedCombinationSets.Count) ? tagBasedCombinationSets[j] : matchingNodeSets).CartesianProduct<IDomNode>();
				foreach (IEnumerable<IDomNode> enumerable2 in enumerable)
				{
					yield return enumerable2;
					if (i >= NodeTextMatching.MaxCombinations)
					{
						yield break;
					}
					num = i;
					i = num + 1;
				}
				IEnumerator<IEnumerable<IDomNode>> enumerator2 = null;
				num = j;
			}
			yield break;
			yield break;
		}

		// Token: 0x0600808E RID: 32910 RVA: 0x001AE514 File Offset: 0x001AC714
		internal static IEnumerable<NodeSequence> GetMatchingNodeSequenceCombinations(List<HashSet<IDomNode>> matchingNodeSets)
		{
			HashSet<string> hashSet = null;
			foreach (HashSet<IDomNode> hashSet2 in matchingNodeSets)
			{
				if (hashSet == null)
				{
					hashSet = hashSet2.Select((IDomNode n) => n.NodeName).ConvertToHashSet<string>();
				}
				else
				{
					hashSet.IntersectWith(hashSet2.Select((IDomNode n) => n.NodeName));
				}
			}
			List<List<List<IDomNode>>> list = new List<List<List<IDomNode>>>();
			list.AddRange(hashSet.Select(delegate(string t)
			{
				Func<IDomNode, bool> <>9__8;
				return matchingNodeSets.Select(delegate(HashSet<IDomNode> s)
				{
					Func<IDomNode, bool> func;
					if ((func = <>9__8) == null)
					{
						func = (<>9__8 = (IDomNode n) => n.NodeName == t);
					}
					return NodeTextMatching.GetMaximalNodes(s.Where(func));
				}).ToList<List<IDomNode>>();
			}));
			list.AddRange(hashSet.Select(delegate(string t)
			{
				Func<IDomNode, bool> <>9__10;
				return matchingNodeSets.Select(delegate(HashSet<IDomNode> s)
				{
					Func<IDomNode, bool> func2;
					if ((func2 = <>9__10) == null)
					{
						func2 = (<>9__10 = (IDomNode n) => n.NodeName == t);
					}
					return NodeTextMatching.GetMinimalNodes(s.Where(func2));
				}).ToList<List<IDomNode>>();
			}));
			list.Add(matchingNodeSets.Select((HashSet<IDomNode> s) => NodeTextMatching.GetMaximalNodes(s)).ToList<List<IDomNode>>());
			list.Add(matchingNodeSets.Select((HashSet<IDomNode> s) => NodeTextMatching.GetMinimalNodes(s)).ToList<List<IDomNode>>());
			list.Add(matchingNodeSets.Select((HashSet<IDomNode> s) => s.ToList<IDomNode>()).ToList<List<IDomNode>>());
			foreach (List<List<IDomNode>> list2 in list)
			{
				List<NodeSequence> topUniformNodeSequenceCombinations = NodeTextMatching.GetTopUniformNodeSequenceCombinations(list2);
				if (topUniformNodeSequenceCombinations != null)
				{
					foreach (NodeSequence nodeSequence in topUniformNodeSequenceCombinations)
					{
						yield return nodeSequence;
					}
					List<NodeSequence>.Enumerator enumerator3 = default(List<NodeSequence>.Enumerator);
				}
			}
			List<List<List<IDomNode>>>.Enumerator enumerator2 = default(List<List<List<IDomNode>>>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x0600808F RID: 32911 RVA: 0x001AE524 File Offset: 0x001AC724
		private static List<List<IDomNode>> GetPrunedSequenceNodeCandidates(List<List<IDomNode>> candidateNodeSets)
		{
			if (candidateNodeSets.Any<List<IDomNode>>())
			{
				if (!candidateNodeSets.Any((List<IDomNode> s) => !s.Any<IDomNode>()))
				{
					int averagePerElement = NodeTextMatching.GetAverageNumCandidatesPerElement(candidateNodeSets);
					if (candidateNodeSets.All((List<IDomNode> s) => s.Count <= averagePerElement))
					{
						return candidateNodeSets;
					}
					List<IDomNode> smallestWindow = NodeTextMatching.GetSmallestWindow(candidateNodeSets);
					if (smallestWindow == null)
					{
						return null;
					}
					return candidateNodeSets.Select((List<IDomNode> s, int i) => (from n in new IDomNode[]
						{
							smallestWindow[i],
							s[0]
						}.Concat(s.OrderBy((IDomNode n) => Math.Abs(n.Start - smallestWindow[i].Start))).Distinct<IDomNode>().Take(averagePerElement)
						orderby n.Start
						select n).ToList<IDomNode>()).ToList<List<IDomNode>>();
				}
			}
			return null;
		}

		// Token: 0x06008090 RID: 32912 RVA: 0x001AE5B8 File Offset: 0x001AC7B8
		public static List<IDomNode> GetSmallestWindow(List<List<IDomNode>> nodeSets)
		{
			IDomNode[] array = (from n in nodeSets.SelectMany((List<IDomNode> s) => s).Distinct<IDomNode>()
				orderby n.Start
				select n).ToArray<IDomNode>();
			HashSet<IDomNode>[] array2 = nodeSets.Select((List<IDomNode> n) => n.ConvertToHashSet<IDomNode>()).ToArray<HashSet<IDomNode>>();
			List<IDomNode> list = null;
			int num = -1;
			for (int i = 0; i < array.Length; i++)
			{
				IDomNode domNode = array[i];
				if (array2[0].Contains(domNode))
				{
					int start = domNode.Start;
					List<IDomNode> list2 = new List<IDomNode> { domNode };
					int num2 = 1;
					int num3 = i + 1;
					while (num3 < array.Length && num2 < nodeSets.Count)
					{
						IDomNode domNode2 = array[num3];
						if (array2[num2].Contains(domNode2))
						{
							list2.Add(domNode2);
							num2++;
						}
						num3++;
					}
					if (list2.Count != nodeSets.Count)
					{
						break;
					}
					int num4 = list2.Last<IDomNode>().Start - domNode.Start;
					if (list == null || num4 < num)
					{
						num = num4;
						list = list2;
					}
				}
			}
			return list;
		}

		// Token: 0x06008091 RID: 32913 RVA: 0x001AE704 File Offset: 0x001AC904
		private static int GetAverageNumCandidatesPerElement(List<List<IDomNode>> candidateSets)
		{
			int averagePerElement = Math.Max(1, NodeTextMatching.GetUniformAveragePerElement(NodeTextMatching.MaxCombinationsPreLearning, candidateSets.Count));
			List<IDomNode>[] array = candidateSets.Where((List<IDomNode> s) => s.Count <= averagePerElement).ToArray<List<IDomNode>>();
			if (!array.Any<List<IDomNode>>() || array.Length == candidateSets.Count)
			{
				return averagePerElement;
			}
			int num = array.Select((List<IDomNode> s) => s.Count).Aggregate(1, (int s1, int s2) => s1 * s2);
			int num2 = NodeTextMatching.MaxCombinationsPreLearning / num;
			if (num2 <= 0)
			{
				return averagePerElement;
			}
			int num3 = candidateSets.Count - array.Length;
			return Math.Max(averagePerElement, NodeTextMatching.GetUniformAveragePerElement(num2, num3));
		}

		// Token: 0x06008092 RID: 32914 RVA: 0x001AE7E2 File Offset: 0x001AC9E2
		public static int GetUniformAveragePerElement(int maxCombinations, int numElements)
		{
			return Convert.ToInt32(Math.Truncate(Math.Pow(2.0, Math.Log((double)maxCombinations, 2.0) / (double)numElements)));
		}

		// Token: 0x06008093 RID: 32915 RVA: 0x001AE810 File Offset: 0x001ACA10
		private static List<NodeSequence> GetTopUniformNodeSequenceCombinations(List<List<IDomNode>> candidateNodeSets)
		{
			List<NodeSequence> list = new List<NodeSequence>();
			if (candidateNodeSets.Count > NodeTextMatching.LongSequenceThreshold)
			{
				list = NodeTextMatching.GetTopUniformNodeSequenceCombinations(candidateNodeSets.Take(NodeTextMatching.NumPrefixBasedCombinationNodes).ToList<List<IDomNode>>(), candidateNodeSets.Skip(NodeTextMatching.NumPrefixBasedCombinationNodes).ToList<List<IDomNode>>());
			}
			List<List<IDomNode>> prunedSequenceNodeCandidates = NodeTextMatching.GetPrunedSequenceNodeCandidates(candidateNodeSets);
			if (prunedSequenceNodeCandidates == null)
			{
				return null;
			}
			IEnumerable<IEnumerable<IDomNode>> enumerable = prunedSequenceNodeCandidates.CartesianProduct<IDomNode>().Take(NodeTextMatching.MaxCombinationsPreLearning * NodeTextMatching.MaxCombinationsRoundingFactor);
			Func<IEnumerable<IDomNode>, bool> func;
			if ((func = NodeTextMatching.<>O.<0>__IsOrderedNonHierarchicalNodeSequence) == null)
			{
				func = (NodeTextMatching.<>O.<0>__IsOrderedNonHierarchicalNodeSequence = new Func<IEnumerable<IDomNode>, bool>(NodeTextMatching.IsOrderedNonHierarchicalNodeSequence));
			}
			return (from c in (from c in enumerable.Where(func)
					select new NodeSequence(c)).Concat(list)
				orderby c.UniqueCommonAncestor == null, NodeTextMatching.HaveCommonNodeNameClassSelector(c.Nodes) descending, NodeTextMatching.NodeDistanceDeviation(c.Nodes), c.Nodes[0].Start, c.Nodes.Sum((IDomNode n) => n.Start)
				select c).ToList<NodeSequence>();
		}

		// Token: 0x06008094 RID: 32916 RVA: 0x001AE97C File Offset: 0x001ACB7C
		private static List<NodeSequence> GetTopUniformNodeSequenceCombinations(List<List<IDomNode>> candidateNodeSets, List<List<IDomNode>> additionalNodeSets)
		{
			if (candidateNodeSets.Count < 1)
			{
				return null;
			}
			List<NodeSequence> topUniformNodeSequenceCombinations = NodeTextMatching.GetTopUniformNodeSequenceCombinations(candidateNodeSets);
			List<NodeSequence> list = new List<NodeSequence>();
			if (topUniformNodeSequenceCombinations == null)
			{
				return null;
			}
			foreach (NodeSequence nodeSequence in topUniformNodeSequenceCombinations)
			{
				List<IDomNode> nodes = nodeSequence.Nodes;
				IDomNode lastNode = nodes.Last<IDomNode>();
				IDomNode uniqueAncestor = nodeSequence.UniqueCommonAncestor;
				string commonSelector = (NodeTextMatching.HaveCommonNodeNameClassSelector(nodes) ? lastNode.NodeNameClassSelector : null);
				double meanNodeDistance = NodeTextMatching.NodeDistanceMean(nodes);
				bool flag = true;
				Func<IDomNode, bool> <>9__0;
				Func<IDomNode, bool> <>9__1;
				Func<IDomNode, bool> <>9__2;
				Func<IDomNode, double> <>9__3;
				Func<IDomNode, bool> <>9__4;
				Func<IDomNode, double> <>9__5;
				foreach (IEnumerable<IDomNode> enumerable in additionalNodeSets)
				{
					Func<IDomNode, bool> func;
					if ((func = <>9__0) == null)
					{
						func = (<>9__0 = (IDomNode n) => n.Start > lastNode.Start);
					}
					IEnumerable<IDomNode> enumerable2 = enumerable.Where(func);
					if (uniqueAncestor != null)
					{
						IEnumerable<IDomNode> enumerable3 = enumerable2;
						Func<IDomNode, bool> func2;
						if ((func2 = <>9__1) == null)
						{
							func2 = (<>9__1 = (IDomNode n) => n.IsAncestor(uniqueAncestor));
						}
						if (enumerable3.Any(func2))
						{
							IEnumerable<IDomNode> enumerable4 = enumerable2;
							Func<IDomNode, bool> func3;
							if ((func3 = <>9__2) == null)
							{
								func3 = (<>9__2 = (IDomNode n) => n.IsAncestor(uniqueAncestor));
							}
							enumerable2 = enumerable4.Where(func3);
						}
					}
					IEnumerable<IDomNode> enumerable5 = enumerable2;
					Func<IDomNode, double> func4;
					if ((func4 = <>9__3) == null)
					{
						func4 = (<>9__3 = (IDomNode n) => Math.Abs((double)(n.Start - lastNode.Start) - meanNodeDistance));
					}
					IDomNode domNode = enumerable5.ArgMin(func4);
					if (domNode != null && commonSelector != null && domNode.NodeNameClassSelector != commonSelector)
					{
						IEnumerable<IDomNode> enumerable6 = enumerable2;
						Func<IDomNode, bool> func5;
						if ((func5 = <>9__4) == null)
						{
							func5 = (<>9__4 = (IDomNode n) => n.NodeNameClassSelector == commonSelector);
						}
						IEnumerable<IDomNode> enumerable7 = enumerable6.Where(func5);
						Func<IDomNode, double> func6;
						if ((func6 = <>9__5) == null)
						{
							func6 = (<>9__5 = (IDomNode n) => Math.Abs((double)(n.Start - lastNode.Start) - meanNodeDistance));
						}
						domNode = enumerable7.ArgMin(func6) ?? domNode;
					}
					if (domNode == null)
					{
						flag = false;
						break;
					}
					nodes.Add(domNode);
					lastNode = domNode;
				}
				if (flag)
				{
					list.Add(new NodeSequence(nodes));
				}
			}
			return list;
		}

		// Token: 0x06008095 RID: 32917 RVA: 0x001AEBFC File Offset: 0x001ACDFC
		public static bool IsOrderedNonHierarchicalNodeSequence(IEnumerable<IDomNode> nodeSequence)
		{
			IDomNode domNode = null;
			foreach (IDomNode domNode2 in nodeSequence)
			{
				if (domNode != null && (domNode2.Start <= domNode.Start || domNode2.IsAncestor(domNode)))
				{
					return false;
				}
				domNode = domNode2;
			}
			return true;
		}

		// Token: 0x06008096 RID: 32918 RVA: 0x001AEC64 File Offset: 0x001ACE64
		private static bool IsUniqueAncestor(IDomNode node, IDomNode anc, List<IDomNode> otherNodes)
		{
			if (!node.IsAncestor(anc))
			{
				return false;
			}
			IDomNode p = node;
			while (p != anc && p != null)
			{
				if (otherNodes.Any((IDomNode n) => n.IsAncestor(p)))
				{
					return false;
				}
				p = p.Parent;
			}
			return true;
		}

		// Token: 0x06008097 RID: 32919 RVA: 0x001AECC4 File Offset: 0x001ACEC4
		private static double NodeDistanceDeviation(IEnumerable<IDomNode> nodeSequence)
		{
			if (nodeSequence.Count<IDomNode>() <= 1)
			{
				return 0.0;
			}
			int num = nodeSequence.First<IDomNode>().Start;
			List<double> list = new List<double>();
			foreach (IDomNode domNode in nodeSequence.Skip(1))
			{
				list.Add((double)(domNode.Start - num));
				num = domNode.Start;
			}
			double meanDistance = list.Sum() / (double)list.Count;
			return list.Sum((double d) => Math.Abs(d - meanDistance)) / (double)list.Count / meanDistance;
		}

		// Token: 0x06008098 RID: 32920 RVA: 0x001AED88 File Offset: 0x001ACF88
		private static double NodeDistanceMean(IEnumerable<IDomNode> nodeSequence)
		{
			if (nodeSequence.Count<IDomNode>() <= 1)
			{
				return 0.0;
			}
			int num = nodeSequence.First<IDomNode>().Start;
			List<double> list = new List<double>();
			foreach (IDomNode domNode in nodeSequence.Skip(1))
			{
				list.Add((double)(domNode.Start - num));
				num = domNode.Start;
			}
			return list.Sum() / (double)list.Count;
		}

		// Token: 0x06008099 RID: 32921 RVA: 0x001AEE1C File Offset: 0x001AD01C
		private static HashSet<IDomNode> GetMatchingNodes(IEnumerable<IDomNode> inputNodes, string matchingString, StringComparer textComparer, bool matchSubstrings = false, string attribute = null, int containmentLevel = 1)
		{
			NodeTextMatching.<>c__DisplayClass19_0 CS$<>8__locals1 = new NodeTextMatching.<>c__DisplayClass19_0();
			CS$<>8__locals1.textComparer = textComparer;
			CS$<>8__locals1.attribute = attribute;
			CS$<>8__locals1.matchingString = matchingString;
			CS$<>8__locals1.containmentLevel = containmentLevel;
			NodeTextMatching.<>c__DisplayClass19_0 CS$<>8__locals2 = CS$<>8__locals1;
			string matchingString2 = CS$<>8__locals1.matchingString;
			CS$<>8__locals2.matchingString = ((matchingString2 != null) ? matchingString2.Trim() : null);
			if (matchSubstrings)
			{
				IDomNode[] array = (from n in inputNodes.Distinct<IDomNode>()
					where NodeTextMatching.CaseInsensitiveContains(n.InnerText, CS$<>8__locals1.matchingString)
					select n).ToArray<IDomNode>();
				CS$<>8__locals1.containingStrings = array.Select((IDomNode n) => n.TrimmedInnerText).ConvertToHashSet<string>();
				CS$<>8__locals1.maxLength = CS$<>8__locals1.matchingString.Length + NodeTextMatching.MatchingMaxLength;
				CS$<>8__locals1.minimalContainingStrings = CS$<>8__locals1.containingStrings.Where((string s) => s.Length < CS$<>8__locals1.maxLength && CS$<>8__locals1.containingStrings.Count((string s1) => s.Contains(s1)) <= CS$<>8__locals1.containmentLevel).ConvertToHashSet<string>();
				return array.Where((IDomNode n) => CS$<>8__locals1.minimalContainingStrings.Contains(n.TrimmedInnerText, CS$<>8__locals1.textComparer)).ConvertToHashSet<IDomNode>();
			}
			if (CS$<>8__locals1.attribute != null)
			{
				return inputNodes.Where((IDomNode n) => CS$<>8__locals1.textComparer.Equals(n.GetAttribute(CS$<>8__locals1.attribute), CS$<>8__locals1.matchingString)).ConvertToHashSet<IDomNode>();
			}
			HashSet<IDomNode> hashSet = inputNodes.Where((IDomNode n) => CS$<>8__locals1.textComparer.Equals(n.TrimmedInnerText, CS$<>8__locals1.matchingString)).ConvertToHashSet<IDomNode>();
			if (!hashSet.Any<IDomNode>())
			{
				string normalizedString = HtmlDoc.NormalizeText(CS$<>8__locals1.matchingString);
				if (!string.IsNullOrEmpty(normalizedString))
				{
					return inputNodes.Where((IDomNode n) => CS$<>8__locals1.textComparer.Equals(n.NormalizedInnerText, normalizedString)).ConvertToHashSet<IDomNode>();
				}
			}
			return hashSet;
		}

		// Token: 0x0600809A RID: 32922 RVA: 0x001AEF92 File Offset: 0x001AD192
		private static bool CaseInsensitiveContains(string text, string substring)
		{
			return text.IndexOf(substring, StringComparison.CurrentCultureIgnoreCase) >= 0;
		}

		// Token: 0x0600809B RID: 32923 RVA: 0x001AEFA4 File Offset: 0x001AD1A4
		private static HashSet<IDomNode> GetTextBasedMinimumNodes(HashSet<IDomNode> nodes)
		{
			return (from n in nodes
				group n by n.TrimmedInnerText into g
				select g.ArgMax((IDomNode n1) => n1.Start)).ConvertToHashSet<IDomNode>();
		}

		// Token: 0x0600809C RID: 32924 RVA: 0x001AF000 File Offset: 0x001AD200
		private static HashSet<IDomNode> GetTextBasedMaximumNodes(HashSet<IDomNode> nodes)
		{
			return (from n in nodes
				group n by n.TrimmedInnerText into g
				select g.ArgMin((IDomNode n1) => n1.Start)).ConvertToHashSet<IDomNode>();
		}

		// Token: 0x0600809D RID: 32925 RVA: 0x001AF05C File Offset: 0x001AD25C
		public static List<IDomNode> GetMaximalNodes(IEnumerable<IDomNode> nodes)
		{
			IDomNode[] array = nodes.OrderBy((IDomNode n) => n.Start).ToArray<IDomNode>();
			HashSet<IDomNode> hashSet = new HashSet<IDomNode>();
			foreach (IDomNode domNode in array)
			{
				bool flag = true;
				for (IDomNode domNode2 = domNode.Parent; domNode2 != null; domNode2 = domNode2.Parent)
				{
					if (hashSet.Contains(domNode2))
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					hashSet.Add(domNode);
				}
			}
			return hashSet.OrderBy((IDomNode n) => n.Start).ToList<IDomNode>();
		}

		// Token: 0x0600809E RID: 32926 RVA: 0x001AF10C File Offset: 0x001AD30C
		public static List<IDomNode> GetMinimalNodes(IEnumerable<IDomNode> nodes)
		{
			IDomNode[] array = nodes.OrderBy((IDomNode n) => n.Start).ToArray<IDomNode>();
			List<IDomNode> list = new List<IDomNode>();
			if (array.Length == 0)
			{
				return list;
			}
			IDomNode domNode = null;
			foreach (IDomNode domNode2 in array)
			{
				if (domNode != null && !NodeTextMatching.IsAncestorOf(domNode, domNode2))
				{
					list.Add(domNode);
				}
				domNode = domNode2;
			}
			list.Add(domNode);
			return list;
		}

		// Token: 0x0600809F RID: 32927 RVA: 0x001AF184 File Offset: 0x001AD384
		private static bool IsAncestorOf(IDomNode n1, IDomNode n2)
		{
			for (IDomNode domNode = n2.Parent; domNode != null; domNode = domNode.Parent)
			{
				if (domNode == n1)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060080A0 RID: 32928 RVA: 0x001AF1AC File Offset: 0x001AD3AC
		private static bool HaveCommonNodeNameClassSelector(IEnumerable<IDomNode> nodeSequence)
		{
			NodeTextMatching.<>c__DisplayClass26_0 CS$<>8__locals1 = new NodeTextMatching.<>c__DisplayClass26_0();
			NodeTextMatching.<>c__DisplayClass26_0 CS$<>8__locals2 = CS$<>8__locals1;
			IDomNode domNode = nodeSequence.FirstOrDefault<IDomNode>();
			CS$<>8__locals2.firstSelector = ((domNode != null) ? domNode.NodeNameClassSelector : null);
			return !string.IsNullOrEmpty(CS$<>8__locals1.firstSelector) && nodeSequence.All((IDomNode n) => n.NodeNameClassSelector == CS$<>8__locals1.firstSelector);
		}

		// Token: 0x040033DB RID: 13275
		private static readonly int MaxCombinations = 100;

		// Token: 0x040033DC RID: 13276
		private static readonly int MaxCombinationsPreLearning = 50000;

		// Token: 0x040033DD RID: 13277
		private static readonly int MaxCombinationsRoundingFactor = 5;

		// Token: 0x040033DE RID: 13278
		private static readonly int NumPrefixBasedCombinationNodes = 5;

		// Token: 0x040033DF RID: 13279
		private static readonly int LongSequenceThreshold = 10;

		// Token: 0x040033E0 RID: 13280
		private static readonly int MatchingMaxLength = 200;

		// Token: 0x020010B0 RID: 4272
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040033E1 RID: 13281
			public static Func<IEnumerable<IDomNode>, bool> <0>__IsOrderedNonHierarchicalNodeSequence;
		}
	}
}
