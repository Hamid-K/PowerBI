using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Extraction.Web.Semantics;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Interactive;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Learning
{
	// Token: 0x020010CD RID: 4301
	internal static class TableNodeMatching
	{
		// Token: 0x06008131 RID: 33073 RVA: 0x001B0278 File Offset: 0x001AE478
		internal static List<TableNodesMatch> GetTableNodeMatches(Grammar grammar, TextTableSpec tableTextSpec, StringComparer textComparer, out string[] matchingAttributes, string[] attributeCandidates)
		{
			TableNodeMatching.<>c__DisplayClass3_0 CS$<>8__locals1 = new TableNodeMatching.<>c__DisplayClass3_0();
			List<TableNodesMatch> list = new List<TableNodesMatch>();
			TextSubsequenceSpec[] columnSpecs = tableTextSpec.ColumnSpecs;
			TableNodeMatching.<>c__DisplayClass3_0 CS$<>8__locals2 = CS$<>8__locals1;
			TextSubsequenceSpec textSubsequenceSpec = columnSpecs.FirstOrDefault<TextSubsequenceSpec>();
			CS$<>8__locals2.inpState = ((textSubsequenceSpec != null) ? textSubsequenceSpec.ProvidedInputs.FirstOrDefault<State>() : null);
			if (CS$<>8__locals1.inpState == null)
			{
				throw new ArgumentException("No valid input state found in column specs");
			}
			Dictionary<int, List<NodeSequence>> dictionary = new Dictionary<int, List<NodeSequence>>();
			Dictionary<int, List<NodeSequence>> dictionary2 = new Dictionary<int, List<NodeSequence>>();
			HashSet<int> hashSet = new HashSet<int>();
			Dictionary<int, int[]> dictionary3 = new Dictionary<int, int[]>();
			Dictionary<int, Dictionary<int, HashSet<IDomNode>>> dictionary4 = new Dictionary<int, Dictionary<int, HashSet<IDomNode>>>();
			matchingAttributes = new string[columnSpecs.Length];
			for (int i = 0; i < columnSpecs.Length; i++)
			{
				TextSubsequenceSpec spec = columnSpecs[i];
				List<string> list2 = spec.PositiveExamples[CS$<>8__locals1.inpState].Cast<string>().ToList<string>();
				dictionary3[i] = (from k in list2.Select(delegate(string s, int k)
					{
						if (!string.IsNullOrWhiteSpace(s))
						{
							return k;
						}
						return -1;
					})
					where k >= 0
					select k).ToArray<int>();
				if (dictionary3[i].IsEmpty<int>())
				{
					hashSet.Add(i);
					dictionary4[i] = new Dictionary<int, HashSet<IDomNode>>();
				}
				else
				{
					List<Tuple<State, string, int>> list3 = (from t in spec.PositiveExamples[CS$<>8__locals1.inpState].Select((object v, int k) => Tuple.Create<State, string, int>(CS$<>8__locals1.inpState, v as string, spec.NodeIndexes[CS$<>8__locals1.inpState][k]))
						where !string.IsNullOrWhiteSpace(t.Item2)
						select t).ToList<Tuple<State, string, int>>();
					List<HashSet<IDomNode>> list4 = null;
					string text = null;
					foreach (string text2 in attributeCandidates.PrependItem(null))
					{
						list4 = NodeTextMatching.GetMatchingNodeSets(grammar, list3, textComparer, text2, false);
						if (list4 != null)
						{
							if (list4.All((HashSet<IDomNode> s) => s.Any<IDomNode>()))
							{
								text = text2;
								break;
							}
						}
						list4 = null;
					}
					matchingAttributes[i] = text;
					if (list4 == null)
					{
						hashSet.Add(i);
						dictionary4[i] = new Dictionary<int, HashSet<IDomNode>>();
					}
					else
					{
						List<NodeSequence> list5 = NodeTextMatching.GetMatchingNodeSequenceCombinations(list4).Take(10).ToList<NodeSequence>();
						if (list4.Count == 1)
						{
							dictionary2[i] = list5;
						}
						else
						{
							dictionary[i] = list5;
						}
						dictionary4[i] = dictionary3[i].ZipWith(list4).ToDictionary((Record<int, HashSet<IDomNode>> t) => t.Item1, (Record<int, HashSet<IDomNode>> t) => t.Item2);
					}
				}
			}
			if (dictionary.IsEmpty<KeyValuePair<int, List<NodeSequence>>>())
			{
				return list;
			}
			HashSet<IDomNode>[] array = dictionary.Select((KeyValuePair<int, List<NodeSequence>> kvp) => (from ns in kvp.Value
				select ns.UniqueCommonAncestor into n
				where n != null
				select n).ConvertToHashSet<IDomNode>()).ToArray<HashSet<IDomNode>>();
			TableNodeMatching.<>c__DisplayClass3_0 CS$<>8__locals4 = CS$<>8__locals1;
			IDomNode domNode;
			if (array.Length != 1)
			{
				domNode = array.Skip(1).Aggregate(array[0], delegate(HashSet<IDomNode> s1, HashSet<IDomNode> s2)
				{
					s1.IntersectWith(s2);
					return s1;
				}).FirstOrDefault<IDomNode>();
			}
			else
			{
				domNode = array[0].FirstOrDefault<IDomNode>();
			}
			CS$<>8__locals4.tableRootNode = domNode;
			if (CS$<>8__locals1.tableRootNode != null)
			{
				dictionary = dictionary.ToDictionary((KeyValuePair<int, List<NodeSequence>> kvp) => kvp.Key, delegate(KeyValuePair<int, List<NodeSequence>> kvp)
				{
					IEnumerable<NodeSequence> value = kvp.Value;
					Func<NodeSequence, bool> func;
					if ((func = CS$<>8__locals1.<>9__13) == null)
					{
						func = (CS$<>8__locals1.<>9__13 = (NodeSequence ns) => ns.UniqueCommonAncestor == CS$<>8__locals1.tableRootNode);
					}
					return value.Where(func).ToList<NodeSequence>();
				});
			}
			List<TableNodesMatch> columnBasedTableNodeMatches = TableNodeMatching.GetColumnBasedTableNodeMatches(dictionary, dictionary2, hashSet, dictionary3);
			list.AddRange(columnBasedTableNodeMatches);
			List<List<IDomNode>> rowNodeSequenceCandidates = TableNodeMatching.GetRowNodeSequenceCandidates(dictionary);
			List<TableNodesMatch> rowBasedTableNodeMatches = TableNodeMatching.GetRowBasedTableNodeMatches(dictionary4, rowNodeSequenceCandidates);
			list.AddRange(rowBasedTableNodeMatches);
			return list.Distinct<TableNodesMatch>().ToList<TableNodesMatch>();
		}

		// Token: 0x06008132 RID: 33074 RVA: 0x001B066C File Offset: 0x001AE86C
		private static List<List<IDomNode>> GetRowNodeSequenceCandidates(Dictionary<int, List<NodeSequence>> multiExampleColumnMatches)
		{
			List<List<IDomNode>> list = new List<List<IDomNode>>();
			IDomNode[] array = (from n in multiExampleColumnMatches.SelectMany((KeyValuePair<int, List<NodeSequence>> kvp) => kvp.Value.Select((NodeSequence ns) => ns.UniqueCommonAncestor)).Distinct<IDomNode>()
				where n != null
				select n).ToArray<IDomNode>();
			list.AddRange(array.Select((IDomNode n) => n.GetChildren().ToList<IDomNode>()));
			List<IDomNode> list2 = (from n in multiExampleColumnMatches.SelectMany((KeyValuePair<int, List<NodeSequence>> kvp) => kvp.Value.SelectMany((NodeSequence ns) => ns.MaxUncommonAncestors)).Distinct<IDomNode>()
				where n != null
				orderby n.Start
				select n).ToList<IDomNode>();
			list.Add(list2);
			return list;
		}

		// Token: 0x06008133 RID: 33075 RVA: 0x001B077C File Offset: 0x001AE97C
		private static List<TableNodesMatch> GetRowBasedTableNodeMatches(Dictionary<int, Dictionary<int, HashSet<IDomNode>>> exampleNodeMatches, List<List<IDomNode>> rowNodeSequenceCandidates)
		{
			List<TableNodesMatch> list = new List<TableNodesMatch>();
			Dictionary<int, List<List<IDomNode>>> dictionary = new Dictionary<int, List<List<IDomNode>>>();
			Dictionary<int, List<int>> dictionary2 = new Dictionary<int, List<int>>();
			foreach (KeyValuePair<int, Dictionary<int, HashSet<IDomNode>>> keyValuePair in exampleNodeMatches)
			{
				int key = keyValuePair.Key;
				foreach (KeyValuePair<int, HashSet<IDomNode>> keyValuePair2 in keyValuePair.Value)
				{
					int key2 = keyValuePair2.Key;
					List<IDomNode> list2 = (from n in NodeTextMatching.GetMinimalNodes(keyValuePair2.Value)
						orderby n.Start
						select n).ToList<IDomNode>();
					if (!dictionary.ContainsKey(key2))
					{
						dictionary[key2] = new List<List<IDomNode>>();
						dictionary2[key2] = new List<int>();
					}
					dictionary[key2].Add(list2);
					dictionary2[key2].Add(key);
				}
			}
			foreach (List<IDomNode> list3 in rowNodeSequenceCandidates)
			{
				Dictionary<int, Dictionary<int, IDomNode>> dictionary3 = new Dictionary<int, Dictionary<int, IDomNode>>();
				foreach (KeyValuePair<int, Dictionary<int, HashSet<IDomNode>>> keyValuePair3 in exampleNodeMatches)
				{
					if (keyValuePair3.Value.IsEmpty<KeyValuePair<int, HashSet<IDomNode>>>())
					{
						dictionary3[keyValuePair3.Key] = new Dictionary<int, IDomNode>();
					}
				}
				Dictionary<int, IDomNode> dictionary4 = new Dictionary<int, IDomNode>();
				List<IDomNode>.Enumerator enumerator4 = list3.GetEnumerator();
				if (enumerator4.MoveNext())
				{
					IDomNode domNode = enumerator4.Current;
					bool flag = true;
					foreach (KeyValuePair<int, List<List<IDomNode>>> keyValuePair4 in dictionary.OrderBy((KeyValuePair<int, List<List<IDomNode>>> p) => p.Key))
					{
						int key3 = keyValuePair4.Key;
						List<List<IDomNode>> value = keyValuePair4.Value;
						bool flag2 = false;
						IDomNode domNode2 = null;
						List<IDomNode> list4 = new List<IDomNode>();
						do
						{
							domNode = enumerator4.Current;
							list4 = TableNodeMatching.GetRowNodeSatisfyingNodeSequence(domNode, value);
							if (list4 != null)
							{
								goto Block_24;
							}
						}
						while (enumerator4.MoveNext());
						IL_025F:
						if (!flag2)
						{
							flag = false;
							break;
						}
						dictionary4[key3] = domNode2;
						for (int i = 0; i < list4.Count; i++)
						{
							int num = dictionary2[key3][i];
							if (!dictionary3.ContainsKey(num))
							{
								dictionary3[num] = new Dictionary<int, IDomNode>();
							}
							dictionary3[num][key3] = list4[i];
						}
						continue;
						Block_24:
						domNode2 = domNode;
						while (enumerator4.MoveNext() && enumerator4.Current.IsAncestor(domNode))
						{
							List<IDomNode> rowNodeSatisfyingNodeSequence = TableNodeMatching.GetRowNodeSatisfyingNodeSequence(enumerator4.Current, value);
							if (rowNodeSatisfyingNodeSequence != null)
							{
								domNode2 = enumerator4.Current;
								list4 = rowNodeSatisfyingNodeSequence;
							}
							domNode = enumerator4.Current;
						}
						flag2 = true;
						goto IL_025F;
					}
					if (flag)
					{
						list.Add(new TableNodesMatch(dictionary3, dictionary4));
					}
				}
			}
			return list;
		}

		// Token: 0x06008134 RID: 33076 RVA: 0x001B0B1C File Offset: 0x001AED1C
		private static List<IDomNode> GetRowNodeSatisfyingNodeSequence(IDomNode rowNode, List<List<IDomNode>> rowNodeCandidateSets)
		{
			List<IDomNode> matchedNodesForRow = new List<IDomNode>();
			Func<IDomNode, bool> <>9__0;
			for (int i = 0; i < rowNodeCandidateSets.Count; i++)
			{
				IEnumerable<IDomNode> enumerable = rowNodeCandidateSets[i];
				Func<IDomNode, bool> func;
				if ((func = <>9__0) == null)
				{
					func = (<>9__0 = (IDomNode n) => n.IsAncestor(rowNode) && !matchedNodesForRow.Contains(n));
				}
				IDomNode domNode = enumerable.FirstOrDefault(func);
				if (domNode == null)
				{
					return null;
				}
				matchedNodesForRow.Add(domNode);
			}
			return matchedNodesForRow;
		}

		// Token: 0x06008135 RID: 33077 RVA: 0x001B0B98 File Offset: 0x001AED98
		private static List<TableNodesMatch> GetColumnBasedTableNodeMatches(Dictionary<int, List<NodeSequence>> multiColumnMatchesMap, Dictionary<int, List<NodeSequence>> singleColumnMatchesMap, HashSet<int> zeroColumnMatches, Dictionary<int, int[]> examplesRowIndexMap)
		{
			IEnumerable<IEnumerable<Tuple<int, NodeSequence>>> enumerable = multiColumnMatchesMap.Select((KeyValuePair<int, List<NodeSequence>> kvp) => kvp.Value.Select((NodeSequence ns) => Tuple.Create<int, NodeSequence>(kvp.Key, ns)).ToList<Tuple<int, NodeSequence>>()).ToList<List<Tuple<int, NodeSequence>>>();
			List<TableNodesMatch> list = new List<TableNodesMatch>();
			IEnumerable<IEnumerable<Tuple<int, NodeSequence>>> enumerable2 = enumerable.CartesianProduct<Tuple<int, NodeSequence>>();
			int num = 0;
			foreach (IEnumerable<Tuple<int, NodeSequence>> enumerable3 in enumerable2)
			{
				num++;
				if (num > 5000)
				{
					break;
				}
				bool flag = true;
				Dictionary<int, IDomNode> dictionary = new Dictionary<int, IDomNode>();
				foreach (Tuple<int, NodeSequence> tuple in enumerable3)
				{
					int[] array = examplesRowIndexMap[tuple.Item1];
					for (int i = 0; i < tuple.Item2.MaxUncommonAncestors.Count; i++)
					{
						IDomNode domNode = tuple.Item2.MaxUncommonAncestors[i];
						int num2 = array[i];
						if (dictionary.GetOrAdd(num2, domNode) != domNode)
						{
							flag = false;
							break;
						}
					}
					if (!flag)
					{
						break;
					}
					if (!NodeTextMatching.IsOrderedNonHierarchicalNodeSequence(dictionary.Values.OrderBy((IDomNode n) => n.Start)))
					{
						flag = false;
					}
				}
				if (flag)
				{
					bool flag2 = true;
					NodeSequence[] array2 = enumerable3.Select((Tuple<int, NodeSequence> t) => t.Item2).ToArray<NodeSequence>();
					for (int j = 0; j < array2.Length; j++)
					{
						for (int k = j + 1; k < array2.Length; k++)
						{
							if (!array2[j].IsAlignedWithinMaxUncommonAncestors(array2[k]))
							{
								flag2 = false;
								break;
							}
						}
						if (!flag2)
						{
							break;
						}
					}
					if (flag2)
					{
						Dictionary<int, Dictionary<int, IDomNode>> columnExamples = new Dictionary<int, Dictionary<int, IDomNode>>();
						foreach (Tuple<int, NodeSequence> tuple2 in enumerable3)
						{
							int item = tuple2.Item1;
							Dictionary<int, IDomNode> dictionary2 = examplesRowIndexMap[item].ZipWith(tuple2.Item2.Nodes).ToDictionary((Record<int, IDomNode> v) => v.Item1, (Record<int, IDomNode> v) => v.Item2);
							columnExamples[item] = dictionary2;
						}
						bool flag3 = true;
						Func<IDomNode, bool> <>9__8;
						foreach (KeyValuePair<int, List<NodeSequence>> keyValuePair in singleColumnMatchesMap)
						{
							int num3 = examplesRowIndexMap[keyValuePair.Key].First<int>();
							IDomNode rowNode;
							dictionary.TryGetValue(num3, out rowNode);
							IEnumerable<IDomNode> enumerable4 = keyValuePair.Value.Select((NodeSequence ns) => ns.Nodes[0]);
							if (rowNode != null)
							{
								enumerable4 = enumerable4.Where((IDomNode n) => n.IsAncestor(rowNode));
							}
							IEnumerable<IDomNode> enumerable5 = enumerable4;
							Func<IDomNode, bool> func;
							if ((func = <>9__8) == null)
							{
								func = (<>9__8 = delegate(IDomNode n)
								{
									Func<IDomNode, bool> <>9__10;
									return columnExamples.Values.All(delegate(Dictionary<int, IDomNode> d)
									{
										IEnumerable<IDomNode> values = d.Values;
										Func<IDomNode, bool> func2;
										if ((func2 = <>9__10) == null)
										{
											func2 = (<>9__10 = (IDomNode n1) => n1 != n);
										}
										return values.All(func2);
									});
								});
							}
							IDomNode domNode2 = enumerable5.FirstOrDefault(func);
							if (domNode2 == null)
							{
								flag3 = false;
								break;
							}
							columnExamples[keyValuePair.Key] = new Dictionary<int, IDomNode> { { num3, domNode2 } };
						}
						if (flag3)
						{
							foreach (int num4 in zeroColumnMatches)
							{
								columnExamples[num4] = new Dictionary<int, IDomNode>();
							}
							list.Add(new TableNodesMatch(columnExamples, dictionary));
							if (list.Count >= 1)
							{
								break;
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x0400344A RID: 13386
		private const int MaxColumnNodeSequences = 10;

		// Token: 0x0400344B RID: 13387
		private const int MaxColumnCombinations = 5000;

		// Token: 0x0400344C RID: 13388
		private const int MaxColumnBasedTableNodeMatches = 1;
	}
}
