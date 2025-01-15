using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.Extraction.Web.Semantics;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Learning
{
	// Token: 0x020010A2 RID: 4258
	internal static class HtmlTableInference
	{
		// Token: 0x06008054 RID: 32852 RVA: 0x001AD4C0 File Offset: 0x001AB6C0
		private static int GetRowSpan(IDomNode n)
		{
			string text = n.GetAttribute("rowspan");
			int num;
			if (int.TryParse(text, out num))
			{
				return num;
			}
			if (!string.IsNullOrWhiteSpace(text))
			{
				text = Regex.Match(text, "\\d+").Value;
				if (!string.IsNullOrEmpty(text) && int.TryParse(text, out num))
				{
					return num;
				}
			}
			return 1;
		}

		// Token: 0x06008055 RID: 32853 RVA: 0x001AD514 File Offset: 0x001AB714
		private static int GetColSpan(IDomNode n)
		{
			string text = n.GetAttribute("colspan");
			int num;
			if (int.TryParse(text, out num))
			{
				return num;
			}
			if (!string.IsNullOrWhiteSpace(text))
			{
				text = Regex.Match(text, "\\d+").Value;
				if (!string.IsNullOrEmpty(text) && int.TryParse(text, out num))
				{
					return num;
				}
			}
			return 1;
		}

		// Token: 0x06008056 RID: 32854 RVA: 0x001AD568 File Offset: 0x001AB768
		private static DomNode[][] GetAlignedHtmlTableNodes(List<DomNode> rowNodes)
		{
			List<List<DomNode>> list = new List<List<DomNode>>();
			int num = 0;
			SortedDictionary<int, HtmlTableInference.CellValueRowSpan> sortedDictionary = new SortedDictionary<int, HtmlTableInference.CellValueRowSpan>();
			foreach (DomNode domNode in rowNodes)
			{
				List<DomNode> list2 = HtmlTableInference.CreateFromTableRow(domNode, sortedDictionary);
				num = Math.Max(num, list2.Count);
				list.Add(list2);
			}
			int count = list.Count;
			DomNode[][] array = new DomNode[num][];
			for (int i = 0; i < num; i++)
			{
				array[i] = new DomNode[count];
				for (int j = 0; j < count; j++)
				{
					List<DomNode> list3 = list[j];
					array[i][j] = ((i < list3.Count) ? list3[i] : null);
				}
			}
			return array;
		}

		// Token: 0x06008057 RID: 32855 RVA: 0x001AD640 File Offset: 0x001AB840
		private static List<DomNode> CreateFromTableRow(DomNode n, IDictionary<int, HtmlTableInference.CellValueRowSpan> rowSpanTracker)
		{
			List<DomNode> list = new List<DomNode>();
			int num = 0;
			foreach (IDomNode domNode in n.Children)
			{
				DomNode domNode2 = (DomNode)domNode;
				if (Witnesses.HasNodeName(domNode2, "TD") || Witnesses.HasNodeName(domNode2, "TH"))
				{
					int colSpan = HtmlTableInference.GetColSpan(domNode2);
					int rowSpan = HtmlTableInference.GetRowSpan(domNode2);
					for (int i = 0; i < colSpan; i++)
					{
						while (rowSpanTracker.ContainsKey(num))
						{
							num++;
						}
						rowSpanTracker.Add(num, new HtmlTableInference.CellValueRowSpan(domNode2, rowSpan));
						num++;
					}
				}
			}
			List<int> list2 = new List<int>();
			foreach (KeyValuePair<int, HtmlTableInference.CellValueRowSpan> keyValuePair in rowSpanTracker)
			{
				list.Add(keyValuePair.Value.Value);
				if (!keyValuePair.Value.TryDecrement())
				{
					list2.Add(keyValuePair.Key);
				}
			}
			foreach (int num2 in list2)
			{
				rowSpanTracker.Remove(num2);
			}
			return list;
		}

		// Token: 0x06008058 RID: 32856 RVA: 0x001AD7A8 File Offset: 0x001AB9A8
		private static string GetTableCellSelector(IDomNode cellNode, Dictionary<IDomNode, string> cache)
		{
			string text;
			if (cache.TryGetValue(cellNode, out text))
			{
				return text;
			}
			string text2 = "";
			string text3 = "";
			string text4 = "";
			string text5 = "";
			string text6 = cellNode.GetAttribute("colspan");
			if (text6 == null)
			{
				text4 = ":not([colspan])";
			}
			else
			{
				IEnumerable<char> enumerable = text6;
				Func<char, bool> func;
				if ((func = HtmlTableInference.<>O.<0>__IsDigit) == null)
				{
					func = (HtmlTableInference.<>O.<0>__IsDigit = new Func<char, bool>(char.IsDigit));
				}
				if (!enumerable.All(func))
				{
					text6 = DomNode.EscapeSpecialCharactersCss(text6);
				}
				text2 = "[colspan=\"" + text6 + "\"]";
			}
			text6 = cellNode.GetAttribute("rowspan");
			if (text6 == null)
			{
				text5 = ":not([rowspan])";
			}
			else
			{
				IEnumerable<char> enumerable2 = text6;
				Func<char, bool> func2;
				if ((func2 = HtmlTableInference.<>O.<0>__IsDigit) == null)
				{
					func2 = (HtmlTableInference.<>O.<0>__IsDigit = new Func<char, bool>(char.IsDigit));
				}
				if (!enumerable2.All(func2))
				{
					text6 = DomNode.EscapeSpecialCharactersCss(text6);
				}
				text3 = "[rowspan=\"" + text6 + "\"]";
			}
			string text7 = string.Concat(new string[] { cellNode.NodeName, text2, text3, text4, text5 });
			IDomNode domNode = cellNode.GetOlderSiblings().FirstOrDefault((IDomNode n) => n.Index == cellNode.Index - 1);
			if (domNode != null)
			{
				text7 = HtmlTableInference.GetTableCellSelector(domNode, cache) + " + " + text7;
			}
			return text7 + string.Format(":nth-child({0}):nth-last-child({1})", cellNode.Index, cellNode.IndexFromLast);
		}

		// Token: 0x06008059 RID: 32857 RVA: 0x001AD940 File Offset: 0x001ABB40
		public static List<string> InferHtmlColumnSelectors(IDomNode tableNode, List<string> rowSelectorDisjuncts, List<List<string>> examples, StringComparer comparer, out IDomNode[][] tableNodeMatches)
		{
			tableNodeMatches = null;
			List<DomNode> rowNodes = tableNode.GetDescendantsByCss(string.Join(", ", rowSelectorDisjuncts));
			if (rowNodes == null || rowNodes.Count == 0)
			{
				return null;
			}
			int num = -1;
			List<string> columnSelectors = null;
			List<List<DomNode>> list;
			if (tableNode.GetDescendants(true).Any((IDomNode n) => n.GetAttribute("rowspan") != null || n.GetAttribute("colspan") != null))
			{
				DomNode[][] alignedNodes = HtmlTableInference.GetAlignedHtmlTableNodes(rowNodes);
				if (alignedNodes == null)
				{
					return null;
				}
				num = alignedNodes.Length;
				Dictionary<IDomNode, string> cellSelectorCache = new Dictionary<IDomNode, string>();
				Func<DomNode, string> <>9__12;
				string[][] array = alignedNodes.Select(delegate(DomNode[] c)
				{
					IEnumerable<DomNode> enumerable2 = c.Where((DomNode n) => n != null);
					Func<DomNode, string> func2;
					if ((func2 = <>9__12) == null)
					{
						func2 = (<>9__12 = (DomNode n) => HtmlTableInference.GetTableCellSelector(n, cellSelectorCache));
					}
					return enumerable2.Select(func2).Distinct<string>().ToArray<string>();
				}).ToArray<string[]>();
				columnSelectors = array.Select(delegate(string[] c)
				{
					if (!c.Any<string>())
					{
						return ":not(*)";
					}
					return string.Join(", ", from t in new IReadOnlyList<string>[] { rowSelectorDisjuncts, c }.CartesianProduct<string>()
						select string.Join(" > ", t));
				}).ToList<string>();
				List<DomNode>[] array2 = columnSelectors.Select(new Func<string, List<DomNode>>(tableNode.GetDescendantsByCss)).ToArray<List<DomNode>>();
				IDomNode[][] tableNodes = Semantics.GetBoundaryBasedRowAlignment(array2, rowNodes);
				HashSet<int> unsatisfiedColIndexes = (from i in alignedNodes.Select(delegate(DomNode[] c, int i)
					{
						IDomNode[] array4 = alignedNodes[i];
						if (!array4.SequenceEqual(tableNodes[i]))
						{
							return i;
						}
						return -1;
					})
					where i != -1
					select i).ConvertToHashSet<int>();
				num -= unsatisfiedColIndexes.Count;
				if (num == 0)
				{
					return null;
				}
				columnSelectors = columnSelectors.Where((string s, int i) => !unsatisfiedColIndexes.Contains(i)).ToList<string>();
				list = array2.Where((List<DomNode> s, int i) => !unsatisfiedColIndexes.Contains(i)).ToList<List<DomNode>>();
			}
			else
			{
				num = rowNodes.Max((DomNode n) => n.ChildrenCount);
				if (num == 0)
				{
					return null;
				}
				columnSelectors = (from i in Enumerable.Range(0, num)
					select string.Join(", ", rowSelectorDisjuncts.Select((string s) => string.Format("{0} > :nth-child({1})", s, i + 1)))).ToList<string>();
				list = (from i in Enumerable.Range(0, num)
					select (from n in rowNodes
						where n != null && i < n.Children.Count
						select n.Children[i] as DomNode).ToList<DomNode>()).ToList<List<DomNode>>();
			}
			if (columnSelectors == null || columnSelectors.IsNullOrEmpty<string>())
			{
				return null;
			}
			tableNodeMatches = Semantics.GetBoundaryBasedRowAlignment(list, rowNodes);
			if (examples == null)
			{
				return columnSelectors;
			}
			List<int> matchingColIndexes = new List<int>();
			bool flag = false;
			Func<int, int> <>9__17;
			foreach (List<string> list2 in examples)
			{
				if (list2.Count == 0)
				{
					matchingColIndexes.Add(-1);
				}
				else
				{
					flag = false;
					IEnumerable<int> enumerable = Enumerable.Range(0, num);
					Func<int, int> func;
					if ((func = <>9__17) == null)
					{
						func = (<>9__17 = (int k) => (matchingColIndexes.Contains(k) > false) ? 1 : 0);
					}
					foreach (int num2 in enumerable.OrderBy(func))
					{
						IDomNode[] array3 = tableNodeMatches[num2];
						if (array3.Length >= list2.Count && array3.Length == rowNodes.Count)
						{
							bool flag2 = true;
							for (int j = 0; j < list2.Count; j++)
							{
								string text = list2[j];
								IDomNode domNode = array3[j];
								if (!comparer.Equals(text, ((domNode != null) ? domNode.NormalizedInnerText : null) ?? string.Empty))
								{
									flag2 = false;
									break;
								}
							}
							if (flag2)
							{
								matchingColIndexes.Add(num2);
								flag = true;
								break;
							}
						}
					}
					if (!flag)
					{
						break;
					}
				}
			}
			if (flag)
			{
				return matchingColIndexes.Select(delegate(int i)
				{
					if (i != -1)
					{
						return columnSelectors[i];
					}
					return "";
				}).ToList<string>();
			}
			return null;
		}

		// Token: 0x020010A3 RID: 4259
		private sealed class CellValueRowSpan
		{
			// Token: 0x0600805A RID: 32858 RVA: 0x001ADD48 File Offset: 0x001ABF48
			public CellValueRowSpan(DomNode value, int remainingSpan)
			{
				this.Value = value;
				this._remainingSpan = remainingSpan;
			}

			// Token: 0x170016A0 RID: 5792
			// (get) Token: 0x0600805B RID: 32859 RVA: 0x001ADD5E File Offset: 0x001ABF5E
			// (set) Token: 0x0600805C RID: 32860 RVA: 0x001ADD66 File Offset: 0x001ABF66
			public DomNode Value { get; private set; }

			// Token: 0x0600805D RID: 32861 RVA: 0x001ADD6F File Offset: 0x001ABF6F
			public bool TryDecrement()
			{
				this.Value = null;
				this._remainingSpan--;
				return this._remainingSpan > 0;
			}

			// Token: 0x040033BB RID: 13243
			private int _remainingSpan;
		}

		// Token: 0x020010A4 RID: 4260
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040033BD RID: 13245
			public static Func<char, bool> <0>__IsDigit;
		}
	}
}
